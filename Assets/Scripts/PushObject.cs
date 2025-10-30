using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushObject : MonoBehaviour
{
    [SerializeField] float startPushAmountX;
    [SerializeField] float startPushAmountY;
    [SerializeField] float pushAmountXIncrease;
    [SerializeField] float pushAmountYIncrease;
    float pushAmountX;
    float pushAmountY;

    private void Start()
    {
        pushAmountX = startPushAmountX;
        pushAmountY = startPushAmountY;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            pushAmountX += pushAmountXIncrease;
            pushAmountY += pushAmountYIncrease;
            collision.rigidbody.AddForce(new Vector2 (pushAmountX, pushAmountY));
        }
    }

    public void resetPushAmount()
    {
        pushAmountX = startPushAmountX;
        pushAmountY = startPushAmountY;
    }
}
