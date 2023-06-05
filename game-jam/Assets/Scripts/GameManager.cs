using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI timerText;

    public static GameManager instance;
    public GameState state;

    const int COUNTDOWN = 6, INTENSITY_DURATION = 20;
    private bool gameStarted = false;

    public object HandleGameover { get; private set; }

    public static event Action<GameState> OnGameStateChange;
    private void Awake()
    {
        if(instance == null )
        instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        UpdateGameState(GameState.CountDown);
    }

    private void Update()
    {
        CheckForWinner();
    }

    public void UpdateGameState(GameState newState)
    {
        state = newState;

        switch (newState)
        {
            case GameState.AddPlayer:
                break;
            case GameState.CountDown:
                StartCoroutine("HandleCountDown");
                break;
            case GameState.IntensityLow:
                StartCoroutine("HandleIntensityLow");
                break;
            case GameState.IntensityMedium:
                StartCoroutine("HandleIntensityMedium");
                break;
            case GameState.IntensityHigh:
                break;
            case GameState.End:
                StartCoroutine("HandleGameEnd");
                break;
        }
        OnGameStateChange?.Invoke(newState);
    }
    IEnumerator HandleCountDown()
    {
        yield return new WaitForSeconds(COUNTDOWN);
        gameStarted = true;
        UpdateGameState(GameState.IntensityLow);
    }

    IEnumerator HandleIntensityLow()
    {
        yield return new WaitForSeconds(INTENSITY_DURATION);
        UpdateGameState(GameState.IntensityMedium);
    }

    IEnumerator HandleIntensityMedium()
    {
        yield return new WaitForSeconds(INTENSITY_DURATION);
        UpdateGameState(GameState.IntensityHigh);
    }
    IEnumerator HandleGameEnd()
    {
        gameStarted = false;
        timerText.gameObject.SetActive(true);
        timerText.text = "You win!";
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Start");
    }
    private void CheckForWinner()
    {
        int currentPlayerCount = GameObject.FindGameObjectsWithTag("Player").Length;
        if (currentPlayerCount == 1 && gameStarted)
        {
            UpdateGameState(GameState.End);
        }
    }
}

public enum GameState
{
    AddPlayer,
    CountDown,
    IntensityLow,
    IntensityMedium,
    IntensityHigh,
    End
}
