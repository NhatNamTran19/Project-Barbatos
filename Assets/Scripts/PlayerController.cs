using Assets.Scripts.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
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
    private bool Turning= false;
    private bool canDash = true;
    private bool isDashing;
    public bool isSliding;
    private bool canSlide ;
    private bool isWallJump;
    public bool isDead=false;
    private bool Jumping = false;

    private int damageDirection;
    public float currentHealth;
    public float[] attackEnemyDetails = new float[2];
    private float maxY;

    [SerializeField] public float maxHealth;
    [SerializeField] private float wallJumpForce;
    [SerializeField] private float dashPower = 24f;
    [SerializeField] private float dashingTime = 0.4f;
    [SerializeField] private float dashCooldown = 1f;
    [SerializeField] private float moveSpeed;   
    [SerializeField] private float jumpForce;
    [SerializeField] private float GravityWall;
    [SerializeField] private float damageFall;
    [SerializeField] private LayerMask jumpGround;
    [SerializeField] private Vector2 hitSpeed;

    private bool canMove = true;



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
       // Debug.Log(rb.velocity.y);

    }
    private void FixedUpdate()
    {
        
        if (!isWallJump)
        {
            Move();
        }
        CheckWallSliding();
        SlideWall();
        TakeDamgefall();
        
    }
    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGround() && canMove )
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            maxY = 0;
            Jumping = true;
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
    //IEnumerator LerpToRotation(float endRotation, float time, float delay)
    //{
    //    yield return new WaitForSeconds(delay);

    //    float startRotation = transform.rotation.eulerAngles.y;
    //    float lerpRotation = startRotation;

    //    float i = 0f;
    //    float rate = 1 / time;
    //    while (i <= 1)
    //    {
    //        i += Time.deltaTime * rate;

    //        lerpRotation = Mathf.Lerp(startRotation, endRotation, i);
    //        transform.rotation = Quaternion.Euler(0f, lerpRotation, 0f);
    //        yield return null;
    //    }
    //    transform.rotation = Quaternion.Euler(0f, endRotation, 0f);
    //}
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

    private void FlipAnim()
    {
        Vector3 currentScale = transform.localScale;
        transform.localScale = new Vector3((-1) * currentScale.x, transform.localScale.y, transform.localScale.z);
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
        if (canMove)
        {
            flip();
            rb.velocity = new Vector2(dicX * moveSpeed, rb.velocity.y);
        }

        if ((transform.localScale.x == 1f && Input.GetKeyDown(KeyCode.LeftArrow)) || (transform.localScale.x == -1f && Input.GetKeyDown(KeyCode.RightArrow)))
        {if (isGround()&&(Mathf.Abs(rb.velocity.x) >10f)) 
            { StartCoroutine(Anima()); }
        }
    }

    IEnumerator Anima()
    {
        animator.SetTrigger("turn");

        AnimatorClipInfo[] clip = animator.GetCurrentAnimatorClipInfo(0);
        float m_CurrentClipLength = clip[0].clip.length;
        canMove = false;
        yield return new WaitForSeconds(m_CurrentClipLength);
        canMove = true;
        //transform.localScale = new Vector3(transform.localScale.x *(-1f), transform.localScale.y, transform.localScale.z);
        rb.AddForce(new Vector2(-dicX, rb.velocity.y));
    }
    private void Dashing()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash && isGround() && dicX != 0f)
        {
            StartCoroutine(Dash());
        }
    }
     private void TakeDamgefall()
    {
        if (rb.velocity.y == 0 && Jumping)
        {
            Jumping = false;
            if (maxY < -40f)
            { DamageFall(); }
        }
        else if (Jumping)
        {
            if (rb.velocity.y < maxY)
            { maxY = rb.velocity.y; }
        }
    }

    private void DamageFall()
    {
            float dam = (damageFall * Mathf.Floor((maxY + 40) / 10))*-1;
            currentHealth -= dam;
            CharacterEvents.characterDamaged.Invoke(gameObject, dam);
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

    //public void Damage(float[] attackEnemyDetails)
    //{
    //    currentHealth -= attackEnemyDetails[0];

    //    rb.velocity = new Vector2(0f, hitSpeed.y) ;       
    //    animator.SetTrigger("hit");
    //    if (currentHealth <= 0)
    //    {
    //        Dead();
    //    }
    //}
    public void Damage2(float attackEnemyDetails)
    {
        currentHealth -= attackEnemyDetails;
        rb.velocity = new Vector2(0f, hitSpeed.y) ;       
        animator.SetTrigger("hit");
        CharacterEvents.characterDamaged.Invoke(gameObject, attackEnemyDetails);
        if (currentHealth <= 0f)
        {
            Dead();
        }
    }

    private void Dead()
    {
        animator.SetBool("death", true);
        isDead = true;
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
