using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingSphere : MonoBehaviour
{
    public GameObject nodeCanvas;
    [SerializeField] private GameObject playerRef;
    private Rigidbody rigidbody;
    // User Inputs
    public float floatStrength;
    [SerializeField] private float floatingPosition;
    private Vector3 posOffset;
    [SerializeField] private float amplitude;
    public float frequency = 0.2f;
    private Vector3 tempPos = new Vector3();
    private OVRGrabbable ovrGrabbable;
    private bool isAnimated;

    // void Start()
    // {
    //     playerRef = GameObject.Find("Stage Object").transform.GetChild(0).gameObject;
    //     if(playerRef == null){
    //         Manager.instance.PrintDebug("null");
    //         playerRef = GameObject.Find("Stage Object/[VRSimulator_CameraRig]");
    //     }
    //     rigidbody = GetComponent<Rigidbody>();

    //     floatingPosition = Random.Range(transform.position.y - 0.2f, transform.position.y + 0.2f);

    //     ovrGrabbable = GetComponent<OVRGrabbable>();
    //     posOffset = new Vector3(transform.position.x, floatingPosition, transform.position.z);
    //     amplitude = (Random.Range(0,2)*2-1) * 0.1f; // -0.1 or 0.1
    // }

    // void Update()
    // {
    //     //nodeCanvas.transform.LookAt(nodeCanvas.transform.position + playerRef.transform.rotation * Vector3.forward, playerRef.transform.rotation * Vector3.up);
    //     if(transform.position.y < floatingPosition){
    //         rigidbody.AddForce(Vector3.up * floatStrength);
    //     }
    //     else{
    //         rigidbody.AddForce(Vector3.down * floatStrength);
    //     }

    //     // Float up/down with a Sin()
    //     // if(ovrGrabbable.isGrabbed){
    //     //     posOffset = transform.position;
    //     // }
    //     // else{
    //     //     tempPos = posOffset;
    //     //     tempPos.y += Mathf.Sin (Time.fixedTime * Mathf.PI * frequency) * amplitude;

    //     //     rigidbody.position = tempPos;
    //     // }
    // }
}