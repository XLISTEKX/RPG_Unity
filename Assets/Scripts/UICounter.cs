using UnityEngine;
using TMPro;

public class UICounter : MonoBehaviour
{
    public float timeToCount;
    public TMP_Text textToUpdate;


    private void Update()
    {
        
    }

    public void startCounter(float time)
    {
        timeToCount = time;
        textToUpdate.text = timeToCount.ToString();
        InvokeRepeating("countDown", 0.1f, 0.1f);
    }
    public void countDown()
    {
        timeToCount -= 0.1f;
        
        textToUpdate.text = timeToCount.ToString("F1");
        if(timeToCount <= 0)
        {
            CancelInvoke("countDown");
            gameObject.SetActive(false);
            
        }
    }

}
