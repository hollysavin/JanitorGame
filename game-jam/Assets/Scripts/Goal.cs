using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private string playerName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Item")
        {
            Item itemScript = other.gameObject.GetComponent<Item>();
            if (itemScript != null)
            {
                if(itemScript.HasOwner()) Debug.Log("GOAL " + itemScript.GetItemOwner());
            }
            
        }
    }
}
