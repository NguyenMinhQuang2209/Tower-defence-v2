using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private Button useBtn;
    private CardItem cardItem = null;
    private void Start()
    {
        transform.DOScale(1f, 0.8f);
        useBtn.onClick.AddListener(() =>
        {
            UseCard();
        });
    }
    public void CardInit(CardItem item)
    {
        cardItem = item;
        if (cardItem != null)
        {
            itemImage.sprite = cardItem.GetImage();
            itemName.text = cardItem.GetItemDisplayName();
        }
    }
    public void UseCard()
    {
        if (cardItem != null)
        {
            cardItem.UseItem();
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(1.1f, 0.4f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(1f, 0.4f);
    }
}
