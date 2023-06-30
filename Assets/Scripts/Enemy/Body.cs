using Assets.Scripts.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D boxcol;
    private SpriteRenderer spriteRenderer;
    private Transform transform;
    private Collider2D playerPotision;
    private Animator animator;


    private float currentHealth;

    [SerializeField] private spawnSoul spawnSoul;
    [SerializeField] private GameObject hitParticle;
    [SerializeField] private GameObject deathBloodParticle;
    [SerializeField] private float maxHealth;
    [SerializeField] private GameObject deathChunkParticle;
    private float[] attackDetails = new float[2];


    void Awake()
    {
        animator = GetComponent<Animator>();
        transform = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxcol = GetComponent<BoxCollider2D>();
        currentHealth = maxHealth;
    }
    // Update is called once per frame
    public void Damage(float[] attackDetails)
    {
        currentHealth -= attackDetails[0];
        Instantiate(hitParticle, animator.transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
        Instantiate(deathBloodParticle, transform.position, deathBloodParticle.transform.rotation);
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Dead();
        }
    }
    private void Dead()
    {
        spawnSoul.Spawn();
        Instantiate(deathChunkParticle, transform.position, deathChunkParticle.transform.rotation);
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
         boxcol.enabled = false;
    }
}
