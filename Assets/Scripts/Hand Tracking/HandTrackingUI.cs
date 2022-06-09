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
        SetLaserOff();
    }
    public void SetLaserOn(){
        laserPointer.laserBeamBehavior = LaserPointer.LaserBeamBehavior.On;
        gameObject.SetActive(true);
    }

    public void SetLaserOff(){
        laserPointer.laserBeamBehavior = LaserPointer.LaserBeamBehavior.Off;
    }
}
