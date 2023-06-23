using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    private Animator animator;

    private float lastInputtime =Mathf.NegativeInfinity;

    private bool gotInput;
    private bool isAttacking;
    private bool isFirstAttack;

    private float[] attackDetails = new float[2];

    [SerializeField] private PlayerController playerController;
    [SerializeField] private bool combatEnable;
    [SerializeField] private float inputTimer;
    [SerializeField] private float attack1Radius;
    [SerializeField] private Transform attack1HitBoxPos;
    [SerializeField] private LayerMask damageAlbe;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("canAttack", combatEnable);
    }
    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
        animator.SetBool("canAttack", combatEnable);
    }

    private void Update()
    {
        CheckCombatInput();
        CheckAttacks();
    }

    private void CheckCombatInput()
    {
        if (Input.GetKeyDown(KeyCode.Z) &&!playerController.isDead)
        {
            if (combatEnable && !playerController.isSliding&& !playerController.isWall())
            {
                gotInput = true;
                lastInputtime = Time.time;
            }
        }
    }
    private void CheckAttacks()
    {
        if(gotInput)
        {
            if(!isAttacking)
            {
                gotInput = false;
                isAttacking = true;
                isFirstAttack = !isFirstAttack;
                animator.SetBool("attk1", true);
                animator.SetBool("firstAttack", isFirstAttack);
                animator.SetBool("isAttacking", isAttacking);

            }
        }
        if(Time.time >= lastInputtime+ inputTimer) 
        {
            gotInput = false;
        }
    }
    //private void CheckAttackHitBox()
    //{
    //    Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attack1HitBoxPos.position, attack1Radius, damageAlbe);
    //    attackDetails[0] = 10;
    //    attackDetails[1] = this.transform.position.x;
    //    foreach (Collider2D enemy in detectedObjects)
    //    {
    //        //collider.transform.parent.SendMessage("damage", attackDetails);
    //        Debug.Log("hit something");
    //        enemy.GetComponent<EnemyTest>().Damage2(attackDetails);
    //        //collider.transform.parent.SendMessage("Damage", attackDetails);
    //    }
    //}
    private void FinishAttack1()
    {
        isAttacking= false;
        animator.SetBool("isAttacking", isAttacking);
        animator.SetBool("attk1", false);
    }
    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawWireSphere(attack1HitBoxPos.position, attack1Radius);
    //}
}
