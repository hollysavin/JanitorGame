using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrushSweepItem : MonoBehaviour
{
    [SerializeField] float sweepForce = 200;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("brush hit");
        if (collision.collider.CompareTag("Item"))
        {
            Debug.Log("brush hit item");
            collision.collider.GetComponent<Rigidbody>().AddForce(0, 0, sweepForce);
        }
    }
}
