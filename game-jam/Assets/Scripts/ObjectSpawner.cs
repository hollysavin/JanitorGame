using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> prefabList;

    private void Start()
    {
        StartCoroutine("DoCheck");
    }

    IEnumerator DoCheck()
    {
        while (true)
        {
            GameObject randomObj = prefabList[Random.Range(0, (prefabList.Count))];
            Instantiate(randomObj, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(1);
        }
    }

}
