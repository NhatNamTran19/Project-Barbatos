using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyBasicCombat : MonoBehaviour
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
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case State.walk:
                UpdateWalking ();
                break;
            case State.hit:
                UpdateHit();
                break;
            case State.dead:
                UpdateDead();
                break;
        }
        Move();

    }
    private void FixedUpdate()
    {
        CheckSurroundings();

    }

    //Walking
    private void Walking()
    {

    }
    private void UpdateWalking()
    {
        if (!groundDetected || wallDetected)
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
    private void ExitWalking()
    {

    }
    //Attack
    private void Attack()
    {

    }
    private void UpdateAttack()
    {

    }
    private void ExitAttack()
    {

    }
    //Hit
    private void Hit()
    {
        hitStartTime = Time.time;
        movement.Set(hitSpeed.x * damageDirection, hitSpeed.y);
        rb.velocity = movement;
        animator.SetInteger("state", (int)State.hit);
    }
    private void UpdateHit()
    {

    }
    private void ExitHit()
    {
        animator.SetInteger("state", (int)State.idle);
    }
    //Dead
    private void Dead()
    {

    }
    private void UpdateDead()
    {

    }
    private void ExitDead()
    {

    }

    private void Move()
    {
        //Flip();
        //movementState = MovementState.walk;
        //animator.SetInteger("state", (int)movementState);
        //if (Vector2.Distance(wayPoints[curWayPointIndex].transform.position, transform.position) < 0.1f)
        //{
        //    curWayPointIndex++;
        //    if (curWayPointIndex >= wayPoints.Length)
        //    {
        //        curWayPointIndex = 0;
        //    }
        //}
        //transform.position = Vector2.MoveTowards(transform.position,
        //    wayPoints[curWayPointIndex].transform.position,
        //    movingSpeed * Time.deltaTime);
        if (!groundDetected || wallDetected)
        {
            Flip();
        }
        else
        {
            movement.Set(movingSpeed * facingDirection, rb.velocity.y);
            rb.velocity = movement;
            animator.SetInteger("state",(int)State.walk);
        }
    }
    private void Flip()
    {
        //if (transform.position.x < (wayPoints[0].transform.position.x + 0.1f))
        //{
        //    transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        //}
        //if (transform.position.x > (wayPoints[1].transform.position.x - 0.1f))
        //{
        //    transform.localScale = new Vector3((-1), transform.localScale.y, transform.localScale.z);
        //}
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

        //Hit particle

        if (currentHealth > 0.0f)
        {
            Hit();
        }
        else if (currentHealth <= 0.0f)
        {
            Destroy(this);
        }
    }
    private void SwitchState(State state)
    {
        switch (currentState)
        {
            case State.walk:
                ExitWalking();
                break;
            case State.hit:
                ExitHit();
                break;
            case State.dead:
                ExitDead();
                break;
        }

        switch (state)
        {
            case State.walk:
                Walking();
                break;
            case State.hit:
                Hit();
                break;
            case State.dead:
                Dead();
                break;
        }

        currentState = state;
    }

}
