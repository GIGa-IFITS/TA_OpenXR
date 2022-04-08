using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VirtualScreen : MonoBehaviour
{
    [SerializeField] protected GameObject dashboardMenu;
    [SerializeField] protected GameObject dashboardPanel;
    [SerializeField] protected GameObject dashboardErrorPanel;
    [SerializeField] protected GameObject dashboardLoading;
    [SerializeField] protected GameObject searchMenu;
    [SerializeField] protected GameObject nodeMenu;
    [SerializeField] protected GameObject nodeMenuDefaultPanel;
    [SerializeField] protected GameObject nodeMenuInfoPanel;
    [SerializeField] protected TextMeshProUGUI journals;
    [SerializeField] protected TextMeshProUGUI conferences;
    [SerializeField] protected TextMeshProUGUI books;
    [SerializeField] protected TextMeshProUGUI thesis;
    [SerializeField] protected TextMeshProUGUI patents;
    [SerializeField] protected TextMeshProUGUI research;
    [SerializeField] protected TextMeshProUGUI nodeMenuTitleText;
    [SerializeField] protected TextMeshProUGUI nodeMenuTotalText;
    [SerializeField] protected TextMeshProUGUI nodeMenuDetailText;
    [SerializeField] protected TextMeshProUGUI nodeMenuNameText;

    public void OnTapDashboard(){
        dashboardLoading.SetActive(true);
        dashboardPanel.SetActive(false);
        dashboardErrorPanel.SetActive(false);
        dashboardMenu.SetActive(true); 
        searchMenu.SetActive(false);     
    }

    public void ShowDashboardData(RawData rawdata){
        dashboardPanel.SetActive(true);
        dashboardLoading.SetActive(false);
        journals.text = rawdata.data[0].dashboard_data[0].hasil_publikasi[0].journals.ToString();
        conferences.text = rawdata.data[0].dashboard_data[0].hasil_publikasi[1].conferences.ToString();
        books.text = rawdata.data[0].dashboard_data[0].hasil_publikasi[2].books.ToString();
        thesis.text = rawdata.data[0].dashboard_data[0].hasil_publikasi[3].thesis.ToString();
        patents.text = rawdata.data[0].dashboard_data[0].hasil_publikasi[4].paten.ToString();
        research.text = rawdata.data[0].dashboard_data[0].hasil_publikasi[5].research.ToString();
    }

    public void ShowDashboardError(){
        dashboardLoading.SetActive(false);
        dashboardPanel.SetActive(false);
        dashboardErrorPanel.SetActive(true);
    }

    public void OnTapSearch(){
        dashboardMenu.SetActive(false);
        searchMenu.SetActive(true);
    }

    public void OnTapStartSearch(string _searchBy){
        searchMenu.SetActive(false);
        nodeMenu.SetActive(true);

        if (_searchBy == "name"){
            nodeMenuTitleText.text = "Searching By:" + "\n" + "Researcher Name";
        }
        else if (_searchBy == "unit"){
            nodeMenuTitleText.text = "Searching By:" + "\n" + "Institution Unit";
        }
        else if (_searchBy == "degree"){
            nodeMenuTitleText.text = "Searching By:" + "\n" + "Academic Degree";
        }
        else if (_searchBy == "keyword"){
            nodeMenuTitleText.text = "Searching By:" + "\n" + "Research Keyword";
        }
    } 

    public void UpdateNodeInfo(string _name, int _total, string _tag, string _nodeId, string _filterName){
        nodeMenuDefaultPanel.SetActive(false);
        nodeMenuInfoPanel.SetActive(true);
        nodeMenuTotalText.text = _total.ToString();
        if(_filterName != "Research Keyword"){
            nodeMenuDetailText.text = "Researchers of";
        }else{
            nodeMenuDetailText.text = "Publications of";
        }
        nodeMenuNameText.text = _name;
    } 

    // public void OnTapNode(string _name, int _total, string _tag, string _nodeId, string _filterName){
    //     nodeMenuDefaultPanel.SetActive(false);
    //     nodeMenuInfoPanel.SetActive(true);


    // }
}
