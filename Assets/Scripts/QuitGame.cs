using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public Transform anchorPoint;

    private GameObject vhsTape;

    private bool isTouching = false;

    void Start()
    {
        anchorPoint = GameObject.FindGameObjectWithTag("Anchor").transform;
    }

    void Update()
    {
        if(isTouching == true)
        {
            Debug.Log("Touching Anchor, Quitting game...");
            Quit();
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

    public void Quit()
    {
        StartCoroutine(QuitProgram());
        //return true;
    }

    IEnumerator QuitProgram()
    {
        yield return new WaitForSeconds(5);
        Application.Quit();
    }


}