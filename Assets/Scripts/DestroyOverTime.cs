using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTime : MonoBehaviour
{
    public float timeToDesytroy;


    private void Start()
    {
        Invoke("deSpawn", timeToDesytroy);
    }

    void deSpawn()
    {
        Destroy(gameObject);    
    }

}
