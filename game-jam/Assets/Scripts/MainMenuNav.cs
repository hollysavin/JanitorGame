using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuNav : MonoBehaviour
{
    public Button startBtn, quitBtn;

    void Start()
    {
        startBtn.onClick.AddListener(StartOnClick);
        quitBtn.onClick.AddListener(QuitOnClick);
    }

    void StartOnClick()
    {
        SceneManager.LoadScene("GameLevel");
    }

    void QuitOnClick()
    {
        //quit
        Debug.Log("quit game");
    }
}