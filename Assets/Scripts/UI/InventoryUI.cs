using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public GameObject evolutionCanvas;
    public PlayerInventory inventory;
    public int inventorySlots;
    public GameObject itemFrames;
    [Header("EQ | Inventory")]
    public List<GameObject> FramesEQ; // 0-2 Weapons; 3 - Armor; 4-Ring; 5-Backpack;
    public GameObject equipButton;
    public GameObject unequipButton;

    [Header("Slider | Inventory")]
    public GameObject slider;
    public GameObject selectImage;
    public List<GameObject> FramesCreated;
    public int pressedFrameID = -1;

    [Header("Stats In Game")]
    public TMP_Text coinsCounter;
    public TMP_Text slotsStat;
    public TMP_Text armorStat;
    public TMP_Text damageStat;
    public TMP_Text levelStat;
    public TMP_Text CritStat;
    public TMP_Text critX;

    GameObject createdSelect;


    private void OnEnable()
    {
        inventorySlots = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().maxInventorySlots;
        statsUpdate();
        slotsUpdate();
        coinsUpdate();
    }

    void coinsUpdate()
    {
        coinsCounter.text = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>().coinsAmount.ToString();

    }
    public void slotsUpdate()
    {
        
        int itemsInInventory = inventory.items.Count;
        if (FramesCreated.Count < inventorySlots)
        {
            for (int i = FramesCreated.Count; i < inventorySlots; i++)
            {
                GameObject x = Instantiate(itemFrames, slider.transform);
                FramesCreated.Add(x);
                ItemFrameClass frame = x.GetComponent<ItemFrameClass>();
                frame.ID = i;
                x.GetComponent<Button>().onClick.AddListener(delegate { chooseFrame(frame.ID); });

                if (i < itemsInInventory)
                {
                    if (inventory.items[i].canStack)
                    {
                        frame.counter.gameObject.SetActive(true);
                        frame.counter.text = inventory.items[i].slotQuantity.ToString();
                    }
                    else
                    {
                        frame.counter.gameObject.SetActive(false);
                    }

                    frame.itemImage.gameObject.SetActive(true);
                    frame.itemImage.sprite = inventory.items[i].itemSprite;

                }
                else
                {
                    frame.counter.gameObject.SetActive(false);
                    frame.itemImage.gameObject.SetActive(false);
                }
            }
        }
        else
        {
            for(int i = 0; i < inventorySlots; i++)
            {
                ItemFrameClass itemFrame = FramesCreated[i].GetComponent<ItemFrameClass>();
                if(i < itemsInInventory)
                {
                    itemFrame.itemImage.gameObject.SetActive(true);
                    itemFrame.itemImage.sprite = inventory.items[i].itemSprite;
                    if (inventory.items[i].canStack)
                    {
                        itemFrame.counter.gameObject.SetActive(true);
                        itemFrame.counter.text = inventory.items[i].slotQuantity.ToString();
                    }
                    else
                    {
                        itemFrame.counter.gameObject.SetActive(false);
                    }
                }
                else
                {
                    itemFrame.counter.gameObject.SetActive(false);
                    itemFrame.itemImage.gameObject.SetActive(false);
                }

            }
        }

    }
    public void statsUpdate()
    {
        levelStat.text = (inventory.playerStats.level + 1).ToString();
        CritStat.text = (inventory.playerStats.critChance * 100).ToString() + "%";
        critX.text = inventory.playerStats.critMnoznik.ToString();

        slotsStat.text = inventory.playerStats.maxInventorySlots.ToString();

        damageStat.text = inventory.playerStats.damage.ToString();
        armorStat.text = inventory.playerStats.armor.ToString();
    }

    public void chooseFrame(int ID)
    {
        if (ID < -1)
        {
            if (FramesEQ[ID + 7].GetComponent<ItemFrameClass>().itemImage.gameObject.activeSelf)
            {
                if (pressedFrameID == -1)
                {
                    pressedFrameID = ID;
                    createdSelect = Instantiate(selectImage, FramesEQ[ID + 7].transform);
                    createdSelect.transform.localScale = new Vector3(0.8f, 0.8f, 1);
                    unequipButton.SetActive(true);
                }
                else if (pressedFrameID == ID)
                {
                    pressedFrameID = -1;
                    Destroy(createdSelect);
                    unequipButton.SetActive(false);
                }
                else
                {
                    equipButton.SetActive(false);
                    unequipButton.SetActive(true);
                    pressedFrameID = ID;
                    Destroy(createdSelect);
                    createdSelect = Instantiate(selectImage, FramesEQ[ID + 7].transform);
                    createdSelect.transform.localScale = new Vector3(0.8f, 0.8f, 1);
                }
            }
            


        }
        else
        {
            int itemsInInventory = inventory.items.Count;
            if (ID < itemsInInventory)
            {
                if (pressedFrameID == -1)
                {
                    pressedFrameID = ID;
                    createdSelect = Instantiate(selectImage, FramesCreated[ID].transform);
                    if (inventory.items[pressedFrameID].itemType != 0)
                    {
                        equipButton.SetActive(true);
                    }

                }
                else if (pressedFrameID == ID)
                {
                    pressedFrameID = -1;
                    Destroy(createdSelect);
                    equipButton.SetActive(false);
                }
                else if(pressedFrameID > -1)
                {
                    Destroy(createdSelect);
                    PlayerInventory inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
                    ItemClass itemTMP = inventory.items[ID];
                    inventory.items[ID] = inventory.items[pressedFrameID];
                    inventory.items[pressedFrameID] = itemTMP;
                    equipButton.SetActive(false);
                    slotsUpdate();

                    pressedFrameID = -1;
                }
                else
                {
                    Destroy (createdSelect);
                    pressedFrameID = ID;
                    createdSelect = Instantiate(selectImage, FramesCreated[ID].transform);
                    unequipButton.SetActive(false);
                    if (inventory.items[pressedFrameID].itemType != 0)
                    {
                        equipButton.SetActive(true);
                    }
                }
            }
        }
        
        
        
    }
    public void equipItem()
    {
        bool equiped = false;
        switch(inventory.items[pressedFrameID].itemType){
            case 0:

                break;
            case 1:
                if(inventory.weapons.Count < 3)
                {
                    
                    inventory.weapons.Add(inventory.items[pressedFrameID]);
                    if (inventory.weapons.Count == 1)
                    {
                        inventory.playerStats.damage += inventory.items[pressedFrameID].damage;
                    }
                    equiped = true;
                }
                break;
            case 2:
                if(inventory.armor == null)
                {
                    inventory.armor = inventory.items[pressedFrameID];
                    equiped = true;
                }
                
                break;
            case 3:
                if (inventory.ring == null)
                {
                    inventory.ring = inventory.items[pressedFrameID];
                    equiped = true;
                }
                break;
            case 4:
                if (inventory.backpack == null)
                {
                    inventory.backpack = inventory.items[pressedFrameID];
                    equiped = true;
                }
                    
                break;
        }
        if (equiped)
        {
            updateEqSlots();
            statsUpdate();
            equipButton.SetActive(false);
            inventory.items.RemoveAt(pressedFrameID);
            Destroy(createdSelect);
            pressedFrameID = -1;
            slotsUpdate();

        }

    }

    public void updateEqSlots()
    {
        for (int i = 0; i < FramesEQ.Count; i++)
        {
            ItemFrameClass frame = FramesEQ[i].GetComponent<ItemFrameClass>();
            frame.itemImage.gameObject.SetActive(false);
            if (i < inventory.weapons.Count)
            {
                frame.itemImage.gameObject.SetActive(true);
                frame.itemImage.sprite = inventory.weapons[i].itemSprite;

               
            }
            else if (i >= 3)
            {
                switch (i)
                {
                    case 3:
                        if(inventory.armor != null)
                        {
                            frame.itemImage.gameObject.SetActive(true);
                            frame.itemImage.sprite = inventory.armor.itemSprite;
                        }
                        break;
                    case 4:
                        if (inventory.ring != null)
                        {
                            frame.itemImage.gameObject.SetActive(true);
                            frame.itemImage.sprite = inventory.ring.itemSprite;
                        }
                        break;
                    case 5:
                        if (inventory.backpack != null)
                        {
                            frame.itemImage.gameObject.SetActive(true);
                            frame.itemImage.sprite = inventory.backpack.itemSprite;
                        }

                        break;
                }
            }
        }
    }
    public void unequipItem()
    {
        switch (pressedFrameID)
        {
            case <= -5:
                if (inventory.weapons.Count == 1)
                {
                    inventory.playerStats.damage -= inventory.weapons[0].damage;
                }
                inventory.AddItem(inventory.weapons[pressedFrameID + 7]);
                inventory.weapons.RemoveAt(pressedFrameID+7);
                break;
            case -4:
                inventory.AddItem(inventory.armor);
                inventory.armor = null;
                break;
            case -3:
                inventory.AddItem(inventory.ring);
                inventory.ring = null;
                break;
            case -2:
                inventory.AddItem(inventory.backpack);
                inventory.backpack = null;
                break;
        }
        updateEqSlots();
        slotsUpdate();
        statsUpdate();
        unequipButton.SetActive(false);
        Destroy(createdSelect);
        pressedFrameID = -1;

    }

    public void openEvolution()
    {
        gameObject.SetActive(false);
        evolutionCanvas.SetActive(true);
    }
}
