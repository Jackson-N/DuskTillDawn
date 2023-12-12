using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarLook : MonoBehaviour
{
    
    [SerializeField] private GameObject Player;

    // Update is called once per frame
    void Update()
    {
        Vector3 LookVector = new Vector3(Player.transform.position.x, 0, Player.transform.position.z);
        transform.LookAt(LookVector);
    }
}
