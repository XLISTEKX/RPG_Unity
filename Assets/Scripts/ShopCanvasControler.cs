using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopCanvasControler : MonoBehaviour
{
    [Header("Shop")]
    int shopCash;
    PlayerInventory playerInventory;
    public GameObject gameplayCanvas;
    public GameObject buyPanel; 
    public GameObject sellPanel;
    public GameObject shop;
    public TMP_Text moneyPlayerCounter;
    public TMP_Text moneyShopCounter;

    [Header("Shop | Inventory")]
    public GameObject itemFrame;
    public List<GameObject> itemFramesCreated;
    public List<GameObject> itemFramesSellCreated;
    public List<ItemClass> items;
    public List<int> slotQuantity;
    public List<int> prises;
    public Transform framesSpawn;
    public Transform framesSellSpawn;

    public void closeShop()
    {
        shop.GetComponent<ShopControler>().shopCash = shopCash;

        gameplayCanvas.SetActive(true);
        Time.timeScale = 1f;
        Destroy(gameObject);
    }

    public void openShop(GameObject gameplayCanvas, GameObject shop, List<ItemClass> content, int shopMoney, List<int> prises, List<int> slotq)
    {
        this.shop = shop;
        playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
        shopCash = shopMoney;
        items = content;
        slotQuantity = slotq;
        this.prises = prises;
        this.gameplayCanvas = gameplayCanvas;

        


        for (int i = 0; i < content.Count; i++)
        {
            GameObject x = Instantiate(itemFrame, framesSpawn);
            itemFramesCreated.Add(x);
            ItemFrameShopClass frame = itemFramesCreated[i].GetComponent<ItemFrameShopClass>();

            if (items[i].canStack)
            {
                frame.counter.text = slotQuantity[i].ToString();
                frame.counter.gameObject.SetActive(true);

            }

            frame.ID = i;
            frame.itemImage.sprite = content[i].itemSprite;
            frame.itemImage.gameObject.SetActive(true);
            frame.priceTXT.text = prises[i].ToString();

            itemFramesCreated[i].GetComponent<Button>().onClick.AddListener(delegate { buyItem(frame.ID); });


        }

        updateMoney();
    }

    public void updateFrames()
    {
        foreach (GameObject frame in itemFramesCreated)
        {
           
            Destroy(frame.gameObject);
        }

        itemFramesCreated.Clear();

        for (int i = 0; i < items.Count; i++)
        {
            GameObject x = Instantiate(itemFrame, framesSpawn);
            itemFramesCreated.Add(x);
            ItemFrameShopClass frame = itemFramesCreated[i].GetComponent<ItemFrameShopClass>();

            if (items[i].canStack)
            {
                frame.counter.text = slotQuantity[i].ToString();
                frame.counter.gameObject.SetActive(true);

            }

            frame.ID = i;
            frame.itemImage.sprite = items[i].itemSprite;
            frame.itemImage.gameObject.SetActive(true);
            frame.priceTXT.text = prises[i].ToString();

            itemFramesCreated[i].GetComponent<Button>().onClick.AddListener(delegate { buyItem(frame.ID); });


        }
    }
    public void updateSellFrames()
    {
        foreach (GameObject frame in itemFramesSellCreated)
        {

            Destroy(frame.gameObject);
        }

        itemFramesSellCreated.Clear();
        PlayerInventory playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();

        for (int i = 0; i < playerInventory.items.Count; i++)
        {
            GameObject x = Instantiate(itemFrame, framesSellSpawn);
            itemFramesSellCreated.Add(x);
            ItemFrameShopClass frame = itemFramesSellCreated[i].GetComponent<ItemFrameShopClass>();

            frame.ID = i;
            frame.itemImage.sprite = playerInventory.items[i].itemSprite;
            frame.itemImage.gameObject.SetActive(true);
            frame.priceTXT.text = playerInventory.items[i].sellPrise.ToString();

            //itemFramesSellCreated[i].GetComponent<Button>().onClick.AddListener(delegate { buyItem(frame.ID); });


        }
    }

    public void updateMoney()
    {
        moneyPlayerCounter.text = playerInventory.coinsAmount.ToString();
        moneyShopCounter.text = shopCash.ToString();
    }

    public void buyItem(int ID)
    {
        if (playerInventory.coinsAmount >= prises[ID])
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>().AddItem(items[ID]);
            playerInventory.coinsAmount -= prises[ID];
            shopCash += prises[ID];


            if (items[ID].canStack)
            {

                slotQuantity[ID] -= 1;
                if(slotQuantity[ID] == 0)
                {
                    items.RemoveAt(ID);
                    prises.RemoveAt(ID);
                    slotQuantity.Remove(ID);
                }
            }
            else
            {
                items.RemoveAt(ID);
                prises.RemoveAt(ID);
                slotQuantity.Remove(ID);
            }
            
            updateFrames();
            updateMoney();
            Debug.Log("Kupione");
        }
        else
        {
            Debug.Log("Za ma³o hajsu");
        }

        
    }

    public void switchBuySell(bool switchBuy)
    {
        if (switchBuy)
        {
            sellPanel.SetActive(false);
            buyPanel.SetActive(true);
        }
        else
        {
            sellPanel.SetActive(true);
            buyPanel.SetActive(false);
            updateSellFrames();
        }
    }

}
