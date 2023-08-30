using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] TextMeshProUGUI timeText;

    public bool inQuest = true;
    public bool inGameOver = false;

    public int enemyKilled = 0;

    private float time = 0f;
    public int currMinute;
    public int currSecond;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (inQuest) UpdateTimer();
        else if (inGameOver) EnableCursor();
    }

    void UpdateTimer()
    {
        time += Time.deltaTime;

        currMinute = Mathf.FloorToInt(time / 60);
        currSecond = Mathf.FloorToInt(time % 60);

        timeText.text = string.Format("{00:00}:{1:00}", currMinute, currSecond);
    }

    void EnableCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

}
