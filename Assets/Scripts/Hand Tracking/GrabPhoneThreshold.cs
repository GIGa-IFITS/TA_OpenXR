using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabPhoneThreshold : MonoBehaviour
{
    public bool thresholdActiveDebug;
    public bool smoothActiveDebug;
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float minY;
    [SerializeField] private float maxY;
    [SerializeField] private float minZ;
    [SerializeField] private float maxZ;
    private Vector3 rotation;

    [SerializeField] private float smoothTime = 1f;

    void Update()
    {
        if(thresholdActiveDebug){
            rotation = transform.eulerAngles;
            rotation.x = Mathf.Clamp(rotation.x, minX, maxX);
            rotation.y = Mathf.Clamp(rotation.y, minY, maxY);
            rotation.z = Mathf.Clamp(rotation.z, minZ, maxZ);

            if(smoothActiveDebug){
                transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, rotation, smoothTime * Time.deltaTime);
            }else{
                transform.eulerAngles = rotation;
            }
        }
        Debug.Log("euler angles " + transform.eulerAngles);
    }
}
