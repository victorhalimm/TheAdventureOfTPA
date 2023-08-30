using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractCharacter : MonoBehaviour
{

    public int healthPoint;
    public int maxHp;
    public int manaPoint;

    public int basicDamage;
    public int heavyDamage;
    public int specialDamage;

    public PlayerMovement playerMovement;
    public bool moveAllowed = false;

    public AbstractCombat combatSystem;
    public Animator anim;

    public HealthBar playerHealthBar;
    public HealthBar currHealthBar;

    public Canvas healthCanvas;

    public UnitToEnemy playerUnits;
    private bool pathFinding = true;
    public AbstractEnemy nearestEnemy;
    private float changeTargetInterval = 1f;
    private float lastChangeTarget = 0;

    public bool canInputAttack = false;
    private Rigidbody playerRb;

    private void Awake()
    {
        healthCanvas = GetComponentInChildren<Canvas>();
        playerUnits = GetComponent<UnitToEnemy>();
        anim = GetComponent<Animator>();
        AwakeChildren();
    }
    protected void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerUnits.target = findNearestEnemy();
        playerRb = GetComponent<Rigidbody>();
        StartChildren();
    }
    protected void Update()
    {
        if (moveAllowed) playerMovement.movePlayer();
        // Delete later just for testing healthbar
        if (Input.GetKeyDown(KeyCode.P))
        {
            TakeDamage(20);
        }
        if (pathFinding && Time.time - lastChangeTarget > changeTargetInterval)
        {
            playerUnits.target = findNearestEnemy();
        }
        UpdateChildren();
    }

    public abstract void AwakeChildren();
    public abstract void StartChildren();
    public abstract void UpdateChildren(); 
    public abstract void BasicAttack();

    public abstract void HeavyAttack();

    public abstract void SpecialAttack();

    public void TakeDamage(int damage)
    {
        healthPoint -= damage;

        updateHealthBar();
    }

    public void updateHealthBar()
    {
        currHealthBar.setMaxHealth(maxHp);
        currHealthBar.setHealth(healthPoint);
    }

    private Transform findNearestEnemy()
    {
        List<AbstractEnemy> currActiveEnemies = EnemyManager.instance.currActiveEnemy;
        float currMinDistance = Vector3.Distance(transform.position, currActiveEnemies[0].transform.position);
        AbstractEnemy nearestEnemy = currActiveEnemies[0];

        foreach (AbstractEnemy enemy in currActiveEnemies)
        {
            float enemyDistance = Vector3.Distance(transform.position, enemy.transform.position);
            if (enemyDistance < currMinDistance)
            {
                currMinDistance = enemyDistance;
                nearestEnemy = enemy;
            }
        }
        return nearestEnemy.transform;
    }

    public void stopMovement()
    {
        playerRb.velocity = Vector3.zero;
        playerRb.angularVelocity = Vector3.zero;
    }
    
}
