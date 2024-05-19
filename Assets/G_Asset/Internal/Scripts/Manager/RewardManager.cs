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
    private void Start()
    {
        ReloadRewardCard();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            InteractWithRewardUI();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadRewardCard();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            InteractWithStoreCardUI();
        }
    }
    private void InteractWithRewardUI()
    {
        GameObject rewardUI = UIManager.instance.cardReward_ui;
        UIManager.instance.ChangeUI("Reward", new() { rewardUI });
    }
    public void InteractWithStoreCardUI()
    {
        GameObject cardStoreUI = UIManager.instance.cardStore_ui;
        UIManager.instance.ChangeUI("StoreCard", new() { cardStoreUI });
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
                current.CardInit(randomCard[i], 1, true);
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
            currentCard.CardInit(newCardItem, amount, false);
        }
        cardsStore[newItemName] = currentCard;
    }
}
