using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopUPController : MonoBehaviour
{
    public GameObject PopUp;
    public void showMessage(string message, float timeShow)
    {
        if(PopUp.activeSelf == false)
        {
            PopUp.SetActive(true);
            PopUp.GetComponentInChildren<TMP_Text>().text = message;
            Invoke("hideMessage", timeShow);
        }
    }

    public void hideMessage()
    {
        PopUp.SetActive(false);

    }
}
