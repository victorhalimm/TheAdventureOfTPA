using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    public void retryGame()
    {
        if (GameManager.instance.gameObject) Destroy(GameManager.instance.gameObject);
        SceneManager.LoadScene("QuestScene");
    }

    public void back()
    {
        if (GameManager.instance.gameObject) Destroy(GameManager.instance.gameObject);
        SceneManager.LoadScene("VillageScene");
    }
}
