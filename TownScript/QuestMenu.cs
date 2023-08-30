using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestMenu : MonoBehaviour
{
    public void playQuest()
    {
        SceneManager.LoadScene("QuestScene", LoadSceneMode.Single);
    }
}
