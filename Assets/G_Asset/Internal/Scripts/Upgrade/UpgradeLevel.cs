using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeLevel : MonoBehaviour
{
    private UpgradeItem upgradeFoundation;
    private UpgradeItem upgradeSollider;
    private UpgradeItem upgradeWeapon;
    private void OnMouseDown()
    {
        string currentCursor = UIManager.instance.GetCurrentUI();
        if (currentCursor != PayMessageExtensions.GetString(PayName.BuildingItem))
        {
            UpgradeManager.instance.InteractWithUpgradeLevel(this);
        }
    }
    public UpgradeItem GetUpdateItem(UpgradeName name)
    {
        switch (name)
        {
            case UpgradeName.Foundation: return upgradeFoundation;
            case UpgradeName.Sollider: return upgradeSollider;
            case UpgradeName.Weapon: return upgradeWeapon;
            default: return null;
        }
    }
    public void AddUpgradeItem(UpgradeName name, UpgradeItem upgradeItem)
    {
        switch (name)
        {
            case UpgradeName.Foundation:
                upgradeFoundation = upgradeItem;
                break;
            case UpgradeName.Sollider:
                upgradeSollider = upgradeItem;
                break;
            case UpgradeName.Weapon:
                upgradeWeapon = upgradeItem;
                break;
            default:
                break;
        }
    }
}
public enum UpgradeName
{
    Foundation,
    Sollider,
    Weapon,
    Other
}