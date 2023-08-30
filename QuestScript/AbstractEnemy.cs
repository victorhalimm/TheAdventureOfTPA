using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractEnemy : MonoBehaviour
{
    protected int health;
    protected int maxHealth;
    public int damage;

    public bool isAttacking;

    protected HealthBar healthBar;
    protected Animator anim;

    public abstract void attack();

    private void Awake()
    {
        AwakeChildren();
    }
    void Start()
    {
        anim = GetComponent<Animator>();
        healthBar = GetComponentInChildren<HealthBar>();
        StartChildren();
    }


    void Update()
    {
        UpdateChildren();
    }
    protected abstract void AwakeChildren();
    protected abstract void UpdateChildren();

    protected abstract void StartChildren();

    public void TakeDamage(int damage)
    {
        health -= damage;
        updateBar();

        if (health <= 0)
        {
            EnemyManager.instance.removeEnemy(this);
        }

    }

    public void updateBar()
    {
        healthBar.setHealth(health);
    }

}
