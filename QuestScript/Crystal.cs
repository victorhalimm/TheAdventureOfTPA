using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Crystal : MonoBehaviour
{

    int health = 500;
    int damage;

    public static Crystal instance;
    [SerializeField] private CrystalBar crystalBar; 

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        crystalBar.setMaxHealth(health);
        crystalBar.setHealth(health);
    }

    public void updateBar()
    {
        crystalBar.setHealth(health);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        updateBar();

        if (health <= 0)
        {
            GameManager.instance.inQuest = false;
            GameManager.instance.inGameOver = true;
            SceneManager.LoadScene("GameOver");
        }
    }

}
