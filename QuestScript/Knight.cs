using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : AbstractCharacter
{


    public override void BasicAttack()
    {
        throw new System.NotImplementedException();
    }

    public override void HeavyAttack()
    {
        throw new System.NotImplementedException();
    }

    public override void SpecialAttack()
    {
        throw new System.NotImplementedException();
    }

    public override void AwakeChildren()
    {
        healthPoint = maxHp = 350;
        manaPoint = 200;
        basicDamage = 15;
        heavyDamage = 25;
        specialDamage = 40;
        currHealthBar = playerHealthBar;
        currHealthBar.setMaxHealth(healthPoint);
        currHealthBar.setHealth(healthPoint);
    }

    public override void StartChildren()
    {
        combatSystem = GetComponent<KnightCombat>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    public override void UpdateChildren()
    {
        if (canInputAttack) combatSystem.checkAttack();
    }

}
