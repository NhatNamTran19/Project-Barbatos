using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GamePanel : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI cherriesText;

    //[SerializeField]
    //private TextMeshProUGUI timeText;
    //private float timeRemaining;
    //private bool timeIsRunning = false;

    private void OnEnable()
    {
        //Dang ky su kien
        //ItemCollector.collectCherryDelegate += OnPlayerCollectCherries;
        //timeIsRunning = true;
    }

    private void Start()
    {
        //cherriesText.text = GameManager.Instance.Cherries.ToString();
    }

    private void Update()
    {       
                
    }

    private void OnDisable()
    {
        //Huy su kien
       // ItemCollector.collectCherryDelegate -= OnPlayerCollectCherries;
    }


    //private void OnPlayerCollectCherries(int value)
    //{
    //    cherriesText.text = value.ToString();
    //}
}
