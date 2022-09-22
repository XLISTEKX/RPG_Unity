using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropController : MonoBehaviour
{

    public int maxCoinDrop;
    public int minCoinDrop;
    public int xpDrop;

    public GameObject coin;
    public GameObject xpText;
    public Vector3 spawnCoinOffset;
    public Vector3 spawnXpOffset;

    public void spawnDrop()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().addXP(xpDrop);
        Instantiate(xpText, transform.position + spawnXpOffset, xpText.transform.rotation).GetComponent<ExpTXT>().changeValue(xpDrop.ToString());

        int x = Random.Range(minCoinDrop, maxCoinDrop + 1);

        for (int y = 1; y <= x; y++)
        {
            Instantiate(coin, transform.position + spawnCoinOffset, coin.transform.rotation);
        }
    }
}
