using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Sequence mySequence;
    [SerializeField] private Image itemImage;
    [SerializeField] private Image cartImage;
    [SerializeField] private TextMeshProUGUI itemNameTxt;
    [SerializeField] private TextMeshProUGUI itemQuantityTxt;
    [SerializeField] private Button clickBtn;
    [SerializeField] private float deactiveSize = 0.4f;
    [SerializeField] private float activeSize = 1f;
    [SerializeField] private float hoverSize = 1.2f;


    [Space(5)]
    [Header("The side of card")]
    [SerializeField] private Sprite downSide;
    [SerializeField] private Sprite upSide;
    private int currentQuantity = 0;
    private CardItem cardItem = null;
    private bool isChooseCard = false;
    private bool isFlip = false;
    private bool canInteract = false;
    private bool canChoose = false;
    private void Start()
    {
        if (clickBtn == null)
        {
            clickBtn = gameObject.AddComponent<Button>();
        }
        clickBtn.onClick.AddListener(() =>
        {
            UseCard();
        });
    }
    public void CardInit(CardItem item, int quantity, bool isChooseCard, bool isFlip = false)
    {
        cardItem = item;
        this.isFlip = isFlip;
        canChoose = !this.isFlip;
        cartImage.sprite = isFlip ? downSide : upSide;
        this.isChooseCard = isChooseCard;
        currentQuantity = quantity;

        if (cardItem != null)
        {
            itemImage.sprite = cardItem.GetImage();
            itemNameTxt.text = cardItem.GetItemDisplayName();
        }
        UpdateQuantityTxt();
        if (this.isFlip)
        {
            itemNameTxt.gameObject.SetActive(false);
            itemImage.gameObject.SetActive(false);
            itemQuantityTxt.gameObject.SetActive(false);
        }
        else
        {
            itemNameTxt.gameObject.SetActive(true);
            if (cardItem == null)
            {
                itemImage.gameObject.SetActive(false);
                itemQuantityTxt.gameObject.SetActive(false);
                itemNameTxt.text = "(Thẻ trống)";
                return;
            }
            else
            {
                itemNameTxt.gameObject.SetActive(true);
                itemImage.gameObject.SetActive(true);
                itemQuantityTxt.gameObject.SetActive(true);
            }
        }
    }
    public void UpdateQuantityTxt()
    {
        itemQuantityTxt.text = LanguageManager.instance.quantityTxt + ": " + currentQuantity.ToString();
    }
    private void UseCard()
    {
        if (isFlip)
        {
            if (RewardManager.instance.CanOpenCard())
            {
                StartRotation();
            }
            return;
        }
        if (!canChoose)
        {
            return;
        }
        if (cardItem != null)
        {
            cardItem.UseItem(isChooseCard);
        }
        else
        {
            UIManager.instance.CloseUI();
        }
    }
    public void AddQuantity(int v = 1)
    {
        currentQuantity += v;
        UpdateQuantityTxt();
    }
    [Tooltip("Minus directly if this have quantity < v this will the negative number (the result when minus value) and directly change quantity to 0")]
    public int MinusQuantity(int v = 1, bool minusDirectly = false)
    {
        int quan = currentQuantity;
        if (quan > v)
        {
            currentQuantity -= v;
            return 1;
        }
        if (quan == v)
        {
            currentQuantity = 0;
            return 0;
        }
        if (minusDirectly)
        {
            currentQuantity = 0;
            return quan - v;
        }
        return -v;
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
        if (isFlip)
        {
            if (RewardManager.instance.IsEnoughCard())
            {
                return;
            }
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
        transform.DOScale(activeSize, 0.5f);
        Invoke(nameof(ChangeCanInteractStatus), 0.6f);
    }
    private void OnDisable()
    {
        canInteract = false;
    }
    private void ChangeCanInteractStatus()
    {
        canInteract = true;
    }

    private void StartRotation()
    {
        isFlip = false;
        Invoke(nameof(ShowData), 0.5f);
        Invoke(nameof(ShowImageBg), 0.5f);
        mySequence = DOTween.Sequence();
        mySequence.Append(transform.DORotate(new Vector3(0, 90, 0), 0.5f));
        mySequence.Append(transform.DORotate(new Vector3(0, 0, 0), 0.5f));
    }
    private void ShowImageBg()
    {
        cartImage.sprite = upSide;
    }
    private void ShowData()
    {
        canChoose = true;
        itemNameTxt.gameObject.SetActive(true);
        itemImage.gameObject.SetActive(true);
        itemQuantityTxt.gameObject.SetActive(true);
    }
}
