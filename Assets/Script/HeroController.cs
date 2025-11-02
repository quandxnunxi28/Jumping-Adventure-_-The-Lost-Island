using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
    float fireRate = 0.5f;
    float nextFire = 5f;

    // Start is called before the first frame update
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        facingRight = true;
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
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            grounded = false;
            myBody.velocity = new Vector2(myBody.velocity.x, jumpForce);

            myAnim.SetBool("isJump", true);
        }


        // tấn công 

        attack();
        if (Input.GetKeyDown(KeyCode.H))
        {
            myAnim.SetTrigger("isDeath");
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            if (Time.time > nextFire)
            {
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

    private void attack()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            myAnim.SetTrigger("isAttack");

        }
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
            float moveDistance = 1.75f; // khoảng cách bạn muốn
            Vector3 newPosition = transform.position;

            if (facingRight)
                newPosition.x += moveDistance;
            else
                newPosition.x -= moveDistance;

            // Dịch chuyển nhân vật
            transform.position = newPosition;
        }
    }
}
