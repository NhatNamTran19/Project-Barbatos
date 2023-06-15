using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    private Collider2D attackCollider;
    [SerializeField] private float damage;

    private void Awake()
    {
        attackCollider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyTest Damage = collision.GetComponent<EnemyTest>();
            if (Damage != null)
            {
                Damage.Damage2(damage);
            }
        }
        if (collision.CompareTag("Player"))
        {
            PlayerController Damage = collision.GetComponent<PlayerController>();
            if (Damage != null)
            {
                Damage.Damage2(damage);
            }
        }

    }
}
