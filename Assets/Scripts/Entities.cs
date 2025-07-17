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

    protected bool isPlayer;
    float timeToWait;

    [SerializeField] protected int timesFlashedInvincibility;
    [SerializeField] protected float flashDelay;

    protected Player playerScript;
    private void Update()
    {
    }

    
    protected IEnumerator TakeDamage(int damageAmount, int currencyDropped, Vector2 knockbackOrigin, Vector2 knockbackPoint, float knockbackAmount)
    {
        canTakeDamage = false;
        currenthealth -= damageAmount;
        Debug.Log(currenthealth);
        if (currenthealth <= 0)
        {
            Death(currencyDropped);
            
        }
        else
        {
            ApplyKnockback(knockbackOrigin, knockbackPoint, knockbackAmount);
            for (timesFlashedInvincibility = 0; timesFlashedInvincibility < 2; timesFlashedInvincibility++)
            {
                //enemySR.color = damageFlashColor;
                spriteRenderer.color = damagedColor;
                timeToWait = flashDelay;
                yield return StartCoroutine(Wait());
                spriteRenderer.color = normalColor;
                timeToWait = flashDelay;
                yield return StartCoroutine(Wait()); 
                spriteRenderer.color = damagedColor;
                timeToWait = flashDelay;
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
    public void ApplyKnockback(Vector2 knockbackOrigin, Vector2 knockbackPoint, float knockbackAmount)
    {
        Vector2 direction = new Vector2((Mathf.Abs(knockbackPoint.x) - Mathf.Abs(knockbackOrigin.x)), 0f).normalized;
        
        direction = new Vector2(direction.x, 0.2f);

        rb.velocity = Vector2.zero;
        rb.AddForce(direction * knockbackAmount, ForceMode2D.Impulse);
        

    }
    protected void Death(int currencyDropped)
    {
        
        DropItem(currencyDropped);

        if (isPlayer)
        {
            GetComponent<Player>().Respawn();
            
        }
        else
        {
            Destroy(gameObject);

        }

    }
    protected void DropItem(int currencyDropped)
    {
        Instantiate(lootObject, transform.position, Quaternion.identity);
            
    }
}
