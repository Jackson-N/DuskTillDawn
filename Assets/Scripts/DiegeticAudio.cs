using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiegeticAudio : MonoBehaviour
{

    SanityController sanity;
    public AudioSource audioSource;

    public AudioClip[] audioClips;

    // Start is called before the first frame update
    void Start()
    {
        sanity = GetComponent<SanityController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void playSpawnQueue()
    {
        audioSource.clip = audioClips[3];
        audioSource.Play();
        audioSource.Stop();
    }

    public void playBearhunt()
    {
        audioSource.clip = audioClips[2];
        audioSource.Play();
        audioSource.Stop();
    }

    public void playBearChase()
    {
        audioSource.clip = audioClips[1];
        audioSource.Play();
        audioSource.Stop();
    }

    public void playPaninc()
    {
        audioSource.clip = audioClips[0];
        audioSource.Play();
        audioSource.Stop();
    }
}
