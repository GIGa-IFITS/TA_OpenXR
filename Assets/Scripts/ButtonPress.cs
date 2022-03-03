using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ButtonPress : MonoBehaviour
{
    [SerializeField] private GameObject btn;
    [SerializeField] bool isPressed;

    void Start()
    {
        isPressed = false;
    }

    private void OnTriggerEnter(Collider other) {
        if(!isPressed && other.gameObject.tag == "InteractHand"){
            btn.transform.localPosition = new Vector3(btn.transform.localPosition.x, btn.transform.localPosition.y, 0f);
            isPressed = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "InteractHand"){
            btn.transform.localPosition = new Vector3(btn.transform.localPosition.x, btn.transform.localPosition.y, -0.01f);
            isPressed = false;
        }
    }
}
