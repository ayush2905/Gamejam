using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    #region Singleton
    public static LevelManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion

    #region parameters
    [SerializeField] LoadingPanel loadingpanel;
    [SerializeField] GameObject titlepanel;
    [SerializeField] GameObject pausepanel;
    private string _curentLevelName;

    public GameState currentGameState = GameState.PREGAME;
    #endregion

    #region SceneLoading
    public void LoadLevel(string levelName)
    {
        _curentLevelName = levelName;

        AsyncOperation ao = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Single);
        ao.completed += OnLoadOperationComplete;
    }

    private void OnLoadOperationComplete(AsyncOperation obj)
    {
        Debug.Log("Loading " + _curentLevelName + "completed");
        loadingpanel.DoOutAnimation();
    }

    public void StartGame()
    {
        LoadLevel("LevelFinal");

        UpdateState(GameState.RUNNING);
    }
    public void Quit()
    {
        Application.Quit();
    }

    public void StartCutscene()
    {
        loadingpanel.DoInAnimation("Cutscene");

        UpdateState(GameState.POSTGAME);
    }
    #endregion

    #region button functions
    public void StartIntro()
    {
        titlepanel.SetActive(false);

        loadingpanel.DoInAnimation("Intro");
    }

    public void Restart()
    {
        loadingpanel.DoInAnimation(SceneManager.GetActiveScene().name);

        UpdateState(GameState.RUNNING);
    }

    public void GoHome()
    {
        titlepanel.SetActive(true);
        loadingpanel.DoInAnimation("Boot");
        UpdateState(GameState.PREGAME);
    }
    #endregion

    private void Update()
    {
        if (currentGameState == GameState.PREGAME || currentGameState == GameState.POSTGAME)
            return;

        if (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    private void UpdateState(GameState state)
    {
        GameState previousGameState = currentGameState;
        currentGameState = state;

        switch (currentGameState)
        {
            case GameState.PREGAME:
                Time.timeScale = 1.0f;
                pausepanel.SetActive(false);
                break;

            case GameState.RUNNING:
                Time.timeScale = 1.0f;
                pausepanel.SetActive(false);
                break;

            case GameState.PAUSED:
                Time.timeScale = 0.0f;
                pausepanel.SetActive(true);
                break;

            case GameState.POSTGAME:
                Time.timeScale = 1.0f;
                pausepanel.SetActive(false);
                break;

            default:
                break;
        }
    }

        public void TogglePause()
    {
        if (currentGameState == GameState.RUNNING)
        {
            UpdateState(GameState.PAUSED);
        }
        else if (currentGameState == GameState.PAUSED)
        {
            UpdateState(GameState.RUNNING);
        }
    }

    public enum GameState
    {
        PREGAME, RUNNING, PAUSED, POSTGAME
    }

}
