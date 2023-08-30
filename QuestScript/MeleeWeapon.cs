using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    private AbstractCombat combat;
    private AbstractCharacter playerStat;

    private void Awake()
    {
        combat = GetComponentInParent<AbstractCombat>();
        playerStat = GetComponentInParent<AbstractCharacter>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && combat.isAttacking)
        {
            other.GetComponent<AbstractEnemy>().TakeDamage(playerStat.basicDamage);
        }
    }
}
