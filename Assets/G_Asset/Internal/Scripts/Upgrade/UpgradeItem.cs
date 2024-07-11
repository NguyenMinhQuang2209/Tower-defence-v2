using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeItem : MonoBehaviour
{
    private Dictionary<LevelPlusName, float> plusDictionary = new();
    [SerializeField] private List<Level> levels = new();
    private int currentLevel = 0;
    private int nextPrice = 0;
    private void Start()
    {
        ReloadLevelPlus();
    }
    public List<Level> GetLevels()
    {
        return new(levels);
    }
    public int CurrentLevel()
    {
        return currentLevel;
    }
    private void StartUpdateLevel()
    {
        if (currentLevel < levels.Count - 1)
        {
            currentLevel++;
            ReloadLevelPlus();
        }
    }
    public bool IsMaxLevel()
    {
        return currentLevel == levels.Count - 1;
    }
    public bool UpdateLevel()
    {
        if (IsMaxLevel())
        {
            return false;
        }
        bool isEnough = CoinManager.instance.EnoughAndRemoveCoin(levels[currentLevel].GetLevelPrice());
        if (isEnough)
        {
            StartUpdateLevel();
            return true;
        }
        return false;
    }
    private void ReloadLevelPlus()
    {
        plusDictionary?.Clear();
        for (int i = 0; i < currentLevel; i++)
        {
            Level level = levels[i];
            List<LevelPlus> currentPlus = level.GetLevelPlus();
            for (int j = 0; j < currentPlus.Count; j++)
            {
                LevelPlus plus = currentPlus[j];
                LevelPlusName key = plus.GetPlusName();
                if (plusDictionary.ContainsKey(key))
                {
                    plusDictionary[key] = plusDictionary[key] + plus.GetPlusValue();
                }
                else
                {
                    plusDictionary[key] = plus.GetPlusValue();
                }
            }
        }
        if (!IsMaxLevel())
        {
            nextPrice = levels[currentLevel].GetLevelPrice();
        }
        else
        {
            nextPrice = -1;
        }
    }
    public int GetNextPrice()
    {
        return nextPrice;
    }
    public float GetPlusValue(LevelPlusName key)
    {
        return plusDictionary.ContainsKey(key) ? plusDictionary[key] : 0f;
    }
}
