using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HandTrackingUI : MonoBehaviour
{
    [SerializeField] private OVRHand hand;
    [SerializeField] private OVRInputModule inputModule;
    [SerializeField] private LaserPointer laserPointer;
    void Start()
    {
        inputModule.rayTransform = hand.PointerPose;
    }
}
