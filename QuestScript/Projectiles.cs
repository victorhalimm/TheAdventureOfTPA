using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    private bool isCollided = false;

    [SerializeField] private GameObject electroHit;
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player") && !isCollided)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                AbstractEnemy enemy = collision.gameObject.GetComponent<AbstractEnemy>();
                enemy.TakeDamage(20);
            }
            var hit = Instantiate(electroHit, collision.contacts[0].point, Quaternion.identity) as GameObject;

            Destroy(gameObject);
            Destroy(hit, 1f);
            isCollided = true;
        }
    }
}
