using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DesktopScreen : MonoBehaviour
{
    [SerializeField] private GameObject dashboardMenu;
    [SerializeField] private GameObject searchMenu;
    [SerializeField] private GameObject dashboardPanel;
    [SerializeField] private GameObject dashboardErrorPanel;
    [SerializeField] private GameObject dashboardLoading;
    private TextMeshProUGUI journals;
    private TextMeshProUGUI conferences;
    private TextMeshProUGUI books;
    private TextMeshProUGUI thesis;
    private TextMeshProUGUI patents;
    private TextMeshProUGUI research;
    private TextMeshProUGUI searchNodeTitleText;
    private TextMeshProUGUI searchNodeTotalText;
    private TextMeshProUGUI searchNodeDetailText;
    private TextMeshProUGUI searchNodeNameText;
    private void Start() {
        // dashboard
        journals = dashboardPanel.transform.GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>();
        conferences = dashboardPanel.transform.GetChild(3).GetChild(1).GetComponent<TextMeshProUGUI>();
        books = dashboardPanel.transform.GetChild(3).GetChild(3).GetComponent<TextMeshProUGUI>();
        thesis = dashboardPanel.transform.GetChild(3).GetChild(3).GetComponent<TextMeshProUGUI>();
        patents = dashboardPanel.transform.GetChild(3).GetChild(4).GetComponent<TextMeshProUGUI>();
        research = dashboardPanel.transform.GetChild(3).GetChild(5).GetComponent<TextMeshProUGUI>();

        // search node page
        // searchNodeTitleText = searchNode.transform.GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>();
        // searchNodeTotalText = searchNode.transform.GetChild(4).GetChild(0).GetComponent<TextMeshProUGUI>();
        // searchNodeDetailText = searchNode.transform.GetChild(4).GetChild(1).GetComponent<TextMeshProUGUI>();
        // searchNodeNameText = searchNode.transform.GetChild(4).GetChild(2).GetComponent<TextMeshProUGUI>();

        // // search list page

        // // detail page
        // detFilterText = detailMenu.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        // detNameText = detailPanel.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>();
        // detFacultyText = detailPanel.transform.GetChild(0).GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>();
        // detDeptText = detailPanel.transform.GetChild(0).GetChild(3).GetChild(1).GetComponent<TextMeshProUGUI>();

        // detJournalText = detailPanel.transform.GetChild(1).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();
        // detConferenceText = detailPanel.transform.GetChild(1).GetChild(1).GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>();
        // detBookText = detailPanel.transform.GetChild(1).GetChild(1).GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>();
        // detThesisText = detailPanel.transform.GetChild(1).GetChild(1).GetChild(3).GetChild(1).GetComponent<TextMeshProUGUI>();
        // detPatentText = detailPanel.transform.GetChild(1).GetChild(1).GetChild(4).GetChild(1).GetComponent<TextMeshProUGUI>();
        // detResearchText = detailPanel.transform.GetChild(1).GetChild(1).GetChild(5).GetChild(1).GetComponent<TextMeshProUGUI>();
    
        //ipText.text = "Connected to:" + "\n" + Client.instance.ip;
    }

    public void OnTapDashboard(){
        dashboardLoading.SetActive(true);
        dashboardPanel.SetActive(false);
        dashboardErrorPanel.SetActive(false);
        dashboardMenu.SetActive(true); 
        searchMenu.SetActive(false);     
        Manager.instance.Dashboard();
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
        dashboardErrorPanel.SetActive(true);
        ClientSend.SendPageType("dashboardError");
    }

    public void OnTapSearch(){
        dashboardMenu.SetActive(false);
        searchMenu.SetActive(true);
        ClientSend.SendPageType("searchMenu");
    }

    void OnEnable(){
        searchMenu.SetActive(false);
    }        
}
