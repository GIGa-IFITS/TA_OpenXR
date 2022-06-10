using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SmartphoneScreen : VirtualScreen
{
    [Header("Smartphone only")]
    [SerializeField] protected TextMeshProUGUI nodeMenuSmallTitleText;
    private GameObject smartphoneCanvas;
    [Header("Others")]
    [SerializeField] private ScrollRect nodeScroll;
    [SerializeField] private ScrollRect cardScroll;
    [SerializeField] private Image nodeBg;
    [SerializeField] private Image cardBg;
    private ScrollRect scrollRect;

    public float multiplier;

    private void Start() {
        smartphoneCanvas = transform.GetChild(0).gameObject;
    }

    public void OnTapSearchName(){
        base.OnTapStartSearch("name");
        if(smartphoneCanvas.activeSelf){
            Manager.instance.getPenelitiAbjadITS2D();
        }
        nodeMenuSmallTitleText.text = "Searching By:" + "\n" + "Researcher Name";
    }

    public void OnTapSearchUnit(){
        base.OnTapStartSearch("unit");
        if(smartphoneCanvas.activeSelf){
            Manager.instance.getPenelitiFakultasITS2D();
        }
        nodeMenuSmallTitleText.text = "Searching By:" + "\n" + "Institution Unit";
    }

    public void OnTapSearchDegree(){
        base.OnTapStartSearch("degree");

        if(smartphoneCanvas.activeSelf){
            Manager.instance.getGelarPenelitiITS2D();
        }
        nodeMenuSmallTitleText.text = "Searching By:" + "\n" + "Academic Degree";
    }

    public void OnTapSearchKeyword(){
        base.OnTapStartSearch("keyword");

        if(smartphoneCanvas.activeSelf){
            Manager.instance.getPublikasiFakultas2D();
        }
        nodeMenuSmallTitleText.text = "Searching By:" + "\n" + "Research Keyword";
    }

    public void ChangeNodeHoverBg(bool hover){
        if(hover){
            nodeBg.color = new Color32(246, 221, 119, 255);
        }else{
            nodeBg.color = new Color32(255, 255, 255, 255);
        }
    }

    public void ChangeCardHoverBg(bool hover){
        if(hover){
            cardBg.color = new Color32(246, 221, 119, 255);
        }else{
            cardBg.color = new Color32(255, 255, 255, 255);
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
            float contentHeight = scrollRect.content.sizeDelta.y;
            float contentShift = scrollSpeed * multiplier * Time.deltaTime;
            float val = scrollRect.verticalNormalizedPosition + (contentShift / contentHeight);
            val = Mathf.Clamp(val, 0f, 1f);
            scrollRect.verticalNormalizedPosition = val;
        }
    }
}
