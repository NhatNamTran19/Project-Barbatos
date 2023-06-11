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
    [SerializeField] private bool combatEnable;
    [SerializeField] private float inputTimer;
    [SerializeField] private float attack1Radius;
    [SerializeField] private float attack1Damage;
    [SerializeField] private Transform attack1HitBoxPos;
    [SerializeField] private LayerMask damageAlbe;

    //private void Awake()
    //{
    //    animator = GetComponent<Animator>();
    //    animator.SetBool("canAttack", combatEnable);
    //}
    private void Start()
    {
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
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (combatEnable)
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
    private void CheckAttackHitBox()
    {
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attack1HitBoxPos.position,attack1Radius, damageAlbe);
        foreach (Collider2D collider in detectedObjects)
        {
            collider.transform.parent.SendMessage("damage", attack1Damage);
        }
    }
    private void FinishAttack1()
    {
        isAttacking= false;
        animator.SetBool("isAttacking", isAttacking);
        animator.SetBool("attk1", false);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attack1HitBoxPos.position, attack1Radius);
    }
}
