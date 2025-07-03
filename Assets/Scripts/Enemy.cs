using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entities
{
    //[SerializeField] float damageTime;
    [SerializeField] protected int dropAmount;
    SpriteRenderer enemySR;
    //[SerializeField] Color normalColor;
    //[SerializeField] Color damageFlashColor;
    //[SerializeField] float damageFlashIntervals;
    protected Weapon weaponScript;
    
    GameObject playerObject;
    //float timeToWait;
    // Start is called before the first frame update
    void Start()
    {
        currenthealth = maxhealth;
    }
    void DetectPlayer()
    {
        

    }

    

}
