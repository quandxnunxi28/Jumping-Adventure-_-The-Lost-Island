using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    // ⚙️ Các thông số có thể chỉnh trong Inspector
    public float patrolSpeed = 2f;      // tốc độ đi tuần
    public float chaseSpeed = 3f;       // tốc độ đuổi theo player
    public float detectRange = 5f;      // phạm vi phát hiện player
    public float attackRange = 1f;      // phạm vi tấn công

    public Transform player;            // tham chiếu đến player (gắn tag Player trong Inspector)

    // 📍 Giới hạn vùng patrol
    private Vector2 leftLimit;
    private Vector2 rightLimit;

    // 🧭 Trạng thái & thành phần
    private bool movingRight = true;    // hướng di chuyển hiện tại
    private Animator animator;
    private Rigidbody2D rb;
    private Vector2 initialPos;         // vị trí bắt đầu

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        initialPos = transform.position;

        // Xác định vùng patrol ±5f quanh vị trí ban đầu
        leftLimit = initialPos + Vector2.left * 5f;
        rightLimit = initialPos + Vector2.right * 5f;
    }

    void Update()
    {
        // 🧮 Tính khoảng cách đến player
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // ⚔ Nếu player đủ gần để tấn công
        if (distanceToPlayer <= attackRange)
        {
            AttackPlayer();
        }
        // 🏃 Nếu player trong vùng phát hiện và vẫn nằm trong phạm vi patrol
        else if (distanceToPlayer <= detectRange && IsPlayerInPatrolZone())
        {
            ChasePlayer();
        }
        // 🚶 Nếu không thấy player hoặc player ra khỏi vùng -> đi tuần
        else
        {
            Patrol();
        }

        // 🔁 Flip mặt theo hướng di chuyển
        FlipDirection();
    }

    // ================== 🧠 Logic đi tuần ==================
    void Patrol()
    {
        animator.SetBool("isMoving", true);

        if (movingRight)
        {
            rb.velocity = new Vector2(patrolSpeed, rb.velocity.y);
            if (transform.position.x >= rightLimit.x)
                movingRight = false;
        }
        else
        {
            rb.velocity = new Vector2(-patrolSpeed, rb.velocity.y);
            if (transform.position.x <= leftLimit.x)
                movingRight = true;
        }
    }

    // ================== 🔥 Đuổi theo Player ==================
    void ChasePlayer()
    {
        animator.SetBool("isMoving", true);

        // Đuổi theo player trên trục X, nhưng không vượt ra khỏi giới hạn patrol
        if (player.position.x > transform.position.x && transform.position.x < rightLimit.x)
        {
            rb.velocity = new Vector2(chaseSpeed, rb.velocity.y);
        }
        else if (player.position.x < transform.position.x && transform.position.x > leftLimit.x)
        {
            rb.velocity = new Vector2(-chaseSpeed, rb.velocity.y);
        }
        else
        {
            // Nếu ra khỏi vùng patrol thì quay lại patrol
            rb.velocity = Vector2.zero;
            Patrol();
        }
    }

    // ================== ⚔ Tấn công Player ==================
    void AttackPlayer()
    {
        animator.SetBool("isMoving", false);
        animator.SetTrigger("attack");
        rb.velocity = Vector2.zero;
    }

    // ================== 🔄 Flip hướng theo di chuyển ==================
    void FlipDirection()
    {
        if (rb.velocity.x > 0.1f)
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        else if (rb.velocity.x < -0.1f)
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    }

    // ================== 🧩 Kiểm tra player có trong vùng patrol không ==================
    bool IsPlayerInPatrolZone()
    {
        return player.position.x >= leftLimit.x && player.position.x <= rightLimit.x;
    }
}
