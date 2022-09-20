using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartphoneGyro : MonoBehaviour
{
    public static SmartphoneGyro instance;
    private Quaternion rot;
    private bool firstTime = true;
    private Quaternion offset;
    [SerializeField] private GameObject leftHandAnchor;
    [SerializeField] private OVRHand ovrHand;
    private Quaternion gyroInitialRotation;
    private Transform rawGyroRotation;
    private Quaternion initialRotation;
    private Quaternion offsetRotation;
    private Quaternion tempGyroRotation;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    private void Update() {
        if(!firstTime){
            transform.rotation = offset * rot;
        }
    }

    private void LateUpdate() {
        // follow position only if data have high confidence, else freeze to avoid hand jitter
        if(ovrHand.IsDataHighConfidence){
            transform.position = leftHandAnchor.transform.position;
        }
    }

    public void ResetPhoneRotation(){
        Debug.Log("reset gesture detected, reset phone position");
        firstTime = true;
    }

    public void SetPhoneRotation(float x, float y, float z, float w){
        rot.x = x;
        rot.y = y;
        rot.z = z;
        rot.w = w;

        if(firstTime){
            Debug.Log("reset phone position");
            Vector3 initialEuler = rot.eulerAngles;
            initialEuler.x = 0f;
            initialEuler.y = 0f;
            Quaternion initialRotation = Quaternion.Euler(initialEuler);
            offset = Quaternion.Euler(90f, 0f, 0f) * Quaternion.Inverse(initialRotation);
            Debug.Log("offset: " + offset.eulerAngles);
            firstTime = false;
        }
    }

    

}

