using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalEnemy : AbstractEnemy
{
    protected UnitCrystal unit;
    public override void attack()
    {
        
    }

    protected override void AwakeChildren()
    {
        
    }

    protected override void StartChildren()
    {
        unit = GetComponent<UnitCrystal>();
        unit.target = Crystal.instance.transform;
        health = 300;
        maxHealth = 300;
        damage = 50;
        healthBar.setMaxHealth(maxHealth);
        healthBar.setHealth(health);
    }

    protected override void UpdateChildren()
    {
        if (anim.GetBool("Attack"))
        {
            isAttacking = true;
        }
        else isAttacking = false;
    }

}
