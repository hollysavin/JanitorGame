using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitDissapear : MonoBehaviour
{
    void Awake()
    {
        StartCoroutine("DoWaitDissapear");
    }

    IEnumerator DoWaitDissapear()
    {
        yield return new WaitForSeconds(4);
        Destroy(gameObject);
    }

}