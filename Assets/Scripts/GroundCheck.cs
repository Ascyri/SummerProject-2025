using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    Player playerScript;
    [SerializeField] float coyoteTime;
    // Start is called before the first frame update
    void Start()
    {
        playerScript = GetComponentInParent<Player>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        playerScript.canJump = true;
        playerScript.canJumpAgain = true;
        playerScript.stoppedJumping = false;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //I don't know why but the trigger happens when i exit play mode and it needs null check
        if (gameObject != null)
        {
            StartCoroutine(DelayedDisable());
        }
    }
    IEnumerator DelayedDisable()
    {
        yield return new WaitForSeconds(coyoteTime);
        playerScript.canJump = false;
    }
}
