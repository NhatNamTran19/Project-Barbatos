using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class HealthBarEnemy : MonoBehaviour
{
    [SerializeField] private Slider healthSlier;
    public Vector3 offset;


   

    void Update()
    {
        healthSlier.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offset);
    }

    public void SetHealth(float health,float maxHealth)
    {
        healthSlier.gameObject.SetActive(health < maxHealth);
        healthSlier.value = health;
        healthSlier.maxValue =  maxHealth;

        healthSlier.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(Color.red, Color.green, healthSlier.normalizedValue);
    }

}


