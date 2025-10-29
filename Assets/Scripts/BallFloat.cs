using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallFloat : MonoBehaviour
{
    [SerializeField] float floatAmount;
    [SerializeField] float maxFloatVelocity;
    [SerializeField] float yLevelFloat;
    Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (gameObject.transform.position.y <= yLevelFloat)
        {
            if (rb.velocity.y <= maxFloatVelocity)
            {
                rb.AddForce(new Vector2(-rb.velocity.x, floatAmount));

            }

        }
    }
}
