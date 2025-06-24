using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootPickup : MonoBehaviour
{

    [SerializeField] int currencyAmount;
    float sizeMultiplier;
    

    private void Start()
    {
        sizeMultiplier = 1 + currencyAmount * 0.1f;
        transform.localScale = Vector3.one * sizeMultiplier;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Player>().currencyHeld += currencyAmount;
            Destroy(gameObject);
        }
    }
}
