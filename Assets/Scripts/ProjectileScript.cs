using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private GameObject player;
    [SerializeField] private float force;


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
}
