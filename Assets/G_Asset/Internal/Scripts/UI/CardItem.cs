using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

[CreateAssetMenu(fileName = "CardItem", menuName = "Card/Item")]
public class CardItem : ScriptableObject
{
    [SerializeField] private Sprite image;
    [SerializeField] private ItemName itemName;
    [SerializeField] private string displayName;
    [SerializeField] private ItemType itemType;
    [SerializeField] private GameObject prefabs;
    public Sprite GetImage()
    {
        return image;
    }
    public ItemName GetItemName()
    {
        return itemName;
    }
    public string GetItemDisplayName()
    {
        return displayName;
    }
    public ItemType GetItemType()
    {
        return itemType;
    }
    public GameObject GetPrefabs()
    {
        return prefabs;
    }

    public void UseItem()
    {
        GameObject itemPrefab = GetPrefabs();
        switch (GetItemType())
        {
            case ItemType.Building:
                if (itemPrefab.TryGetComponent<BuildingItem>(out var buildingItem))
                {
                    BuildingManager.instance.ChangeBuildingItem(buildingItem);
                }
                break;
        }
    }
}
