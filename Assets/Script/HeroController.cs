using System;
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
    float fireRate = 1f;
    float nextFire = 1f;
    private bool isRolling = false; // trạng thái đang roll
    private bool canMove = true; // kiểm soát việc di chuyển
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
        float move = canMove ? Input.GetAxis("Horizontal") : 0f;
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
        roll();
        attack();
        if (Input.GetKeyDown(KeyCode.H))
        {
            myAnim.SetTrigger("isDeath");
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            if (mana >= 10) {
                if (Time.time > nextFire)
                {
                    TakeMana(10);
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
    private void roll()
    {
        if (Input.GetKeyDown(KeyCode.L) && !isRolling)
        {
            myAnim.SetTrigger("isRoll");
            StartCoroutine(PerformRoll());
        }
    }

    IEnumerator PerformRoll()
    {
        isRolling = true;
        canMove = false; //Ngăn người chơi di chuyển khi roll
        float rollSpeed = 21f; // tốc độ lăn
        float rollDuration = 0.6f;  // thời gian khớp với animation
        float elapsed = 0f;
        // Xác định hướng
        float direction = facingRight ? 1f : -1f;
        // Trong lúc roll, disable input di chuyển (tùy chọn)
        myAnim.SetBool("isRolling", true);
        myAnim.SetFloat("Speed", 0); //  Dừng anim chạy (Speed = 0)
        while (elapsed < rollDuration)
        {
            // Giữ y-velocity để không ảnh hưởng nhảy
            myBody.velocity = new Vector2(direction * rollSpeed, myBody.velocity.y);
            elapsed += Time.deltaTime;
            yield return null;
        }
        // Dừng lại khi kết thúc roll
        myBody.velocity = new Vector2(0, myBody.velocity.y);
        myAnim.SetBool("isRolling", false);
        isRolling = false;
        canMove = true; //  Cho phép di chuyển lại
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
