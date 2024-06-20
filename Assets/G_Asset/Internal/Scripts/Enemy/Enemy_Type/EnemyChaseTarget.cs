using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseTarget : Enemy
{
    private void Start()
    {
        EnemyInit();
    }
    private void Update()
    {
        if (isDealth)
        {
            return;
        }
        Movement();
    }
    public override void Attack()
    {
        Debug.Log("Attack");
    }
}
