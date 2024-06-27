using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;

    [SerializeField] private TextMeshProUGUI timeTxt;
    [SerializeField] private TextMeshProUGUI dayTxt;
    [SerializeField] private float rateTimeSpeed = 1f;
    float currentTime = 0f;
    int currentDay = 0;
    bool isStartGame = false;
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
        StartGame();
    }
    private void Update()
    {
        if (!isStartGame)
        {
            return;
        }
        currentTime = Mathf.Min(currentTime + Time.deltaTime * rateTimeSpeed, 24f * 60f);
        if (currentTime == 24f * 60f)
        {
            currentTime = 0f;
            currentDay += 1;
        }
        float showTime = Mathf.Round(currentTime);
        StringBuilder timeShow = new();
        int minute = (int)(showTime / 60f);
        float second = showTime - minute * 60f;
        timeShow.Append(minute >= 10 ? minute.ToString() : "0" + minute);
        timeShow.Append(":");
        timeShow.Append(second >= 10 ? second.ToString() : "0" + second);
        timeTxt.text = timeShow.ToString();
        dayTxt.text = "Ngày: " + currentDay.ToString();
    }
    public void StartGame()
    {
        isStartGame = true;
    }
    public int GetCurrentDay()
    {
        return currentDay;
    }
    public float GetCurrentTime()
    {
        return currentTime;
    }
}
