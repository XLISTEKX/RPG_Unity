using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelCanvasControler : MonoBehaviour
{
    public GameObject inventoryCanvas;
    public TMP_Text xpCounter;
    public TMP_Text xpPointsCounter;
    public TMP_Text levelCounter;


    public GameObject upgradeButtons;
    public TMP_Text levelStrengthCounter;
    public TMP_Text levelAgilityCounter;
    public TMP_Text levelPowerCounter;

    PlayerStats playerStats;



    private void OnEnable()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        updateCanvas();
    }

    public void updateCanvas()
    {
        if (playerStats.evolutionPoints != 0)
            upgradeButtons.SetActive(true);
        else
            upgradeButtons.SetActive(false);


        
        levelCounter.text = (playerStats.level + 1).ToString();
        xpPointsCounter.text = playerStats.evolutionPoints.ToString();
        levelStrengthCounter.text = playerStats.strength.ToString();
        levelAgilityCounter.text = playerStats.agility.ToString();
        levelPowerCounter.text = playerStats.power.ToString();

        xpCounter.text = playerStats.xp.ToString() + " / " + playerStats.xpToLevel[playerStats.level + 1];

        

        
    }

    public void closeEvolution()
    {
        gameObject.SetActive(false);
        inventoryCanvas.SetActive(true);

    }

    public void rankUP(int statToUP)
    {
        switch (statToUP)
        {
            case 0:
                playerStats.strength++;
                break;
            case 1:
                playerStats.agility++;
                break;
            case 2:
                playerStats.power++;
                break;
        }

        playerStats.evolutionPoints--;
        updateCanvas();
    }

}
