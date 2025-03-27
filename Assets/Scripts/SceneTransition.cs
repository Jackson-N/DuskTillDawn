using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SceneTransition : MonoBehaviour
{
    //public FadeScreen fadeScreen;
    public TextMeshPro textMesh;
    public Collision collision;
    public GameObject playGameTape;
    public GameObject quitGameTape;
    private GameObject self;
    public bool isHovering;
    public bool isTouchingPlay;
    public bool isTouchingQuit;

    void Start()
    {
        self = this.gameObject;
    }

    public void Update()
    {
        checkButton();
    }

    public void GoToSceneAsync(int sceneIndex)
    {
        StartCoroutine(GoToSceneAsyncRoutine(sceneIndex));
    }

    IEnumerator GoToSceneAsyncRoutine(int sceneIndex)
    {
        //fadeScreen.FadeOut();
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);
        asyncLoad.allowSceneActivation = false;
        textMesh.text = "Loading...";

        float timer = 0.0f;
        while (timer <= 7.0f && !asyncLoad.isDone)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        //Lowpass();

        asyncLoad.allowSceneActivation = true;
    }

    //check for if the object is touching either the play or quit button
    private void checkButton()
    {
        //check if the player is still holding the tape. if the player lets go of the tape, then run the code
        
        //check for the tape that is touching self
        if(!isHovering)
        {
            if (playGameTape.transform.position == self.transform.position)
            
            {
                StartCoroutine(GoToSceneAsyncRoutine(3));
            }
            //GoToSceneAsync(0);
            else if (quitGameTape.transform.position == self.transform.position)
            {
                //quit game

                Application.Quit();
                UnityEditor.EditorApplication.isPlaying = false;

            }
        }
        //GoToSceneAsync(4);
    }
}
