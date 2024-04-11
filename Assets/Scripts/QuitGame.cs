using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public Transform qgTape;

    private GameObject vhsTape;

    private bool isTouching = false;

    void Start()
    {
        qgTape = GameObject.FindGameObjectWithTag("Component").transform;
    }

    /*
    void Update()
    {
        if(isTouching == true)
        {
            Debug.Log("Touching Anchor, Quitting game...");
            Quit();
            Debug.Log("Game Quit");
        }
    }
    */

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("QuitButton"))
        {
            Debug.Log("Touching Tape...");
            isTouching = true;
            Debug.Log(isTouching);
        }
        else isTouching = false;
    }

    public void Quit()
    {
        Debug.Log("Touching Tape, Quitting game...");
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
        Debug.Log("Game Quit");
    }


}