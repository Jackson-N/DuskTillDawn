using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearAudioQueues : MonoBehaviour
{

    BearController bear;
    public AudioSource audioSource;

    public AudioClip audioClip;

    // Start is called before the first frame update
    void Start()
    {
        bear = GetComponent<BearController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playBearRoar()
    {
        audioSource.clip = audioClip;
        audioSource.Play();
        audioSource.Stop();
    }

}
