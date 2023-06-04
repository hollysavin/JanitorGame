using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    public int countdownTime;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI instructionText;
    public AudioSource audioSource;
    public AudioClip coundownAudio;

private void Start()
    {
        audioSource.PlayOneShot(coundownAudio);
        StartCoroutine(CountdownToStart());
    }

    IEnumerator CountdownToStart()
    {
        while(countdownTime > 0)
        {
            timerText.text = countdownTime.ToString();

            yield return new WaitForSeconds(2f);

            countdownTime--;
        }

        timerText.text = "GO!";

        //GameManager.BeginGame();

        yield return new WaitForSeconds(3f);
        instructionText.gameObject.SetActive(false);
        timerText.gameObject.SetActive(false);
    }
}
