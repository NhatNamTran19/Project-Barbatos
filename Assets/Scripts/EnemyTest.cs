using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D boxcol;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Transform transform;

    private int facingDirection;
    private int damageDirection;
    private float currentHealth;
    private float hitStartTime;

    private bool groundDetected;
    private bool wallDetected;


    [SerializeField] private float groundCheckDistance;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private float maxHealth;
    [SerializeField] private float knockbackDuration;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Vector2 hitSpeed;
    [SerializeField] private float movingSpeed = 2f;
    [SerializeField] private LayerMask whatIsGround;
    private int curWayPointIndex = 0;
    private Vector2 movement;

    private enum State
    {
        idle,
        walk,
        hit,
        dead,
    }

    private State currentState;

    void Awake()
    {
        transform = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxcol = GetComponent<BoxCollider2D>();
        facingDirection = 1;
        currentHealth = maxHealth;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();

    }
    private void FixedUpdate()
    {
        CheckSurroundings();
    }

    private void Move()
    {
        if ((!groundDetected && rb.velocity.y > 0) || wallDetected)
        {
            Flip();
        }
        else
        {
            movement.Set(movingSpeed * facingDirection, rb.velocity.y);
            rb.velocity = movement;
            animator.SetInteger("state", (int)State.walk);
        }
    }
    private void Flip()
    {
        facingDirection *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }
    private void CheckSurroundings()
    {
        groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
        wallDetected = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
    }

    public void Damage(float[] attackDetails)
    {
        currentHealth -= attackDetails[0];

        if (attackDetails[1] > transform.position.x)
        {
            damageDirection = -1;
        }
        else
        {
            damageDirection = 1;
        }
        movement.Set(hitSpeed.x * damageDirection, hitSpeed.y);
        rb.velocity = movement;
        animator.SetTrigger("hit");
        if (currentHealth <= 0)
        {
            Dead();
        }
    }
    private void Dead()
    {       
            animator.SetBool("death", true);
        //animator.SetBool("hit", false);
        boxcol.size = new Vector2(2, 1f);
        boxcol.offset = new Vector2(boxcol.offset.x, -1.5f);
        boxcol.usedByEffector = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        this.enabled = false;
    }
}

