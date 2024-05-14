using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Map", menuName = "Map")]
public class MapScriptableObject : ScriptableObject
{
    public Texture2D map;
    public List<EnemyItemConfig> enemies;
    public WinningType winningType;

    [Tooltip("This is value for winning type, if time is time this will be second, if time is day this will be day")]
    public float winValue = 0f;
}
[System.Serializable]
public class EnemyItemConfig
{
    public EnemyName name;
    [Tooltip("How many time this enemy be spawned")]
    public int spawnTime;
    [Tooltip("Wait until the next time spawn new enemy")]
    public float timeBwtSpawn;
    [Tooltip("Enemy spawn at time")]
    public float spawnAt;
    [HideInInspector] public Enemy enemy;
}