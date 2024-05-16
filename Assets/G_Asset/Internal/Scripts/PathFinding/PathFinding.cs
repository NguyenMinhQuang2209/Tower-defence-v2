using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    public static int DIALOG = 14;
    public static int STRAIGHT = 10;
    public static PathFinding instance;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    public List<Vector2> FindPath(Vector2 start, Vector2 end, int x, int y, List<int> blocked)
    {
        float offset = MapGenerator.instance.GetOffset();

        Vector2Int startIndex = GetIndexPosition(start, offset);
        Vector2Int endIndex = GetIndexPosition(end, offset);

        List<Vector2> finalPaths = FindPath(startIndex, endIndex, x, y, blocked);
        if (finalPaths.Count > 0 && !finalPaths.Contains(end))
        {
            finalPaths.Add(end);
        }
        return finalPaths;
    }
    public Vector2Int GetIndexPosition(Vector2 start, float offset)
    {
        int startX = (int)Mathf.Floor(start.x / offset);
        int startY = (int)Mathf.Floor(start.y / offset);

        int startIndexX = start.x - (startX * offset) < ((startX + 1) * offset) - start.x ? startX : startX + 1;
        int startIndexY = start.y - (startY * offset) < ((startY + 1) * offset) - start.y ? startY : startY + 1;

        return new(startIndexX, startIndexY);
    }
    public List<Vector2> FindPath(Vector2Int start, Vector2Int end, int x, int y, List<int> blocked)
    {
        float offset = MapGenerator.instance.GetOffset();
        List<Vector2> paths = new();
        FindPath(new(start.x, start.y), new(end.x, end.y), x, y, blocked ?? new(), paths);
        paths.Reverse();
        for (int i = 0; i < paths.Count; i++)
        {
            Vector2 currentPath = paths[i];
            paths[i] = new(currentPath.x * offset, currentPath.y * offset);
        }
        return paths;
    }
    public List<Vector2> FindPath2(Vector2Int start, Vector2Int end, int x, int y, List<int> blocked)
    {
        float offset = MapGenerator.instance.GetOffset();
        List<Vector2> paths = new();
        FindPath2(new(start.x, start.y), new(end.x, end.y), x, y, blocked ?? new(), paths);
        paths.Reverse();
        for (int i = 0; i < paths.Count; i++)
        {
            Vector2 currentPath = paths[i];
            paths[i] = new(currentPath.x * offset, currentPath.y * offset);
        }
        return paths;
    }

    public List<Vector2> FindPath2(Vector2 start, Vector2 end, int x, int y, List<int> blocked)
    {
        float offset = MapGenerator.instance.GetOffset();

        Vector2Int startIndex = GetIndexPosition(start, offset);
        Vector2Int endIndex = GetIndexPosition(end, offset);

        List<Vector2> finalPaths = FindPath2(startIndex, endIndex, x, y, blocked);
        if (finalPaths.Count > 0 && !finalPaths.Contains(end))
        {
            finalPaths.Add(end);
        }
        return finalPaths;
    }
    private void FindPath(int2 start, int2 end, int x, int y, List<int> blocked, List<Vector2> finalPaths)
    {
        NativeList<int2> neighbours = new(Allocator.Temp)
            {
                new(+1,+1),
                new(-1,-1),
                new(+1,-1),
                new(-1,+1),
                new(+1,0),
                new(0,+1),
                new(-1,0),
                new(0,-1)
            };
        FindPath(start, end, x, y, blocked, finalPaths, neighbours);
        neighbours.Dispose();
    }

    private void FindPath2(int2 start, int2 end, int x, int y, List<int> blocked, List<Vector2> finalPaths)
    {
        NativeList<int2> neighbours = new(Allocator.Temp)
            {
                new(+1,0),
                new(0,+1),
                new(-1,0),
                new(0,-1)
            };
        FindPath(start, end, x, y, blocked, finalPaths, neighbours);
        neighbours.Dispose();
    }

    private void FindPath(int2 start, int2 end, int x, int y, List<int> blocked, List<Vector2> finalPaths, NativeList<int2> neighbours)
    {
        NativeArray<NodePath> nodes = new NativeArray<NodePath>(x * y, Allocator.Temp);
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                int index = i * y + j;
                NodePath node = new()
                {
                    index = index,
                    x = i,
                    y = j,
                    gCost = int.MaxValue,
                    hCost = 0,
                    isWalkable = !blocked.Contains(index),
                    cameFromNode = -1
                };
                node.CalculateFCost();
                nodes[index] = node;
            }
        }
        NativeList<int> processed = new NativeList<int>(Allocator.Temp);
        NativeList<int> closed = new NativeList<int>(Allocator.Temp);

        int startIndex = GetIndex(start, y);
        int endIndex = GetIndex(end, y);

        NodePath startNode = nodes[startIndex];

        startNode.cameFromNode = -1;
        startNode.gCost = 0;
        startNode.hCost = GetDistance(start, end);
        startNode.CalculateFCost();

        nodes[startIndex] = startNode;

        processed.Add(startIndex);


        while (processed.Length > 0)
        {
            NodePath current = GetLowestNode(processed, nodes);
            if (current.index == endIndex)
            {
                break;
            }

            for (int i = 0; i < processed.Length; i++)
            {
                int currentIndex = processed[i];
                if (current.index == currentIndex)
                {
                    processed.RemoveAtSwapBack(i);
                    break;
                }
            }
            closed.Add(current.index);

            for (int i = 0; i < neighbours.Length; i++)
            {
                int2 offsetNeighbour = neighbours[i];
                int2 neibourPos = new(current.x + offsetNeighbour.x, current.y + offsetNeighbour.y);
                if (IsValidPosition(neibourPos, x, y))
                {
                    int neighbourIndex = GetIndex(neibourPos, y);
                    if (closed.Contains(neighbourIndex))
                    {
                        continue;
                    }

                    if (processed.Contains(neighbourIndex))
                    {
                        continue;
                    }

                    NodePath neighbour = nodes[neighbourIndex];
                    if (!neighbour.isWalkable)
                    {
                        closed.Add(neighbourIndex);
                        continue;
                    }

                    int tentactiveGCost = current.gCost + GetDistance(current.GetPosition(), neighbour.GetPosition());
                    if (tentactiveGCost < neighbour.gCost)
                    {
                        neighbour.gCost = tentactiveGCost;
                        neighbour.hCost = GetDistance(neighbour.GetPosition(), end);
                        neighbour.cameFromNode = current.index;
                        neighbour.CalculateFCost();
                        nodes[neighbourIndex] = neighbour;

                        if (!processed.Contains(neighbourIndex))
                        {
                            processed.Add(neighbourIndex);
                        }
                    }
                }
            }
        }


        NodePath endNode = nodes[endIndex];
        if (endNode.cameFromNode == -1)
        {
            Debug.Log("No path found");
        }
        else
        {
            NodePath current = endNode;
            int2 currentPos = current.GetPosition();
            finalPaths.Add(new(currentPos.x, currentPos.y));

            while (current.cameFromNode != -1)
            {
                NodePath nRoot = nodes[current.cameFromNode];
                int2 rootPos = nRoot.GetPosition();
                finalPaths.Add(new(rootPos.x, rootPos.y));
                current = nRoot;
            }
        }
        processed.Dispose();
        closed.Dispose();
        nodes.Dispose();
    }
    private bool IsValidPosition(int2 pos, int x, int y)
    {
        return pos.x >= 0 && pos.y >= 0 && pos.x < x && pos.y < y;
    }
    private int GetDistance(int2 start, int2 end)
    {
        int x = Mathf.Abs(start.x - end.x);
        int y = Mathf.Abs(start.y - end.y);
        int remain = Mathf.Abs(x - y);
        return DIALOG * Mathf.Min(x, y) + STRAIGHT * remain;
    }
    private NodePath GetLowestNode(NativeList<int> indexes, NativeArray<NodePath> nodes)
    {
        NodePath lowest = nodes[indexes[0]];
        for (int i = 1; i < indexes.Length; i++)
        {
            NodePath current = nodes[indexes[i]];
            if (current.fCost < lowest.fCost)
            {
                lowest = current;
            }
        }
        return lowest;
    }
    public int GetIndex(int2 point, int height)
    {
        return point.x * height + point.y;
    }
    public struct NodePath
    {
        public int index;
        public int x;
        public int y;
        public int gCost;
        public int hCost;
        public int fCost;
        public bool isWalkable;
        public int cameFromNode;
        public void CalculateFCost()
        {
            fCost = gCost + hCost;
        }
        public int2 GetPosition()
        {
            return new(x, y);
        }
    }
}
