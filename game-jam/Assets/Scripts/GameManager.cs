using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public AudioSource MusicTrackSource;
    public AudioSource AlarmSource;

    public AudioClip MusicTrackClip1;
    public AudioClip MusicTrackClip2;
    public AudioClip MusicTrackClip3;
    public AudioClip AlarmClip;

    public TextMeshProUGUI timerText;

    public static GameManager _instance;

    public GameState state;

    const int COUNTDOWN = 6, INTENSITY_DURATION = 20;
    private int currentPlayerCount = 0;
    private bool gameStarted = false;

    public object HandleGameover { get; private set; }

    public static event Action<GameState> OnGameStateChange;
    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        UpdateGameState(GameState.CountDown);
    }

    private void Update()
    {
        currentPlayerCount = GameObject.FindGameObjectsWithTag("Player").Length;
        CheckForWinner();
    }

    public void UpdateGameState(GameState newState)
    {
        state = newState;

        switch (newState)
        {
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
                HandleIntensityHigh();
                break;
            case GameState.End:
                HandleGameEnd();
                break;
        }
        OnGameStateChange?.Invoke(newState);
    }

    private void HandleGameEnd()
    {
        gameStarted = false;
        MusicTrackSource.Stop();
        timerText.gameObject.SetActive(true);
        timerText.text = "You win!";
        StartCoroutine("ExitToMenu");
    }

    IEnumerator HandleCountDown()
    {
        yield return new WaitForSeconds(COUNTDOWN);
        gameStarted = true;
        if (currentPlayerCount != 1)
        {
            UpdateGameState(GameState.IntensityLow);
        }
    }

    IEnumerator HandleIntensityLow()
    {
        MusicTrackSource.PlayOneShot(MusicTrackClip1);
        yield return new WaitForSeconds(INTENSITY_DURATION);
        if (currentPlayerCount != 1)
        {
            UpdateGameState(GameState.IntensityMedium);
        }
    }
    IEnumerator HandleIntensityMedium()
    {
        AlarmSource.PlayOneShot(AlarmClip);
        MusicTrackSource.Stop();
        MusicTrackSource.PlayOneShot(MusicTrackClip2);
        yield return new WaitForSeconds(INTENSITY_DURATION);
        if (currentPlayerCount != 1)
        {
            UpdateGameState(GameState.IntensityHigh);
        }
    }

    IEnumerator ExitToMenu()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Start");
    }

    private void HandleIntensityHigh()
    {
        AlarmSource.PlayOneShot(AlarmClip);
        MusicTrackSource.Stop();
        MusicTrackSource.PlayOneShot(MusicTrackClip3);
    }

    private void CheckForWinner()
    {
        if (currentPlayerCount == 1 && gameStarted)
        {
            UpdateGameState(GameState.End);
        }
    }
}

public enum GameState
{
    CountDown,
    IntensityLow,
    IntensityMedium,
    IntensityHigh,
    End
}
