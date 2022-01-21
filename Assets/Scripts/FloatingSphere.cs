using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingSphere : MonoBehaviour
{
    public GameObject nodeCanvas;
    [SerializeField] private GameObject playerRef;
    private OVRGrabbable ovrGrabbable;
    private Rigidbody rigidbody;
    // User Inputs
    public float floatStrength;
    public float maxVelocity;
    private float sqrMaxVelocity;
    private float floatingPosition;

    void Start()
    {
        playerRef = GameObject.Find("Stage Object/OVRCameraRig");
        if(playerRef == null){
            playerRef = GameObject.Find("Stage Object/[VRSimulator_CameraRig]");
        }
        ovrGrabbable = GetComponent<OVRGrabbable>();
        rigidbody = GetComponent<Rigidbody>();

        floatingPosition = Random.Range(transform.position.y - 0.2f, transform.position.y + 0.2f);
        sqrMaxVelocity = maxVelocity * maxVelocity;
    }

    void FixedUpdate()
    {
        if(!ovrGrabbable.isGrabbed){
            nodeCanvas.transform.LookAt(nodeCanvas.transform.position + playerRef.transform.rotation * Vector3.forward, playerRef.transform.rotation * Vector3.up);

            if(transform.position.y < floatingPosition){
                rigidbody.AddForce(Vector3.up * floatStrength);
            }
            if(transform.position.y > floatingPosition){
                rigidbody.AddForce(Vector3.down * floatStrength);
            }
        }

        // clamp velocity if necessary, use sqr for faster performance
        Vector3 v = rigidbody.velocity;
        if(v.sqrMagnitude > sqrMaxVelocity){
            rigidbody.velocity = v.normalized * maxVelocity;
        } 
    }
}