using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    public static MapGenerator instance;

    public static Action<Vector2, Vector2> onGenerateMapDoneAction;

    [SerializeField] private Texture2D map;
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
    private List<Vector2> spawnEnemyPos = new();
    private Vector2 spawnPos = new();
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
    private void GenerateMap()
    {
        width = map.width;
        height = map.height;

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                int index = i * height + j;
                Color currentColor = map.GetPixel(i, j);
                if (currentColor.Equals(spawnEnemyPosColor))
                {
                    spawnEnemyPos.Add(new(i * offset, j * offset));
                }

                if (currentColor.Equals(spawnPosColor))
                {
                    spawnPos = new(i * offset, j * offset);
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
        onGenerateMapDoneAction?.Invoke(spawnPos, new(width * offset - offset, height * offset - offset));
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