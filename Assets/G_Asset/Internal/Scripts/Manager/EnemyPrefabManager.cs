using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPrefabManager : MonoBehaviour
{
    public static EnemyPrefabManager instance;
    [SerializeField] private List<Enemy> enemies = new();
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    public List<Enemy> GetEnemyItems()
    {
        return enemies;
    }
}
