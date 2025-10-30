using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryEnd8 : MonoBehaviour
{
    [SerializeField] Transform teleportPosition;
    Animator animatorCharacter1;
    [SerializeField] float cutsceneTime;
    [SerializeField] Transform cameraPosition;
    [SerializeField] GameObject character8;
    [SerializeField] Animator animatorCharacter8;

    [SerializeField] GameObject home;
    [SerializeField] GameObject room;
    [SerializeField] GameObject outside;

    Player playerScript;
    private void Start()
    {
        animatorCharacter1 = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            collision.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            playerScript = collision.GetComponent<Player>();
            StartCoroutine(Cutscene());
        }
    }
    IEnumerator Cutscene()
    {
        Debug.Log(playerScript);
        Debug.Log(playerScript.mainCamera);
        Debug.Log(cameraPosition);
        //playerScript.mainCamera.transform.position = new Vector3(cameraPosition.position;

        GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSecondsRealtime(3f);

        animatorCharacter1.enabled = true;




        yield return new WaitForSeconds(6f);
        character8.SetActive(true);
        yield return new WaitForSeconds(cutsceneTime);
        animatorCharacter1.enabled = false;
        animatorCharacter1.transform.localPosition = new Vector3(9.3f, -4.54f, 0);
        GetComponent<SpriteRenderer>().enabled = false;

        playerScript.gameObject.transform.position = teleportPosition.position;

        character8.SetActive(false);
        gameObject.transform.localPosition = new Vector3(-30.42f, 0.1f, 0);

        outside.SetActive(false);
        room.SetActive(true);


        playerScript.MemoryLandReset();

    }
}
