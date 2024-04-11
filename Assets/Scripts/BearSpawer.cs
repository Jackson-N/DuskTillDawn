using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearSpawer : MonoBehaviour
{
    public float startingTimer = 90f;

    public float bearPosX;
    public float bearPosY;
    public float bearPosZ;

    public bool hasSpawned = false;

    [SerializeField] private GameObject BearPrefab;

    // Start is called before the first frame update
    void Start()
    {
        bearPosX = gameObject.transform.position.x;
        bearPosY = gameObject.transform.position.y;
        bearPosZ = gameObject.transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        startingTimer -= Time.deltaTime;

        if (!hasSpawned)
        {
            if (startingTimer <= 0.0f)
            {
                startingTimer = 0.0f;
                //SpawnBear();
                hasSpawned = true;
                Debug.Log("Bear has spawned");
                SpawnBear();
            }
        }
    }

    public void SpawnBear()
    {
        if (startingTimer == 0.0f)
        {
            Instantiate(BearPrefab, new Vector3(bearPosX, bearPosY, bearPosZ), Quaternion.identity);
            BearPrefab.transform.position = transform.position + new Vector3(bearPosX, bearPosY, bearPosZ);
            //Debug.Log("Bear has spawned");
        }
    }
}
