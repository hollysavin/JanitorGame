using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    public int countdownTime;
    public TextMeshProUGUI timerText;

    public AudioSource audioSource;
    public AudioClip coundownAudio;

private void Start()
    {
        audioSource.PlayOneShot(coundownAudio);
        StartCoroutine(CountdownToStart());
    }

    IEnumerator CountdownToStart()
    {
        Debug.Log("started Countdown");
        while(countdownTime > 0)
        {
            timerText.text = countdownTime.ToString();

            yield return new WaitForSeconds(2f);

            countdownTime--;
            Debug.Log(countdownTime);
        }

        timerText.text = "GO!";

        //GameManager.BeginGame();

        yield return new WaitForSeconds(2f);

        timerText.gameObject.SetActive(false);
    }
}
