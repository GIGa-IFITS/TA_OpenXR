using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GestureDetector : MonoBehaviour
{
    [SerializeField] private GameObject startText;
    
    [SerializeField] private OVRPassthroughLayer ovrPassthroughLayer;
    public bool isSwipe;
    private Vector3 startPos;
    private Vector3 endPos;
    [SerializeField] private GameObject rightHand;
    public float threshold;
    public float duration;

    Coroutine swipeDuration;

    public void StartTask(){
        Debug.Log("start task called");
        StartCoroutine(StartingTask());
    }

    IEnumerator StartingTask(){
        startText.SetActive(true);
        yield return new WaitForSeconds(1f);
        Debug.Log("User starting task");
        startText.SetActive(false);
    }

    public void setPassthroughVisibility(){
        ovrPassthroughLayer.hidden = !ovrPassthroughLayer.hidden;
    }

    public void StartSwipe(){
        isSwipe = true;
        startPos =  rightHand.transform.position;
        Debug.Log("start swipe");
        if(swipeDuration != null){
            StopCoroutine(swipeDuration);
        }
        swipeDuration = StartCoroutine(SwipeDuration());
    }

    IEnumerator SwipeDuration(){
        yield return new WaitForSeconds(duration);
        Debug.Log("swipe check duration ended");
        isSwipe = false;
    }

    public void CheckEndSwipe(){
        Debug.Log("end swipe gesture");
        if(isSwipe){
            Debug.Log("check if swipe");
            endPos = rightHand.transform.position;
            if((endPos - startPos).sqrMagnitude > threshold){
                Debug.Log("swipe detected");
                ScreenManager.instance.SetScreenMode("down");
                StopCoroutine(swipeDuration);
            }
            isSwipe = false;
        }
    }
}
