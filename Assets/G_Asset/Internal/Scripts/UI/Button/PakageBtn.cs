using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PakageBtn : MonoBehaviour
{
    Button btn;
    private void Start()
    {
        if (GetComponent<Button>() != null)
        {
            btn = GetComponent<Button>();
        }
        else
        {
            btn = gameObject.AddComponent<Button>();
        }
        btn.onClick.AddListener(() =>
        {
            HandleOpenPackage();
        });
    }
    private void HandleOpenPackage()
    {
        RewardManager.instance.InteractWithStoreCardUI();
    }
}
