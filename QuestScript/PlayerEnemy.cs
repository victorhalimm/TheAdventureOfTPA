using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnemy : AbstractEnemy
{
    AbstractCharacter[] playerList;

    [SerializeField] private float changeTargetInterval;

    float lastChangeTarget;

    protected UnitPlayer unit;

    public override void attack()
    {
        
    }

    protected override void AwakeChildren()
    {
        unit = GetComponentInChildren<UnitPlayer>();
    }

    protected override void StartChildren()
    {
        playerList = PlayerManager.instance.getAllPlayers();
        health = 200;
        maxHealth = 200;
        damage = 25;
        healthBar.setMaxHealth(maxHealth);
        healthBar.setHealth(health);
        lastChangeTarget = Time.time;
        unit.target = nearestPlayer();
    }

    protected override void UpdateChildren()
    {
        if (Time.time - lastChangeTarget > changeTargetInterval)
        {
            unit.target = nearestPlayer();
        }
        if (anim.GetBool("Attack"))
        {
            isAttacking = true;
        }
        else isAttacking = false;
    }

    private Transform nearestPlayer()
    {
        float currMinDistance = Vector3.Distance(transform.position, playerList[0].transform.position);
        AbstractCharacter nearestPlayer = playerList[0];

        foreach (AbstractCharacter player in playerList)
        {
            float playerDistance = Vector3.Distance(transform.position, player.transform.position);
            if (playerDistance < currMinDistance)
            {
                currMinDistance = playerDistance;
                nearestPlayer = player;
            }
        }
        return nearestPlayer.transform;
    }


}
