using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] float movementSpeed;
    [SerializeField] Rigidbody2D rb;
    bool isFacingRight;
    Vector2 playerMoveInput;

    [SerializeField] private float maxVelocity;
    [SerializeField] private float accelerationMultiplier;
    [SerializeField] private float accelerationMultiplier1;

    [SerializeField] GameObject memoryLand;
    [SerializeField] GameObject memory1;
    [SerializeField] GameObject memory2;
    [SerializeField] GameObject memory3;
    [SerializeField] GameObject memory4;
    //Movement
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float stopJumpGravityMultiplier;
    [SerializeField] private float fallGravityMultiplier;
    [SerializeField] private float maxFallSpeed;
    [SerializeField] protected float defaultGravityScale;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Color memoryBackgroundColor;
    private Color memoryLandBackgroundColor;

    bool jumping;
    //public bool doubleJumpUnlocked;
    public bool stoppedJumping;
    public bool canJump;
    //public bool canJumpAgain;
    ////bool delayTurn = false;
    ////Attack
    //public bool attackUnlocked;
    //[SerializeField] private float attackCooldown;
    //[SerializeField] GameObject weaponGO;
    //[SerializeField] private Animator attackAnimator;
    //[SerializeField] float animationDelay;

    void Start()
    {
        //    currenthealth = maxhealth;
        //isPlayer = true;
        rb = GetComponent<Rigidbody2D>();
        //currencyText = GetComponentInChildren<TextMeshProUGUI>();
        //attackAnimator = GetComponentInChildren<Animator>();
        memoryLandBackgroundColor = mainCamera.backgroundColor;
    }

    // Update is called once per frame
    void Update()
    {
        VelocityLimiter();
        Jump();
        Movement();
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
        
    }
    void SetGravityScale(float gravityScale)
    {
        rb.gravityScale = gravityScale;
        rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -maxFallSpeed));
    }
    void Jump()
    {
        float force;
        force = jumpSpeed;


        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {

            force -= rb.velocity.y;

            rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
            canJump = false;
        }
        stoppedJumping = false;
        jumping = true;
        if (Input.GetKeyUp(KeyCode.Space) && rb.velocity.y > 0)
        {

            jumping = false;
            stoppedJumping = true;
        }
        else
                {
                    SetGravityScale(defaultGravityScale);
                }
    }
        void Gravity()
    {
        if (stoppedJumping)
        {
            //Cancelled jump
            SetGravityScale(defaultGravityScale * stopJumpGravityMultiplier);
        }
        else if (rb.velocity.y < 0)
        {
            //Fall
            SetGravityScale(defaultGravityScale * fallGravityMultiplier);
        }
        else
        {
            SetGravityScale(defaultGravityScale);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Memory1")
        {
            if (Input.GetKey(KeyCode.E))
            {
                StartCoroutine(MemoryCutscene(1));
            }
        }
        else if (collision.tag == "Memory2")
        {
            if (Input.GetKey(KeyCode.E))
            {
                StartCoroutine(MemoryCutscene(2));
            }
        }
        else if (collision.tag == "Memory3")
        {
            if (Input.GetKey(KeyCode.E))
            {
                StartCoroutine(MemoryCutscene(3));
            }
        }
        else if (collision.tag == "Memory4")
        {
            if (Input.GetKey(KeyCode.E))
            {
                StartCoroutine(MemoryCutscene(4));
            }
        }
    }

    IEnumerator MemoryCutscene(int memory)
    {
        rb.bodyType = RigidbodyType2D.Static;
        memoryLand.SetActive(false);
        if (memory == 1)
        {
            memory1.SetActive(true);
            mainCamera.backgroundColor = memoryBackgroundColor;
            gameObject.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
            yield return new WaitForSecondsRealtime(3);
            memory1.SetActive(false);
        }
        else if (memory == 2)
        {
            memory2.SetActive(true);
            yield return new WaitForSecondsRealtime(3);
            memory2.SetActive(false);

        }
        else if (memory == 3)
        {
            memory3.SetActive(true);
            yield return new WaitForSecondsRealtime(3);
            memory3.SetActive(false);

        }
        else if (memory == 4)
        {
            memory4.SetActive(true);
            yield return new WaitForSecondsRealtime(3);
            memory4.SetActive(false);

        }

        ResetMemoryLand();
        
    }
    void ResetMemoryLand()
    {
        gameObject.transform.localScale = Vector3.one;
        memoryLand.SetActive(true);
        mainCamera.backgroundColor = memoryLandBackgroundColor;
        rb.bodyType = RigidbodyType2D.Dynamic;
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


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            canJump = true;
            stoppedJumping = false;
        }
    }
}
