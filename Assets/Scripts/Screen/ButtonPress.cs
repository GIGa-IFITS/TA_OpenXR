using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ButtonPress : MonoBehaviour
{
    private GameObject btnObject;
    private Button btn;
    void Awake()
    {
        btnObject = gameObject;
        btn = GetComponent<Button>();
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "InteractHand"){
            ExecuteEvents.Execute (btnObject, new PointerEventData (EventSystem.current), ExecuteEvents.pointerEnterHandler);
            Debug.Log("hover on " + gameObject.name);
            ScreenManager.instance.SetHoverButton(this);
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "InteractHand"){
            ExecuteEvents.Execute (btnObject, new PointerEventData (EventSystem.current), ExecuteEvents.pointerExitHandler);
            Debug.Log("stop hover on " + gameObject.name);
            ScreenManager.instance.ResetHoverButton(this);
        }
    }

    public void ButtonPressed(){
        Debug.Log("click on " + gameObject.name);
        btn.onClick.Invoke();
    }
}
