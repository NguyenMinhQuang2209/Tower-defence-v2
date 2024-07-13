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
[System.Serializable]
public class DefaultLevel
{
    [SerializeField] private float health = 100f;
    [SerializeField] private float damage = 1f;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float range = 1f;
    [SerializeField] private int numberOfBullet = 1;
    public float GetHealth()
    {
        return health;
    }
    public float GetDamage()
    {
        return damage;
    }
    public float GetSpeed()
    {
        return speed;
    }
    public float GetRange()
    {
        return range;
    }
    public int GetNumberOfBullet()
    {
        return numberOfBullet;
    }

    public DefaultLevel(float health, float damage, float speed, float range, int numberOfBullet)
    {
        this.health = health;
        this.damage = damage;
        this.speed = speed;
        this.range = range;
        this.numberOfBullet = numberOfBullet;
    }

}
public enum LevelPlusName
{
    Health,
    Damage,
    Speed,
    Range,
    NumberOfBullet
}
