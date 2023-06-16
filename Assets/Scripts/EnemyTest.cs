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
    private Collider2D playerPotision;

    private int facingDirection;
    private int damageDirection;
    private float currentHealth;
    private float[] attackEnemyDetails = new float[2];


    private bool groundDetected;
    private bool wallDetected;
    private bool detectTarget;
    private bool canDamage = true;
    private bool isDamaging;


    [SerializeField] private AttackZone attackZone;
    [SerializeField] private DetectedZone detectedZone;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private float maxHealth;
    [SerializeField] private float knockbackDuration;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Vector2 hitSpeed;
    [SerializeField] private float movingSpeed ;
    [SerializeField] private LayerMask whatIsGround;
    private int curWayPointIndex = 0;
    private Vector2 movement;

    [SerializeField] private Transform attack1HitBoxPos;
    [SerializeField] private float attack1Radius;
    [SerializeField] private LayerMask damageAlbe;


    private enum State
    {
        idle,
        walk,
        hit,
        dead,
    }

    private State currentState;

    public bool _hasTarget = false;
    public bool HasTarget
    {
        get { return _hasTarget; }
        private set
        {
            _hasTarget = value;
            animator.SetBool("hasTarget", value);
        }
    }
    public bool canMove
    {
        get
        {
            return animator.GetBool("canMove");
        }
    }
    


    void Awake()
    {
        transform = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxcol = GetComponent<BoxCollider2D>();
        facingDirection = 1;
        currentHealth = maxHealth;
        PlayerController playerPotision = GetComponent<PlayerController>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        HasTarget = attackZone.detectedCollieders.Count > 0;
        detectTarget = detectedZone.detectedCollieders.Count > 0;
        //Debug.Log(canMove);
        //Move();
        ChasePlayer();

    }
    private void FixedUpdate()
    {
        CheckSurroundings();
        ChasePlayer();
    }
    private void ChasePlayer()
    {
        if (detectTarget && groundDetected)
        {
            movement.Set(movingSpeed *2.5f* facingDirection, rb.velocity.y);
            rb.velocity = movement;
        }
        else
        {
            CanMove();
        }
    }

    private void CanMove()
    {
        if (canMove)
        {
            Move();
        }
        else
        { rb.velocity = new Vector2(0, rb.velocity.y);
            //movingSpeed = 0f;
        }
    }

    private void Move()
    {
        if ((!groundDetected && rb.velocity.y >= 0) || wallDetected)
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
        //Gizmos.DrawWireSphere(attack1HitBoxPos.position, attack1Radius);
    }

    public void Damage2(float[] attackDetails)
    {
        currentHealth -= attackDetails[0];
        if (attackDetails[1] < this.transform.position.x)
        {
            damageDirection = 200;
        }
        else
        {
            damageDirection = -200;
        }
        //Debug.Log(rb.velocity.x);
        if (!HasTarget)
        {
            Flip();
        }
        movement.Set(hitSpeed.x * damageDirection, hitSpeed.y);
        rb.velocity = movement;
        rb.AddForce(movement);
        animator.SetTrigger("hit");
        if (currentHealth <= 0)
        {
            Dead();
        }
    }


    public void Damage(float[] attackEnemyDetails)
    {
        if (canDamage)
        {
            StartCoroutine(Damaging(attackEnemyDetails));
            //Debug.Log(playerPotision.transform.position.x);
        }
    }

    private IEnumerator Damaging(float[] attackDetails)
    {
        canDamage = false; //khi nhat vat luot, set = false de nhan vat ko luot lien tuc 2 lan
        isDamaging = true; //ngan chan input cua nhan vat khi dang luot
                           // float originaljumpForce = jumpForce;
                           //currentHealth -= attackEnemyDetails[0];
                           //if (attackEnemyDetails[1] < this.transform.position.x)
                           //{
                           //    damageDirection = 2;
                           //}
                           //else
                           //{
                           //    damageDirection = -2;
                           //}
                           //if (!HasTarget)
                           //{
                           //    Flip();            
                           //}
                           //Debug.Log("ZZZZ");
                           //movement.Set(hitSpeed.x * damageDirection, hitSpeed.y);
                           //rb.velocity = movement;
                           //rb.AddForce(movement);
                           //animator.SetTrigger("hit");
        Damage2(attackDetails);
        if (currentHealth <= 0)
        {
            Dead();
        }
        yield return new WaitForSeconds(0.2f); // khoang thoi gian khi nhan vat luot
        isDamaging = false; // cho nhan vat di chuyen binh thuong
        yield return new WaitForSeconds(.5f); //thoi gian cho de luot lan tiep theo
        canDamage = true; // cho nhan vat luot lan tiep theo
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
    
    //private void CheckAttackHitBox()
    //{
    //    Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attack1HitBoxPos.position, attack1Radius, damageAlbe);
    //    attackEnemyDetails[0] = 10;
    //    attackEnemyDetails[1] = this.transform.position.x;
    //    foreach (Collider2D enemy in detectedObjects)
    //    {
    //        //collider.transform.parent.SendMessage("damage", attackDetails);
    //        Debug.Log("hit something");
    //        enemy.GetComponent<PlayerController>().Damage(attackEnemyDetails);
    //        //collider.transform.parent.SendMessage("Damage", attackDetails);
    //    }
    //}
}

