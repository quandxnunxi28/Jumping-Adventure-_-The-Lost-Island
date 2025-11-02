using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speed = 20f;
    public int damage = 40;
    public Rigidbody2D rb;
    public GameObject impactEffect;

    private Rigidbody2D myBody;
    // Use this for initialization
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }

    //private void Awake()
    //{
    //    myBody = GetComponent<Rigidbody2D>();
    //    // kiem tra goc quay cua vien dan
    //    if (transform.localRotation.z > 0)
    //    {
    //        //them 1 luc vat li cho doi tuong co rigidbody -> giup : chuyen dong tang toc , day theo 1 huong nao do
    //        myBody.AddForce(new Vector2(-1, 0) * speed, ForceMode2D.Impulse);
    //    }
    //    else
    //    {
    //        myBody.AddForce(new Vector2(1, 0) * speed, ForceMode2D.Impulse);

    //    }
    //}

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        BossHealth boss = hitInfo.GetComponent<BossHealth>();
        EnemyHeath enemy = hitInfo.GetComponent<EnemyHeath>();

        if (boss != null)
        {
            boss.TakeDamage(damage);
            Debug.Log(boss.health.ToString());
            Destroy(gameObject);
            Instantiate(impactEffect, transform.position, transform.rotation);
        } 

        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            Debug.Log(enemy.health.ToString());
            Destroy(gameObject);
            Instantiate(impactEffect, transform.position, transform.rotation);

        }
        

    }

}
