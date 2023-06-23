using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemCollector : MonoBehaviour
{
    //public delegate void CollectCherry(int cherry); //Dinh nghia ham delegate
    //public static CollectCherry collectCherryDelegate; //Khai bao ham delegate

    private int key;
    private int _lock;
    private int soul;

    private void Start()
    {
        if(GameManager.Instance != null)
        {
            key = GameManager.Instance.Key;
            _lock = GameManager.Instance.Lock;
            soul = GameManager.Instance.Soul;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Key"))
        {
            //if (AudioManager.HasInstance)
            //{
            //    AudioManager.Instance.PlaySE(AUDIO.SE_COLLECT);
            //}
            key++;
            //if(GameManager.Instance != null)
            //{
            //    GameManager.Instance.UpdateCherries(cherries);
            //}
            //collectCherryDelegate(cherries); //phat su kien
            Debug.Log("key: " + key);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Soul"))
        {
            //if (AudioManager.HasInstance)
            //{
            //    AudioManager.Instance.PlaySE(AUDIO.SE_COLLECT);
            //}
            soul++;
            //if(GameManager.Instance != null)
            //{
            //    GameManager.Instance.UpdateCherries(cherries);
            //}
            //collectCherryDelegate(cherries); //phat su kien
            Debug.Log("soul: " + soul);
            Destroy(collision.gameObject);
        }
    }
}
