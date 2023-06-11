using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deadzone : MonoBehaviour
{
    [SerializeField] private GameObject spawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Item")
        {
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "Player")
        {
            other.gameObject.transform.position = spawnPoint.transform.position;
        }
    }
}
