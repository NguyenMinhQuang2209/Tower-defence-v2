using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public static EnemySpawnManager instance;
    [SerializeField] private List<EnemyItem> enemyItems = new();
    private List<Vector2> spawnPos = new();
    private List<EnemyItemConfig> currentEnemies = new();
    private Dictionary<EnemyName, Enemy> enemyStore = new();
    private float currentTimer = 0f;
    [System.Serializable]
    public struct EnemyItem
    {
        public Enemy enemy;
        public EnemyName name;
    }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    private void Update()
    {
        currentTimer += Time.deltaTime;
        for (int i = 0; i < enemyItems.Count; i++)
        {

        }
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
        List<EnemyItem> temp = new(enemyItems);
        for (int i = 0; i < enemies.Count; i++)
        {
            EnemyItemConfig current = enemies[i];
            if (enemyStore.ContainsKey(current.name))
            {
                current.enemy = enemyStore[current.name];
            }
            else
            {
                for (int j = 0; j < temp.Count; j++)
                {
                    EnemyItem currentItem = temp[j];
                    enemyStore[currentItem.name] = currentItem.enemy;
                    if (currentItem.name == current.name)
                    {
                        current.enemy = currentItem.enemy;
                    }
                }
                temp?.Clear();
            }
        }
        currentEnemies = enemies;
    }
}
