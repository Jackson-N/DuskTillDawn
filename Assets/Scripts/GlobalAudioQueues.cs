using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalAudioQueues : MonoBehaviour
{

    
    public AudioSource audioSource;

    public AudioClip audioClip;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playSpawnDrone()
    {
        audioSource.clip = audioClip;
        audioSource.Play();
        audioSource.Stop();
    }
    

}
