using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodControler : MonoBehaviour
{
    public Transform offset;

    public void spawnBlood(GameObject blood)
    {
        Instantiate(blood, transform.position, blood.transform.rotation).GetComponent<ParticleSystem>().collision.AddPlane(offset);
        Invoke("deactivate", 2f);

    }
    private void deactivate()
    {
        Destroy(gameObject);
    }
}
