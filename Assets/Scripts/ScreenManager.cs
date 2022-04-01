using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    public static ScreenManager instance;
    public SmartphoneScreen smartphoneScreen;
    public DesktopScreen desktopScreen;
    public ButtonPress hoveredButton;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    public void SetHoverButton(ButtonPress _hoveredButton){
        hoveredButton = _hoveredButton;
    }

    public void ResetHoverButton(ButtonPress _hoveredButton){
        hoveredButton = null;
    }

    public void TouchButton(){
        Debug.Log("touch button called");
        // if there are any button hovered, touch that button
        if(hoveredButton != null){
            hoveredButton.ButtonPressed();
        }
    }  

    public void OnTapDashboard(){
        smartphoneScreen.OnTapDashboard();
        desktopScreen.OnTapDashboard();
        ClientSend.SendPageType("dashboardMenu");      
    }

    public void ShowDashboardData(RawData rawdata){
        smartphoneScreen.ShowDashboardData(rawdata);
        desktopScreen.ShowDashboardData(rawdata);
        ClientSend.SendPageType("dashboardData");
    }

    public void ShowDashboardError(){
        smartphoneScreen.ShowDashboardError();
        desktopScreen.ShowDashboardError();
        ClientSend.SendPageType("dashboardError");
    }

    public void OnTapSearch(){
        smartphoneScreen.OnTapSearch();
        desktopScreen.OnTapSearch();
        ClientSend.SendPageType("searchMenu");
    }
}
