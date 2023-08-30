using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerManager : MonoBehaviour
{
    public AbstractCharacter[] characters;

    [SerializeField] private CinemachineFreeLook knightCamera;
    [SerializeField] private CinemachineFreeLook dogCamera;
    [SerializeField] private CinemachineFreeLook wizardCamera;

    [SerializeField] private HealthBar screenHealth;

    public static PlayerManager instance;

    private CinemachineFreeLook[] camCollection;

    int currPlayer = 0;
    
    private void Start()
    {
        camCollection = new CinemachineFreeLook[] { knightCamera, dogCamera , wizardCamera};
        instance = this;
        characters[currPlayer].moveAllowed = true;
        characters[currPlayer].playerUnits.enabled = false;
        characters[currPlayer].healthCanvas.gameObject.SetActive(false);
        characters[currPlayer].currHealthBar = screenHealth;
        characters[currPlayer].canInputAttack = true;
        characters[currPlayer].updateHealthBar();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            changePlayer();
        }
    }

    void changePlayer()
    {
        camCollection[currPlayer].Priority = 1;
        characters[currPlayer].healthCanvas.gameObject.SetActive(true);
        characters[currPlayer].currHealthBar = characters[currPlayer].playerHealthBar;
        characters[currPlayer].updateHealthBar();
        characters[currPlayer].playerUnits.enabled = true;
        characters[currPlayer].moveAllowed = false;
        characters[currPlayer].canInputAttack = false;
        characters[currPlayer].combatSystem.resetAllBool();

        currPlayer = (currPlayer + 1) % camCollection.Length;

        camCollection[currPlayer].Priority = 10;
        characters[currPlayer].moveAllowed = true;
        characters[currPlayer].canInputAttack = true;
        characters[currPlayer].playerUnits.target = null;
        characters[currPlayer].playerUnits.stopFollowing();
        characters[currPlayer].playerUnits.enabled = false;
        characters[currPlayer].stopMovement();
        characters[currPlayer].currHealthBar = screenHealth;
        characters[currPlayer].updateHealthBar();
        characters[currPlayer].combatSystem.resetAllBool();
        characters[currPlayer].healthCanvas.gameObject.SetActive(false);
    }

    public AbstractCharacter[] getAllPlayers()
    {
        return characters;
    }

/*    public void updateHealthBar(HealthBar bar, AbstractCharacter player)
    {
        Debug.Log(player.maxHp);
        bar.setMaxHealth(player.maxHp);
        bar.setHealth(player.healthPoint);
    }*/
}
