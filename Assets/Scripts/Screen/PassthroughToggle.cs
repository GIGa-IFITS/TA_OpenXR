using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassthroughToggle : MonoBehaviour
{
    
    private bool on;
    [SerializeField] private Sprite activeSprite;
    [SerializeField] private Sprite activeHoverSprite;
    [SerializeField] private Sprite activePressedSprite;
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private Sprite defaultHoverSprite;
    [SerializeField] private Sprite defaultPressedSprite;
    [SerializeField] private Button btn;
    [SerializeField] private Image toggleImage;

    public void SwitchToggle(bool on){
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
