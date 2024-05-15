using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [SerializeField] private Camera mCamera;
    public static BuildingManager instance;
    private BuildingItem buildingItem = null;
    Vector2 pos;
    Vector2 rot;
    Vector2Int index;
    private SpriteRenderer spriteRender;
    private Vector3 rootSize = new();
    private BoxCollider2D buildingCollider = null;

    public BuildingItem test;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    public void ChangeBuildingItem(BuildingItem newBuildingItem)
    {
        if (buildingItem != null)
        {
            DestroyImmediate(buildingItem.gameObject);
        }

        if (newBuildingItem != null)
        {
            Vector2 mousePos = mCamera.ScreenToWorldPoint(Input.mousePosition);
            float offset = MapGenerator.instance.GetOffset();
            index = GetIndexPosition(mousePos, offset);
            pos = new(index.x * offset, index.y * offset);
            buildingItem = Instantiate(newBuildingItem, pos, Quaternion.Euler(rot));
            spriteRender = buildingItem.GetComponent<SpriteRenderer>();
            if (buildingItem.TryGetComponent<BoxCollider2D>(out buildingCollider))
            {
                rootSize = buildingCollider.size;
                buildingCollider.size = new(rootSize.x - 0.02f, rootSize.y - 0.02f);
            }
        }
        else
        {
            buildingItem = null;
        }
    }
    private void Update()
    {
        if (buildingItem != null)
        {
            Vector2 mousePos = mCamera.ScreenToWorldPoint(Input.mousePosition);
            float offset = MapGenerator.instance.GetOffset();
            index = GetIndexPosition(mousePos, offset);
            pos = new(index.x * offset, index.y * offset);
            buildingItem.transform.SetPositionAndRotation(pos, Quaternion.Euler(rot));

            if (spriteRender != null)
            {
                Color color = buildingItem.CanBuilding() ? Color.green : Color.red;
                spriteRender.color = color;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeBuildingItem(test);
        }
        if (Input.GetMouseButtonDown(1))
        {
            BuildItem();
        }
    }
    public void BuildItem()
    {
        if (buildingItem != null && buildingItem.CanBuilding())
        {
            if (!buildingItem.IsWalkable())
            {
                MapGenerator.instance.AddBlockPosition(index);
                buildingItem.BuildItem();
                buildingCollider.size = rootSize;
                spriteRender.color = Color.white;
                buildingItem = null;
                spriteRender = null;
                buildingCollider = null;
            }
        }
    }
    public Vector2Int GetIndexPosition(Vector2 start, float offset)
    {
        int startX = (int)Mathf.Floor(start.x / offset);
        int startY = (int)Mathf.Floor(start.y / offset);

        int startIndexX = start.x - (startX * offset) < ((startX + 1) * offset) - start.x ? startX : startX + 1;
        int startIndexY = start.y - (startY * offset) < ((startY + 1) * offset) - start.y ? startY : startY + 1;

        return new(startIndexX, startIndexY);
    }
}
