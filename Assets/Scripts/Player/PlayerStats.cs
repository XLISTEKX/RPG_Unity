using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float damage = 1f;
    public float critChance = 0f;
    public float critMnoznik = 2f;

    public int maxInventorySlots;
    public int armor;

    public int level = 0;
    public int evolutionPoints = 0;
    public int maxLevel = 30;
    public int[] xpToLevel;
    public int xp;
    bool canLevel = true;

    public int strength;
    public int agility;
    public int power;

    public GameObject particlesLVL;

    private void Start()
    {
        gameObject.GetComponent<PlayerInventory>().UpdateMaxSlots(maxInventorySlots);
    }

    public void addXP(int expToAdd)
    {
        if (canLevel)
        {
            xp += expToAdd;
            while (xp >= xpToLevel[level + 1])
            {
                if (xp >= xpToLevel[level + 1])
                {
                    xp -= xpToLevel[level + 1];
                    levelUP();
                }
            }
            
        }
    }

    public void levelUP()
    {
        level++;
        evolutionPoints++;

        GameObject.FindGameObjectWithTag("PopUP").GetComponent<PopUPController>().showMessage("Level UP", 0.8f);
        Instantiate(particlesLVL, transform);

        if(level >= maxLevel)
        {
            canLevel = false;
        }
    }

}
