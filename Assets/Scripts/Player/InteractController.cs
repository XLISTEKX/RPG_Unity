
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractController : MonoBehaviour
{
    public GameObject interactButton;
    GameObject interactObject;
    public List<GameObject> interactObjects;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag != "Enemy" && collision.gameObject.tag != "Untagged" && collision.gameObject.tag != "Hitbox")
        {
            interactButton.SetActive(true);
            interactObjects.Add(collision.gameObject);
        }
        
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag != "Enemy" && collision.gameObject.tag != "Untagged" && collision.gameObject.tag != "Hitbox")
        {
            interactObjects.Remove(collision.gameObject);
            if (interactObjects.Count == 0)
            {
                interactButton.SetActive(false);
            }
        }
        
        
    }

    public void Interact()
    {
        if (interactObjects[0].tag == "Item")
        {
            GameObject.Find("Player").GetComponent<PlayerInventory>().takeItem(interactObjects[0]);
        }
        else if (interactObjects[0].tag == "Chest")
        {
            interactObjects[0].GetComponent<Chest>().OpenChest();

        }
        else if (interactObjects[0].tag == "Portal")
        {
            interactObjects[0].GetComponent<PortalTeleport>().useTeleport();
        }
        else if (interactObjects[0].tag == "Shop")
        {
            interactObjects[0].GetComponent<ShopControler>().openShop();
        }
        else if (interactObjects[0].tag == "Spawner")
        {
            interactObjects[0].GetComponent<Spawner>().changeSpawn();
        }
    }



}
