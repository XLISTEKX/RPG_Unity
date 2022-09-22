using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkellSetTarget : MonoBehaviour
{
    public SkellController sc;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "HitboxPlayer")
        {
            sc.target = collision.gameObject;
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "HitboxPlayer")
        {
            sc.lastPosition = collision.gameObject.transform.position;
            sc.target = null;
        }

    }
}
