using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrushSweepItem : MonoBehaviour
{
    [SerializeField] float sweepForce = 200;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Item"))
        {
            collision.collider.GetComponent<Rigidbody>().AddForce(0, 0, sweepForce);
        }
    }
}
