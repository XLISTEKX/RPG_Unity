using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    [Header("UI")]
    public Joystick leftJoystick;
    public Rigidbody2D playerRigedbody;
    public Joystick rightJoystick;
    
    [Header("Movement")]
    public Transform attackHitBox;
    
    public float movementSpeed;
    public bool isMoving;
    
    public float dashTime;
    public float dashCooldown;
    public float dashforce;
    bool isDashing;
    bool canDash = true;
    public UICounter coolDownCounterDash;
    [Header("Attack")]
    public bool canAttack = true;
    public float attackSpeed;
    public float attackRadius;
    

    bool isCrit;

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

            if (leftJoystick.Horizontal != 0 || leftJoystick.Vertical != 0)
            {
                changeAnimation(true);
                changeDirection();

                Vector3 toMove = transform.position + new Vector3(leftJoystick.Horizontal, leftJoystick.Vertical, 0) * movementSpeed * Time.deltaTime;
                playerRigedbody.MovePosition(toMove);
            }

            else
            {
                changeAnimation(false);
            }

    }
    void changeAnimation(bool move)
    {
        if (canAttack)
        {
            if (move)
            {
                gameObject.GetComponent<Animator>().SetBool("MoveRight", true);
            }
            else
            {
                gameObject.GetComponent<Animator>().SetBool("MoveRight", false);
            }
        }
        
    }
    void changeDirection()
    {
        if (leftJoystick.Horizontal > 0)
        {
            transform.rotation = new Quaternion(0,0,0,0);
        }
        else
        {
            transform.rotation = new Quaternion(0, 180, 0, 0);
        }
    }

    public void Attack()
    {
        if (canAttack)
        {
            gameObject.GetComponent<Animator>().SetFloat("AttackSpeed", 1 / attackSpeed);
            gameObject.GetComponent<Animator>().SetTrigger("AttackTrigger");
            canAttack = false;

            Collider2D[] hitObjects =  Physics2D.OverlapCircleAll(attackHitBox.position, attackRadius);

            foreach(Collider2D hitObject in hitObjects)
            {
                if (hitObject.gameObject.tag == "Hitbox")
                {
                    isCrit = false;
                    PlayerStats stats = GetComponent<PlayerStats>();
                    float damage = stats.damage;
                    if (stats.critChance == 1f)
                    {
                        damage *= stats.critMnoznik;
                        isCrit = true;
                    }
                    else
                    {

                        if(Random.value <= stats.critChance)
                        {
                            damage *= stats.critMnoznik;
                            isCrit = true;
                        }
                    }

                    hitObject.gameObject.GetComponentInParent<EnemyHealth>().takeDamage(damage, isCrit);
                }
            }

            Invoke("resetAttack", attackSpeed);

        }
        
    }
    private void OnDrawGizmosSelected()
    {
        if (attackHitBox == null)
            return;
        Gizmos.DrawWireSphere(attackHitBox.position, attackRadius);
    }
    public void resetAttack()
    {
        canAttack = true;
    }

    
    public void dash()
    {
        if(leftJoystick.Horizontal != 0 && leftJoystick.Vertical != 0 && canDash )
        {
            Vector2 location = new Vector2(leftJoystick.Horizontal, leftJoystick.Vertical) * dashforce;
            StartCoroutine(doDash(location));
            coolDownCounterDash.gameObject.SetActive(true);
            coolDownCounterDash.startCounter(dashCooldown);
        }
        
    }

    public IEnumerator doDash(Vector2 direction)
    {
        isDashing = true;
        canDash = false;
        //playerRigedbody.velocity = direction;
        playerRigedbody.AddForce(direction, ForceMode2D.Impulse);
        yield return new WaitForSeconds(dashTime);
        playerRigedbody.velocity = Vector2.zero;

        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown("d"))
        {
            dash();
            
        }
    }

}
