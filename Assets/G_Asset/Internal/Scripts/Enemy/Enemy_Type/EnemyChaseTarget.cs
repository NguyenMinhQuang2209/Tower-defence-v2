using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseTarget : Enemy
{
    private void Start()
    {
        EnemyInit();
    }
    public override void Attack()
    {
        Debug.Log("Attack");
    }
}
