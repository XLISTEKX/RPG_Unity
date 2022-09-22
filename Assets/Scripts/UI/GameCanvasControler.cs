using UnityEngine;

public class GameCanvasControler : MonoBehaviour
{
    public GameObject windowInventory;
    public GameObject windowGameplay;

    public void openInventory()
    {
        windowInventory.SetActive(true);
        windowGameplay.SetActive(false);

        Time.timeScale = 0;

    }
    public void openGameplay()
    {
        windowInventory.SetActive(false);
        windowGameplay.SetActive(true);
        Time.timeScale = 1;
    }
}
