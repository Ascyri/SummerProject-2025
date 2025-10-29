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
    [SerializeField] GameObject memory5;
    [SerializeField] GameObject memory6;
    [SerializeField] GameObject memory7;
    [SerializeField] GameObject memory8;
    [SerializeField] GameObject memory9;
    //Movement
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float stopJumpGravityMultiplier;
    [SerializeField] private float fallGravityMultiplier;
    [SerializeField] private float maxFallSpeed;
    [SerializeField] protected float defaultGravityScale;
    public Camera mainCamera;
    [SerializeField] private Color memory1BackgroundColor;
    [SerializeField] private Color memory2BackgroundColor;
    [SerializeField] private Color memory3BackgroundColor;
    [SerializeField] private Color memory4BackgroundColor;
    [SerializeField] private Color memory6BackgroundColor;
    [SerializeField] private Color memory7BackgroundColor;
    [SerializeField] private Color memory8BackgroundColor;
    private Color memoryLandBackgroundColor;

    SpriteRenderer spriteRenderer;
    [SerializeField] Color memoryColor;
    [SerializeField] Color memoryLandColor;
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
        spriteRenderer = GetComponent<SpriteRenderer>();
        //currencyText = GetComponentInChildren<TextMeshProUGUI>();
        //attackAnimator = GetComponentInChildren<Animator>();
        MemoryLandReset();
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
        //if (playerMoveInput.x != 0)
        //{
        //    CheckDirectionToFace(playerMoveInput.x > 0);

        //}

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
                Memory1Scene();
                //StartCoroutine(MemoryCutscene(1));
            }
        }
        else if (collision.tag == "Memory2")
        {
            if (Input.GetKey(KeyCode.E))
            {
                Memory2Scene();
                //StartCoroutine(MemoryCutscene(2));
            }
        }
        else if (collision.tag == "Memory3")
        {
            if (Input.GetKey(KeyCode.E))
            {
                Memory3Scene();
                //StartCoroutine(MemoryCutscene(3));
            }
        }
        else if (collision.tag == "Memory4")
        {
            if (Input.GetKey(KeyCode.E))
            {
                Memory4Scene();
                //StartCoroutine(MemoryCutscene(4));
            }
        }
        else if (collision.tag == "Memory5")
        {
            if (Input.GetKey(KeyCode.E))
            {
                Memory5Scene();
            }
        }
        else if (collision.tag == "Memory6")
        {
            if (Input.GetKey(KeyCode.E))
            {
                Memory6Scene();
            }
        }
        else if (collision.tag == "Memory7")
        {
            if (Input.GetKey(KeyCode.E))
            {
                Memory7Scene();
            }
        }
        else if (collision.tag == "Memory8")
        {
            if (Input.GetKey(KeyCode.E))
            {
                Memory8Scene();
            }
        }
        else if (collision.tag == "Memory9")
        {
            if (Input.GetKey(KeyCode.E))
            {
                Memory9Scene();
            }
        }

    }
    public void MemoryLandReset()
    {
        memory1.SetActive(false);
        memory2.SetActive(false);
        memory3.SetActive(false);
        memory4.SetActive(false);
        memory5.SetActive(false);
        //memory6.SetActive(false);
        //memory7.SetActive(false);
        //memory8.SetActive(false);
        //memory9.SetActive(false);
        memoryLand.SetActive(true);
        mainCamera.backgroundColor = memoryLandBackgroundColor;
        mainCamera.transform.parent = transform;
        mainCamera.transform.localPosition = new Vector3(0, 2, -10);
        gameObject.transform.localScale = Vector3.one;
        spriteRenderer.enabled = true;
        spriteRenderer.color = memoryLandColor;
        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        maxVelocity = 4;
        jumpSpeed = 10;
        maxFallSpeed = 2;
        defaultGravityScale = 3;
        canJump = true;
    }

    void Memory1Scene()
    {
        memory1.SetActive(true);
        memoryLand.SetActive(false);
        mainCamera.backgroundColor = memory1BackgroundColor;
        gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        spriteRenderer.color = memoryColor;
        maxVelocity = 6;
        jumpSpeed = 20;
        maxFallSpeed = 10;
        defaultGravityScale = 8;

    }
    void Memory2Scene()
    {
        memory2.SetActive(true);
        memoryLand.SetActive(false);
        mainCamera.backgroundColor = memory2BackgroundColor;
        gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        spriteRenderer.color = memoryColor;
        maxVelocity = 6;
        canJump = false;

    }
    
    void Memory3Scene()
    {
        memory3.SetActive(true);
        memoryLand.SetActive(false);
        mainCamera.backgroundColor = memory3BackgroundColor;
        gameObject.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
        spriteRenderer.color = memoryColor;
        maxVelocity = 3;
        canJump = false;
    }

    void Memory4Scene()
    {
        memory4.SetActive(true);
        memoryLand.SetActive(false);
        mainCamera.backgroundColor = memory3BackgroundColor;
        gameObject.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
        spriteRenderer.color = memoryColor;
        maxVelocity = 4;
        canJump = false;
    }
    void Memory5Scene()
    {
        memory5.SetActive(true);
        memoryLand.SetActive(false);
        mainCamera.backgroundColor = memory3BackgroundColor;
        gameObject.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
        spriteRenderer.color = memoryColor;
        maxVelocity = 4;
        canJump = false;
    }
    void Memory6Scene()
    {
        memory6.SetActive(true);
        memoryLand.SetActive(false);
        mainCamera.backgroundColor = memory6BackgroundColor;
        gameObject.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
        spriteRenderer.color = memoryColor;
    }
    void Memory7Scene()
    {
        memory7.SetActive(true);
        memoryLand.SetActive(false);
        mainCamera.backgroundColor = memory7BackgroundColor;
        gameObject.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
        spriteRenderer.color = memoryColor;
    }
    void Memory8Scene()
    {
        memory8.SetActive(true);
        memoryLand.SetActive(false);
        mainCamera.backgroundColor = memory8BackgroundColor;
        gameObject.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
        spriteRenderer.color = memoryColor;
    }
    void Memory9Scene()
    {
        memory9.SetActive(true);
        memoryLand.SetActive(false);
        mainCamera.backgroundColor = memory3BackgroundColor;
        gameObject.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
        spriteRenderer.color = memoryColor;
    }
    //not using anymore
    //IEnumerator MemoryCutscene(int memory)
    //{
    //    rb.bodyType = RigidbodyType2D.Static;
    //    memoryLand.SetActive(false);
    //    if (memory == 1)
    //    {
    //        memory1.SetActive(true);
    //        mainCamera.backgroundColor = memory1BackgroundColor;
    //        gameObject.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
    //        yield return new WaitForSecondsRealtime(3);
    //        memory1.SetActive(false);
    //    }
    //    else if (memory == 2)
    //    {
    //        memory2.SetActive(true);
    //        yield return new WaitForSecondsRealtime(3);
    //        memory2.SetActive(false);

    //    }
    //    else if (memory == 3)
    //    {
    //        memory3.SetActive(true);
    //        yield return new WaitForSecondsRealtime(3);
    //        memory3.SetActive(false);

    //    }
    //    else if (memory == 4)
    //    {
    //        memory4.SetActive(true);
    //        yield return new WaitForSecondsRealtime(8);
    //        memory4.SetActive(false);

    //    }

    //    ResetMemoryLand();

    //}
    //void ResetMemoryLand()
    //{
    //    gameObject.transform.localScale = Vector3.one;
    //    memoryLand.SetActive(true);
    //    mainCamera.backgroundColor = memoryLandBackgroundColor;
    //    rb.bodyType = RigidbodyType2D.Dynamic;
    //}
    //void CheckDirectionToFace(bool isMovingRight)
    //{
    //    if (isMovingRight != isFacingRight)
    //        Turn();
    //}
    //private void Turn()
    //{
    //    Vector3 scale = transform.localScale;
    //    scale.x *= -1;
    //    transform.localScale = scale;

    //    isFacingRight = !isFacingRight;
    //}


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            canJump = true;
            stoppedJumping = false;
        }
    }
}
