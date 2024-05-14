using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    public List<Vector2> FindPaths(Vector2 start, Vector2 end)
    {
        Vector2Int size = MapGenerator.instance.GetMapSize();
        List<int> blockedListIndex = MapGenerator.instance.GetBlockedList();
        List<Vector2> paths = PathFinding.instance.FindPath(start, end, size.x, size.y, blockedListIndex);
        return paths;
    }
    public List<Vector2> FindPathsToTargetPoint(Vector2 start)
    {
        Vector2Int size = MapGenerator.instance.GetMapSize();
        List<int> blockedListIndex = MapGenerator.instance.GetBlockedList();
        Vector2 target = MapGenerator.instance.GetTargetPoint();
        List<Vector2> paths = PathFinding.instance.FindPath(start, target, size.x, size.y, blockedListIndex);
        return paths;
    }
}
