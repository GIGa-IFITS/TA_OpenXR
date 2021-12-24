using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;
using UnityEngine.Events;

public class ButtonListener : MonoBehaviour
{
    public UnityEvent proximityEvent;
    public UnityEvent contactEvent;
    public UnityEvent actionEvent;
    public UnityEvent defaultEvent;
    public HandTrackingGrabber handReference;

    void Start()
    {
        GetComponent<ButtonController>().InteractableStateChanged.AddListener(InitiateEvent);
    }

    void InitiateEvent(InteractableStateArgs state){
        if(state.NewInteractableState == InteractableState.ProximityState)
            proximityEvent.Invoke();
        else if(state.NewInteractableState == InteractableState.ContactState && handReference.GetGrabbedObject() == null)
            contactEvent.Invoke();
        else if(state.NewInteractableState == InteractableState.ActionState && handReference.GetGrabbedObject() == null)
            actionEvent.Invoke();   
        else
            defaultEvent.Invoke();
    }
    
}
