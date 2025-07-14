using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crawler : Enemy
{
    
    void Movement()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Weapon")
        {
            weaponScript = collision.GetComponent<Weapon>();
            StartCoroutine(TakeDamage(weaponScript.playerWeaponDamage, dropAmount, collision.transform.position, transform.position, weaponScript.playerWeaponKnockback));
        }
    }
}
