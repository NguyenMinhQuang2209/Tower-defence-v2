using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LuckyWheelBtn : MonoBehaviour
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
            HandleOpenReward();
        });
    }
    private void HandleOpenReward()
    {
        RewardManager.instance.InteractWithRewardUI(true, true);
    }
}
