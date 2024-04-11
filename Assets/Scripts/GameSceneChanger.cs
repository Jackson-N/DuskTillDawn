using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSceneChanger : MonoBehaviour
{

    public Transform anchorPoint;

    public SceneTransition sceneTransition;

    private GameObject vhsTape;
    int sceneIndex = 0;

    public static bool isTouching = false;

    // Start is called before the first frame update
    void Start()
    {
        anchorPoint = GameObject.FindGameObjectWithTag("Anchor").transform;
        sceneTransition = GameObject.FindGameObjectWithTag("Anchor").GetComponent<SceneTransition>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isTouching)
        {
            Debug.Log("Touching Anchor, changing scene...");
            ChangeScene();
            Debug.Log("Scene Changed");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Anchor"))
        {
            isTouching = true;
        }
        else isTouching = false;
        Debug.Log(isTouching);
    }

    public bool ChangeScene()
    {
        bool isChanging = false;
        Debug.Log("Changing Scene...");
        sceneTransition.GoToSceneAsync(0);
        isChanging = true;
        return isChanging;
    }
}
