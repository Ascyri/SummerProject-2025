using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entities
{
    //[SerializeField] float damageTime;

    [SerializeField] protected int dropAmount;
    [SerializeField] protected int attackDamage;
    [SerializeField] protected int knockbackAmount;
    SpriteRenderer enemySR;
    bool turnedRight = true;
    

    //[SerializeField] Color normalColor;
    //[SerializeField] Color damageFlashColor;
    //[SerializeField] float damageFlashIntervals;
    protected Weapon weaponScript;
    
    //float timeToWait;
    // Start is called before the first frame update
    void Start()
    {
        currenthealth = maxhealth;
    }
    private void Update()
    {
        Movement();
    }
    void Movement()
    {   
        if (turnedRight)
        {
            rb.velocity = Vector2.right * movementSpeed;

        }
        else if (!turnedRight)
        {
            rb.velocity = Vector2.left * movementSpeed;

        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerScript = collision.gameObject.GetComponent<Player>();

            playerScript.InitiateTakeDamage(attackDamage, collision.GetContact(0).point, collision.transform.position, knockbackAmount);
            
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Weapon" && canTakeDamage)
        {
            weaponScript = collision.GetComponent<Weapon>();
            StartCoroutine(TakeDamage(weaponScript.playerWeaponDamage, dropAmount, collision.transform.position, transform.position, weaponScript.playerWeaponKnockback));

        }
    }


}
