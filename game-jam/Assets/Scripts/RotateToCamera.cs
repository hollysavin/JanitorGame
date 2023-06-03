using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToCamera : MonoBehaviour
{
    void Update()
    {
        this.transform.rotation = Camera.main.transform.rotation;
    }
}
