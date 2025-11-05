using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float patrolSpeed = 2f;// tốc độ đi tuần
    public float chaseSpeed = 7f;// tốc độ đuổi theo player
    public float detectRange = 5f;// phạm vi phát hiện player   
    public Transform player;

    private Vector2 leftLimit;
    private Vector2 rightLimit;
    private bool movingRight = true;
    private bool facingRight = true;
    private Animator animator;
    private Rigidbody2D rb;
    private Vector2 initialPos;

    private float lastFlipTime = 0f;
    public float flipCooldown = 0.2f;
    private float epsilon = 0.05f;

    public float stopDistance = 0.5f;

    public int attackDamage = 5;
    public Vector3 attackOffset;
    public float attackRange = 2f;
    public LayerMask attackMask;
    void Start()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        initialPos = transform.position;

        leftLimit = initialPos + Vector2.left * detectRange;
        rightLimit = initialPos + Vector2.right * detectRange;
    }

    void Update()
    {
      
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < attackRange)
        {
            AttackPlayer();

            Debug.Log("attack" + player);
        }
        else if (distanceToPlayer <= detectRange)
        {
            ChasePlayer();
            
            Debug.Log("chase" + player +"with speed" + chaseSpeed);
        }
        else
        {
            Patrol();
            Debug.Log(gameObject.name + "patrol");
        }

        FlipDirection();
    }

    void Patrol()
    {
        animator.SetBool("isMoving", true);

        if (movingRight)
        {
            rb.velocity = new Vector2(patrolSpeed, rb.velocity.y);
            if (transform.position.x >= rightLimit.x - epsilon)
                movingRight = false;
        }
        else
        {
            rb.velocity = new Vector2(-patrolSpeed, rb.velocity.y);
            if (transform.position.x <= leftLimit.x + epsilon)
                movingRight = true;
        }
    }

    void ChasePlayer()
    {
        animator.SetBool("isMoving", true);

        float dir = player.position.x - transform.position.x;
        if (Mathf.Abs(dir) > attackRange + 0.01f) // thêm 0.01 để tránh rung
        {
            if (player.position.x > transform.position.x && transform.position.x < rightLimit.x)
            {
                rb.velocity = new Vector2(chaseSpeed, rb.velocity.y);
                movingRight = true;
            }
            else if (player.position.x < transform.position.x && transform.position.x > leftLimit.x)
            {
                rb.velocity = new Vector2(-chaseSpeed, rb.velocity.y);
                movingRight = false;
            }
            else
            {
                rb.velocity = Vector2.zero;
                Patrol();
            }
        }
        else
        {
            // Đã gần đủ (nhưng Update sẽ gọi AttackPlayer) -> tạm thời giữ tốc độ 0
            rb.velocity = Vector2.zero;
        }
    }

    void AttackPlayer()
    { 
        rb.velocity = Vector2.zero;

        animator.SetBool("isMoving", false);
        animator.SetTrigger("attack");
       
    }
    public void Attack()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
        if (colInfo != null)
        {
            colInfo.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
        }
    }

    void FlipDirection()
    {
        if (Time.time - lastFlipTime < flipCooldown) return;
        lastFlipTime = Time.time;

        if (movingRight && !facingRight)
        {
            facingRight = true;
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
        else if (!movingRight && facingRight)
        {
            facingRight = false;
            Vector3 scale = transform.localScale;
            scale.x = -Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
    }
}
