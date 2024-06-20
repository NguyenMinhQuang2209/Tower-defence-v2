using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TxtShow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txt;
    float floatTime = 0f;
    bool isInit = false;
    public void TxtShowInit(string txt, Color color, float remainTime, float floatTime)
    {
        this.txt.text = txt;
        this.txt.color = color;
        this.floatTime = floatTime;
        isInit = true;
        Invoke(nameof(DeactiveItem), remainTime);
    }
    private void Update()
    {
        if (!isInit)
        {
            return;
        }
        transform.position = new(transform.position.x, transform.position.y + floatTime * Time.deltaTime, transform.position.z);
    }
    public void TxtShowInit(string txt, float remainTime, float floatTime)
    {
        TxtShowInit(txt, Color.red, remainTime, floatTime);
    }
    public void DeactiveItem()
    {
        isInit = false;
        gameObject.SetActive(false);
        TxtShowManager.instance.AddShowTxt(this);
    }
}
