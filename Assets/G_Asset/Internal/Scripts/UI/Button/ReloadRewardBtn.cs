using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ReloadRewardBtn : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Button btn;
    Image image;
    private void Start()
    {
        if (GetComponent<Button>() != null)
        {
            btn = GetComponent<Button>();
        }
        else
        {
            btn = gameObject.AddComponent<Button>();
        }
        btn.onClick.AddListener(() =>
        {
            HandleReloadReward();
        });
        image = GetComponent<Image>();
        Color color = image.color;
        color.a = 100f / 255f;
        image.color = color;
    }
    private void HandleReloadReward()
    {
        RewardManager.instance.ReloadRewardCard(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Color color = image.color;
        color.a = 100f / 255f;
        image.color = color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Color color = image.color;
        color.a = 1f;
        image.color = color;
    }
}
