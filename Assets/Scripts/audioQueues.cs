using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioQueues : MonoBehaviour
{

    SanityController sanity;
    Timer timer;
    BearController bear;
    LightControiller light;

    public Transform player;

    private Vector3 playerPos;

    public AudioSource[] audioSources;

    public AudioClip[] audioClips;
    
    // Start is called before the first frame update
    void Start()
    {
        sanity = GetComponent<SanityController>();
        timer = GetComponent<Timer>();
        bear = GetComponent<BearController>();
        light = GetComponent<LightControiller>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerPos = player.transform.position;
        audioSources[0] = GetComponent<AudioSource>();
        audioSources[1] = GetComponent<AudioSource>();
        audioSources[2] = GetComponent<AudioSource>();
        audioSources[3] = GetComponent<AudioSource>();
        audioSources[4] = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        //Debug.Log(bear.currentState);
        //check if bear has spawned
        if (bear.currentState == BearState.BearSpawn)
        {
            //audioSources[0] = GetComponent<AudioSource>();
            audioSources[0].clip = audioClips[4];
            Debug.Log("Audio Clip: " + audioClips[4] + " Playing");
            audioSources[0].Play();
            audioSources[0].Stop();
        }

        //check if bear is stalking player
        if (bear.currentState == BearState.BearStalk)
        {
            //audioSources[3] = GetComponent<AudioSource>();
            audioSources[3].clip = audioClips[3];
            Debug.Log("Audio Clip: " + audioClips[3] + " Playing");
            audioSources[3].Play();
            audioSources[3].Stop();
        }

        //check if bear is near player
        if (bear.currentState == BearState.BearHunt)
        {
            //audioSources[3] = GetComponent<AudioSource>();
            audioSources[3].clip = audioClips[2];
            Debug.Log("Audio Clip: " + audioClips[2] + " Playing");
            audioSources[3].Play();
            audioSources[3].Stop();
        }

        //check if bear is chasing player
        if (bear.currentState == BearState.BearChase)
        {
            //audioSources[3] = GetComponent<AudioSource>();
            audioSources[3].clip = audioClips[1];
            Debug.Log("Audio Clip: " + audioClips[1] + " Playing");
            audioSources[3].Play();
            audioSources[3].Stop();
        }

        //check if player has moved
        if (player.transform.position != playerPos)
        {
            //audioSources[2] = GetComponent<AudioSource>();
            audioSources[2].clip = audioClips[7];
            Debug.Log("Audio Clip: " + audioClips[7] + " Playing");
            audioSources[2].Play();
            audioSources[2].Stop();
        }

        //check if player is holding light
        if (light.isTouching == true)
        {
            //audioSources[2] = GetComponent<AudioSource>();
            audioSources[2].clip = audioClips[6];
            audioSources[4].clip = audioClips[6];
            Debug.Log("Audio Clip: " + audioClips[6] + " Playing");
            audioSources[2].Play();
            audioSources[4].Play();
            audioSources[2].Stop();
            audioSources[4].Stop();
        }

        if (bear.currentState == BearState.BearHunt)
        {
            //audioSources[1] = GetComponent<AudioSource>();
            audioSources[1].clip = audioClips[5];
            Debug.Log("Audio Clip: " + audioClips[5] + " Playing");
            audioSources[1].Play();
            audioSources[1].Stop();
        }


    }
}
