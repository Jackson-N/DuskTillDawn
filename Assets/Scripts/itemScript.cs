using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemScript : MonoBehaviour
{
    //private bool isTouching = false;

    public GameObject target;
    //public GameObject cloth;
    public GameObject candleMaker;
    public LightDetection lightDetection;

    // Start is called before the first frame update
    // Update is called once per frame

    void Start()
    {
        target = this.gameObject;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Cloth"))
        {
            //isTouching = true;
            Debug.Log("Player is touching the item.");
            Destroy(collision.gameObject);
            Destroy(target);
            makeCandle();
            //add candle to lightsource array
            lightDetection.lightSource[lightDetection.index++] = candleMaker;
        }

        if (collision.gameObject.CompareTag("Item"))
        {
            Debug.Log("Player is touching the campfire.");
        }
    }

    private void makeCandle()
    {
        Instantiate(candleMaker, transform.position, Quaternion.identity);
        //isTouching = false;
    }
}
