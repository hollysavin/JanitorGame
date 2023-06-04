using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screamzone : MonoBehaviour
{
    public AudioSource ScreamSource;
    public AudioClip ScreamAudio;


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
        
            ScreamSource.PlayOneShot(ScreamAudio);
            
        }
    }
}
