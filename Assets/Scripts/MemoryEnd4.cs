using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryEnd4 : MonoBehaviour
{
    [SerializeField] Transform teleportPosition;
    [SerializeField] Animator character3;
    [SerializeField] float cutsceneTime;
    Player playerScript;
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
        GetComponent<SpriteRenderer>().enabled = true;
        character3.enabled = true ;
        yield return new WaitForSecondsRealtime(cutsceneTime);

        character3.enabled = false;
        character3.gameObject.transform.localPosition = new Vector3(22.55f, -4.28f, 0);
        character3.gameObject.transform.localRotation = Quaternion.identity;
        GetComponent<SpriteRenderer>().enabled = false;
        playerScript.gameObject.transform.position = teleportPosition.position;
        playerScript.MemoryLandReset();

    }
}
