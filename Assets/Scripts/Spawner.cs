using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public bool canSpawn;
    public GameObject mobToSpawn;
    public float cooldown;


    public void spawnMob()
    {
        int x = Random.Range(0, 2) * 2 - 1;
        int y = Random.Range(0, 2) * 2 - 1;
        Vector3 offset = new Vector3 (Random.Range(1, 2f) * x, Random.Range(1, 2f) * y, 0);
        Instantiate(mobToSpawn, transform.position + offset, mobToSpawn.transform.rotation);
    }
    public void changeSpawn()
    {
        if (canSpawn)
        {
            GameObject.FindGameObjectWithTag("PopUP").GetComponent<PopUPController>().showMessage("Spawner: Disabled", 1f);
            CancelInvoke("spawnMob");
            canSpawn = false;
        }
        else
        {
            GameObject.FindGameObjectWithTag("PopUP").GetComponent<PopUPController>().showMessage("Spawner: Actived", 1f);
            InvokeRepeating("spawnMob", cooldown, cooldown);
            canSpawn = true;
        }
    }
}
