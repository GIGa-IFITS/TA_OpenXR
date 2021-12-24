using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingSphere : MonoBehaviour
{
    // User Inputs
    public float degreesPerSecond = 1.0f;
    public float amplitude = 0.1f;
    public float frequency = 2.2f;
    public int orientation = 1;

    // Position Storage Variables
    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();

    // Start is called before the first frame update
    void Start()
    {
        posOffset = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        // Float up/down with a Sin()
        tempPos = posOffset;

        if(orientation < 0)
        {
            tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;
        }
        else
        {
            tempPos.y -= Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;
        }

        transform.position = tempPos;
    }
}
