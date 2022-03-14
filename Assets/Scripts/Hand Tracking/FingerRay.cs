using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerRay : MonoBehaviour
{
    private GameObject uiHelpers;

    void Start(){
        uiHelpers = GameObject.Find("UIHelpers");
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("UI")){
            var buttonPress = other.gameObject.GetComponent<ButtonPress>();
            if(buttonPress){
                uiHelpers.SetActive(false);
            }
        }  
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("UI")){
            var buttonPress = other.gameObject.GetComponent<ButtonPress>();
            if(buttonPress){
                uiHelpers.SetActive(true);
            }
        }
    }
}
