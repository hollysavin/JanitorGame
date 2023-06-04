using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public AudioSource MusicTrackSource;
    public AudioSource AlarmSource;

    public AudioClip MusicTrackClip1;
    public AudioClip MusicTrackClip2;
    public AudioClip MusicTrackClip3;
    public AudioClip AlarmClip;



    public static GameManager _instance;

    public GameState state;

    const int COUNTDOWN = 6, INTENSITY_DURATION = 20;

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
    }

    IEnumerator HandleCountDown()
    {
        
        yield return new WaitForSeconds(COUNTDOWN);
        UpdateGameState(GameState.IntensityLow);
    }

    IEnumerator HandleIntensityLow()
    {

        MusicTrackSource.PlayOneShot(MusicTrackClip1);
        yield return new WaitForSeconds(INTENSITY_DURATION);
        UpdateGameState(GameState.IntensityMedium);
    }
    IEnumerator HandleIntensityMedium()
    {
        AlarmSource.PlayOneShot(AlarmClip);
        MusicTrackSource.Stop();
        MusicTrackSource.PlayOneShot(MusicTrackClip2);
        yield return new WaitForSeconds(INTENSITY_DURATION);
        UpdateGameState(GameState.IntensityHigh);
    }

    private void HandleIntensityHigh()
    {
        AlarmSource.PlayOneShot(AlarmClip);
        MusicTrackSource.Stop();
        MusicTrackSource.PlayOneShot(MusicTrackClip3);
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
