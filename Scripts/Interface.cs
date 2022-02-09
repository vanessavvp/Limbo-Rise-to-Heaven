using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interface : MonoBehaviour
{
    private AudioSource audioSource;
    public float delayTime = 3f;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayDelayed(delayTime);
    }
}
