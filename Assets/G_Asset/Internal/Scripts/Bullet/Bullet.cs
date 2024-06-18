using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void BulletInit(Vector3 dir, float bulletSpeed, float delayDieTime)
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
        Vector2 direction = dir - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new(0, angle, 0));

        rb.AddForce(transform.up * bulletSpeed, ForceMode2D.Impulse);
        Destroy(gameObject, delayDieTime);
    }
}
