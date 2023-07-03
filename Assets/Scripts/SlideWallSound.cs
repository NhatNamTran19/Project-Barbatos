using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class SlideWallSound : MonoBehaviour
{
    private BoxCollider2D boxcol;
    [SerializeField] private LayerMask jumpGround;

    public bool isSliding;
    private bool canSlide;
    private float dicX;


    void Awake()
    {
        boxcol = GetComponent<BoxCollider2D>();
    }

    void FixedUpdate()
    { 
        dicX = Input.GetAxis("Horizontal");
        isWall(); isGround(); SlideWall();
        CheckWallSliding();
    }

        public bool isGround()
    {
        return Physics2D.BoxCast(boxcol.bounds.center, boxcol.bounds.size, 0f, Vector2.down, 0.1f, jumpGround);
    }
    public bool isWall()
    {
        if (dicX > 0)
        {
            return Physics2D.BoxCast(boxcol.bounds.center, boxcol.bounds.size, 0f, Vector2.right, 0.1f, jumpGround);
        }
        else if (dicX < 0)
        {
            return Physics2D.BoxCast(boxcol.bounds.center, boxcol.bounds.size, 0f, Vector2.left, 0.1f, jumpGround);
        }
        else { return false; }
    }

    private void SlideWall()
    {
        if (isWall() && canSlide)
        {
            isSliding = true;
            if (AudioManager.HasInstance)
            {
                if (!AudioManager.Instance.AttachSESource.isPlaying)
                {
                    AudioManager.Instance.PlaySE(AUDIO.SE_SLIDING);
                }
            }
        }
        if (isGround()||!isWall())
        {
            AudioManager.Instance.AttachSESource.Stop();
        }

    }
    private void CheckWallSliding()
    {
        if (isWall() && !isGround()  && dicX != 0)
        {
            canSlide = true;
        }
        else
        {
            canSlide = false;
        }
    }
}
