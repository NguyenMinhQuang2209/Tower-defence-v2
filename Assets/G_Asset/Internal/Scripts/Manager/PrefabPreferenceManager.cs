using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabPreferenceManager : MonoBehaviour
{
    public static PrefabPreferenceManager instance;
    [field: SerializeField] public Card card { get; private set; }

    [SerializeField] private List<CardItem> cardItems = new();
    private Dictionary<ItemName, CardItem> cardDictionary = new();

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    public void SetListCardItem(List<CardItem> list)
    {
        cardItems = new(list);
    }
    public CardItem GetCardItem(ItemName itemName)
    {
        if (!cardDictionary.ContainsKey(itemName))
        {
            for (int i = 0; i < cardItems.Count; i++)
            {
                ItemName cardName = cardItems[i].GetItemName();
                cardDictionary[cardName] = cardItems[i];
            }
        }
        return cardDictionary[itemName];
    }
    public List<CardItem> GetCardItemRandomly(int amount)
    {
        List<CardItem> cards = new();
        for (int i = 0; i < amount; i++)
        {
            int pos = Random.Range(0, cardItems.Count);
            cards.Add(cardItems[pos]);
        }
        return cards;
    }
}
