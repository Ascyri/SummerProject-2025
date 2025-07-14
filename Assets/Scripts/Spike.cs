using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    Player player;
    [SerializeField] int spikeDamage;
    [SerializeField] float knockbackAmount;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player = collision.gameObject.GetComponent<Player>();

            player.InitiateTakeDamage(spikeDamage, collision.GetContact(0).point, collision.transform.position, knockbackAmount);
            
        }
    }
}
