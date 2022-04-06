using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DesktopScreen : VirtualScreen
{
    public void OnTapSearchUnit(){
        base.OnTapStartSearch("unit");

        if(gameObject.activeSelf){
            Manager.instance.getPenelitiFakultasITS();
        }
    }
}
