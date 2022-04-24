using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SmartphoneScreen : VirtualScreen
{
    public void OnTapSearchUnit(){
        base.OnTapStartSearch("unit");
        if(gameObject.activeSelf){
            Manager.instance.getPenelitiFakultasITS2D();
        }
    }
}
