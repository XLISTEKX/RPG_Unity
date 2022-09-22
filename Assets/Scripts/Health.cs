using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Health : MonoBehaviour
{
    public int health;
    public GameObject healthBar;

    public void takeDamage()
    {
        health--;
        healthBar.GetComponentInChildren<TMP_Text>().text = health.ToString();
        healthBar.GetComponent<Animator>().SetTrigger("ShowHealth");
        
        if (health <= 0)
        {
            SceneManager.LoadScene(0);
        }

    }


}
