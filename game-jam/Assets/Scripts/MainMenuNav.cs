using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuNav : MonoBehaviour
{
    public Button onePlayerBtn;

    void Start()
    {
        Button btn = onePlayerBtn.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        SceneManager.LoadScene("GameLevel");
    }
}