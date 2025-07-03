using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entities : MonoBehaviour
{
    [SerializeField] protected float movementSpeed;
    [SerializeField] protected float defaultGravityScale;

    [SerializeField] protected int maxhealth;
    [SerializeField] protected int currenthealth;
    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField] protected Color normalColor;
    [SerializeField] protected Color damagedColor;
    protected bool canTakeDamage = true;


    [SerializeField] protected Rigidbody2D rb;
    protected bool canAttack = true;
    protected bool attacking = false;

    [SerializeField]GameObject lootObject;

    float timeToWait;
    void Start()
    {
        currenthealth = maxhealth;
    }

    private void Update()
    {
    }

    
    protected IEnumerator TakeDamage(int damageAmount, int currencyDropped)
    {
        Debug.Log(gameObject.name +" Took damage");
        canTakeDamage = false;
        currenthealth -= damageAmount;

        if (currenthealth <= 0)
        {
            Death(currencyDropped);
            
        }
        else
        {
            for (int timeFlashed = 0; timeFlashed < 2; timeFlashed++)
            {
                //enemySR.color = damageFlashColor;
                spriteRenderer.color = damagedColor;
                timeToWait = 0.1f;
                yield return StartCoroutine(Wait());
                spriteRenderer.color = normalColor;
                timeToWait = 0.1f;
                yield return StartCoroutine(Wait()); 
                spriteRenderer.color = damagedColor;
                timeToWait = 0.1f;
                yield return StartCoroutine(Wait());
                spriteRenderer.color = normalColor;
                //StartCoroutine(DamageFlash());
                //yield return new WaitForSeconds(damageFlashIntervals);
                //enemySR.color = normalColor;

            }
            //while (timesFlashed < 5)
            //{
                
            //    timesFlashed++;
            //}

        }

        canTakeDamage = true;
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(timeToWait);
    }

    protected void Death(int currencyDropped)
    {
        DropItem(currencyDropped);
        Destroy(gameObject);

    }
    protected void DropItem(int currencyDropped)
    {
        Instantiate(lootObject, transform.position, Quaternion.identity);
            
    }
}
