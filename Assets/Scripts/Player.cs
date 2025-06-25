using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entities
{
    //Movement
    Vector2 playerMoveInput;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float maxVelocity;
    [SerializeField] private float accelerationMultiplier;
    [SerializeField] private float accelerationMultiplier1;
    private bool canJump;
    bool isFacingRight;
    //Attack
    public bool attackUnlocked;
    [SerializeField] private float attackCooldown;
    [SerializeField] GameObject weaponGO;
    [SerializeField] private Animator attackAnimator;

    public int currencyHeld;

    void Start()
    {
        base.rb = GetComponent<Rigidbody2D>();
        //attackAnimator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        VelocityLimiter();
        Movement();
        Jump();
        AttackCheck();
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
        playerMoveInput.x = Input.GetAxisRaw("Horizontal");
        Debug.Log(playerMoveInput.x);
        if (playerMoveInput.x != 0)
        {
            CheckDirectionToFace(playerMoveInput.x > 0);

        }

        float targetSpeed = playerMoveInput.x * maxVelocity;

        float acceleration = (Mathf.Abs(targetSpeed) > 0.01 ? accelerationMultiplier : accelerationMultiplier1);

        float speedDif = targetSpeed - rb.velocity.x;

        float movement = speedDif * acceleration;

        rb.AddForce(movement * Vector2.right, ForceMode2D.Force);
        //if (Input.GetKey(KeyCode.D))
        //{
        //    rb.AddForce(Vector2.right * movementSpeed * Time.deltaTime);
        //}
        //if (Input.GetKey(KeyCode.A))
        //{
        //    rb.AddForce(Vector2.left * movementSpeed * Time.deltaTime);
        //}
    }
    void CheckDirectionToFace(bool isMovingRight)
    {
        if (isMovingRight != isFacingRight)
            Turn();
    }
    private void Turn()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;

        isFacingRight = !isFacingRight;
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
        if (attacking) 
        {
            return;
        }
        if (!isFacingRight)
        {
            weaponGO.transform.eulerAngles = new Vector3(0, 0, -288);
        }
        else if (isFacingRight)
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
        attacking = true;
        if (!isFacingRight)
        {
            Debug.Log(isFacingRight);
            attackAnimator.Play("AttackLeft");
        }
        else
        {
            Debug.Log(isFacingRight);
            attackAnimator.Play("AttackLeft");
        }
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
        attacking = false;
    }
}
