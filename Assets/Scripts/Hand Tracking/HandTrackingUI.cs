using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HandTrackingUI : MonoBehaviour
{
    [SerializeField] private OVRHand hand;
    [SerializeField] private OVRInputModule inputModule;
    [SerializeField] private LaserPointer laserPointer;
    [SerializeField] private GameObject indexFingerTip;
    [SerializeField] private Vector3 rot;
    void Start()
    {
        inputModule.rayTransform = hand.PointerPose;
        laserPointer.laserBeamBehavior = LaserPointer.LaserBeamBehavior.Off;
        StartCoroutine(setRayTransform(2f));
    }

    IEnumerator setRayTransform(float seconds){
        yield return new WaitForSeconds(seconds);
        GameObject indexFingerTipRef = hand.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).gameObject;
        indexFingerTip = Instantiate(indexFingerTipRef, indexFingerTipRef.transform.parent.gameObject.transform);
        indexFingerTip.transform.localEulerAngles = rot;
        inputModule.rayTransform = indexFingerTip.transform;
    }
}
