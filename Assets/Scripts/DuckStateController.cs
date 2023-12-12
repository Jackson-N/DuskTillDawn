using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DuckStateController : MonoBehaviour
{

    public GameObject player;
    public DuckState currentState;
    public Animator dsa; 
    public Light light;
    public float sanity;
    public float currentSanity = 0.0f;
    public bool isTouchingPlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        dsa = GetComponent<Animator>();
        light = GetComponent<Light>();
        sanity = 100.0f;
        SetState(new DuckSpawn(this));
        
    }

    // Update is called once per frame
    void Update()
    {
        currentState.CheckTransisions();
        currentState.Act();
    }

    public void SetState(DuckState ds)
    {
        if (currentState != null)
        {
            currentState.OnStateExit();
        }

        currentState = ds;

        if (currentState != null)
        {
            currentState.OnStateEnter();
        }
    }

    public void AnimationControl(string da)
    {
        dsa.Play(da);
    }
}
