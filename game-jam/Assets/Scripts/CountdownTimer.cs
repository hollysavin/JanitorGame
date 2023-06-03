using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    public int countdownTime;
    public Text coundownDisplay;

    IEnumerator CountdownToStart()
    {
        while(countdownTime > 0)
        {
            coundownDisplay.text = countdownTime.ToString();

            yield return new WaitForSeconds(1f);

            countdownTime--;
        }
    }
}
