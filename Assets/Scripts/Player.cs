using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entities
{
    //Movement
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float maxVelocity;
    private bool canJump;
    [SerializeField] bool turned;

    //Attack
    public bool attackUnlocked;
    [SerializeField] private float attackCooldown;
    [SerializeField] GameObject weaponGO;
    [SerializeField] private Animator attackAnimator;
    // Start is called before the first frame update
    void Start()
    {
        base.rb = GetComponent<Rigidbody2D>();
        //attackAnimator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        TurnChecker();
        VelocityLimiter();
        Movement();
        Jump();
        AttackCheck();
    }
    void TurnChecker()
    {
        if (rb.velocity.x < 0)
        {
            turned = true;
        }
        else if (rb.velocity.x > 0)
        {
            turned = false;
        }
    }
    private void VelocityLimiter()
    {
        if (rb.velocity.x >= maxVelocity)
        {
            rb.velocity = new Vector2(maxVelocity, rb.velocity.y);
        }
        if (rb.velocity.x <= -maxVelocity)
        {
            rb.velocity = new Vector2(-maxVelocity, rb.velocity.y);
        }
    }
    private void Movement()
    {
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(Vector2.right * movementSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(Vector2.left * movementSpeed * Time.deltaTime);
        }
    }

    private void Jump()
    {
        if (Input.GetKey(KeyCode.Space) && canJump)
        {
            rb.AddForce(Vector2.up * jumpSpeed);
            canJump = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            canJump = true;

        }
    }

    public void AttackCheck()
    {
        if (!attackUnlocked)
        {
            return;
        }
        weaponGO.SetActive(true);
        if (turned)
        {
            weaponGO.transform.eulerAngles = new Vector3(0, 0, -288);
        }
        else if (!turned)
        {
            weaponGO.transform.eulerAngles = new Vector3(0, 0, -80);

        }
        if (Input.GetKeyDown(KeyCode.Mouse0) && canAttack)
        {
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        canAttack = false;
        if (turned)
        {
            attackAnimator.Play("AttackLeft");
        }
        else
        {
            attackAnimator.Play("AttackRight");
        }
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
}
