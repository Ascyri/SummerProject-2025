using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryEnd5 : MonoBehaviour
{
    [SerializeField] Transform teleportPosition;
    [SerializeField] Animator character1;
    [SerializeField] float cutsceneTime;
    [SerializeField] GameObject carCrash;
    [SerializeField] GameObject home;
    [SerializeField] Transform cameraPosition;
    [SerializeField] GameObject car;
    [SerializeField] GameObject corpse;
    [SerializeField] GameObject character3;
    Player playerScript;
    float time1 = 2;
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
        character1.enabled = true;

        yield return new WaitForSecondsRealtime(time1);
        carCrash.SetActive(true);
        home.SetActive(false);
        playerScript.mainCamera.transform.position = cameraPosition.position;


        car.SetActive(true);

        yield return new WaitForSeconds(7.225f);
        character3.SetActive(false);
        corpse.SetActive(true);
        yield return new WaitForSeconds(cutsceneTime);
        character1.enabled = false;
        character1.transform.localPosition = new Vector3(9.3f, -4.54f, 0);
        GetComponent<SpriteRenderer>().enabled = false;
        playerScript.gameObject.transform.position = teleportPosition.position;

        corpse.SetActive(false);
        character3.SetActive(true);
        character3.transform.localPosition = new Vector3(0.940002441f, -0.606998444f, 0);
        character3.transform.localRotation = Quaternion.identity;
        carCrash.SetActive(false);
        home.SetActive(true);


        playerScript.MemoryLandReset();

    }
}
