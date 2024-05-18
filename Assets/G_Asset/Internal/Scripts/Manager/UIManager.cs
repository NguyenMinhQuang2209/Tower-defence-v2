using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    private string currentUI = "";
    private List<GameObject> currentList = new();

    public GameObject cardReward_ui;
    public GameObject cardStore_ui;

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
        cardReward_ui.SetActive(false);
        cardStore_ui.SetActive(false);
    }
    public void ChangeUI(string newUI, List<GameObject> list)
    {
        if (currentUI == newUI)
        {
            InactiveList();
            currentUI = "";
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
}