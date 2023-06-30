using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class HealthBarBoss : MonoBehaviour
{
    [SerializeField] private Slider healthSlier;
    public Vector3 offset;


   

    //void Update()
    //{
    //    healthSlier.transform.position = Camera.main.WorldToScreenPoint(transform.position);
    //}

    public void SetHealth(float health,float maxHealth)
    {
        //healthSlier.gameObject.SetActive(health < maxHealth);
        healthSlier.value = CalPercentage(health , maxHealth);
        //healthSlier.maxValue =  maxHealth;

        //healthSlier.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(Color.red, Color.green, healthSlier.normalizedValue);
        
    }
    private float CalPercentage(float currentHealth, float maxHealth)
    {
        return currentHealth / maxHealth;
    }
}


