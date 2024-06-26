using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public static EnemySpawnManager instance;
    private List<Vector2> spawnPos = new();
    private List<EnemyItemConfig> currentEnemies = new();
    private Dictionary<EnemyName, Enemy> enemyStore = new();
    private float currentTimer = 0f;
    private int currentDay = 0;
    int current = 0;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    private void Start()
    {
        currentDay = TimeManager.instance.GetCurrentDay();
    }
    private void Update()
    {
        int tempCurrentDay = TimeManager.instance.GetCurrentDay();
        if (currentDay != tempCurrentDay)
        {
            currentDay = tempCurrentDay;
            currentTimer = 0f;
            ResetSpawnTime();
        }
        currentTimer += Time.deltaTime;
        float currentTime = TimeManager.instance.GetCurrentTime();
        for (int i = 0; i < currentEnemies.Count; i++)
        {
            EnemyItemConfig enemy = currentEnemies[i];
            if (currentDay < enemy.spawnAtDay)
            {
                continue;
            }
            if (enemy.stopSpawnAtDay > 0 && currentDay >= enemy.stopSpawnAtDay)
            {
                continue;
            }
            if (currentTime < enemy.spawnAt)
            {
                continue;
            }
            if (enemy.stopSpawnAt > 0 && currentTime >= enemy.stopSpawnAt)
            {
                continue;
            }

            float time = enemy.currentTime * enemy.timeBwtSpawn;
            if (currentTimer >= time)
            {

                if (enemy.spawnTime > 0 && enemy.currentAmount + 1 > enemy.spawnTime)
                {
                    currentEnemies.RemoveAt(i);
                    i--;
                }
                else
                {
                    enemy.currentAmount += 1;
                    SpawnEnemy(enemy.enemy);
                    enemy.currentTime += 1;
                }
            }
        }
    }
    public void SpawnEnemy(Enemy enemy)
    {
        Vector2 spawnAt = spawnPos[current];
        Instantiate(enemy, spawnAt, Quaternion.identity);
        current = current == spawnPos.Count - 1 ? 0 : current + 1;
    }
    private void OnEnable()
    {
        MapGenerator.onGenerateMapDoneAction_enemySpawn += OnGenerateMapDone;
    }
    private void OnDisable()
    {
        MapGenerator.onGenerateMapDoneAction_enemySpawn -= OnGenerateMapDone;
    }

    private void OnGenerateMapDone(List<Vector2> list, List<EnemyItemConfig> enemies)
    {
        spawnPos = list;
        currentEnemies?.Clear();
        List<Enemy> temp = new(EnemyPrefabManager.instance.GetEnemyItems());
        for (int i = 0; i < enemies.Count; i++)
        {
            EnemyItemConfig current = enemies[i].Clone();
            int spawnTime = 0;
            if (current.spawnAt > 0)
            {
                spawnTime = (int)Mathf.Ceil(current.spawnAt / current.timeBwtSpawn);
            }
            current.currentAmount = 0;
            current.currentTime = spawnTime;

            if (enemyStore.ContainsKey(current.name))
            {
                current.enemy = enemyStore[current.name];
            }
            else
            {
                for (int j = 0; j < temp.Count; j++)
                {
                    Enemy currentItem = temp[j];
                    EnemyName name = currentItem.GetEnemyName();
                    enemyStore[name] = currentItem;
                    if (name.Equals(current.name))
                    {
                        current.enemy = currentItem;
                    }
                }
                temp?.Clear();
            }
            currentEnemies.Add(current);
        }
    }
    private void ResetSpawnTime()
    {
        for (int i = 0; i < currentEnemies.Count; i++)
        {
            EnemyItemConfig current = currentEnemies[i];
            int spawnTime = 0;
            if (current.spawnAt > 0)
            {
                spawnTime = (int)Mathf.Ceil(current.spawnAt / current.timeBwtSpawn);
            }
            current.currentTime = spawnTime;
            currentEnemies[i] = current;
        }
    }
}
