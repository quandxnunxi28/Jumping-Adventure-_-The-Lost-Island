using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    public float maxSpeed = 5f;
    public float jumpForce = 7f;

    private Rigidbody2D myBody;
    private Animator myAnim;
    private bool grounded = true;
    // Start is called before the first frame update
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //di chuyển
        float move = Input.GetAxis("Horizontal");
        myBody.velocity = new Vector2(move * maxSpeed, myBody.velocity.y);
        //chuyển trạng thái chạy    
        myAnim.SetFloat("Speed", Mathf.Abs(move));

        //nhảy
        if(Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            grounded = false;
            myBody.velocity = new Vector2(myBody.velocity.x, jumpForce);
            myAnim.SetBool("isJump", true);
        }

        // tấn công 
        if (Input.GetKeyDown(KeyCode.J))
        {
            myAnim.SetTrigger("isAttack");
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            myAnim.SetTrigger("isHurt");
        }

        
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
