using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RewardManager : MonoBehaviour
{
    public static RewardManager instance;
    [SerializeField] private TextMeshProUGUI priceTxt;
    private TextMeshProUGUI remainFree_txt;
    [SerializeField] private int rewardAmount = 3;
    private Dictionary<ItemName, Card> cardsStore = new();
    [SerializeField] private int openAmount = 1;
    private int currentAmount = 0;
    private int freeTimer = 0;
    private int currentPrice = 1;
    private float getRewardAt = 1f;
    int currentDay = 0;
    private bool isStartGameMode = false;
    private bool isPlayingMode = false;
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
        remainFree_txt = UIManager.instance.remainFree_txt;
        freeTimer = GlobalManager.instance.defaultFreeReward;
        currentPrice = GlobalManager.instance.defaultFirstRewardPrice;
        getRewardAt = GlobalManager.instance.defaultGetFreeRewardAt;
        currentDay = GlobalManager.instance.defaultStartDay - 1;
        UpdatePriceTxt();
    }
    private void Update()
    {
        if (isStartGameMode)
        {
            string currentCursor = UIManager.instance.GetCurrentUI();
            if (currentCursor != PayMessageExtensions.GetString(PayName.Reward))
            {
                InteractWithRewardUI(true, true);
                if (freeTimer == 0)
                {
                    GameManager.instance.StartGame();
                }
            }
            return;
        }
        if (!isPlayingMode)
        {
            return;
        }
        float currentTimeWorld = TimeManager.instance.GetCurrentTime();
        int currentDayWorld = TimeManager.instance.GetCurrentDay();
        if (currentDay != currentDayWorld)
        {
            if (currentTimeWorld >= getRewardAt)
            {
                currentDay = currentDayWorld;
                freeTimer++;
                UpdatePriceTxt();
            }
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            InteractWithStoreCardUI();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            InteractWithRewardUI(true, true);
        }
    }
    public void RemainFreeTxt()
    {
        remainFree_txt.text = PayMessageExtensions.GetString(PayMessage.FreeRewardRemain) + freeTimer;
    }
    [Tooltip("RewardAMount the number card the user can flip")]
    public void InteractWithRewardUI(bool needReload = true, bool isFlip = false, int rewardAmount = 1)
    {
        int price = currentPrice;
        if (freeTimer > 0)
        {
            price = 0;
            freeTimer--;
        }
        bool isEnough = CoinManager.instance.EnoughAndRemoveCoin(price);
        if (!isEnough)
        {
            LogManager.instance.Log(PayMessageExtensions.GetString(PayErrorMessage.NotEnoughCoin));
            return;
        }
        if (needReload)
        {
            ReloadRewardCard(isFlip);
        }
        ResetAmount(rewardAmount);
        GameObject rewardUI = UIManager.instance.cardReward_wrap;
        UIManager.instance.ChangeUI(PayMessageExtensions.GetString(PayName.Reward), new() { rewardUI });
        RemainFreeTxt();
        UpdatePriceTxt();
    }
    public void InteractWithStoreCardUI()
    {
        GameObject cardStoreUI = UIManager.instance.cardStore_ui;
        UIManager.instance.ChangeUI(PayMessageExtensions.GetString(PayName.StoreCard), new() { cardStoreUI });
    }
    public void ReloadRewardCard(bool isFlip = false)
    {
        ResetAmount();
        Transform rewardUI = UIManager.instance.cardReward_ui.transform;
        Card card_ui = PrefabPreferenceManager.instance.card;
        int remainUICard = rewardUI.childCount;
        foreach (Transform child in rewardUI.transform)
        {
            child.gameObject.SetActive(false);
        }
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
                current.gameObject.SetActive(true);
                current.CardInit(randomCard[i], 1, true, isFlip);
            }
        }
    }
    private void ResetAmount(int newAmount)
    {
        openAmount = newAmount;
        ResetAmount();
    }
    private void ResetAmount()
    {
        currentAmount = 0;
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
    public bool UseCardItem(ItemName? cardName)
    {
        if (cardName is ItemName nonNullableCardName)
        {
            if (cardsStore.ContainsKey(nonNullableCardName))
            {
                Card useCard = cardsStore[nonNullableCardName];
                int remain = useCard.MinusQuantity();
                useCard.UpdateQuantityTxt();
                if (remain == 1)
                {
                    return true;
                }
                else
                {
                    Destroy(useCard.gameObject);
                    cardsStore.Remove(nonNullableCardName);
                }
            }
        }
        return false;
    }
    public bool CanOpenCard()
    {
        currentAmount++;
        return currentAmount <= openAmount;
    }
    public bool IsEnoughCard()
    {
        return currentAmount >= openAmount;
    }
    private void UpdatePriceTxt()
    {
        string newTxt = currentPrice.ToString();
        if (freeTimer > 0)
        {
            newTxt = "0";
        }
        priceTxt.text = newTxt;
    }
    private void OnEnable()
    {
        GameManager.ChangeGameModeEvent += HandleStartGameMode;
    }
    private void OnDisable()
    {
        GameManager.ChangeGameModeEvent -= HandleStartGameMode;
    }

    private void HandleStartGameMode(GameManager.GameMode mode)
    {
        isPlayingMode = GameManager.instance.IsPlayingMode(mode);
        isStartGameMode = GameManager.instance.IsStartMode(mode);
    }
}
