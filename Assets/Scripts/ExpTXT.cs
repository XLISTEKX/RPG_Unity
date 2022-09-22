using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ExpTXT : MonoBehaviour
{
    public TMP_Text text;
    public float speed;

    void Start()
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 1) * speed);
    }

    public void changeValue(string xpGain)
    {
        text.text = "+" + xpGain + "xp";
    }

}
