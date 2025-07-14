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
    [SerializeField] private float stopJumpGravityMultiplier;
    [SerializeField] private float fallGravityMultiplier;
    [SerializeField] private float maxFallSpeed;
    bool jumping;
    bool doubleJumpUnlocked;
    public bool stoppedJumping;
    public bool canJump;
    public bool canJumpAgain;
    bool isFacingRight;
    bool delayTurn = false;
    //Attack
    public bool attackUnlocked;
    [SerializeField] private float attackCooldown;
    [SerializeField] GameObject weaponGO;
    [SerializeField] private Animator attackAnimator;
    [SerializeField] float animationDelay;


    public int currencyHeld;

    public RespawnPoint latestRespawnPoint;
    void Start()
    {
        currenthealth = maxhealth;
        player = true;
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
        Gravity();

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
        if (!canTakeDamage)
        {
            return;
        }
        playerMoveInput.x = Input.GetAxisRaw("Horizontal");
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
    void Gravity()
    {
        if (stoppedJumping)
        {
            SetGravityScale(defaultGravityScale * stopJumpGravityMultiplier);
        }
        else if (rb.velocity.y < 0)
        {
            SetGravityScale(defaultGravityScale * fallGravityMultiplier);
        }
        else
        {
            SetGravityScale(defaultGravityScale);
        }
    }

    void SetGravityScale(float gravityScale)
    {
        rb.gravityScale = gravityScale;
        rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -maxFallSpeed));
    }
    void CheckDirectionToFace(bool isMovingRight)
    {
        if (isMovingRight != isFacingRight)
            Turn();
    }
    private void Turn()
    {
        if (attacking)
        {
            delayTurn = true;
            return;
        }
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;

        isFacingRight = !isFacingRight;
    }
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            float force;
            force = jumpSpeed;
            if (rb.velocity.y < 0)
            {
                force -= rb.velocity.y;
            }
            rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
            canJump = false;
        }
        //else if (Input.GetKeyDown(KeyCode.Space) && canJumpAgain)
        //{
        //    rb.AddForce(Vector2.up * jumpSpeed * 1.5f);
        //    canJumpAgain = false;
        //}
        if (Input.GetKey(KeyCode.Space))
        {
            jumping = true;
        }
        if (Input.GetKeyUp(KeyCode.Space) && rb.velocity.y > 0)
        {
            jumping = false;
            stoppedJumping = true;
        }
    }

    //private void OnCollisionTrigger2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Floor")
    //    {
    //        canJump = true;
    //        canJumpAgain = true;
    //        stoppedJumping = false;
    //    }
    //}

    public void AttackCheck()
    {
        if (!attackUnlocked)
        {
            return;
        }
        weaponGO.SetActive(true);
        if (!canAttack) 
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
            attackAnimator.Play("AttackLeft");
        }
        else
        {
            attackAnimator.Play("AttackLeft");
        }
        yield return new WaitForSeconds(animationDelay);
        if (delayTurn)
        {
            delayTurn = false;
            Turn();
            attacking = false;
        }
        yield return new WaitForSeconds(attackCooldown); 
        
        canAttack = true;

    }
    
    public void InitiateTakeDamage(int damage, Vector2 knockbackOrigin, Vector2 knockbackPoint, float knockbackAmount)
    {
        StartCoroutine(TakeDamage(damage, currencyHeld, knockbackOrigin, knockbackPoint, knockbackAmount));
    }

    
    public void Respawn()
    {
        transform.position = latestRespawnPoint.transform.position;
        currencyHeld = 0;
        currenthealth = maxhealth;
    }
    

}
