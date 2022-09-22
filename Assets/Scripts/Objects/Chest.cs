using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public List<ItemClass> chestContent;
    public int coinsNR;
    public GameObject coin;
    public BoxCollider2D chestTrigger;
    public Sprite openSprite;
    public Vector3 offset;
    bool isClosed = true;


    public void OpenChest()
    {
        if (isClosed)
        {
            for (int i = 0; i < coinsNR; i++)
            {
                Instantiate(coin, gameObject.transform.position + offset, coin.transform.rotation);
            }

            for(int i = 0; i < chestContent.Count; i++)
            {
                
                Instantiate(chestContent[i].itemContainer, gameObject.transform.position + offset, chestContent[i].itemContainer.transform.rotation);
                
            }
            
            chestTrigger.enabled = false;
            chestContent.Clear();
            gameObject.GetComponent<SpriteRenderer>().sprite = openSprite;
        }
    }
}
