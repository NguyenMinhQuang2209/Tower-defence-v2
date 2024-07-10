using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private List<MapScriptableObject> maps = new();
    [SerializeField] private int currentMap = 0;
    [SerializeField] private GameMode gameMode;

    public static Action<GameMode> ChangeGameModeEvent;
    public enum GameMode
    {
        Start,
        Play,
        Other
    }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        ChangeGameModeEvent?.Invoke(gameMode);
    }
    public void ChangeGameMode(GameMode newMode)
    {
        gameMode = newMode;
        ChangeGameModeEvent?.Invoke(gameMode);
    }
    public GameMode GetCurrentGameMode()
    {
        return gameMode;
    }
    public bool IsPlayingMode(GameMode checkMode)
    {
        return checkMode == GameMode.Play;
    }
    public bool IsStartMode(GameMode mode)
    {
        return mode == GameMode.Start;
    }
    public void LoadMap(List<MapScriptableObject> mapsList)
    {
        maps = new(mapsList);
        mapsList?.Clear();
    }
    public List<Vector2> FindPaths(Vector2 start, Vector2 end)
    {
        Vector2Int size = MapGenerator.instance.GetMapSize();
        List<int> blockedListIndex = MapGenerator.instance.GetBlockedList();
        List<Vector2> paths = PathFinding.instance.FindPath(start, end, size.x, size.y, blockedListIndex);

        // Find nearly position if there are no way to go to the end point
        /*if (paths.Count == 0)
        {
            Vector2Int targetIndex = MapGenerator.instance.GetTargetIndex();
            int neighbourOffsetSize = 2;
            Vector2Int[] neighbour = new Vector2Int[8] {
            new(+1,+1),
            new(-1,-1),
            new(+1,0),
            new(+1,-1),
            new(-1,+1),
            new(0,+1),
            new(-1,0),
            new(0,-1)
            };

            while (paths.Count == 0)
            {
                Vector2 newTargetPosition = new();
                float distance = -1f;
                bool hasNewPos = false;

                for (int i = 0; i < neighbour.Length; i++)
                {
                    Vector2Int neighbourOffset = neighbour[i];
                    Vector2Int neighbourVectorIndex = new(targetIndex.x + neighbourOffset.x * neighbourOffsetSize, targetIndex.y + neighbourOffset.y * neighbourOffsetSize);
                    int neighbourIndex = MapGenerator.instance.GetIndex(neighbourVectorIndex);

                    bool isValuePos = neighbourVectorIndex.x >= 0 && neighbourVectorIndex.y >= 0 && neighbourVectorIndex.x < size.x && neighbourVectorIndex.y < size.y;
                    if (isValuePos)
                    {
                        if (!blockedListIndex.Contains(neighbourIndex))
                        {
                            hasNewPos = true;
                            if (distance == -1)
                            {
                                newTargetPosition = MapGenerator.instance.GetIndexPosition(neighbourVectorIndex);
                                distance = Vector2.Distance(start, newTargetPosition);
                            }
                            else
                            {
                                Vector2 tempNewTargetPosition = MapGenerator.instance.GetIndexPosition(neighbourVectorIndex);
                                float pointsDistance = Vector2.Distance(start, tempNewTargetPosition);
                                if (pointsDistance > distance)
                                {
                                    newTargetPosition = tempNewTargetPosition;
                                    distance = pointsDistance;
                                }
                            }
                        }
                    }
                }
                if (hasNewPos)
                {
                    Debug.Log(newTargetPosition);
                    paths = PathFinding.instance.FindPath(start, newTargetPosition, size.x, size.y, blockedListIndex);
                }
                neighbourOffsetSize++;
                if (neighbourOffsetSize >= 5)
                {
                    break;
                }
            }
        }*/
        return paths;
    }
    public List<Vector2> FindPaths(Vector2 start)
    {
        Vector2 target = MapGenerator.instance.GetTargetPoint();
        List<Vector2> paths = FindPaths(start, target);
        return paths;
    }
    public Vector2 GetTargetPosition()
    {
        return MapGenerator.instance.GetTargetPoint();
    }
    public void ChooseMap(int map)
    {
        currentMap = map;
    }
    public MapScriptableObject GetCurrentMap()
    {
        return maps[currentMap];
    }
    public void StartGame()
    {
        ChangeGameMode(GameMode.Play);
    }
}
