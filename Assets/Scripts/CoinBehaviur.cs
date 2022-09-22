using UnityEngine;
using System.Collections;

public class CoinBehaviur : MonoBehaviour
{
    public Rigidbody2D rigidbody2;
    GameObject player;
    public float speed1;
    public float speed2;
    bool magnet = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        rigidbody2 = GetComponent<Rigidbody2D>();

        int x = Random.Range(0, 2) * 2 - 1;
        int y = Random.Range(0, 2) * 2 - 1;
        Vector2 direction = new Vector2(Random.Range(0, 1f) * x, Random.Range(0, 1f) * y);

        rigidbody2.AddForce(direction * speed1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if( collision.tag == "CoinBox")
        {
            magnet = true;
        }
        else if( collision.tag == "HitboxPlayer")
        {
            collision.gameObject.GetComponentInParent<PlayerInventory>().coinsAmount += 1;
            Destroy(gameObject);
        }
    }
    private void FixedUpdate()
    {
        if (magnet)
        {
            Vector3 x = player.transform.position - gameObject.transform.position;

            rigidbody2.AddForce(x * speed2 * Time.deltaTime);
        }
    }
}
