using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmLighting : MonoBehaviour
{
    public Light light1;
    public Light light2;

    private int intensityLow= 1;
    private int intensityHigh = 5;

    private void Awake()
    {
        GameManager.OnGameStateChange += GameManagerOnGameStateChanged;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChange -= GameManagerOnGameStateChanged;
    }

    private void GameManagerOnGameStateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.IntensityLow:
                StartCoroutine("ShineLight");
                break;
            case GameState.IntensityMedium:
                StartCoroutine("ShineLight");
                break;
            case GameState.IntensityHigh:
                StartCoroutine("ShineLight");
                break;
            case GameState.End:
                //Stop
                break;
        }
    }

    IEnumerator ShineLight()
    {
        light1.intensity = intensityHigh;
        light2.intensity = intensityHigh;
        yield return new WaitForSeconds(2.5f);
        light1.intensity = intensityLow;
        light2.intensity = intensityLow;
    }
}
