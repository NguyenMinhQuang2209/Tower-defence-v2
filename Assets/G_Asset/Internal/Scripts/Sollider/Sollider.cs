using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Sollider : MonoBehaviour
{
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private Transform sizeObject;
    [SerializeField] private LayerMask attackMask;
    [SerializeField] private Transform weaponWrap;
    private Transform target = null;
    private UpgradeLevel upgradeLevel;
    private Weapon currentWeapon;
    private void Start()
    {
        upgradeLevel = GetComponent<UpgradeLevel>();
        if (upgradeLevel != null && TryGetComponent<UpgradeItem>(out var upgradeItem))
        {
            upgradeLevel.AddUpgradeItem(UpgradeName.Sollider, upgradeItem);
        }
        sizeObject.transform.localScale = new(attackRange * 2f, attackRange * 2f, 0f);
        ShowAttackSize();
    }
    private void Update()
    {
        if (target == null)
        {
            ScanTargetObject();
            if (currentWeapon != null)
            {
                currentWeapon.ResetAttack();
            }
        }
        else
        {
            AttackTargetObject();
        }
    }
    public void ScanTargetObject()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, attackRange, attackMask);
        if (collider.Length > 0)
        {
            target = collider[0].gameObject.transform;
            float distance = Vector2.Distance(transform.position, target.position);
            for (int i = 1; i < collider.Length; i++)
            {
                Transform setTarget = collider[i].gameObject.transform;
                float newDistance = Vector2.Distance(transform.position, setTarget.position);
                if (distance > newDistance)
                {
                    distance = newDistance;
                    target = setTarget;
                }
            }
        }
    }
    public void AttackTargetObject()
    {
        Vector3 targetPos = target.position - transform.position;
        float angle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
        bool direction = targetPos.x > 0 ? true : false;
        weaponWrap.rotation = Quaternion.Euler(new(0f, direction ? 0f : 180f, direction ? angle : 180 - angle));
        transform.rotation = Quaternion.Euler(new(0f, targetPos.x > 0 ? 0f : 180f, 0f));
        float distance = Vector2.Distance(transform.position, target.position);
        if (distance >= attackRange)
        {
            target = null;
        }
        else
        {
            if (currentWeapon != null)
            {
                currentWeapon.Attack(target.position);
            }
        }
    }
    public void EquipmentWeapon(Weapon weapon, bool isPrefab = false)
    {
        if (currentWeapon != null)
        {
            Destroy(currentWeapon.gameObject);
        }
        if (isPrefab)
        {
            currentWeapon = Instantiate(weapon, weaponWrap.transform);
        }
        else
        {
            weapon.transform.SetParent(weaponWrap);
            currentWeapon = weapon;
        }
        currentWeapon.transform.localPosition = Vector3.zero;
        currentWeapon.transform.localRotation = Quaternion.identity;
        if (currentWeapon.TryGetComponent<UpgradeItem>(out var upgradeWeapon))
        {
            upgradeLevel.AddUpgradeItem(UpgradeName.Weapon, upgradeWeapon);
        }
        currentWeapon.WeaponInit(attackRange);
    }
    public void HideAttackSize()
    {
        sizeObject.gameObject.SetActive(false);
    }
    public void ShowAttackSize()
    {
        sizeObject.gameObject.SetActive(true);
    }
    public void ChangeStoreParent(UpgradeItem upgradeParent)
    {
        if (upgradeParent != null)
        {
            upgradeLevel.AddUpgradeItem(UpgradeName.Foundation, upgradeParent);
        }
    }
}
