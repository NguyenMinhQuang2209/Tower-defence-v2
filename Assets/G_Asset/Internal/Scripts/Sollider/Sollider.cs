using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sollider : MonoBehaviour
{
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private Transform sizeObject;
    [SerializeField] private LayerMask attackMask;
    private Transform target = null;
    private void Start()
    {
        sizeObject.transform.localScale = new(attackRange * 2f, attackRange * 2f, 0f);
        ShowAttackSize();
    }
    private void Update()
    {
        if (target == null)
        {
            ScanTargetObject();
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
        float xAxis = target.position.x - transform.position.x;
        transform.rotation = Quaternion.Euler(new(0f, xAxis > 0 ? 0f : 180f, 0f));
        float distance = Vector2.Distance(transform.position, target.position);
        if (distance >= attackRange)
        {
            target = null;
        }
    }
    public void HideAttackSize()
    {
        sizeObject.gameObject.SetActive(false);
    }
    public void ShowAttackSize()
    {
        sizeObject.gameObject.SetActive(true);
    }
}
