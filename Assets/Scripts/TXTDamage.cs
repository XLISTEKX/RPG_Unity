using UnityEngine;
using TMPro;

public class TXTDamage : MonoBehaviour
{
    public TMP_Text text;
    public float lifeTime = 0.5f;
    public float speedMultiplayer = 1;

    public void changeValue(string damageTaken)
    {
        text.text = "-" + damageTaken;
    }

    private void Start()
    {
        Invoke("deleteTXT", lifeTime);

        int i  = Random.Range(0, 2) * 2 - 1;
        Vector2 direction = new Vector2(i * Random.Range(0.4f, 1), 0);

        gameObject.GetComponent<Rigidbody2D>().AddForce(direction * speedMultiplayer);
    }
    void deleteTXT()
    {
        Destroy(gameObject);
    }
}
