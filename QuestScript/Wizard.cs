using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : AbstractCharacter
{
    public override void BasicAttack()
    {
        combatSystem.checkAttack();
    }

    public override void HeavyAttack()
    {
        throw new System.NotImplementedException();
    }

    public override void SpecialAttack()
    {
        throw new System.NotImplementedException();
    }


    /*    public static Wizard instance;*/

    /*    private void Awake()
        {
            instance = this;
        }*/

    public override void AwakeChildren()
    {
        healthPoint = maxHp = 250;
        manaPoint = 200;
        basicDamage = 20;
        heavyDamage = 0;
        specialDamage = 40;
        currHealthBar = playerHealthBar;
        currHealthBar.setMaxHealth(healthPoint);
        currHealthBar.setHealth(healthPoint);
    }
    public override void StartChildren()
    {
        specialDamage = 40;
        playerMovement = GetComponent<PlayerMovement>();
        combatSystem = GetComponent<WizardCombat>();
        playerMovement.enabled = false;
    }

    public override void UpdateChildren()
    {
        if (Input.GetMouseButtonDown(0) && canInputAttack) BasicAttack();
    }

}
