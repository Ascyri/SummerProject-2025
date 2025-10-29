using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushObject : MonoBehaviour
{
    [SerializeField] float pushAmountX;
    [SerializeField] float pushAmountY;
    [SerializeField] float pushAmountXIncrease;
    [SerializeField] float pushAmountYIncrease;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            pushAmountX += pushAmountXIncrease;
            pushAmountY += pushAmountYIncrease;
            collision.rigidbody.AddForce(new Vector2 (pushAmountX, pushAmountY));
        }
    }
}
