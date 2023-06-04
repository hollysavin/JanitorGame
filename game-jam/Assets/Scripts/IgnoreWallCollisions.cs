using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreWallCollisions : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>()); ;
        }
    }
}
