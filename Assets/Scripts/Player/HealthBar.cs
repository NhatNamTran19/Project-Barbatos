using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;using TMPro;
using System;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider healthSlier;
    [SerializeField] private TMP_Text healthBarText;


    Damageable playerDamageable;
    PlayerController playerController;


    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        healthSlier = GetComponent<Slider>();
        playerDamageable = player.GetComponent<Damageable>();
        playerController = player.GetComponent<PlayerController>();
    }
    void Start()
    {

    }

    

    // Update is called once per frame
    void Update()
    {
        healthSlier.value = CalPercentage(playerController.currentHealth, playerController.maxHealth);
        healthBarText.text = "HP: " + (playerController.currentHealth / playerController.maxHealth) * 100 + "%";
        ChangeColor();
    }

    private void ChangeColor()
    {
        //ColorBlock cb = healthSlier.colors;
        
        //if (healthSlier.value <= 0.7f)
        //{        
        //    cb.normalColor = Color.yellow;         
        //}
        //if (healthSlier.value <= 0.3f)
        //{
        //    cb.normalColor = new Color(0.631f, 0f, 0f, 1f);
        //}
        //healthSlier.colors = cb;
        healthSlier.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(Color.red,Color.green,healthSlier.normalizedValue);
    }

    private float CalPercentage(float currentHealth, float maxHealth)
    {
        return currentHealth / maxHealth;
    }
}
