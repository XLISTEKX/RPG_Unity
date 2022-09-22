using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public GameObject damageTXT;
    public GameObject deadModel;
    public GameObject bloodTranform;
    public GameObject bloodType;
    public Vector3 TXTOffset = new Vector2(1, 2);

    public float health;
    public void takeDamage(float damage, bool crit, float knockbackValue = 5f)
    {
        health -= damage;

        if(health <= 0)
        {
            Instantiate(deadModel, transform.position, deadModel.transform.rotation);
            Instantiate(bloodTranform, transform.position, bloodTranform.transform.rotation).GetComponent<BloodControler>().spawnBlood(bloodType);
            GetComponent<DropController>().spawnDrop();
            
            Destroy(gameObject);
        }
        gameObject.GetComponent<Animator>().SetTrigger("Damage");
        
        Vector2 knockback = (gameObject.transform.position - GameObject.Find("Player").transform.position) * 1000; 
        gameObject.GetComponent<Rigidbody2D>().AddForce(knockback * knockbackValue);

        TXTDamage txt = Instantiate(damageTXT, transform.position + TXTOffset, damageTXT.transform.rotation).GetComponent<TXTDamage>();
        
        if (crit)
        {
            txt.text.fontStyle = TMPro.FontStyles.Bold;
            txt.text.fontSize += 0.1f;
            txt.changeValue(damage.ToString());
        }
        else
        {
            txt.changeValue(damage.ToString());
        }

    }


}
