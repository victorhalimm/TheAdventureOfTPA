using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogKnight : AbstractCharacter
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
        healthPoint = maxHp = 600;
        manaPoint = 200;
        basicDamage = 25;
        heavyDamage = 35;
        specialDamage = 45;
        currHealthBar = playerHealthBar;
        currHealthBar.setMaxHealth(healthPoint);
        currHealthBar.setHealth(healthPoint);
    }

    public override void StartChildren()
    {
        combatSystem = GetComponent<DogCombat>();
    }

    public override void UpdateChildren()
    {
        if (canInputAttack) combatSystem.checkAttack();
    }
}
