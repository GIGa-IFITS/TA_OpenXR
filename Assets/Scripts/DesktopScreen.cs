using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DesktopScreen : VirtualScreen
{
    public new void OnTapSearchName(){
        base.OnTapSearchName();
        Manager.instance.getPenelitiAbjadITS();
    }
}
