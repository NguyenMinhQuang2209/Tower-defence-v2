using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager instance;
    private UpgradeLevel upgradeLevel;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    public void InteractWithUpgradeLevel(UpgradeLevel newUpgradeLevel)
    {
        upgradeLevel = newUpgradeLevel;
        if (upgradeLevel == null)
        {
            UIManager.instance.CloseUI();
        }
        else
        {

        }

    }
}
