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
        inputModule.rayTransform = hand.PointerPose;
        // Transform indexFingerTip = hand.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).gameObject.transform;
        // indexFingerTip.transform.Rotate(0f, 0f, 90f, Space.World);
        // inputModule.rayTransform = indexFingerTip;
    }
}
