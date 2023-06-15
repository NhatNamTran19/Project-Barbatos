using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackZone : MonoBehaviour
{
    Collider2D col;

    [SerializeField] public List<Collider2D> detectedCollieders = new List<Collider2D>();

    private void Awake()
    {
        col = GetComponent<Collider2D>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        { detectedCollieders.Add(collision); }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        detectedCollieders.Remove(collision);
    }
}
