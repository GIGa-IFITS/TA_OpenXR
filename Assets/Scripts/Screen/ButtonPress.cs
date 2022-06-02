using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ButtonPress : MonoBehaviour
{
    private GameObject btnObject;
    private Button btn;
    private bool isVisible = true;
    private bool isNode = false;
    private RectTransform viewport;
    void Awake()
    {
        btnObject = gameObject;
        btn = GetComponent<Button>();
    }

    private void Start() {
        if(GetComponent<NodeScript>() != null){
            isNode = true;
            isVisible = false;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.name == "Scroll Collider"){
            isVisible = true;
        }

        if(other.gameObject.tag == "InteractHand"){
            if(!isNode || isVisible){
                ExecuteEvents.Execute (btnObject, new PointerEventData (EventSystem.current), ExecuteEvents.pointerEnterHandler);
                Debug.Log("hover on " + gameObject.name + ",smartphone mode");
                ScreenManager.instance.SetHoverButton(this);
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.name == "Scroll Collider"){
            isVisible = false;
        }

        if(other.gameObject.tag == "InteractHand"){
            ExecuteEvents.Execute (btnObject, new PointerEventData (EventSystem.current), ExecuteEvents.pointerExitHandler);
            Debug.Log("stop hover on " + gameObject.name + ",smartphone mode");
            ScreenManager.instance.ResetHoverButton(this);
        }
    }

    public void ButtonPressed(){
        Debug.Log("click on " + gameObject.name + ",smartphone mode");
        btn.onClick.Invoke();
    }
}
