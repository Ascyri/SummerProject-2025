using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Movement
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float maxVelocity;
    [SerializeField] private float attackCooldown;
    private Rigidbody2D playerRB;
    private bool canJump;
    bool turned;

    //Attack
    public bool attackUnlocked;
    [SerializeField] GameObject weaponGO;
    [SerializeField] private Animator attackAnimator;
    bool canAttack = true;
    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
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
        if (playerRB.velocity.x < 0)
        {
            turned = true;
        }
        else if (playerRB.velocity.x > 0)
        {
            turned = false;
        }
    }
    private void VelocityLimiter()
    {
        if (playerRB.velocity.x >= maxVelocity)
        {
            playerRB.velocity = new Vector2(maxVelocity, playerRB.velocity.y);
        }
        if (playerRB.velocity.x <= -maxVelocity)
        {
            playerRB.velocity = new Vector2(-maxVelocity, playerRB.velocity.y);
        }
    }
    private void Movement()
    {
        if (Input.GetKey(KeyCode.D))
        {
            playerRB.AddForce(Vector2.right * movementSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            playerRB.AddForce(Vector2.left * movementSpeed * Time.deltaTime);
        }
    }

    private void Jump()
    {
        if (Input.GetKey(KeyCode.Space) && canJump)
        {
            playerRB.AddForce(Vector2.up * jumpSpeed);
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
            weaponGO.transform.position = new Vector3(transform.position.x-1, transform.position.y, transform.position.z);
        }
        else
        {
            weaponGO.transform.eulerAngles = new Vector3(0, 0, -80);
            weaponGO.transform.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);

        }
        if (Input.GetKeyDown(KeyCode.Mouse0) && canAttack)
        {
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        canAttack = false;
        attackAnimator.Play("Attack");
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
}
