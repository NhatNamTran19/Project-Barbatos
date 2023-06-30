using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : BaseManager<UIManager>
{
    [SerializeField]
    private MenuPanel menuPanel;
    public MenuPanel MenuPanel => menuPanel;

    [SerializeField]
    private GamePanel gamePanel;
    public GamePanel GamePanel => gamePanel;

    [SerializeField]
    private SettingPanel settingPanel;
    public SettingPanel SettingPanel => settingPanel;

    [SerializeField]
    private LosePanel losePanel;
    public LosePanel LosePanel => losePanel;

    [SerializeField]
    private VictoryPanel victoryPanel;
    public VictoryPanel VictoryPanel => victoryPanel;

    [SerializeField]
    private PausePanel pausePanel;
    public PausePanel PausePanel => pausePanel;

    [SerializeField]
    private LoadingPanel loadingPanel;
    public LoadingPanel LoadingPanel => loadingPanel;

    void Start()
    {
        ActiveMenuPanel(true);
        ActiveGamePanel(false);
        ActiveSettingPanel(false);
        ActiveLosePanel(false);
        ActiveVictoryPanel(false);
        ActivePausePanel(false);
        ActiveLoadingPanel(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if(GameManager.HasInstance && GameManager.Instance.IsPlaying)
            {
                //GameManager.Instance.PauseGame();
                ActivePausePanel(true);
            }
        }
         if (Input.GetKeyDown(KeyCode.W))
        {
            if(GameManager.HasInstance && GameManager.Instance.IsPlaying)
            {
                //GameManager.Instance.ResumeGame();
                ActivePausePanel(false);
            }
        }

        Debug.Log(GameManager.HasInstance);
        Debug.Log(UIManager.HasInstance);

    }

    public void ActiveMenuPanel(bool active)
    {
        menuPanel.gameObject.SetActive(active);
    }

    public void ActiveGamePanel(bool active)
    {
        gamePanel.gameObject.SetActive(active);
    }

    public void ActiveSettingPanel(bool active)
    {
        settingPanel.gameObject.SetActive(active);
    }

    public void ActiveLosePanel(bool active)
    {
        losePanel.gameObject.SetActive(active);
    }

    public void ActiveVictoryPanel(bool active)
    {
        victoryPanel.gameObject.SetActive(active);
    }

    public void ActivePausePanel(bool active)
    {
        pausePanel.gameObject.SetActive(active);
    }

    public void ActiveLoadingPanel(bool active)
    {
        loadingPanel.gameObject.SetActive(active);
    }
}
