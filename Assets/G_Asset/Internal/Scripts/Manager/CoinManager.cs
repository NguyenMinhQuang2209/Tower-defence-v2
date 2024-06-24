using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;
    [SerializeField] private TextMeshProUGUI coinTxt;
    int currentCoin = 0;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    private void Start()
    {
        ReloadCoinTxt();
    }
    public void AddCoin(int value)
    {
        currentCoin += value;
        ReloadCoinTxt();
    }
    public bool EnoughCoin(int value)
    {
        return currentCoin >= value;
    }
    public bool EnoughAndRemoveCoin(int value)
    {
        if (currentCoin < value)
        {
            return false;
        }
        RemoveCoin(value);
        return true;
    }
    private void RemoveCoin(int value)
    {
        currentCoin = Mathf.Max(currentCoin + value, 0);
    }
    private void ReloadCoinTxt()
    {
        coinTxt.text = currentCoin.ToString();
    }

}
