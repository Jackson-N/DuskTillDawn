using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Audio;

public class SceneTransition : MonoBehaviour
{
    //public FadeScreen fadeScreen;
    public TextMeshPro textMesh;
    public Collision collision;

    public bool isTouchingPlay = false;
    public bool isTouchingQuit = false;

    public void Start()
    {
        textMesh = GetComponent<TextMeshPro>();
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
        if (isTouchingPlay == true && isTouchingQuit == false)
        {
            StartCoroutine(GoToSceneAsyncRoutine(0));
        }
        //GoToSceneAsync(0);
        else if (isTouchingQuit == true && isTouchingPlay == false)
        {
            StartCoroutine(GoToSceneAsyncRoutine(4));
        }
        //GoToSceneAsync(4);
    }
}
