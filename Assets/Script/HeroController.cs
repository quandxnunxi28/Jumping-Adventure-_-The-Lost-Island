using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    public float maxSpeed = 5f;
    public float jumpForce = 7f;

    private Rigidbody2D myBody;
    private Animator myAnim;
    private bool grounded = true;
    bool facingRight;
    float moveDistance = 2f; // khoảng cách bạn muốn

    //bo sung cac bien thuc hien hoat dong ban dan
    public Transform gunTip;
    public GameObject bullet;
    float fireRate = 2f;
    float nextFire = 1f;

    // Audio
    public AudioClip jumpSound;
    public AudioClip runSound;
    public AudioClip attackSound;
    private AudioSource audioSource;
    private bool isRunning = false;
    public PlayerManaBar manaBar;
    public int maxMana = 100;
    public int mana =100;
    public TMP_Text manaText;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerStats.Instance != null)
        {
            mana = PlayerStats.Instance.mana;
            maxMana = PlayerStats.Instance.maxMana;
        }

        myBody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogWarning("AudioSource chưa được gắn trên Heka! Script sẽ thêm tự động.");
            audioSource = gameObject.AddComponent<AudioSource>(); // tự thêm component
        }
        facingRight = true;

        if (manaBar != null)
            manaBar.SetMana(mana,maxMana);
        if(manaText!=null)
            manaText.text = $"{mana}/{maxMana}";
    }

    // Update is called once per frame
    void Update()
    {

        //di chuyển
        float move = Input.GetAxis("Horizontal");
        myBody.velocity = new Vector2(move * maxSpeed, myBody.velocity.y);

        if (move > 0 && !facingRight)
        {
            flip();
        }
        else if (move < 0 && facingRight)
        {
            flip();
        }
        //chuyển trạng thái chạy    
        myAnim.SetFloat("Speed", Mathf.Abs(move));

        //nhảy


        if (move != 0 && grounded)
        {
            if (!isRunning)
            {
                // Start run sound
                audioSource.clip = runSound;
                audioSource.loop = true;
                audioSource.Play();
                isRunning = true;
            }
        }
        else
        {
            if (isRunning)
            {
                // Dừng run sound
                audioSource.Stop();
                isRunning = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.W) && grounded)
        {
            grounded = false;
            myBody.velocity = new Vector2(myBody.velocity.x, jumpForce);

            myAnim.SetBool("isJump", true);

            if (jumpSound != null) 
            {
                StartCoroutine(PlayJumpSoundWithDelay(0.2f));
                audioSource.PlayOneShot(jumpSound);
            }
                
        }



        // tấn công 

        attack();
        if (Input.GetKeyDown(KeyCode.H))
        {
            myAnim.SetTrigger("isDeath");
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            if (mana >= 20) {
                if (Time.time > nextFire)
                {
                    TakeMana(20);
                    nextFire = Time.time + fireRate; //xac dinh tgian tiep thieo vien dan duoc ban ra
                    if (facingRight)
                    {
                        //ban ra vien dan
                        Instantiate(bullet, gunTip.position, Quaternion.Euler(new Vector3(0, 0, 0)));
                    }
                    else
                    {
                        if (!facingRight)
                        {
                            Instantiate(bullet, gunTip.position, Quaternion.Euler(new Vector3(0, 0, 180)));
                        }
                    }
                }
            }
        }



    }

    public void TakeMana(int manatieuhao)
    {
        mana -= manatieuhao;
        PlayerStats.Instance.mana = mana;
        // 🔹 Cập nhật thanh mana
        if (manaBar != null)
            manaBar.SetMana(mana, maxMana);
        if (manaText != null)
            manaText.text = $"{mana}/{maxMana}";
    }
    private void attack()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            myAnim.SetTrigger("isAttack");
            StartCoroutine(PlayJumpSoundWithDelay(0.3f));
                audioSource.PlayOneShot(attackSound);
        }
    }

    IEnumerator PlayJumpSoundWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        audioSource.PlayOneShot(jumpSound);
    }
    private void flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Va cham voi: " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
            myAnim.SetBool("isJump", false);
           
        }
    }
}
