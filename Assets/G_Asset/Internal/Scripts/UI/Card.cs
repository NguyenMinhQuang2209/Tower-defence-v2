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
    [SerializeField] private TextMeshProUGUI itemNameTxt;
    [SerializeField] private TextMeshProUGUI itemQuantityTxt;
    [SerializeField] private Button useBtn;
    [SerializeField] private TextMeshProUGUI btn_Txt;
    [SerializeField] private float deactiveSize = 0.4f;
    [SerializeField] private float activeSize = 1f;
    [SerializeField] private float hoverSize = 1.2f;
    private int currentQuantity = 0;
    private CardItem cardItem = null;
    private bool isChooseCard = false;
    private void Start()
    {
        useBtn.onClick.AddListener(() =>
        {
            UseCard();
        });
    }
    public void CardInit(CardItem item, int quantity, bool isChooseCard)
    {
        cardItem = item;
        this.isChooseCard = isChooseCard;
        currentQuantity = quantity;
        string cardBtnTxt = LanguageManager.instance.GetCardBtn(isChooseCard);
        btn_Txt.text = cardBtnTxt;

        if (cardItem != null)
        {
            itemImage.sprite = cardItem.GetImage();
            itemNameTxt.text = cardItem.GetItemDisplayName();
        }
        UpdateQuantityTxt();
    }
    private void UpdateQuantityTxt()
    {
        itemQuantityTxt.text = LanguageManager.instance.quantityTxt + ": " + currentQuantity.ToString();
    }
    public void UseCard()
    {
        if (cardItem != null)
        {
            cardItem.UseItem(isChooseCard);
        }
    }
    public void AddQuantity(int v = 1)
    {
        currentQuantity += v;
        UpdateQuantityTxt();
    }
    public int GetCurrentQuantity()
    {
        return currentQuantity;
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
