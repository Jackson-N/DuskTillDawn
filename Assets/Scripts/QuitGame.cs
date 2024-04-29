using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{

    
    void Update()
    {
        Debug.Log("Touching Anchor, Quitting game...");
        Quit();
        Debug.Log("Game Quit");
    }

    public void Quit()
    {
        
        //OnCollisionEnter(collision);
        Debug.Log("Touching Tape, Quitting game...");
        UnityEditor.EditorApplication.isPlaying = false;
        Debug.Log("Game Quit");
        Application.Quit();
        
    }


}