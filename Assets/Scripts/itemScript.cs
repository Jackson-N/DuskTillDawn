using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemScript : MonoBehaviour
{
    private bool isTouching = false;
    public GameObject candleMaker;

    // Start is called before the first frame update
    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Component"))
        {
            isTouching = true;
            Destroy(gameObject);
            makeCandle();
        }
        else isTouching = false;
    }

    private void makeCandle()
    {
        Instantiate(candleMaker, transform.position, Quaternion.identity);
        isTouching = false;
    }
}
