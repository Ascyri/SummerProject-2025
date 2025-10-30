using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeRoom : MonoBehaviour
{
    [SerializeField] GameObject room;
    [SerializeField] GameObject home;
    [SerializeField] GameObject outside;

    Player playerScript;

    Animator cutsceneAnimatorCharacter1;
    [SerializeField] SpriteRenderer character8;
    [SerializeField] Animator cutsceneAnimatorCharacter8;
    [SerializeField] Animator cutsceneAnimatorCharacter4;
    [SerializeField] Animator cutsceneAnimatorCharacter5;

    private void Start()
    {
        cutsceneAnimatorCharacter1 = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerScript = collision.GetComponent<Player>();
        }
        if (gameObject.tag == "ChangeRoom")
        {
            StartCoroutine(ChangeFromRoom());
            playerScript.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            playerScript.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else if (gameObject.tag == "ChangeHome")
        {
            playerScript = collision.GetComponent<Player>();
            playerScript.SetCanJumpOn();
            home.SetActive(false);
            outside.SetActive(true);
        }
        else if (gameObject.tag == "ChangeHome2")
        {
            playerScript.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            playerScript.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            StartCoroutine(ChangeToRoom());
        }
    }
    IEnumerator ChangeFromRoom()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSecondsRealtime(2f);
        GetComponent<SpriteRenderer>().enabled = false;
        room.SetActive(false);
        home.SetActive(true);
        playerScript.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        playerScript.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        playerScript.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    IEnumerator ChangeToRoom()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSecondsRealtime(1f);
        cutsceneAnimatorCharacter1.enabled = true;
        yield return new WaitForSecondsRealtime(1f);
        character8.enabled = true;
        cutsceneAnimatorCharacter8.enabled = true;
        cutsceneAnimatorCharacter5.enabled = true;
        cutsceneAnimatorCharacter4.enabled = true;

        yield return new WaitForSecondsRealtime(8f);
        GetComponent<SpriteRenderer>().enabled = false;
        room.SetActive(true);
        home.SetActive(false);
        playerScript.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        playerScript.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        playerScript.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        transform.localPosition = new Vector3(-34, -4.56f, 0);
        character8.enabled = false;
        cutsceneAnimatorCharacter1.enabled = false;
        cutsceneAnimatorCharacter8.enabled = false;
        cutsceneAnimatorCharacter5.enabled = false;
        cutsceneAnimatorCharacter4.enabled = false;
    }
}
