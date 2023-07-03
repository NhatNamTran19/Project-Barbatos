using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemCollector : MonoBehaviour
{
    public delegate void CollectSoul(int soul); //Dinh nghia ham delegate
    public static CollectSoul collectSoulDelegate; //Khai bao ham delegate

    private int key;
    private int soul;

    private void Start()
    {
        if(GameManager.Instance != null)
        {
            soul = GameManager.Instance.Soul;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Soul"))
        {
            if (AudioManager.HasInstance)
            {
                AudioManager.Instance.PlaySE(AUDIO.SE_COLLECT);
            }
            soul++;
            key++;
            if (GameManager.Instance != null)
            {
                GameManager.Instance.UpdateSoul(soul);
            }
            collectSoulDelegate(soul); //phat su kien
            Debug.Log("key: " + key);
            Destroy(collision.gameObject);
        }
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
            Debug.Log("soul: " + soul);
            Destroy(collision.gameObject);
        }
    }
}
