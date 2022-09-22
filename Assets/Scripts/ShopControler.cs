using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopControler : MonoBehaviour
{
    public int shopCash;
    public GameObject shopCanvas;
    public List<ItemClass> shopContent;
    public List<int> prises;
    public List<int> slotQuantity;

    public void openShop()
    {
        GameObject canvas = GameObject.FindGameObjectWithTag("PopUP");

        Instantiate(shopCanvas).GetComponent<ShopCanvasControler>().openShop(canvas,gameObject, shopContent, shopCash, prises, slotQuantity);
        canvas.SetActive(false);
        
        Time.timeScale = 0f;
    }
}
