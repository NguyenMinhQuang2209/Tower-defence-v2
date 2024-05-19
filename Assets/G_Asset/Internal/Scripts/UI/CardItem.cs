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
