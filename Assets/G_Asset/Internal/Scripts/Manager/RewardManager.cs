using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardManager : MonoBehaviour
{
    public static RewardManager instance;
    [SerializeField] private int rewardAmount = 3;
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
        ReloadRewardCard();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            InteractWithUI();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadRewardCard();
        }
    }
    private void InteractWithUI()
    {
        GameObject rewardUI = UIManager.instance.cardReward_ui;
        UIManager.instance.ChangeUI("Reward", new() { rewardUI });
    }
    private void ReloadRewardCard()
    {
        Transform rewardUI = UIManager.instance.cardReward_ui.transform;
        Card card_ui = PrefabPreferenceManager.instance.card;
        int remainUICard = rewardUI.childCount;
        List<CardItem> randomCard = PrefabPreferenceManager.instance.GetCardItemRandomly(rewardAmount);
        for (int i = 0; i < rewardAmount; i++)
        {
            Card current;
            if (i < remainUICard)
            {
                current = rewardUI.GetChild(i).gameObject.GetComponent<Card>();
            }
            else
            {
                current = Instantiate(card_ui, rewardUI.transform);
            }

            if (current != null)
            {
                current.CardInit(randomCard[i]);
            }
        }
    }
}
