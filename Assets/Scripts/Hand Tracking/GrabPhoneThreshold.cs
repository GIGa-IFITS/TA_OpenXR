using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabPhoneThreshold : MonoBehaviour
{
    [SerializeField] private GameObject playerRef;
    [SerializeField] private float threshold = 0.7f;
    public float x;
    public float y;
    public float z;

    void Update()
    {
        //take the dot product of the facing direction of phone compared to the direction from phone to player
        //if it's 1, phone is looking exactly at player
        //if it's -1, phone is looking completely the opposite direction to player
        //if it's somewhere between 0 and 1, they're looking roughty in the right direction
        //threshold is to define as "not perfectly".
        Vector3 dirFromPhonetoCam = (playerRef.transform.position - transform.position).normalized;
        float dotProd = Vector3.Dot(dirFromPhonetoCam, transform.forward);

        transform.LookAt(playerRef.transform);
        transform.Rotate(x,y,z);

        if(dotProd < threshold) {
            // phone is looking mostly opposite of player
            transform.LookAt(playerRef.transform);
            transform.Rotate(x,y,z);
        }
    }
}
