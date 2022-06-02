using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndexFingerCollider : MonoBehaviour
{
    private OVRSkeleton skeleton;
    private GameObject thumbTip;
    private GameObject indexTip;
    private Vector3 triggerSize = new Vector3(0.005f, 0.005f, 0.005f);
    void Start() {
        skeleton = GetComponent<OVRSkeleton>();
        StartCoroutine(addTriggerToTip(2f));
    }

    IEnumerator addTriggerToTip(float seconds){
        //Wait for 2 seconds so the bone is initialized
        yield return new WaitForSeconds(seconds);

        //Add collider to tip of index finger
        GameObject indexTip = transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).gameObject;
        Rigidbody rigidbody = indexTip.AddComponent<Rigidbody>();
        rigidbody.isKinematic = true;
        BoxCollider boxCollider = indexTip.AddComponent<BoxCollider>();
        boxCollider.isTrigger = true;
        boxCollider.size = triggerSize;
        indexTip.tag = "InteractHand";
        indexTip.AddComponent<FingerRay>();
    }
}
