using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject Player;
    public Vector3 offset;


    void Update()
    {
        transform.position = Player.transform.position + offset;
    }
}
