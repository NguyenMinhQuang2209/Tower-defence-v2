using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Level
{
    [SerializeField] private int price = 0;
    [SerializeField] private List<LevelPlus> levelPlus = new();
    public Level(int price, List<LevelPlus> levelPlus)
    {
        this.price = price;
        this.levelPlus = levelPlus;
    }

    public int GetLevelPrice()
    {
        return price;
    }
    public List<LevelPlus> GetLevelPlus()
    {
        return new(levelPlus);
    }
}
[System.Serializable]
public class LevelPlus
{
    [SerializeField] private float plusValue = 1f;
    [SerializeField] private LevelPlusName plusName;
    public LevelPlus(float plusValue, LevelPlusName plusName)
    {
        this.plusValue = plusValue;
        this.plusName = plusName;
    }
    public float GetPlusValue()
    {
        return plusValue;
    }
    public LevelPlusName GetPlusName()
    {
        return plusName;
    }
}
public enum LevelPlusName
{
    Health,
    Damage,
    Speed,
    Range,
    NumberOfBullet,
}
