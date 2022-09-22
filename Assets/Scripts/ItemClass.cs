using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class ItemClass : ScriptableObject
{
    public string itemName;
    public bool canStack;
    public int itemID;
    public int itemType; //0 - none, 1 - Weapon, 2 - Armor, 3 - Ring, 4 - Backpack
    public GameObject itemContainer;
    public Sprite itemSprite;
    public int slotQuantity;
    public int damage;
    public int sellPrise;

    public ItemClass(string itemName, bool canStack, int itemID,int itemType, GameObject itemContainer, Sprite itemSprite, int itemQuant, int damage, int sellPrise)
    {
        this.itemName = itemName;
        this.canStack = canStack;
        this.itemID = itemID;
        this.itemType = itemType;
        this.itemContainer = itemContainer;
        this.itemSprite = itemSprite;
        this.slotQuantity = itemQuant;
        this.damage = damage;
        this.sellPrise = sellPrise;
    }
    public void init(string itemName, bool canStack, int itemID, int itemType, GameObject itemContainer, Sprite itemSprite, int itemQuant, int damage, int sellPrise)
    {
        this.itemName = itemName;
        this.canStack = canStack;
        this.itemID = itemID;
        this.itemType = itemType;
        this.itemContainer = itemContainer;
        this.itemSprite = itemSprite;
        this.slotQuantity = itemQuant;
        this.damage = damage;
        this.sellPrise = sellPrise;
    }
}
