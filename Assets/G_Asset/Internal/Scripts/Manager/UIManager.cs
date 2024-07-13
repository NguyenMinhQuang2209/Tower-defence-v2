using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] private string currentUI = "";
    private List<GameObject> currentList = new();

    [field: SerializeField] public GameObject cardReward_ui { get; private set; }
    [field: SerializeField] public GameObject cardReward_wrap { get; private set; }
    [field: SerializeField] public GameObject cardStore_ui { get; private set; }
    [field: SerializeField] public Transform cardStore_storeUI { get; private set; }
    [field: SerializeField] public TextMeshProUGUI remainFree_txt { get; private set; }


    [Header("UpgradeSystem")]
    [SerializeField] private Button foundationBtn;
    [field: SerializeField] public GameObject upgradeUI { get; private set; }
    [field: SerializeField] public Button closeBtn { get; private set; }
    [field: SerializeField] public Button solliderBtn { get; private set; }
    [field: SerializeField] public Color btnActiveColor { get; private set; }
    [field: SerializeField] public Color btnInActiveColor { get; private set; }
    [field: SerializeField] public Button weaponBtn { get; private set; }
    [field: SerializeField] public TextMeshProUGUI currentLevelTxt { get; private set; }
    [field: SerializeField] public TextMeshProUGUI nextLevelTxt { get; private set; }
    [field: SerializeField] public Button deleteBtn { get; private set; }
    [field: SerializeField] public Button upgradeBtn { get; private set; }
    [field: SerializeField] public TextMeshProUGUI upgradePriceTxt { get; private set; }
    public Button FoundationBtn
    {
        get => foundationBtn;
        private set => foundationBtn = value;
    }

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
        cardReward_wrap.SetActive(true);
        cardStore_ui.SetActive(true);
        upgradeUI.SetActive(true);

        solliderBtn.onClick.AddListener(() =>
        {
            ClickSolliderBtn();
        });
        weaponBtn.onClick.AddListener(() =>
        {
            ClickWeaponBtn();
        });
        foundationBtn.onClick.AddListener(() =>
        {
            ClickFoundationBtn();
        });
        closeBtn.onClick.AddListener(() =>
        {
            CloseUpgradeUI();
        });

        cardReward_wrap.SetActive(false);
        cardStore_ui.SetActive(false);
        upgradeUI.SetActive(false);
    }
    public void CloseUpgradeUI()
    {
        CloseUI();
    }
    public void ClickSolliderBtn()
    {
        ChangeColor(solliderBtn, btnActiveColor);
        ChangeColor(foundationBtn, btnInActiveColor);
        ChangeColor(weaponBtn, btnInActiveColor);
        UpgradeManager.instance.ChangeUpgradeItem(UpgradeName.Sollider);
    }
    public void ClickFoundationBtn()
    {
        ChangeColor(solliderBtn, btnInActiveColor);
        ChangeColor(foundationBtn, btnActiveColor);
        ChangeColor(weaponBtn, btnInActiveColor);
        UpgradeManager.instance.ChangeUpgradeItem(UpgradeName.Foundation);
    }
    public void ClickWeaponBtn()
    {
        ChangeColor(solliderBtn, btnInActiveColor);
        ChangeColor(foundationBtn, btnInActiveColor);
        ChangeColor(weaponBtn, btnActiveColor);
        UpgradeManager.instance.ChangeUpgradeItem(UpgradeName.Weapon);
    }
    public string GetCurrentUI()
    {
        return currentUI;
    }

    public void ChangeUI(string newUI, List<GameObject> list)
    {
        if (currentUI == newUI)
        {
            InactiveList();
            currentUI = "";
            return;
        }
        if (currentUI == PayMessageExtensions.GetString(PayName.Reward))
        {
            LogManager.instance.Log(PayMessageExtensions.GetString(PayErrorMessage.PleaseChooseReward));
            return;
        }
        currentUI = newUI;
        InactiveList();
        currentList = new(list);
        ActiveList();
    }
    public void ChangeUI(string newUI)
    {
        if (currentUI == newUI)
        {
            InactiveList();
            currentUI = "";
            return;
        }
        currentUI = newUI;
        InactiveList();
        currentList = new();
    }
    private void InactiveList()
    {
        ChangeListState(false);
    }
    private void ActiveList()
    {
        ChangeListState(true);
    }
    private void ChangeListState(bool v)
    {
        for (int i = 0; i < currentList.Count; i++)
        {
            currentList[i].SetActive(v);
        }
    }
    public void CloseUI()
    {
        InactiveList();
        currentUI = "";
        currentList = new();
    }
    public void BuildingItem()
    {
        ChangeUI(PayMessageExtensions.GetString(PayName.BuildingItem), new());
    }
    public void UpgradeItem()
    {
        ChangeColor(solliderBtn, btnActiveColor);
        ChangeColor(foundationBtn, btnInActiveColor);
        ChangeColor(weaponBtn, btnInActiveColor);
        if (currentUI != PayMessageExtensions.GetString(PayName.Upgrade))
        {
            ChangeUI(PayMessageExtensions.GetString(PayName.Upgrade), new() { upgradeUI });
        }
    }
    private void ChangeColor(Button btn, Color btnColor)
    {
        if (btn.TryGetComponent<Image>(out var image))
        {
            image.color = btnColor;
        }
    }
}
