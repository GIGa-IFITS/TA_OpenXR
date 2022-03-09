using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndexFingerCollider : MonoBehaviour
{
    private OVRSkeleton skeleton;
 
    void Start() {
        skeleton = GetComponent<OVRSkeleton>();
        // Add collider to tip of index finger
        foreach(OVRBone bone in skeleton.Bones) {
            if (bone.Id == OVRSkeleton.BoneId.Hand_IndexTip) {
                bone.Transform.gameObject.AddComponent<SphereCollider>();
                bone.Transform.gameObject.GetComponent<SphereCollider>().radius = 0.01f;        
                bone.Transform.gameObject.tag = "InteractHand";
            }
        }
    }
}
