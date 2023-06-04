using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> prefabList;
    private bool isRunning = false;
    //Controls the spawn rate of items in seconds
    //LOW, MEDIUM, HIGH intensity levels for each state
    private int spawnRate;
    private const int LOW = 3, MEDIUM = 2, HIGH = 1;

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
                if (isRunning == false)
                {
                    spawnRate = LOW;
                    StartCoroutine("SpawnItem");
                }
                break;
            case GameState.IntensityMedium:
                spawnRate = MEDIUM;
                break;
            case GameState.IntensityHigh:
                spawnRate = HIGH;
                break;
            case GameState.End:
                isRunning = false;
                break;
        }
    }

    IEnumerator SpawnItem()
    {
        isRunning = true;
        while (isRunning)
        {
            GameObject randomObj = prefabList[Random.Range(0, (prefabList.Count))];
            Instantiate(randomObj, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(spawnRate);
        }
    }

}
