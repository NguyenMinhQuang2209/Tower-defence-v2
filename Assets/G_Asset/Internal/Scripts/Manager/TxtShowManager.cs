using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TxtShowManager : MonoBehaviour
{
    [SerializeField] private TxtShow txtShowPrefab;
    [SerializeField] private float showTime = 2f;
    [SerializeField] private float floatTime = 0.5f;
    public static TxtShowManager instance;

    private Queue<TxtShow> lstPooling = new();
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    public void ShowTxt(string txt, Color color, Vector3 pos, float remainTime)
    {
        TxtShow current;
        if (lstPooling.Count > 0)
        {
            current = lstPooling.Dequeue();
            current.transform.position = pos;
            current.gameObject.SetActive(true);
        }
        else
        {
            current = Instantiate(txtShowPrefab, pos, Quaternion.identity);
        }
        current.TxtShowInit(txt, color, remainTime, floatTime);
    }
    public void ShowTxt(string txt, Color color, Vector3 pos)
    {
        ShowTxt(txt, color, pos, showTime);
    }
    public void ShowTxtDamage(string txt, Vector3 pos)
    {
        ShowTxt(txt, Color.red, pos);
    }
    public void ShowTxtDamage(string txt, Vector3 pos, float remainTime)
    {
        ShowTxt(txt, Color.red, pos, remainTime);
    }
    public void AddShowTxt(TxtShow txt)
    {
        lstPooling.Enqueue(txt);
    }
}
