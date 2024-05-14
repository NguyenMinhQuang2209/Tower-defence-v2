using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    [SerializeField, Min(0f)] protected float maxHealth = 1f;
    protected float currentHealth = 0f;
    protected float plusHealth = 0f;
    protected bool isDealth = false;
    protected bool immortal = false;
    public void HealthInit()
    {
        currentHealth = GetMaxHealth();
    }
    ///<summary>
    /// Adding default health value
    ///</summary>
    public void HealthInit(float health)
    {
        currentHealth = Mathf.Min(health, GetMaxHealth());
    }
    public float GetMaxHealth()
    {
        return maxHealth + plusHealth;
    }
    public float RecoverHealth(float heal)
    {
        float nextHealth = currentHealth + heal;
        currentHealth = Mathf.Min(nextHealth, GetMaxHealth());
        return nextHealth - currentHealth;
    }
    public void RecoverFullHealth()
    {
        currentHealth = GetMaxHealth();
    }
    public void ChangePlusHealth(float v)
    {
        plusHealth = v;
    }
    public void AddPlusHealth(float v)
    {
        ChangePlusHealth(plusHealth + v);
    }
    public void MinusPlusHealth(float v)
    {
        ChangePlusHealth(Mathf.Max(0f, plusHealth - v));
    }
    public void TakeDamage(float damage)
    {
        if (isDealth || immortal)
        {
            return;
        }
        currentHealth = Mathf.Max(currentHealth - damage, 0f);
        if (currentHealth == 0f)
        {
            isDealth = true;
            Dealth();
        }
    }
    public GameObject TakeDamage(float damage, GameObject sender)
    {
        TakeDamage(damage);
        return sender;
    }
    public IEnumerable TriggerImmortal(float time)
    {
        immortal = true;
        yield return new WaitForSeconds(time);
        immortal = false;
    }
    public virtual void Dealth()
    {

    }
}
