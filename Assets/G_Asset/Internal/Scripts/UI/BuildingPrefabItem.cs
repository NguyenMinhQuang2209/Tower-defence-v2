using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class BuildingPrefabItem : PrefabItem
{
    public override void UseItem(CardItem item, bool v)
    {
        if (TryGetComponent<BuildingItem>(out var buildingItem))
        {
            if (v)
            {
                RewardManager.instance.AddStoreCards(item);
                UIManager.instance.CloseUI();
            }
            else
            {
                BuildingManager.instance.ChangeBuildingItem(buildingItem, item.GetItemName());
                UIManager.instance.BuildingItem();
            }
        }
    }
    public override void UseItem(bool v)
    {
        if (TryGetComponent<BuildingItem>(out var buildingItem))
        {
            BuildingManager.instance.ChangeBuildingItem(buildingItem);
            UIManager.instance.BuildingItem();
        }
    }
}
