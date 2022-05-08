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
}
