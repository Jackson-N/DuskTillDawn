using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapeRespawn : MonoBehaviour
{
    //public Trasform tapeRespawn;

    public GameObject respawnPoint;
    public GameObject tape;
    bool isTouching = false;

    private float tapePosX;
    private float tapePosY;
    private float tapePosZ;

    // Start is called before the first frame update
    /*
    void Start()
    {
        //tapeRespawn = respawnPoint.position;
        tape = GameObject.FindGameObjectWithTag("PlayButton");
        respawnPoint = GameObject.FindGameObjectWithTag("Respawn");
    }

    // Update is called once per frame

    /*
    void Update()
    {
        if (isTouching)
        {
            Debug.Log("Touching Tape, Respawn...");
            Respawn();
            Debug.Log("Tape Respawned");
        }
    }
    */

    private void OnCollisionEnter(Collision collision)
    {
        //check if tape is touching the floor
        if (collision.gameObject.CompareTag("Floor"))
        {
            isTouching = true;
            Debug.Log("Touching Tape...");
            Respawn();
        }
        else isTouching = false;
    }

    public void Respawn()
    {
        //move object to respawn point
        tape.transform.position = respawnPoint.transform.position;
        //Debug.Log("Tape Respawned");
    }

}
