using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ButtonPress : MonoBehaviour
{
    [SerializeField] private GameObject btnObject;
    private Button btn;
    [SerializeField] private bool isPressed;
    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite pressedSprite;
    void Start()
    {
        isPressed = false;
        btn = btnObject.GetComponent<Button>();
    }

    private void OnTriggerEnter(Collider other) {
        if(!isPressed && other.gameObject.tag == "InteractHand"){
            btnObject.transform.localPosition = new Vector3(btnObject.transform.localPosition.x, btnObject.transform.localPosition.y, 0f);
            isPressed = true;
            btn.image.sprite = pressedSprite;
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "InteractHand"){
            btnObject.transform.localPosition = new Vector3(btnObject.transform.localPosition.x, btnObject.transform.localPosition.y, -0.01f);
            isPressed = false;
            btn.image.sprite = normalSprite;
        }
    }
}
