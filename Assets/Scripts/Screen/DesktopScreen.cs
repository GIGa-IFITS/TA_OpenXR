using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DesktopScreen : VirtualScreen
{    
    [Header("Card Menu - Desktop only")]
    [SerializeField] protected TextMeshProUGUI cardMenuTitleText;
    public void OnTapSearchName(){
        base.OnTapStartSearch("name");

        if(gameObject.activeSelf){
            Manager.instance.getPenelitiAbjadITS();
        }
        cardMenuTitleText.text = "Searching By:" + "\n" + "Researcher Name";
    }
    
    public void OnTapSearchUnit(){
        base.OnTapStartSearch("unit");

        if(gameObject.activeSelf){
            Manager.instance.getPenelitiFakultasITS();
        }
        cardMenuTitleText.text = "Searching By:" + "\n" + "Institution Unit";
    }

    public void OnTapSearchDegree(){
        base.OnTapStartSearch("degree");

        if(gameObject.activeSelf){
            Manager.instance.getGelarPenelitiITS();
        }
        cardMenuTitleText.text = "Searching By:" + "\n" + "Academic Degree";
    }

    public void OnTapSearchKeyword(){
        base.OnTapStartSearch("keyword");

        if(gameObject.activeSelf){
            Manager.instance.getPublikasiFakultas();
        }
        cardMenuTitleText.text = "Searching By:" + "\n" + "Research Keyword";
    }
}
