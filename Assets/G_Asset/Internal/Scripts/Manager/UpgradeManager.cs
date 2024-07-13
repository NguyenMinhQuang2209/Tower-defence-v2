using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager instance;
    private UpgradeLevel upgradeLevel;
    private UpgradeItem upgradeItem;

    private TextMeshProUGUI currentLevelTxt = null;
    private TextMeshProUGUI nextLevelTxt = null;
    private TextMeshProUGUI upgradePriceTxt = null;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    private void Start()
    {
        if (currentLevelTxt == null)
        {
            currentLevelTxt = UIManager.instance.currentLevelTxt;
        }
        if (nextLevelTxt == null)
        {
            nextLevelTxt = UIManager.instance.nextLevelTxt;
        }
        if (upgradePriceTxt == null)
        {
            upgradePriceTxt = UIManager.instance.upgradePriceTxt;
        }
    }
    public void InteractWithUpgradeLevel(UpgradeLevel newUpgradeLevel)
    {
        if (upgradeLevel == newUpgradeLevel)
        {
            upgradeLevel = null;
            UIManager.instance.CloseUI();
            return;
        }
        upgradeLevel = newUpgradeLevel;
        if (upgradeLevel == null)
        {
            UIManager.instance.CloseUI();
        }
        else
        {
            ChangeUpgradeItem(UpgradeName.Sollider);
            UIManager.instance.UpgradeItem();

        }
    }
    public void ChangeUpgradeItem(UpgradeName name)
    {
        upgradeItem = upgradeLevel.GetUpdateItem(name);
        ResetUpgradeUI();
    }
    private void ResetUpgradeUI()
    {
        if (upgradeItem == null)
        {
            currentLevelTxt.text = "";
            nextLevelTxt.text = "";
            upgradePriceTxt.text = "-1";
            return;
        }
        currentLevelTxt.text = upgradeItem.GetCurrentStr();
        nextLevelTxt.text = upgradeItem.GetNextLevelStr();
        upgradePriceTxt.text = upgradeItem.GetNextPriceStr();
    }
}
