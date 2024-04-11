using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneTransition : MonoBehaviour
{
    //public FadeScreen fadeScreen;
    public TextMeshPro textMesh;

    public void Start()
    {
        textMesh = GetComponent<TextMeshPro>();
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
        while (timer <= 8.0f && !asyncLoad.isDone)
        {
            timer += Time.deltaTime;
            if ((timer == 0.0f) || (timer == 4.0f))
            {
                textMesh.text = "Loading";
            }
            else if ((timer == 1.0f) || (timer == 5.0f))
            {
                textMesh.text = "Loading.";
            }
            else if ((timer == 2.0f) || (timer == 6.0f))
            {
                textMesh.text = "Loading..";
            }
            else if ((timer == 3.0f) || (timer == 7.0f))
            {
                textMesh.text = "Loading...";
            }
            yield return null;
        }

        asyncLoad.allowSceneActivation = true;
    }
}
