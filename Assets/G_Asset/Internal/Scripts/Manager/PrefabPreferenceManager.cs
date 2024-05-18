using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabPreferenceManager : MonoBehaviour
{
    public static PrefabPreferenceManager instance;
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
}
