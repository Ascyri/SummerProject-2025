using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkAnimation : MonoBehaviour
{
    [SerializeField] Animator character1;
    [SerializeField] Animator character2;
    [SerializeField] Animator character3;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (character1.enabled == false)
        //{
        //    character1.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        //    character1.SetTrigger("PlayAnimation");
        //    character1.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        //}
        character1.enabled = true;
        character2.enabled = true;
        character3.enabled = true;
        //StartCoroutine(KillYourself());
    }

    //IEnumerator KillYourself()
    //{
    //    yield return new WaitForSecondsRealtime(14);

    //    character1.enabled = false;
    //}
}
