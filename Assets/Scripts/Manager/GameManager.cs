using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : BaseManager<GameManager>
{
    //private const string CherryKey = "Cherry";

    private int soul = 0;
    public int Soul => soul;

    public float maxHP = 50;
    private float hpPlayer;
    public float HpPlayer => hpPlayer;



    private bool isPlaying = false;
    public bool IsPlaying => isPlaying;

    protected override void Awake()
    {
        base.Awake();
        hpPlayer = maxHP;
        //cherries = PlayerPrefs.GetInt(CherryKey, 0);
    }

    public void UpdateSoul(int value)
    {
        soul = value;
    }
    public void UpdateHpPlayer(float value)
    {
        hpPlayer = value;
    }


    public void StartGame()
    {
        isPlaying = true;
        Time.timeScale = 1f;
    }

    public void PauseGame()
    {
        if (isPlaying)
        {
            isPlaying = false;
            Time.timeScale = 0f;
        }
    }

    public void ResumeGame()
    {
        if (!isPlaying)
        {
            isPlaying = true;
            Time.timeScale = 1f;
        }
    }

    public void RestartGame()
    {
        ChangeScene("StartScene");
        hpPlayer = maxHP;
        //SceneManager.LoadScene("StartScene");
        if (UIManager.HasInstance)
        {
            UIManager.Instance.ActiveMenuPanel(true);
            UIManager.Instance.ActiveGamePanel(false);
            UIManager.Instance.ActiveVictoryPanel(false);
            UIManager.Instance.ActiveLosePanel(false);
        }
        if (AudioManager.HasInstance && UIManager.HasInstance)
        {
            AudioManager.Instance.PlayBGM(AUDIO.BGM_BGM_01);
        }
    }

    public void EndGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    //    private void OnApplicationQuit()
    //    {
    //        PlayerPrefs.SetInt(CherryKey, cherries);
    //    }
}