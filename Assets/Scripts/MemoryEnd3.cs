using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryEnd3 : MonoBehaviour
{

    [SerializeField] Transform teleportPosition;
    [SerializeField] GameObject character5;
    [SerializeField] float cutsceneTime;
    Player playerScript;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StartCoroutine(Cutscene());
            collision.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            collision.GetComponent < Rigidbody2D >().constraints = RigidbodyConstraints2D.FreezeAll;
            playerScript = collision.GetComponent<Player>();
        }
    }
    IEnumerator Cutscene()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        character5.SetActive(true);
        yield return new WaitForSecondsRealtime(cutsceneTime);
        playerScript.gameObject.transform.position = teleportPosition.position;
        playerScript.MemoryLandReset();

    }
}
