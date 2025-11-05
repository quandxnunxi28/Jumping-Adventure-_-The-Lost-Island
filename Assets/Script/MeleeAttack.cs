using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public Transform attackPoint;      // điểm tấn công (đặt trước mặt player)
    public float attackRange = 0.5f;   // phạm vi đánh
    public int attackDamage = 20;      // sát thương
    public float attackRate = 5f;      // số lần đánh mỗi giây
    float nextAttackTime = 0f;

    public LayerMask enemyLayers;      // layer enemy

    Animator anim;
    HeroController hero;               // để biết trái-phải

    void Start()
    {
        anim = GetComponent<Animator>();
        hero = GetComponent<HeroController>();
    }

    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    void Attack()
    {
        // bật animation
        anim.SetTrigger("isAttack");

        // lấy danh sách kẻ địch trong phạm vi
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            // Gọi hàm trừ máu
            enemy.GetComponent<EnemyHeath>()?.TakeDamage(attackDamage);
            enemy.GetComponent<BossHealth>()?.TakeDamage(attackDamage);
        }
    }

    // để vẽ phạm vi trong Scene
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
