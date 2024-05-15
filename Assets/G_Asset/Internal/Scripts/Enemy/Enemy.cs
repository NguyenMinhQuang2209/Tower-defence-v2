using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public abstract class Enemy : Health
{
    [SerializeField] private EnemyScriptableObject enemyDefault;
    protected float currentWaitTimer = 0f;
    protected List<Vector2> paths = new();
    protected Rigidbody2D rb;
    protected Vector2 target;
    protected bool isHasPath;
    protected Animator animator;
    protected EnemyName enemyName;
    public void EnemyInit()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        maxHealth = enemyDefault.maxHealth;
        HealthInit();
        FindPath();
    }
    public void Movement()
    {
        if (!isHasPath)
        {
            Vector2 targetPos = target - (Vector2)transform.position;
            targetPos.Normalize();
            rb.MovePosition(rb.position + enemyDefault.speed * Time.deltaTime * targetPos);
            return;
        }
        float distance = Vector2.Distance(transform.position, target);
        if (distance > enemyDefault.stopDistance)
        {
            Vector2 targetPos = target - (Vector2)transform.position;
            targetPos.Normalize();
            rb.MovePosition(rb.position + enemyDefault.speed * Time.deltaTime * targetPos);
            return;
        }
        if (paths.Count > 0)
        {
            target = paths[0];
            paths.RemoveAt(0);
            Rotation(target);
        }
        else
        {
            Vector2 o_target = GameManager.instance.GetTargetPosition();
            float o_distance = Vector2.Distance(o_target, transform.position);
            if (o_distance < enemyDefault.stopDistance)
            {
                CallAttack();
            }
            else
            {
                FindPath();
            }
        }
    }
    public void Rotation(Vector2 target)
    {
        float x = target.x - transform.position.x;
        transform.rotation = Quaternion.Euler(0f, x > 0 ? 0f : 180f, 0f);
    }
    public void CallAttack()
    {
        currentWaitTimer += Time.deltaTime;
        if (currentWaitTimer >= enemyDefault.timeBwtAttack)
        {
            Attack();
            currentWaitTimer = 0f;
        }
    }
    public virtual void Attack()
    {

    }
    public void FindPath()
    {
        paths?.Clear();
        paths = GameManager.instance.FindPaths(transform.position);
        isHasPath = true;
        if (paths == null || paths.Count == 0)
        {
            isHasPath = false;
            target = GameManager.instance.GetTargetPosition();
            Rotation(target);
        }
    }
    public EnemyName GetEnemyName()
    {
        return enemyName;
    }
    private void OnEnable()
    {
        MapGenerator.reloadMapAction += ReloadMapEvent;
    }

    private void ReloadMapEvent()
    {
        FindPath();
    }

    private void OnDisable()
    {
        MapGenerator.reloadMapAction -= ReloadMapEvent;
    }
}
