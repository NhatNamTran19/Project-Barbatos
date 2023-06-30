using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private float maxHealth=5f;
    private float currentHealth;
    private BoxCollider2D boxcol;
    private SpriteRenderer spriteRenderer;
    private Transform transform;
    private Collider2D playerPotision;
    private Animator animator;



    [SerializeField] private GameObject player;
    [SerializeField] private float force;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        transform = GetComponent<Transform>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxcol = GetComponent<BoxCollider2D>();
        currentHealth = maxHealth;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        Vector3 dicection = player.transform.position - transform.position;
        rb.velocity = new Vector2(dicection.x, dicection.y).normalized * force;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Damage(float[] attackDetails)
    {
        currentHealth -= attackDetails[0];
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().currentHealth -= 50;
            Destroy(gameObject);
        }
    }


}
