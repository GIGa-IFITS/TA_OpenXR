using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndexFingerCollider : MonoBehaviour
{
    private OVRSkeleton skeleton;
    void Start() {
        skeleton = GetComponent<OVRSkeleton>();
        StartCoroutine(addTriggerToTip(2f));
    }

    IEnumerator addTriggerToTip(float seconds){
        //Wait for n seconds
        yield return new WaitForSeconds(seconds);
        //Add collider to tip of index finger
        GameObject indexTip = transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).gameObject;
        Rigidbody rigidbody = indexTip.AddComponent<Rigidbody>();
        rigidbody.isKinematic = true;
        BoxCollider boxCollider = indexTip.AddComponent<BoxCollider>();
        boxCollider.isTrigger = true;
        boxCollider.size = new Vector3(0.01f, 0.01f, 0.01f);
        indexTip.tag = "InteractHand";
        indexTip.AddComponent<FingerRay>();
    }
}
