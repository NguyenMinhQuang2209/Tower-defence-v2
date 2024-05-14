using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Health
{
    [SerializeField] private EnemyScriptableObject enemyDefault;
    public void EnemyInit()
    {
        maxHealth = enemyDefault.maxHealth;
        HealthInit();
    }
    public virtual void Attack()
    {

    }
}
