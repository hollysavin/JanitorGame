using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
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
    private const float LOW = 1, MEDIUM = 1.2f, HIGH = 1.7f;

    [SerializeField]
    private Material[] materials;

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
                SetMaterial(materials[0], materials[1]);
                break;
            case GameState.IntensityMedium:
                speed = MEDIUM;
                direction *= -1;
                SetMaterial(materials[2], materials[3]);
                break;
            case GameState.IntensityHigh:
                speed = HIGH;
                direction *= -1;
                SetMaterial(materials[4], materials[5]);
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

    private void SetMaterial(Material forward, Material inverse)
    {
        Material[] tempMats = GetComponent<MeshRenderer>().materials;
        if (IsInverse())
        {
            tempMats[1] = inverse;
            GetComponent<MeshRenderer>().materials = tempMats;
        } else
        {
            tempMats[1] = forward;
            GetComponent<MeshRenderer>().materials = tempMats;
        }
    }

    private bool IsInverse()
    {
        return (direction.x == 1 || direction.z == -1);
    }

}
