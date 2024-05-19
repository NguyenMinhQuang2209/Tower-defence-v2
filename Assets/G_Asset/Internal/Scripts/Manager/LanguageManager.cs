using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageManager : MonoBehaviour
{
    public static LanguageManager instance;

    public string cardUseBtn { get; private set; } = "Sử dụng";
    public string cardChooseBtn { get; private set; } = "Chọn";
    public string quantityTxt { get; private set; } = "Số lượng";
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    public string GetCardBtn(bool isChooseCard)
    {
        return isChooseCard ? cardChooseBtn : cardUseBtn;
    }
}
