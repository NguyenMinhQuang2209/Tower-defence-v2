using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected Bullet bullet;
    [SerializeField] protected Transform shootPos;
    [SerializeField] protected float damage = 1f;
    [SerializeField] protected float speed = 1f;
    [SerializeField] protected float timeBwtAttack = 1f;
    [SerializeField] protected float attackRange = 1f;
    protected float currentTimeBwtAttack = 0f;
    public virtual void WeaponInit(float attackRange)
    {
        if (GetComponent<BuildingItem>() != null)
        {
            Destroy(GetComponent<BuildingItem>());
        }
        if (GetComponent<Collider2D>() != null)
        {
            Destroy(GetComponent<Collider2D>());
        }
        if (GetComponent<Rigidbody2D>() != null)
        {
            Destroy(GetComponent<Rigidbody2D>());
        }
        currentTimeBwtAttack = 0f;
        ChangeAttackRange(attackRange);
    }
    protected void ChangeAttackRange(float range)
    {
        attackRange = range;
    }
    public void Attack(Vector3 dir)
    {
        currentTimeBwtAttack += Time.deltaTime;
        if (currentTimeBwtAttack >= timeBwtAttack)
        {
            currentTimeBwtAttack = 0f;
            Shoot(dir);
        }
    }
    public void ResetAttack()
    {
        currentTimeBwtAttack += Time.deltaTime;
        currentTimeBwtAttack = Mathf.Min(currentTimeBwtAttack, timeBwtAttack);
    }
    public virtual void Shoot(Vector3 dir)
    {
        Bullet currentBullet = Instantiate(bullet, shootPos.position, Quaternion.identity);
        currentBullet.BulletInit(dir, speed, attackRange, damage);
    }
}
