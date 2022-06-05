using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public struct Gesture
{
    public string name;
    public List<Vector3> fingerDatas;
    public UnityEvent onRecognized;
}

public class GestureDetector : MonoBehaviour
{
    public float threshold = 0.01f;
    public OVRSkeleton skeleton;
    private List<OVRBone> fingerBones;
    public Gesture startGesture;
    private Gesture previousGesture;

    void Start()
    {
        StartCoroutine(InitFinger(2f));
        previousGesture = new Gesture();
    }

    IEnumerator InitFinger(float seconds){
        yield return new WaitForSeconds(seconds);
        fingerBones = new List<OVRBone>(skeleton.Bones);
        previousGesture = new Gesture();
    }

    private void Update() {
        Gesture currentGesture = Recognize();
        bool hasRecognized = !currentGesture.Equals(new Gesture());

        if(hasRecognized && !currentGesture.Equals(previousGesture)){
            Debug.Log("gesture found: " + currentGesture.name);
            Debug.Log("User starting task");
            previousGesture = currentGesture;
            currentGesture.onRecognized.Invoke();
            // notify user that timer has started, gesture is recognized, debug.log saying task has started
        }
    }

    public void Save()
    {
        startGesture = new Gesture();
        startGesture.name = "Starting Task Gesture";
        List<Vector3> data = new List<Vector3>();
        foreach(var bone in fingerBones){
            // finger position relative to root
            data.Add(skeleton.transform.InverseTransformPoint(bone.Transform.position));
        }

        startGesture.fingerDatas = data;
    }

    Gesture Recognize(){
        Gesture currentGesture = new Gesture();
        float currentMin = Mathf.Infinity;

        float sumDistance = 0;
        bool isDiscarded = false;
        for(int i=0; i<fingerBones.Count; i++){
            Vector3 currentData = skeleton.transform.InverseTransformPoint(fingerBones[i].Transform.position);
            float distance = Vector3.SqrMagnitude(currentData - startGesture.fingerDatas[i]);
            if(distance > threshold){
                isDiscarded = true;
                break;
            }

            sumDistance += distance;
        }

        if(!isDiscarded && sumDistance < currentMin){
            currentMin = sumDistance;
            currentGesture = startGesture;
        }

        return currentGesture;
    }
}
