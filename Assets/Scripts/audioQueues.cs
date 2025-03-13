/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioQueues : MonoBehaviour
{

    SanityController sanity;
    Timer timer;
    BearController bear;
    LightControiller light;

    [SerializeField] private Transform player;

    private Vector3 playerPos;

    public AudioSource audioSource;

    public AudioClip audioClip;
    
    // Start is called before the first frame update
    void Awake()
    {
        sanity = GetComponent<SanityController>();
        timer = GetComponent<Timer>();
        bear = GetComponent<BearController>();
        light = GetComponent<LightControiller>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerPos = player.transform.position;
        //audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerPos != player.transform.position)
        {
            playFootsteps();
            playerPos = player.transform.position;
        }
    }

    public void playFootsteps()
    {
        audioSource.clip = audioClip;
        audioSource.Play();
        audioSource.Stop();
    }
    
}
*/