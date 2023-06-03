using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    public int countdownTime;
    public TextMeshProUGUI timerText;

private void Start()
    {
        StartCoroutine(CountdownToStart());
    }

    IEnumerator CountdownToStart()
    {
        Debug.Log("started Countdown");
        while(countdownTime > 0)
        {
            timerText.text = countdownTime.ToString();

            yield return new WaitForSeconds(1f);

            countdownTime--;
            Debug.Log(countdownTime);
        }

        timerText.text = "GO!";

        //GameManager.BeginGame();

        yield return new WaitForSeconds(1f);

        timerText.gameObject.SetActive(false);
    }
}
