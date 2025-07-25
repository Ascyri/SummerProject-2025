using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCheck : MonoBehaviour
{
    Enemy enemyScript;

    private void OnTriggerExit2D(Collider2D collision)
    {
        // 6 = Ground
        if (collision.gameObject.layer == 6 && GetComponentInParent<Enemy>() != null)
        {
            enemyScript = GetComponentInParent<Enemy>();
            enemyScript.TurnEnemy();
        }
    }
}
