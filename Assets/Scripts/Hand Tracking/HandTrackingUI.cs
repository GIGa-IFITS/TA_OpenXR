using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HandTrackingUI : MonoBehaviour
{
    public OVRHand hand;
    public OVRInputModule inputModule;
    void Start()
    {
        //inputModule.rayTransform = hand.PointerPose;
        inputModule.rayTransform = hand.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).gameObject.transform;
    }
}
