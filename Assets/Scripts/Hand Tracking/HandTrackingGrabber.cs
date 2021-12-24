﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;

public class HandTrackingGrabber : OVRGrabber
{
    private OVRHand hand;
    public float pinchThreshold = 0.7f;

    protected override void Start()
    {
        base.Start();
        hand = GetComponent<OVRHand>();
    }

    public override void Update()
    {
        base.Update();
        CheckIndexPinch();
    }

    void CheckIndexPinch(){
        float pinchStrength = hand.GetFingerPinchStrength(OVRHand.HandFinger.Index);
        bool isPinching = pinchStrength > pinchThreshold;

        if(!m_grabbedObj && isPinching && m_grabCandidates.Count > 0)
            GrabBegin();
        else if(m_grabbedObj && !isPinching)
            GrabEnd();
            
    }

    public OVRGrabbable GetGrabbedObject(){
        return m_grabbedObj;
    }
}
