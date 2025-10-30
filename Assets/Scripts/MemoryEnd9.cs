using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryEnd9 : MonoBehaviour
{
    [SerializeField] GameObject trash1;
    [SerializeField] GameObject trash2;
    [SerializeField] GameObject otherCutsceneCharacter1;
    [SerializeField] float cutsceneTime;
    Player playerScript;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerScript = collision.GetComponent<Player>();
            StartCoroutine(Cutscene());
        }
    }
    IEnumerator Cutscene()
    {
        yield return new WaitForSecondsRealtime(4);
        trash1.SetActive(false);
        yield return new WaitForSecondsRealtime(2);
        trash2.SetActive(false);
        yield return new WaitForSecondsRealtime(cutsceneTime);
        otherCutsceneCharacter1.transform.position = new Vector3(-34.93f, -4.44f, 0);
        trash1.SetActive(true);
        trash2.SetActive(true);
        playerScript.MemoryLandReset();
    }
}
