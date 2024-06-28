using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardManager : MonoBehaviour
{
    public static RewardManager instance;
    [SerializeField] private int rewardAmount = 3;
    private Dictionary<ItemName, Card> cardsStore = new();
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            InteractWithStoreCardUI();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            InteractWithRewardUI();
        }
    }
    public void InteractWithRewardUI(bool needReload = true, bool isFlip = false)
    {
        if (needReload)
        {
            ReloadRewardCard(isFlip);
        }
        GameObject rewardUI = UIManager.instance.cardReward_wrap;
        UIManager.instance.ChangeUI("Reward", new() { rewardUI });
    }
    public void InteractWithStoreCardUI()
    {
        GameObject cardStoreUI = UIManager.instance.cardStore_ui;
        UIManager.instance.ChangeUI("StoreCard", new() { cardStoreUI });
    }
    public void ReloadRewardCard(bool isFlip = false)
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
                current.CardInit(randomCard[i], 1, true, isFlip);
            }
        }
    }
    public void AddStoreCards(CardItem newCardItem, int amount = 1)
    {
        Transform cardStoreUI = UIManager.instance.cardStore_storeUI.transform;
        Card card_ui = PrefabPreferenceManager.instance.card;
        ItemName newItemName = newCardItem.GetItemName();
        Card currentCard;
        if (cardsStore.ContainsKey(newItemName))
        {
            currentCard = cardsStore[newItemName];
            currentCard.AddQuantity(amount);
        }
        else
        {
            currentCard = Instantiate(card_ui, cardStoreUI.transform);
            currentCard.CardInit(newCardItem, amount, false, false);
        }
        cardsStore[newItemName] = currentCard;
    }
}
