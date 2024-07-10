using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    float damage = 0f;
    Vector3 rootPos;
    float attackRange = 0f;
    bool isInit = false;
    LayerMask enemyMask;
    [SerializeField] private float bulletOffset = -90f;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rootPos = transform.position;
        enemyMask = GlobalManager.instance.enemiesMask;
    }
    public void BulletInit(Vector3 dir, float bulletSpeed, float attackRange, float damage)
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
            rootPos = transform.position;
            enemyMask = GlobalManager.instance.enemiesMask;
        }
        Vector2 direction = dir - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new(0f, 0f, angle + bulletOffset));
        this.damage = damage;
        this.attackRange = attackRange;
        rb.AddForce(transform.up * bulletSpeed, ForceMode2D.Impulse);
        isInit = true;
    }
    private void Update()
    {
        if (isInit)
        {
            float distance = Vector2.Distance(transform.position, rootPos);
            if (distance >= attackRange)
            {
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        int layer = collision.gameObject.layer;
        if (((1 << layer) & enemyMask) != 0)
        {
            if (collision.gameObject.TryGetComponent<Health>(out var health))
            {
                health.TakeDamage(damage, gameObject);
                Destroy(gameObject);
            }
        }
    }
}
