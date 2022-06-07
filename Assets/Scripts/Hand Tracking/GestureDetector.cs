using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GestureDetector : MonoBehaviour
{
    [SerializeField] private GameObject startText;
    [SerializeField] private GameObject centerEyeAnchor;

    public void StartTask(){
        Debug.Log("start task called");
        StartCoroutine(StartingTask());
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

    
}
