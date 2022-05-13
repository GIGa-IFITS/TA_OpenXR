using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeLook : MonoBehaviour
{
    private GameObject centerEyeAnchor;

    void Start()
    {
        centerEyeAnchor = GameObject.Find("/Stage Object/OVRCameraRig/TrackingSpace/CenterEyeAnchor");
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(transform.position + centerEyeAnchor.transform.rotation * Vector3.forward, centerEyeAnchor.transform.rotation * Vector3.up);
    }
}
