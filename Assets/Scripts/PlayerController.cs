using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D boxcol;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Transform transform;

    private MovementState movementState;
    private float dicX;
    private float scaleX;
    private float scaleY;
    private int facingDirection;

    private float Right ;
    private bool isRuning;
    private bool isTurn;
    private bool canDash = true;
    private bool isDashing;
    public bool isSliding;
    private bool canSlide ;
    private bool isWallJump;

    private int damageDirection;
    private float currentHealth;
    private float[] attackEnemyDetails = new float[2];

    [SerializeField] private float maxHealth;
    [SerializeField] private float wallJumpForce;
    [SerializeField] private float dashPower = 24f;
    [SerializeField] private float dashingTime = 0.4f;
    [SerializeField] private float dashCooldown = 1f;
    [SerializeField] private float moveSpeed;   
    [SerializeField] private float jumpForce;
    [SerializeField] private float GravityWall;
    [SerializeField] private LayerMask jumpGround;
    [SerializeField] private Vector2 hitSpeed;



    private enum MovementState 
    {
        idle,
        runing,
        jumping,
        falling
    }

    void Awake()
    {
        transform = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();  
        boxcol = GetComponent<BoxCollider2D>();
        scaleX = transform.localScale.x;
        facingDirection = 1;
        currentHealth = maxHealth;

    }

    void Update()
    {
        if (isDashing)
        {
            return;
        }

        dicX = Input.GetAxis("Horizontal");

        Jump();
        UpdateState();
        Dashing();
        WallJump();
        Debug.Log(scaleY);
       
    }
    private void FixedUpdate()
    {
        
        if (!isWallJump)
        {
            Move();
        }
        CheckWallSliding();
        SlideWall();
        

    }
    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGround() )
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }
    private void CheckWallSliding()
    {
        if(isWall() && !isGround() && rb.velocity.y<.1f && dicX !=0)
        {
            canSlide = true;
        }
        else
        {
            canSlide = false;
        }
    }
    private void flip()
    {
        if (dicX > 0)
        {
            transform.localScale = new Vector3(scaleX, transform.localScale.y, transform.localScale.z);
        }
        if (dicX < 0)
        {
            transform.localScale = new Vector3((-1) * scaleX, transform.localScale.y, transform.localScale.z);
        }

    }
    private void WallJump()
    {
        if(Input.GetButtonDown("Jump")&&isSliding)
        {
            isWallJump = true;
            //Vector2 force = new Vector2(-dicX*wallJumpForce, jumpForce);
            rb.velocity = new Vector2(-dicX*wallJumpForce, jumpForce);
            //rb.velocity = Vector2.zero;
            //rb.AddForce(force, ForceMode2D.Impulse);
            Invoke("StopWallJummp", .1f);
        }
    }
    private void StopWallJummp()
    {
        isWallJump = false;
    }
    private void SlideWall()
    {
        if (isWall() && canSlide)
        {
            isSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, -GravityWall);
        }
        if (isGround() || !isWall())
        {
            isSliding = false;
        }
    }
   
    private void Move()
    {
        flip();
        rb.velocity = new Vector2(dicX * moveSpeed, rb.velocity.y);
    }
    private void Dashing()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash && isGround() && dicX != 0f)
        {
            StartCoroutine(Dash());
        }
    }
    

    private void Turn()
    {
      
    }

    private void UpdateState()
     {
        if(dicX > 0f)
        {         
            movementState = MovementState.runing;
        }
        else if(dicX < 0f) 
        {
            movementState = MovementState.runing;
        }
        else 
        {
            movementState = MovementState.idle;
        }

        if (rb.velocity.y>.1f)
        {
            movementState = MovementState.jumping;
        }
        else if(rb.velocity.y<-.1f)
        {
            movementState = MovementState.falling;
        }
        animator.SetInteger("state",(int)movementState);
        animator.SetBool("slide", isSliding);
    }

    private bool isGround()
    {
        return Physics2D.BoxCast(boxcol.bounds.center, boxcol.bounds.size, 0f, Vector2.down, 0.1f, jumpGround);
    }
    private IEnumerator Dash()
    {
        canDash = false; //khi nhat vat luot, set = false de nhan vat ko luot lien tuc 2 lan
        isDashing = true; //ngan chan input cua nhan vat khi dang luot
       // float originaljumpForce = jumpForce;
        rb.velocity = new Vector2(dicX * dashPower, 0f); // set khoang cach luot cua nhan vat
        animator.SetTrigger("dash");
        yield return new WaitForSeconds(dashingTime); // khoang thoi gian khi nhan vat luot
        isDashing = false; // cho nhan vat di chuyen binh thuong
        //jumpForce = originaljumpForce;
        yield return new WaitForSeconds(dashCooldown); //thoi gian cho de luot lan tiep theo
        canDash = true; // cho nhan vat luot lan tiep theo
    }
    public bool isWall()
    {
        if (dicX>0)
        {
            return Physics2D.BoxCast(boxcol.bounds.center, boxcol.bounds.size, 0f, Vector2.right, 0.1f, jumpGround);
        }
        else if (dicX<0)
        {
            return Physics2D.BoxCast(boxcol.bounds.center, boxcol.bounds.size, 0f, Vector2.left, 0.1f, jumpGround);
        }
        else { return false; }
    }

    public void Damage(float[] attackEnemyDetails)
    {
        currentHealth -= attackEnemyDetails[0];

        rb.velocity = new Vector2(moveSpeed * hitSpeed.x * damageDirection, hitSpeed.y) ;       
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
        //boxcol.size = new Vector2(2, 1.4f);
        //boxcol.size = new Vector2(2.1f, 1.5f);
        boxcol.usedByEffector = true;
       // transform.position = new Vector2(transform.position.x, transform.position.y+2f);
        this.enabled = false;
        boxcol.enabled = false;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

    }


}
