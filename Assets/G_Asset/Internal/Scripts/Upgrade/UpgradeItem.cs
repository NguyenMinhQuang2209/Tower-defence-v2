using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class UpgradeItem : MonoBehaviour
{
    private Dictionary<LevelPlusName, float> plusDictionary = new();
    [SerializeField] private DefaultLevel defaultLevel;
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
        if (currentLevel < levels.Count)
        {
            currentLevel++;
            ReloadLevelPlus();
        }
    }
    public string GetCurrentStr()
    {
        StringBuilder str = new();
        AddStr(LevelPlusName.Health, str);
        AddStr(LevelPlusName.Damage, str);
        AddStr(LevelPlusName.Range, str);
        AddStr(LevelPlusName.NumberOfBullet, str);
        AddStr(LevelPlusName.Speed, str);
        return str.ToString();
    }
    public string GetNextLevelStr()
    {
        StringBuilder str = new();
        if (IsMaxLevel())
        {
            str.AppendLine(PayMessageExtensions.MAX_LEVEL);
            return str.ToString();
        }
        Level level = levels[currentLevel];
        List<LevelPlus> currentPlus = level.GetLevelPlus();
        Dictionary<LevelPlusName, float> tempLevelPlus = new();
        for (int j = 0; j < currentPlus.Count; j++)
        {
            LevelPlus plus = currentPlus[j];
            LevelPlusName key = plus.GetPlusName();
            if (tempLevelPlus.ContainsKey(key))
            {
                tempLevelPlus[key] = tempLevelPlus[key] + plus.GetPlusValue();
            }
            else
            {
                tempLevelPlus[key] = plus.GetPlusValue();
            }
        }
        AddStr(tempLevelPlus, LevelPlusName.Health, str);
        AddStr(tempLevelPlus, LevelPlusName.Damage, str);
        AddStr(tempLevelPlus, LevelPlusName.Range, str);
        AddStr(tempLevelPlus, LevelPlusName.NumberOfBullet, str);
        AddStr(tempLevelPlus, LevelPlusName.Speed, str);
        tempLevelPlus?.Clear();
        return str.ToString();
    }
    private void AddStr(Dictionary<LevelPlusName, float> dic, LevelPlusName name, StringBuilder str)
    {
        if (dic.ContainsKey(name))
        {
            str.Append(PayMessageExtensions.GetString(name) + ": +");
            str.AppendLine(dic[name].ToString());
        }
    }
    private void AddStr(LevelPlusName name, StringBuilder str)
    {
        str.Append(PayMessageExtensions.GetString(name) + ": ");
        float plusV = 0;
        if (plusDictionary.ContainsKey(name))
        {
            plusV = plusDictionary[name];
        }
        str.AppendLine(plusV.ToString());
    }
    public bool IsMaxLevel()
    {
        if (levels == null || levels.Count == 0)
        {
            return true;
        }
        return currentLevel == levels.Count;
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
    private void PlusToDictionary(LevelPlusName key, float plusValue)
    {
        if (plusDictionary.ContainsKey(key))
        {
            plusDictionary[key] = plusDictionary[key] + plusValue;
        }
        else
        {
            plusDictionary[key] = plusValue;
        }
    }
    private void ReloadLevelPlus()
    {
        plusDictionary?.Clear();

        if (defaultLevel != null)
        {
            PlusToDictionary(LevelPlusName.Health, defaultLevel.GetHealth());
            PlusToDictionary(LevelPlusName.Damage, defaultLevel.GetDamage());
            PlusToDictionary(LevelPlusName.Range, defaultLevel.GetRange());
            PlusToDictionary(LevelPlusName.NumberOfBullet, defaultLevel.GetNumberOfBullet());
            PlusToDictionary(LevelPlusName.Speed, defaultLevel.GetSpeed());
        }

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
    public string GetNextPriceStr()
    {
        if (GetNextPrice() == -1)
        {
            return "-1";
        }
        return GetNextPrice().ToString();
    }
    public float GetPlusValue(LevelPlusName key)
    {
        return plusDictionary.ContainsKey(key) ? plusDictionary[key] : 0f;
    }
}
