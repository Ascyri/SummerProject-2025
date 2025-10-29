using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryEnd2 : MonoBehaviour
{
    Animator character1;
    [SerializeField] Animator character3;
    [SerializeField] Animator character4;
    [SerializeField] float cutsceneLength;
    [SerializeField] Transform teleportPosition;
    Player playerScript;
    private void Start()
    {
        character1 = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
            collision.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            playerScript = collision.GetComponent<Player>();
            playerScript.mainCamera.transform.parent = gameObject.transform;
            Debug.Log(playerScript.mainCamera.transform.parent);
            playerScript.mainCamera.transform.localPosition = new Vector3(0, 2, -10);
            StartCoroutine(Cutscene());
            
        }
    }

    IEnumerator Cutscene()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        character1.enabled = true;
        character3.enabled = true;
        character4.enabled = true;

        yield return new WaitForSecondsRealtime(cutsceneLength);
        playerScript.gameObject.transform.position = teleportPosition.position;
        GetComponent<SpriteRenderer>().enabled = false;
        character1.enabled = false;
        character3.enabled = false;
        character4.enabled = false;
        character1.gameObject.transform.localPosition = new Vector3(9, -1.06f, 0);
        character3.gameObject.transform.localPosition = new Vector3(11, -.57f, 0);
        character4.gameObject.transform.localPosition = new Vector3(10, -1.15f, 0);
        playerScript.MemoryLandReset();
    }
    
}
