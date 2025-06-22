using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float movementSpeed;
    [SerializeField] int maxhealth;
    [SerializeField] int currenthealth;
    Rigidbody2D enemyRB;
    Weapon weaponScript;

    // Start is called before the first frame update
    void Start()
    {
        currenthealth = maxhealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "weapon")
        {
            weaponScript = collision.gameObject.GetComponent<Weapon>();
            currenthealth -= weaponScript.playerWeaponDamage;
        }
    }
}
