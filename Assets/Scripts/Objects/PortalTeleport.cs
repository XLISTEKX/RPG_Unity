using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PortalTeleport : MonoBehaviour
{
    public int mapIndex;
    

    public void useTeleport()
    {
        SceneManager.LoadScene(mapIndex);
    }
}
