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
    [SerializeField] private float deactiveSize = 0.4f;
    [SerializeField] private float activeSize = 1f;
    [SerializeField] private float hoverSize = 1.2f;
    private CardItem cardItem = null;
    private void Start()
    {
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
        transform.DOScale(hoverSize, 0.4f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(activeSize, 0.4f);
    }
    private void OnEnable()
    {
        transform.localScale = new(deactiveSize, deactiveSize, deactiveSize);
        transform.DOScale(activeSize, 0.8f);
    }
}
