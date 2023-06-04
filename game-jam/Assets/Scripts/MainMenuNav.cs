using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuNav : MonoBehaviour
{
    public Button startBtn, quitBtn;

    public AudioSource MenuClickSource;
    public AudioClip MenuClickClip;
    public AudioSource MenuBackSource;
    public AudioClip MenuBackClip;
    public AudioSource MenuMusicSource;
    public AudioClip MenuMusicClip;

    void Start()
    {
        MenuMusicSource.PlayOneShot(MenuMusicClip);
        startBtn.onClick.AddListener(StartOnClick);
        quitBtn.onClick.AddListener(QuitOnClick);
    }

    void StartOnClick()
    {
        MenuClickSource.PlayOneShot(MenuClickClip);
        MenuMusicSource.Stop();
        SceneManager.LoadScene("GameLevel");
        
    }

    void QuitOnClick()
    {
        MenuBackSource.PlayOneShot(MenuBackClip);
        //quit
        Debug.Log("quit game");
    }
}