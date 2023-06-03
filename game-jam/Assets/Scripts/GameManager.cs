using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;

    public GameState state;

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
                HandleGameCountDown();
                break;
            case GameState.IntensityLow:
                HandleIntensityLow();
                break;
            case GameState.IntensityMedium:
                HandleIntensityMedium();
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


    private void HandleGameCountDown()
    {
        StartCoroutine("Countdown");
    }
    private void HandleIntensityLow()
    {

    }
    private void HandleIntensityMedium()
    {
        throw new NotImplementedException();
    }
    private void HandleIntensityHigh()
    {
        throw new NotImplementedException();
    }
    private void HandleGameEnd()
    {
        throw new NotImplementedException();
    }

    IEnumerator Countdown()
    {
        yield return new WaitForSeconds(3);
        UpdateGameState(GameState.IntensityLow);
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
