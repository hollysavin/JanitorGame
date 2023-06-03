using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> prefabList;
    private bool isRunning = false;

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
        if(state == GameState.IntensityLow && isRunning == false)
        {
            StartCoroutine("SpawnItem");
            isRunning = true;
        }
    }

    IEnumerator SpawnItem()
    {
        while (true)
        {
            GameObject randomObj = prefabList[Random.Range(0, (prefabList.Count))];
            Instantiate(randomObj, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(2);
        }
    }

}
