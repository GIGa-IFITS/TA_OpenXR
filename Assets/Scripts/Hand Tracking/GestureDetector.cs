using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Gesture
{
    public string name;
    public List<Vector3> fingerDatas;
}

public class GestureDetector : MonoBehaviour
{
    public float threshold = 0.01f;
    public OVRSkeleton skeleton;
    private List<OVRBone> fingerBones;
    public Gesture startGesture;
    private Gesture previousGesture;
    private bool isInit = false;
    [SerializeField] private GameObject startText;
    [SerializeField] private GameObject centerEyeAnchor;

    void Start()
    {
        StartCoroutine(InitFinger(2f));
        previousGesture = new Gesture();
    }

    IEnumerator InitFinger(float seconds){
        yield return new WaitForSeconds(seconds);
        fingerBones = new List<OVRBone>(skeleton.Bones);
        previousGesture = new Gesture();
        isInit = true;
    }

    private void Update() {
        if(isInit){
            Gesture currentGesture = Recognize();
            bool hasRecognized = !currentGesture.Equals(new Gesture());

            if(hasRecognized && !currentGesture.Equals(previousGesture)){
                Debug.Log("gesture found: " + currentGesture.name);
                StartCoroutine(StartingTask());
            }    
            previousGesture = currentGesture;
        }

    }

    IEnumerator StartingTask(){
        startText.SetActive(true);
        Vector3 offset = centerEyeAnchor.transform.forward;
        offset *= 2f;
        startText.transform.position = centerEyeAnchor.transform.position + offset;
        startText.transform.LookAt(startText.transform.position + centerEyeAnchor.transform.rotation * Vector3.forward, centerEyeAnchor.transform.rotation * Vector3.up);
        yield return new WaitForSeconds(1f);
        Debug.Log("User starting task");
        startText.SetActive(false);
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
