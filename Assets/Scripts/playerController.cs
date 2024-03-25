using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{

    GameObject player;

    public int sanity = 100;
    public int currentSanity;

    public bool isTouchingEnemy = false;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(currentSanity);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            isTouchingEnemy = true;
        }
        else isTouchingEnemy = false;
    }
}
