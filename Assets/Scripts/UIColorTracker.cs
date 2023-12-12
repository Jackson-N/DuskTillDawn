using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIColorTracker : MonoBehaviour
{
    private Material StartingColor;
    private bool hasColorBeenChanged = false;
    private string newColor;

    [SerializeField] private GameObject target;
    private Material TargetStartingColor;

    // Start is called before the first frame update
    void Start()
    {
        StartingColor = transform.parent.gameObject.GetComponent<Renderer>().material;
        TargetStartingColor = target.GetComponent<Renderer>().material;
    }

    private void OnTriggerEnter(Collider enter)
    {
        if(!hasColorBeenChanged)
        {
            if(enter.transform.gameObject.tag == "ColorCube")
            {
                transform.parent.gameObject.GetComponent<Renderer>().material = enter.transform.gameObject.GetComponent<Renderer>().material;
                target.GetComponent<Renderer>().material = enter.gameObject.GetComponent<Renderer>().material;
                newColor = enter.transform.gameObject.name;
                hasColorBeenChanged = true;
            }
        }
    }

    private void OnTriggerExit(Collider exit)
    {
        if(hasColorBeenChanged)
        {
            if(exit.transform.gameObject.tag == "ColorCube" && exit.transform.gameObject.name == newColor)
            {
                transform.parent.gameObject.GetComponent<Renderer>().material = StartingColor;
                target.GetComponent<Renderer>().material = TargetStartingColor;
                hasColorBeenChanged = false;
            }
        }
    }

}
