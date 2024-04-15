using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearSpawer : MonoBehaviour
{

    [SerializeField] public float bearPosX;
    [SerializeField] public float bearPosY;
    [SerializeField] public float bearPosZ;

    public Vector3 spawnPos;

    public bool hasSpawned = false;

    // Start is called before the first frame update
    void Start()
    {
        bearPosX = gameObject.transform.position.x;
        bearPosY = gameObject.transform.position.y;
        bearPosZ = gameObject.transform.position.z;
        spawnPos = new Vector3(bearPosX, bearPosY, bearPosZ);
    }
    
}
