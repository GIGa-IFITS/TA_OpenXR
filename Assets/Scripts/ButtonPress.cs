using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ButtonPress : MonoBehaviour, IPointerEnterHandler
{
    private GameObject btnObject;
    private Button btn;
    private bool isPressed;
    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite pressedSprite;
    void Awake()
    {
        isPressed = false;
        btnObject = gameObject;
        btn = GetComponent<Button>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("The cursor entered the selectable UI element.");
        SmartphoneScreen.instance.SetHoverButton(this);
    }

    public void ButtonPressed(){
        Debug.Log("button pressed");
        btn.image.sprite = pressedSprite;
        invokeButtonClick();
    }

    IEnumerator invokeButtonClick(){
        yield return new WaitForSeconds(0.11f);
        btn.onClick.Invoke();
    }

    private void OnEnable() {
        btn.image.sprite = normalSprite;
    }
}
