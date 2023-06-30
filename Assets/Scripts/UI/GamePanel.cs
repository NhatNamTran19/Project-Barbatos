using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GamePanel : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI soulText;

    private void Start()
    {
        soulText.text = GameManager.Instance.Soul.ToString();
    }

    private void OnEnable()
    {
        //Dang ky su kien
        ItemCollector.collectSoulDelegate += OnPlayerCollectSoul;
        
    }

    private void OnDisable()
    {
        //Huy su kien
        ItemCollector.collectSoulDelegate -= OnPlayerCollectSoul;
    }

    private void OnPlayerCollectSoul(int value)
    {
        soulText.text = value.ToString();
    }
}
