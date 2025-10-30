using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryEnd6 : MonoBehaviour
{
    Player playerScript;
    [SerializeField] float cutsceneTime;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StartCoroutine(Cutscene());
            collision.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            collision.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            playerScript = collision.GetComponent<Player>();
        }
    }
    IEnumerator Cutscene()
    {

        yield return new WaitForSecondsRealtime(cutsceneTime);
        playerScript.MemoryLandReset();
    }
}
