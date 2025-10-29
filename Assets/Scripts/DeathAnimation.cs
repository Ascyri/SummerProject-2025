using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAnimation : MonoBehaviour
{
    Animator anim;
    [SerializeField] float animationTime;
    [SerializeField] GameObject corpse;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Car")
        {
            StartCoroutine(Death());
            anim = GetComponent<Animator>();
            anim.SetTrigger("Collision");
            //anim.enabled = true;
            //Debug.Log(anim);
        }

    }
    IEnumerator Death()
    {   
        yield return new WaitForSecondsRealtime(animationTime);
        corpse.SetActive(true);
    }
}
