using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereBoard : MonoBehaviour
{
    public GameObject nodeCanvas;
    [SerializeField] private GameObject playerRef;

    void Start()
    {
        playerRef = GameObject.Find("Stage Object/OVRCameraRig/TrackingSpace/CenterEyeAnchor").transform.GetChild(0).gameObject;
        if(playerRef == null){
            Manager.instance.PrintDebug("null");
            playerRef = GameObject.Find("Stage Object/[VRSimulator_CameraRig]");
        }
    }

    void Update()
    {
        nodeCanvas.transform.LookAt(nodeCanvas.transform.position + playerRef.transform.rotation * Vector3.forward, playerRef.transform.rotation * Vector3.up);
    }
}