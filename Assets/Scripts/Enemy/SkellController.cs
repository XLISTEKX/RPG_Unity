using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkellController : MonoBehaviour
{
    public float speed;
    public float timeToLook;
    public float rangeToLook;
    public float attackRange;
    public GameObject target;
    public GameObject AtackHitBox;
    public Vector3 lastPosition;
    public Health health;
    bool isInvoked = false;
    bool isRandomPos = false;
    bool canMove = true;


    private void Start()
    {
        //target = GameObject.FindGameObjectWithTag("Player");
        lastPosition = transform.position;
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            if (target == null)
            {
                if (!isRandomPos)
                {

                    if (transform.position != lastPosition)
                    {
                        isRandomPos = false;
                        moveTo(lastPosition);
                        return;
                    }
                    else
                    {
                        idle();
                    }
                }
                else
                {
                    if (transform.position == lastPosition)
                    {
                        isRandomPos = false;
                        idle();
                    }
                    else
                        moveTo(lastPosition);

                }


            }
            else
            {
                if (Vector3.Magnitude(transform.position - target.transform.position) <= attackRange)
                {
                    attack();
                    
                }
                else
                {
                    moveTo(target.transform.position);
                }

            }
        }

        else
        {
            if (target.transform.position.x > transform.position.x)
            {
                transform.rotation = new Quaternion(0, 180, 0, 0);
            }
            else
                transform.rotation = new Quaternion(0, 0, 0, 0);
        }
            



    }
    public void attack()
    {
        gameObject.GetComponent<Animator>().Play("Animation_Skellet_Attack");
        canMove = false;
        AtackHitBox.SetActive(true);
        Invoke("changeCanMove", gameObject.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0).Length);
    }
    public void changeCanMove()
    {
        canMove = true;
        AtackHitBox.SetActive(false);
    }
    public void idle()
    {
        gameObject.GetComponent<Animator>().SetBool("Moving", false);
        if (!isRandomPos && !isInvoked)
        {
            isInvoked = true;
            Invoke("randomPatrol", timeToLook);
        }


    }

    public void randomPatrol()
    {
        isRandomPos = true;
        lastPosition = transform.position + new Vector3(Random.Range(-rangeToLook, rangeToLook), Random.Range(-rangeToLook, rangeToLook),0);
        isInvoked = false;
        //moveTo(lastPosition);
    }
    public void moveTo(Vector3 targetVektor)
    {
        gameObject.GetComponent<Animator>().SetBool("Moving", true);
        var step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetVektor, step);
        if (targetVektor.x - transform.position.x > 0)
        {
            transform.rotation = new Quaternion(0, 180, 0, 0);
        }
        else
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }
    }
    


}
