using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Audio;

public class GameSceneChanger : MonoBehaviour
{

    public Transform anchorPoint;

    public SceneTransition sceneTransition;

    public AudioMixerSnapshot menuSnapshot;
    public AudioMixerSnapshot loadingSnapshot;

    private GameObject vhsTape;
    //public int sceneIndex;

    //public static bool isTouching = false;

    // Start is called before the first frame update
    void Start()
    {
        //sceneTransition = GameObject.FindGameObjectWithTag("VHS").GetComponent<SceneTransition>();
        Highpass();
    }

    public void GameScene()
    {
        sceneTransition.isTouchingPlay = true;
        sceneTransition.isTouchingQuit = false;
    }

    public void QuitGame()
    {
        sceneTransition.isTouchingQuit = true;
        sceneTransition.isTouchingPlay = false;
        //quit game, and quit in editor
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
        
    }

    public void Lowpass()
    {
        loadingSnapshot.TransitionTo(0.5f);
    }

    public void Highpass()
    {
        menuSnapshot.TransitionTo(0.5f);
    }
}
