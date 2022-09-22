using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [Header("Equiped Items")]
    public List<ItemClass> weapons;
    public ItemClass armor;
    public ItemClass ring;
    public ItemClass backpack;


    [Header("Backpack Items")]
    public List<ItemClass> items;
    public int coinsAmount;

    public PlayerStats playerStats;
    int maxItems;

    public void UpdateMaxSlots(int newMaxSlots)
    {
        maxItems = newMaxSlots;
    }

    public void takeItem(GameObject itemToAdd)
    {
        ItemScript itemScript = itemToAdd.GetComponent<ItemScript>();
        ItemClass item = ScriptableObject.CreateInstance("ItemClass") as ItemClass;
        item.init(itemScript.item.itemName, itemScript.item.canStack, itemScript.item.itemID, itemScript.item.itemType, itemScript.item.itemContainer, itemScript.item.itemSprite, itemScript.itemCount, itemScript.item.damage, itemScript.item.sellPrise);
       
        if (itemScript.item.canStack)
        {
            bool found = false; 
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].itemID == item.itemID)
                {
                    items[i].slotQuantity += itemScript.itemCount;
                    found = true;
                    Destroy(itemToAdd);
                    break;
                }
            }
            if (!found)
            {
                if(items.Count < maxItems)
                {
                    items.Add(item);
                    Destroy(itemToAdd);
                }
                else
                {
                    GameObject.FindGameObjectWithTag("PopUP").GetComponent<PopUPController>().showMessage("Inventory Full !", 1f);
                }

            }


        }
        else if (items.Count < maxItems)
        {
            items.Add(item);
            Destroy(itemToAdd);
            
        }
        else
        {
            GameObject.FindGameObjectWithTag("PopUP").GetComponent<PopUPController>().showMessage("Inventory Full !", 1f);
        }
    }

    public void AddItem(ItemClass item, int amountToAdd = 1, int quantity = 1)
    {
        for (int a = 0; a < amountToAdd; a++)
        {
            if (item.canStack)
            {
                bool found = false;
                for (int i = 0; i < items.Count; i++)
                {
                    if (items[i].itemID == item.itemID)
                    {
                        items[i].slotQuantity += quantity;
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    if (items.Count < maxItems)
                    {
                        items.Add(item);
                    }
                    else
                    {
                        GameObject.FindGameObjectWithTag("PopUP").GetComponent<PopUPController>().showMessage("Inventory Full !", 1f);
                    }

                }


            }
            else if (items.Count < maxItems)
            {
                items.Add(item);

            }
            else
            {
                GameObject.FindGameObjectWithTag("PopUP").GetComponent<PopUPController>().showMessage("Inventory Full !", 1f);
            }
        }
        

    }
}
