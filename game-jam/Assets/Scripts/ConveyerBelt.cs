using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyerBelt : MonoBehaviour
{
    private float speed;
    [SerializeField]
    private Vector3 direction;

    private Rigidbody body;
    private bool isRunning;
    //Controls the speed of the conveyer belt
    //LOW, MEDIUM, HIGH intensity levels for each state
    private const float LOW = 1, MEDIUM = 2, HIGH = 2.5f;

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
                speed = LOW;
                if (isRunning == false)
                {
                    isRunning = true;
                }
                break;
            case GameState.IntensityMedium:
                speed = MEDIUM;
                direction *= -1;
                break;
            case GameState.IntensityHigh:
                speed = HIGH;
                direction *= -1;
                break;
            case GameState.End:
                isRunning = false;
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (isRunning)
        {
            Vector3 pos = body.position;
            body.position += direction * speed * Time.deltaTime;
            body.MovePosition(pos);
        }
    }

}
