using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ButtonPress : MonoBehaviour
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

    public void ButtonPressed(){
        btnObject.transform.localPosition = new Vector3(btnObject.transform.localPosition.x, btnObject.transform.localPosition.y, 0f);
        isPressed = true;
        btn.image.sprite = pressedSprite;

        StartCoroutine(backToDefault(0.1f));
    }

    IEnumerator backToDefault(float seconds){
        //Wait for n seconds
        yield return new WaitForSeconds(seconds);
        btnObject.transform.localPosition = new Vector3(btnObject.transform.localPosition.x, btnObject.transform.localPosition.y, -0.01f);
        isPressed = false;
        btn.image.sprite = normalSprite;
    }

    private void OnEnable() {
        btnObject.transform.localPosition = new Vector3(btnObject.transform.localPosition.x, btnObject.transform.localPosition.y, -0.01f);
        isPressed = false;
        btn.image.sprite = normalSprite;
    }
}
