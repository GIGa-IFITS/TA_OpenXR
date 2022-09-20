using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassthroughToggle : MonoBehaviour
{
    [SerializeField] private OVRPassthroughLayer ovrPassthroughLayer;
    private bool on;
    [SerializeField] private Sprite activeSprite;
    [SerializeField] private Sprite activeHoverSprite;
    [SerializeField] private Sprite activePressedSprite;
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private Sprite defaultHoverSprite;
    [SerializeField] private Sprite defaultPressedSprite;
    [SerializeField] private Button btn;
    [SerializeField] private Image toggleImage;

    public void SwitchToggle(){
        Debug.Log("switch toggle");
        ovrPassthroughLayer.hidden = !ovrPassthroughLayer.hidden;
        on = !ovrPassthroughLayer.hidden;

        Debug.Log("passthrough val: " + on);

        if(on){    
            toggleImage.sprite = activeSprite;
            SpriteState ss = new SpriteState();
            ss.highlightedSprite = activeHoverSprite;
            ss.pressedSprite = activePressedSprite;
            btn.spriteState = ss;
        }else{
            toggleImage.sprite = defaultSprite;
            SpriteState ss = new SpriteState();
            ss.highlightedSprite = defaultHoverSprite;
            ss.pressedSprite = defaultPressedSprite;
            btn.spriteState = ss;
        }
    }
}
