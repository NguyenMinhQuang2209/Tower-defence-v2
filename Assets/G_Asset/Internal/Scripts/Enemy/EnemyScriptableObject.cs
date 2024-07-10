using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDefaultValue", menuName = "Enemy/defaultValue")]
public class EnemyScriptableObject : ScriptableObject
{
    public float maxHealth = 100f;
    public float speed = 1f;
    public EnemyName enemyName;
    public float enemyDamage = 1f;
    public float timeBwtAttack = 1f;
    public float stopDistance = 0.01f;
    public Vector2Int coin = new(1, 5);
}
