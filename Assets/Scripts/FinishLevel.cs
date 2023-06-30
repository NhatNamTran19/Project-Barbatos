using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLevel : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CompleteLevel();
        }
    }

    private void CompleteLevel()
    {
        if (SceneManager.GetActiveScene().name.Equals("Level1"))
        {
            SceneManager.LoadScene("Level2");
            if (AudioManager.HasInstance && UIManager.HasInstance)
            {
                AudioManager.Instance.PlaySE(AUDIO.SE_FINISH);
                AudioManager.Instance.PlayBGM(AUDIO.BGM_BGM_02);
            }


        }
        if (SceneManager.GetActiveScene().name.Equals("Level2"))
        {
            if (AudioManager.HasInstance && UIManager.HasInstance)
            {
                AudioManager.Instance.PlaySE(AUDIO.SE_FINISH);
                AudioManager.Instance.PlayBGM(AUDIO.BGM_BGM_03);
            }
            SceneManager.LoadScene("Level3");
        }
        
        if (SceneManager.GetActiveScene().name.Equals("Level3"))
        {
            if (AudioManager.HasInstance && UIManager.HasInstance)
            {
                AudioManager.Instance.PlaySE(AUDIO.SE_FINISH);
                AudioManager.Instance.PlayBGM(AUDIO.BGM_BGM_04);
            }
            SceneManager.LoadScene("LevelBoss");
        }
        if (SceneManager.GetActiveScene().name.Equals("LevelBoss"))
        {
            if (AudioManager.HasInstance && UIManager.HasInstance)
            {
                AudioManager.Instance.PlaySE(AUDIO.SE_FINISH);
                AudioManager.Instance.PlayBGM(AUDIO.BGM_BGM_01);
            }
            if (UIManager.HasInstance)
            {
                Time.timeScale = 0f;
                UIManager.Instance.ActiveVictoryPanel(true);
            }
            SceneManager.LoadScene("LevelBoss");
        }

        //else
        //{
        //    if (UIManager.HasInstance)
        //    {
        //        Time.timeScale = 0f;
        //        UIManager.Instance.ActiveVictoryPanel(true);
        //    }
        //}
    }
}
