using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{

    public Transform anchorPoint;

    SceneTransition sceneTransition;

    private GameObject vhsTape;
    int sceneIndex = 0;

    private bool isTouching = false;

    // Start is called before the first frame update
    void Start()
    {
        anchorPoint = GameObject.FindGameObjectWithTag("Anchor").transform;
        sceneTransition = GameObject.FindGameObjectWithTag("TransitionManager").GetComponent<SceneTransition>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isTouching)
        {
            sceneTransition.GoToSceneAsync(sceneIndex);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Anchor"))
        {
            isTouching = true;
        }
        else isTouching = false;
    }
}
