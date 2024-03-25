using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemScript1 : MonoBehaviour
{
    private bool isTouching = false;

    // Start is called before the first frame update
    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            isTouching = true;
            Destroy(gameObject);
        
        }
        else isTouching = false;
    }

}
