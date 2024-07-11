using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    private string currentUI = "";
    private List<GameObject> currentList = new();

    [field: SerializeField] public GameObject cardReward_ui { get; private set; }
    [field: SerializeField] public GameObject cardReward_wrap { get; private set; }
    [field: SerializeField] public GameObject cardStore_ui { get; private set; }
    [field: SerializeField] public Transform cardStore_storeUI { get; private set; }
    [field: SerializeField] public TextMeshProUGUI remainFree_txt { get; private set; }

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
        cardReward_wrap.SetActive(false);
        cardStore_ui.SetActive(false);
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

    }
}
