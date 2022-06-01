﻿using System.Collections;
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

    private void LateUpdate() {
        // follow position only if data have high confidence, else freeze to avoid hand jitter
        if(ovrHand.IsDataHighConfidence){
            transform.position = leftHandAnchor.transform.position;
        }
    }

    public void SetPhoneRotation(float x, float y, float z, float w){
        rot.x = x;
        rot.y = y;
        rot.z = z;
        rot.w = w;

        if(firstTime){
            offset = transform.rotation * Quaternion.Inverse(rot);
            firstTime = false;
        }else{
            transform.rotation = offset * rot;
        }
    }

}

