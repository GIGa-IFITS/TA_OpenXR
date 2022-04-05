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
        Manager.instance.Dashboard();
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

    public void OnTapSearchName(){
        smartphoneScreen.OnTapSearchName();
        desktopScreen.OnTapSearchName();
        ClientSend.SendPageType("name");
    }

    public void UpdateNodeInfo(string _name, int _total, string _tag, string _nodeId, string _filterName){
        smartphoneScreen.UpdateNodeInfo(_name, _total, _tag, _nodeId, _filterName);
        desktopScreen.UpdateNodeInfo(_name, _total, _tag, _nodeId, _filterName);
        
        // send to phone
    }

    // public void ShowNextNode(string _name, int _total, string _tag, string _nodeId, string _filterName){
    //     //smartphoneScreen.OnTapNode(_name, _total, _tag, _nodeId, _filterName);
    //     //desktopScreen.OnTapNode(_name, _total, _tag, _nodeId, _filterName);
    //     ClientSend.SendFilterSummary(_name, _total, _tag, _nodeId, _filterName);
    // }

    // public void ShowNodeDetail(string _nodeId, string _filterName){
    //     //smartphoneScreen.OnTapNodeDetail(_nodeId, _filterName);
    //     //desktopScreen.OnPinchNodeDetail( _nodeId, _filterName);

    //     //send data to smartphone
    // }

     public void OnSelectNode(NodeVariable nodeObject, bool selected){
        if (nodeObject != null){
            if (nodeObject.CompareTag("ListPenelitiAbjad"))
            {
                string nodeId = nodeObject.kode_peneliti;
                Debug.Log(nodeId + " <- NodeVariable");
                
                string name = nodeObject.nama;
                int total = nodeObject.jumlah;
                string tag = "ListPenelitiAbjad";
                string filterName = "Researcher Name";

                // update screen                
                // if select node
                if(selected){
                    // update node info and also update node list if in phone mode

                    // spawn node if desktop mode
                    if(desktopScreen.gameObject.activeSelf){
                        Manager.instance.getPenelitiInisialITS(nodeId);
                    }
                }else{ // only hover
                    UpdateNodeInfo(name, total, tag, nodeId, filterName);
                }
                
            }
            else if(nodeObject.CompareTag("ListPenelitiInisial"))
            {
                // string nodeId = nodeObject.kode_peneliti;
                // Debug.Log(nodeId + " <- NodeVariable");

                // string filterName = "Researcher Name";

                // // update screen
                // ShowNodeDetail(nodeId, filterName);
            }
            
            else if(nodeObject.CompareTag("ListPenelitiFakultas"))
            {
               
                
            }
            else if(nodeObject.CompareTag("ListPenelitiDepartemen"))
            {
               
            }
            else if(nodeObject.CompareTag("ListPenelitiDepartemenDetail"))
            {
               
            }
            
            else if(nodeObject.CompareTag("ListGelar"))
            {
              
            }
            else if (nodeObject.CompareTag("ListGelarDetail"))
            {
              
            }


            else if(nodeObject.CompareTag("ListPublikasiFakultas"))
            {
              
            }
            else if (nodeObject.CompareTag("ListPublikasiKataKunci"))
            {
              
            }
            else if (nodeObject.CompareTag("ListKataKunciPeneliti"))
            {
               
            }
        }
    }
}
