using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DesktopScreen : VirtualScreen
{    
    public void OnTapSearchName(){
        base.OnTapStartSearch("name");

        if(gameObject.activeSelf){
            Manager.instance.getPenelitiAbjadITS();
        }
    }
    
    public void OnTapSearchUnit(){
        base.OnTapStartSearch("unit");

        if(gameObject.activeSelf){
            Manager.instance.getPenelitiFakultasITS();
        }
    }

    public void OnTapSearchDegree(){
        base.OnTapStartSearch("degree");

        if(gameObject.activeSelf){
            Manager.instance.getGelarPenelitiITS();
        }
    }

    public void OnTapSearchKeyword(){
        base.OnTapStartSearch("keyword");

        if(gameObject.activeSelf){
            Manager.instance.getPublikasiFakultas();
        }
    }

    public void ChangeNodeHoverBg(bool hover){
        if(hover){
            nodeMenuTotalText.color = new Color32(246, 221, 119, 255);
        }else{
            nodeMenuTotalText.color = new Color32(50, 59, 89, 255);
        }
    }

    public void ChangeCardHoverBg(bool hover){
        if(hover){
            cardMenuTotalText.color = new Color32(246, 221, 119, 255);
        }else{
            cardMenuTotalText.color = new Color32(50, 59, 89, 255);
        }
    }
}
