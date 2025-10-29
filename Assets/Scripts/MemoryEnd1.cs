using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryEnd1 : MonoBehaviour
{
    [SerializeField] Transform mainCamera;
    [SerializeField] float cutsceneTime;
    [SerializeField] GameObject Character2;
    [SerializeField] GameObject ball;
    [SerializeField] Transform teleportPosition;
    Player playerScript;
    GameObject player;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player = collision.gameObject;
            playerScript = collision.GetComponent<Player>();
            StartCoroutine(Cutscene());
        }
    }

    IEnumerator Cutscene()
    {
        mainCamera.parent = null;
        Instantiate(Character2, new Vector2(player.transform.position.x, player.transform.position.y + 10), Quaternion.identity);
        yield return new WaitForSecondsRealtime(cutsceneTime);
        ball.transform.localPosition = new Vector3(-4, 0, 0);
        player.transform.position = teleportPosition.position;
        //mainCamera.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 2, player.transform.position.z - 10);

        playerScript.MemoryLandReset();
    }
}
