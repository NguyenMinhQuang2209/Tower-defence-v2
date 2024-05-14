using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    public static MapGenerator instance;

    public static Action<Vector2, Vector2, float> onGenerateMapDoneAction;

    public static Action<List<Vector2>, List<EnemyItemConfig>> onGenerateMapDoneAction_enemySpawn;

    [SerializeField] private Target target;
    [SerializeField] private float offset = 0.16f;
    private int width = 0;
    private int height = 0;
    [SerializeField] private Tilemap tileMap;
    [SerializeField] private Color spawnPosColor;
    [SerializeField] private Color spawnEnemyPosColor;
    [SerializeField] private TileBase groundTile;
    [SerializeField] private List<TileRepresentItem> tiles = new();
    private Dictionary<Color, TileRepresentItem> tileStore = new();
    private List<int> blocked = new();
    private Vector2 spawnPosIndex = new();
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
        GenerateMap();
    }
    public float GetOffset()
    {
        return offset;
    }
    public Vector2Int GetMapSize()
    {
        return new(width, height);
    }
    public Vector2 GetTargetPoint()
    {
        return new(spawnPosIndex.x * offset, spawnPosIndex.y * offset);
    }
    public List<int> GetBlockedList()
    {
        return blocked;
    }
    private void GenerateMap()
    {
        MapScriptableObject currentMap = GameManager.instance.GetCurrentMap();
        Texture2D map = currentMap.map;

        width = map.width;
        height = map.height;
        List<Vector2> spawnEnemiesPos = new();

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                int index = i * height + j;
                Color currentColor = map.GetPixel(i, j);
                if (currentColor.Equals(spawnEnemyPosColor))
                {
                    spawnEnemiesPos.Add(new(i, j));
                }

                if (currentColor.Equals(spawnPosColor))
                {
                    spawnPosIndex = new(i, j);
                }

                TileRepresentItem c_tile = null;

                if (tileStore.ContainsKey(currentColor))
                {
                    c_tile = tileStore[currentColor];
                }
                else
                {
                    for (int z = 0; z < tiles.Count; z++)
                    {
                        TileRepresentItem t_Tile = tiles[z];
                        tileStore[t_Tile.color] = t_Tile;
                        if (t_Tile.color.Equals(currentColor))
                        {
                            c_tile = t_Tile;
                            tiles.RemoveAtSwapBack(z);
                            break;
                        }
                    }
                }

                if (c_tile == null)
                {
                    c_tile = new()
                    {
                        isWalkable = true,
                        tile = groundTile
                    };
                }
                if (!c_tile.isWalkable)
                {
                    blocked.Add(index);
                }

                tileMap.SetTile(new(i, j), c_tile.tile);
            }
        }
        Vector2 targetSpawnPostion = GetTargetPoint();
        Instantiate(target, targetSpawnPostion, Quaternion.identity);
        onGenerateMapDoneAction?.Invoke(targetSpawnPostion, new(width - 1, height - 1), offset);
        for (int i = 0; i < spawnEnemiesPos.Count; i++)
        {
            Vector2 spawnPos = spawnEnemiesPos[i];
            spawnEnemiesPos[i] = new(spawnPos.x * offset, spawnPos.y * offset);
        }
        onGenerateMapDoneAction_enemySpawn?.Invoke(spawnEnemiesPos, currentMap.enemies);
    }
    public Vector2Int GetGridSize()
    {
        return new(width, height);
    }
}
[System.Serializable]
public class TileRepresentItem
{
    public Color color;
    public bool isWalkable;
    public TileBase tile;
}