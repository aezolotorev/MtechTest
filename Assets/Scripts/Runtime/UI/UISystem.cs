using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements.Experimental;
using Zenject;
using UniRx;
using System;
using UnityEngine.UI;
using CustomEventBus.Signals;

public class UISystem : MonoBehaviour, IDisposable
{
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject levelEndPanel;

    [SerializeField] private GameObject currentPanel;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI lifeText;

    [SerializeField] private Button pauseButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button gameOverRestartButton;
    [SerializeField] private Button nextLevelButton;
    [SerializeField] private Button continueButton;


    private GameManager gameManager;
    private CustomEventBus.EventBus eventBus;

    private CompositeDisposable compositeDisposable = new CompositeDisposable();

    [Inject]
    public void Construct(CustomEventBus.EventBus eventBus, GameManager gameManager)
    {
        this.eventBus = eventBus;
        this.gameManager = gameManager;
        gameManager.score.Subscribe(x => scoreText.text = "Score: " +  x.ToString()).AddTo(compositeDisposable);
        eventBus.Subscribe<OnLevelEndEvent>(LevelEnd);
        eventBus.Subscribe<OnPlayerDamagedEvent>(PlayerDamaged);
        eventBus.Subscribe<OnPlayerDeadEvent>(PlayerDead);
    }

    private void PlayerDamaged(OnPlayerDamagedEvent ctx)
    {
        lifeText.text = "Life: " +  ctx.Health.ToString();
    }

    private void PlayerDead(OnPlayerDeadEvent ctx)
    {
        ActivatePanel(gameOverPanel);
    }

    private void LevelEnd(OnLevelEndEvent ctx)
    {
        ActivatePanel(levelEndPanel);
    }

    private void Start()
    {
        levelText.text = "Level: " + (gameManager.Level+1).ToString();
        InitPanels();
        InitButtons();       
    }

    private void InitButtons(){
        pauseButton.onClick.AddListener(PauseGame);
        restartButton.onClick.AddListener(RestartGame);
        gameOverRestartButton.onClick.AddListener(RestartGame);
        nextLevelButton.onClick.AddListener(NextLevel);
        continueButton.onClick.AddListener(ContinueGame);
    }

    private void InitPanels(){                
        ActivatePanel(mainPanel);        
    }

    private void PauseGame(){
        ActivatePanel(pausePanel);
        Time.timeScale = 0f;
    }

    private void RestartGame(){        
        Time.timeScale = 1f;
        gameManager.Restart();
    }

    private void NextLevel()
    { 
        gameManager.NextLevel();
        Time.timeScale = 1f;
    }

    private void ContinueGame(){
        ActivatePanel(mainPanel);
        Time.timeScale = 1f;
    }

    private void ActivatePanel(GameObject panel){
        if(currentPanel != null)
        currentPanel.SetActive(false);
        panel.SetActive(true);
        currentPanel = panel;
    }
    public void OnDestroy()
    {

        eventBus.Unsubscribe<OnLevelEndEvent>(LevelEnd);
        eventBus.Unsubscribe<OnPlayerDamagedEvent>(PlayerDamaged);
        eventBus.Unsubscribe<OnPlayerDeadEvent>(PlayerDead);

    }

    public void Dispose()
    {
        compositeDisposable.Clear();

    }
   
}
