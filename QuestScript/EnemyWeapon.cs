using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public AbstractEnemy enemyStats;
    

    private void Start()
    {
        enemyStats = GetComponentInParent<AbstractEnemy>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("Crystal") || other.CompareTag("Player")) && enemyStats.isAttacking)
        {
            if (other.CompareTag("Crystal")) other.GetComponent<Crystal>().TakeDamage(enemyStats.damage);
            if (other.CompareTag("Player")) other.GetComponent<AbstractCharacter>().TakeDamage(enemyStats.damage);
        }
    }
}
