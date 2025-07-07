using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void ActivateRespawn()
    {
        animator.Play("Activate");
    }
    public void UnActivateRespawn()
    {
        animator.Play("Unactivated");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            ActivateRespawn();
            collision.GetComponent<Player>().latestRespawnPoint = this;
        }
    }
}
