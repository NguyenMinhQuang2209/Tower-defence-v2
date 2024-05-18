using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPrefabItem : PrefabItem
{
    public override void UseItem()
    {
        if (TryGetComponent<BuildingItem>(out var buildingItem))
        {
            BuildingManager.instance.ChangeBuildingItem(buildingItem);
            UIManager.instance.CloseUI();
        }
    }
}
