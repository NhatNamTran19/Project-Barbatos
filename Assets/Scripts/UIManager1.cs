using Assets.Scripts.Event;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager1 : MonoBehaviour
{
    [SerializeField]private GameObject damageTextPrefab;
    [SerializeField]private GameObject healthTextPrefab;
    [SerializeField]private Canvas gameCanvas;

    private void Awake()
    {
        gameCanvas = FindObjectOfType<Canvas>();
    }

    private void OnEnable()
    {
        CharacterEvents.characterDamaged += CharacterTookDamage;
    }

    private void OnDisable()
    {
        CharacterEvents.characterDamaged -= CharacterTookDamage;
    }
    public void CharacterTookDamage(GameObject character, float damgaeRecuived)
    {
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);  

        TMP_Text tmpText = Instantiate(damageTextPrefab,spawnPosition, Quaternion.identity,gameCanvas.transform).GetComponent<TMP_Text>();
        tmpText.text = "-"+damgaeRecuived.ToString();
    }

   
}
