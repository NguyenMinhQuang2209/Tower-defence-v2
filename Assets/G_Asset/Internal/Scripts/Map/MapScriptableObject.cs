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
    public int spawnTime = 0;
    [Tooltip("Wait until the next time spawn new enemy, set it < 0 to let it have infinity spawn time")]
    public float timeBwtSpawn = 2f;
    [Tooltip("Enemy spawn at time minute")]
    public float spawnAt = -1f;
    [Tooltip("Enemy stop spawn at,set it < 0 to let it don't have stop time")]
    public float stopSpawnAt = -1f;
    [Tooltip("Enemy spawn at day")]
    public int spawnAtDay = -1;
    [Tooltip("Enemy stop spawn at day, set it < 0 to let it don't have stop time")]
    public int stopSpawnAtDay = -1;
    [HideInInspector] public Enemy enemy;
    [HideInInspector] public int currentAmount = 0;
    [HideInInspector] public int currentTime = 0;
    public EnemyItemConfig(EnemyName name, int spawnTime, float timeBwtSpawn, float spawnAt, int spawnAtDay, Enemy enemy, int currentAmount, int currentTime, float stopSpawnAt, int stopSpawnAtDay)
    {
        this.name = name;
        this.spawnTime = spawnTime;
        this.timeBwtSpawn = timeBwtSpawn;
        this.spawnAt = spawnAt;
        this.spawnAtDay = spawnAtDay;
        this.enemy = enemy;
        this.currentAmount = currentAmount;
        this.currentTime = currentTime;
        this.stopSpawnAtDay = stopSpawnAtDay;
        this.stopSpawnAt = stopSpawnAt;
    }

    public EnemyItemConfig Clone()
    {
        return new(name, spawnTime, timeBwtSpawn, spawnAt, spawnAtDay, enemy, currentAmount, 0, stopSpawnAt, stopSpawnAtDay);
    }
}