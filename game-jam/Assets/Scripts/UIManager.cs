using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI timerText;
    [SerializeField]
    private TextMeshProUGUI instructionText;
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
            case GameState.CountDown:
                StartCoroutine(CountdownToStart());
                break;
            case GameState.IntensityLow:
                break;
            case GameState.End:
                break;
        }
    }

    IEnumerator CountdownToStart()
    {
        int countdownTime = 3;

        while (countdownTime > 0)
        {
            timerText.text = countdownTime.ToString();

            yield return new WaitForSeconds(2f);

            countdownTime--;
        }
        timerText.text = "SURVIVE!";

        yield return new WaitForSeconds(3f);
        instructionText.gameObject.SetActive(false);
        timerText.gameObject.SetActive(false);
    }

}
