using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDefaultValue", menuName = "Enemy/defaultValue")]
public class EnemyScriptableObject : ScriptableObject
{
    public float maxHealth;
    public float speed;
    public EnemyName enemyName;
    public float enemyDamage;
    public float timeBwtAttack;
    public float stopDistance;
    public Vector2Int coin = new();
}
