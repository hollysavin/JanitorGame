using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;

    public GameState state;

    const int COUNTDOWN = 6, INTENSITY_DURATION = 6;

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
                Debug.Log("LOW");
                StartCoroutine("HandleIntensityLow");
                break;
            case GameState.IntensityMedium:
                Debug.Log("MEDIUM");
                StartCoroutine("HandleIntensityMedium");
                break;
            case GameState.IntensityHigh:
                Debug.Log("HIGH");
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
        yield return new WaitForSeconds(INTENSITY_DURATION);
        UpdateGameState(GameState.IntensityMedium);
    }
    IEnumerator HandleIntensityMedium()
    {
        yield return new WaitForSeconds(INTENSITY_DURATION);
        UpdateGameState(GameState.IntensityHigh);
    }

    private void HandleIntensityHigh()
    {
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
