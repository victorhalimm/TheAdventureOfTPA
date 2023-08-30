using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    public static EnemyManager instance;

    [SerializeField] private AbstractEnemy crystalEnemy;
    [SerializeField] private AbstractEnemy playerEnemy;

    public List<AbstractEnemy> currActiveEnemy;
    int enemyCount = 0;

    int maxEnemy = 6;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        // buat ngespawn 3 musuh awal
        AbstractEnemy initialPlayer = Instantiate(playerEnemy, transform.position, Quaternion.identity);
        AbstractEnemy initialCrystal = Instantiate(crystalEnemy, transform.position, Quaternion.identity);
        AbstractEnemy initialCrystal2 = Instantiate(crystalEnemy, transform.position, Quaternion.identity);
        currActiveEnemy = new List<AbstractEnemy>();
        currActiveEnemy.Add(initialPlayer);
        currActiveEnemy.Add(initialCrystal);
        currActiveEnemy.Add(initialCrystal2);
        enemyCount = 3;
        StartCoroutine(spawnEnemies());
    }
    
    void spawnEnemy(AbstractEnemy enemy)
    {
        if (enemyCount < maxEnemy)
        {
            AbstractEnemy newEnemy = Instantiate(enemy, transform.position, Quaternion.identity);
            enemyCount++;
        }
    }

    public void removeEnemy(AbstractEnemy enemy)
    {
        currActiveEnemy.Remove(enemy);
        Destroy(enemy.gameObject);

        enemyCount--;
        GameManager.instance.enemyKilled++;
    }
    IEnumerator spawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(15);
            if (Random.value < 0.7f) spawnEnemy(crystalEnemy);
            else spawnEnemy(playerEnemy);
        }
    }
}
