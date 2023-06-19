using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEditor.SceneManagement;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    private Collider2D attackCollider;
    [SerializeField] public float damage;
    private float[] attackEnemyDetails = new float[2];
    [SerializeField] private GameObject player;

    private void Awake()
    {
        attackCollider = GetComponent<Collider2D>();
        attackEnemyDetails[0] = damage;
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        attackEnemyDetails[1] = player.transform.position.x;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyTest Enemy = collision.GetComponent<EnemyTest>();
        PlayerController Player = collision.GetComponent<PlayerController>();
        if (collision.CompareTag("Enemy"))
        {           
            if (Enemy != null)
            {      
                Enemy.Damage(attackEnemyDetails);
            }
        }
        if (collision.CompareTag("Player"))
        {         
            if (Player != null)
            {
                Player.Damage2(damage);
            }
        }

    }
}
