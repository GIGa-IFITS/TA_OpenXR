using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SmartphoneScreen : VirtualScreen
{
    private GameObject smartphoneCanvas;
    [SerializeField] private ScrollRect nodeScroll;
    [SerializeField] private ScrollRect cardScroll;
    private ScrollRect scrollRect;

    public float multiplier;
    public float duration;

    private void Start() {
        smartphoneCanvas = transform.GetChild(0).gameObject;
    }

    public void OnTapSearchName(){
        base.OnTapStartSearch("name");
        if(smartphoneCanvas.activeSelf){
            Manager.instance.getPenelitiAbjadITS2D();
        }
    }

    public void OnTapSearchUnit(){
        base.OnTapStartSearch("unit");
        if(smartphoneCanvas.activeSelf){
            Manager.instance.getPenelitiFakultasITS2D();
        }
    }

    public void OnTapSearchDegree(){
        base.OnTapStartSearch("degree");

        if(smartphoneCanvas.activeSelf){
            Manager.instance.getGelarPenelitiITS2D();
        }
    }

    public void OnTapSearchKeyword(){
        base.OnTapStartSearch("keyword");

        if(smartphoneCanvas.activeSelf){
            Manager.instance.getPublikasiFakultas2D();
        }
    }

    public void SetScroll(float scrollSpeed){
        if(smartphoneCanvas.activeSelf && cardMenu.activeSelf){ // card menu dulu, karna kalau card menu aktif node menu pasti aktif.
            scrollRect = cardScroll;
        }else if(smartphoneCanvas.activeSelf && nodeMenu.activeSelf){
            scrollRect = nodeScroll;
        }else{
            scrollRect = null;
        }

        if(scrollRect != null){
            Debug.Log("Set scroll with scroll speed: " + scrollSpeed);
            float contentHeight = scrollRect.content.sizeDelta.y;
            float contentShift = scrollSpeed * multiplier * Time.deltaTime;
            float val = scrollRect.verticalNormalizedPosition + (contentShift / contentHeight);
            val = Mathf.Clamp(val, 0f, 1f);
            StartCoroutine(LerpToPos(val));
        }
    }

    private IEnumerator LerpToPos(float endValue){
        float time = 0;
        float startValue = scrollRect.verticalNormalizedPosition;
        while (time < duration)
        {
            scrollRect.verticalNormalizedPosition = Mathf.Lerp(startValue, endValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        scrollRect.verticalNormalizedPosition = endValue;
    }
}
