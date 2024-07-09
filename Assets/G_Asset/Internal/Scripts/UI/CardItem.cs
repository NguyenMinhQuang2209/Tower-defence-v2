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
    [SerializeField] private PrefabItem prefabs;

    public void SetName(ItemName itemName)
    {
        this.itemName = itemName;
    }
    public void SetDisplayName(string displayName)
    {
        this.displayName = displayName;
    }
    public void SetPrefabItem(PrefabItem prefabItem)
    {
        this.prefabs = prefabItem;
    }
    public void SetImage(Sprite image)
    {
        this.image = image;
    }
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
    public PrefabItem GetPrefabs()
    {
        return prefabs;
    }

    public void UseItem(bool v)
    {
        prefabs.UseItem(this, v);
    }
}
