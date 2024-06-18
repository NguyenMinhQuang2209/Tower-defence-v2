using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected Bullet bullet;
    public virtual void WeaponInit()
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
    }
    public virtual void Shoot()
    {
        Vector3 dir = transform.forward;
    }
}
