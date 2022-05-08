using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SmartphoneScreen : VirtualScreen
{
    [SerializeField] private ScrollRect nodeScroll;
    [SerializeField] private ScrollRect cardScroll;
    private ScrollRect scrollRect;

    public float multiplier;
    public float duration;

    public void OnTapSearchName(){
        base.OnTapStartSearch("name");
        if(gameObject.activeSelf){
            Manager.instance.getPenelitiAbjadITS2D();
        }
    }

    public void OnTapSearchUnit(){
        base.OnTapStartSearch("unit");
        if(gameObject.activeSelf){
            Manager.instance.getPenelitiFakultasITS2D();
        }
    }

    public void OnTapSearchDegree(){
        base.OnTapStartSearch("degree");

        if(gameObject.activeSelf){
            Manager.instance.getGelarPenelitiITS2D();
        }
    }

    public void OnTapSearchKeyword(){
        base.OnTapStartSearch("keyword");

        if(gameObject.activeSelf){
            Manager.instance.getPublikasiFakultas2D();
        }
    }

    public void SetScroll(float scrollSpeed){
        if(cardMenu.activeSelf){ // card menu dulu, karna kalau card menu aktif node menu pasti aktif.
            scrollRect = cardScroll;
            ScreenManager.instance.PrintDebug("cardScroll\n");
        }else if(nodeMenu.activeSelf){
            scrollRect = nodeScroll;
            ScreenManager.instance.PrintDebug("nodeScroll\n");
        }else{
            scrollRect = null;
            ScreenManager.instance.PrintDebug("card and node is not active\n");
        }

        if(scrollRect != null){
            Debug.Log("Set scroll!" + scrollSpeed);
            float contentHeight = scrollRect.content.sizeDelta.y;
            float contentShift = scrollSpeed * multiplier * Time.deltaTime;
            Debug.Log("content height: " + contentHeight + " content shift " + contentShift + " result: " + contentShift / contentHeight);
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
        Debug.Log("vertical normalized pos: " + scrollRect.verticalNormalizedPosition);
    }
}
