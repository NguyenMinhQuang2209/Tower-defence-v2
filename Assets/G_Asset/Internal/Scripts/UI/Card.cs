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
    bool canInteract = false;
    private void Start()
    {
        canInteract = false;
        useBtn.onClick.AddListener(() =>
        {
            UseCard();
        });
    }
    public void CardInit(CardItem item, int quantity, bool isChooseCard)
    {
        cardItem = item;
        if (cardItem == null)
        {
            itemImage.gameObject.SetActive(false);
            itemNameTxt.text = "(Thẻ trống)";
            itemQuantityTxt.gameObject.SetActive(false);
            useBtn.gameObject.SetActive(false);
            btn_Txt.gameObject.SetActive(false);
            return;
        }
        else
        {
            itemImage.gameObject.SetActive(true);
            itemQuantityTxt.gameObject.SetActive(true);
            useBtn.gameObject.SetActive(true);
            btn_Txt.gameObject.SetActive(true);
        }
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
        if (!canInteract)
        {
            return;
        }
        transform.DOScale(hoverSize, 0.4f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!canInteract)
        {
            return;
        }
        transform.DOScale(activeSize, 0.4f);
    }
    private void OnEnable()
    {
        transform.localScale = new(deactiveSize, deactiveSize, deactiveSize);
        transform.DOScale(activeSize, 0.8f);
        Invoke(nameof(ChangeCanInteractStatus), 1f);
    }
    private void OnDisable()
    {
        canInteract = false;
    }
    private void ChangeCanInteractStatus()
    {
        canInteract = true;
    }
}
