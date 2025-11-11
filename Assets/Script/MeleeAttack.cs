using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public Transform attackPoint;      // điểm tấn công (đặt trước mặt player)
    public float attackRange = 0.5f;   // phạm vi đánh
    public int minAttackDamage = 15;   // sát thương nhỏ nhất
    public int maxAttackDamage = 25;   // sát thương lớn nhất
    public float attackRate = 5f;      // số lần đánh mỗi giây

    float nextAttackTime = 0f;

    public LayerMask enemyLayers;      // layer enemy
    public LayerMask CuiBapLayers;

    Animator anim;
    HeroController hero;               // để biết trái-phải

    public TMP_Text atk_Text;

    void Start()
    {
        if (PlayerStats.Instance != null)
        {
            minAttackDamage = PlayerStats.Instance.minAttackDamage;
            maxAttackDamage = PlayerStats.Instance.maxAttackDamage;
        }
        anim = GetComponent<Animator>();
        hero = GetComponent<HeroController>();
        atk_Text.text = $"Atk Damage: {(minAttackDamage + maxAttackDamage) / 2}";
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
        int randomDamage = Random.Range(minAttackDamage, maxAttackDamage + 1);
        Debug.Log(randomDamage);
        // lấy danh sách kẻ địch trong phạm vi
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            // Gọi hàm trừ máu
            enemy.GetComponent<EnemyHeath>()?.TakeDamage(randomDamage);
            enemy.GetComponent<BossHealth>()?.TakeDamage(randomDamage);
            enemy.GetComponent<Boss3_Health>()?.TakeDamage(randomDamage);

        }
        if (CuiBapLayers != null)
        {

            Collider2D[] hitCuiBap = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, CuiBapLayers);
            foreach (Collider2D enemy in hitCuiBap)
            {
                // Gọi hàm trừ máu
                enemy.GetComponent<EnemyHeath>()?.TakeDamage(randomDamage);
                enemy.GetComponent<BossHealth>()?.TakeDamage(randomDamage);
                enemy.GetComponent<Boss3_Health>()?.TakeDamage(randomDamage);

            }
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
