using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VirtualScreen : MonoBehaviour
{
    [Header("Dashboard Menu")]
    [SerializeField] protected GameObject dashboardMenu;
    [SerializeField] protected GameObject dashboardPanel;
    [SerializeField] protected GameObject dashboardErrorPanel;
    [SerializeField] protected GameObject dashboardLoading;
    [SerializeField] protected TextMeshProUGUI journals;
    [SerializeField] protected TextMeshProUGUI conferences;
    [SerializeField] protected TextMeshProUGUI books;
    [SerializeField] protected TextMeshProUGUI thesis;
    [SerializeField] protected TextMeshProUGUI patents;
    [SerializeField] protected TextMeshProUGUI research;
    [Header("Search Menu")]
    [SerializeField] protected GameObject searchMenu;
    [Header("Node Menu")]
    public GameObject nodeMenu;
    [SerializeField] protected GameObject nodeMenuDefaultPanel;
    [SerializeField] protected GameObject nodeMenuInfoPanel;
    [SerializeField] protected GameObject nodeMenuLoadingText;
    [SerializeField] protected TextMeshProUGUI nodeMenuTitleText;
    [SerializeField] protected TextMeshProUGUI nodeMenuTotalText;
    [SerializeField] protected TextMeshProUGUI nodeMenuDetailText;
    [SerializeField] protected TextMeshProUGUI nodeMenuNameText;
    [SerializeField] protected GameObject nodeMenuBackButton;
    [Header("Card Menu")]
    public GameObject cardMenu;
    [SerializeField] protected GameObject cardMenuLoadingText;
    [SerializeField] protected TextMeshProUGUI cardMenuTotalText;
    [SerializeField] protected TextMeshProUGUI cardMenuDetailText;
    [SerializeField] protected TextMeshProUGUI cardMenuNameText;

    [Header("Detail Menu")]
    public GameObject detailMenu;
    [SerializeField] protected TextMeshProUGUI detailName;
    [SerializeField] protected TextMeshProUGUI detailFaculty;
    [SerializeField] protected TextMeshProUGUI detailDept;
    [SerializeField] protected TextMeshProUGUI detailJournals;
    [SerializeField] protected TextMeshProUGUI detailConferences;
    [SerializeField] protected TextMeshProUGUI detailBooks;
    [SerializeField] protected TextMeshProUGUI detailThesis;
    [SerializeField] protected TextMeshProUGUI detailPatents;
    [SerializeField] protected TextMeshProUGUI detailResearch;

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
        cardMenu.SetActive(false);
        nodeMenu.SetActive(true);
        nodeMenuDefaultPanel.SetActive(false);
        nodeMenuLoadingText.SetActive(true);
        nodeMenuInfoPanel.SetActive(false);
        nodeMenuBackButton.SetActive(true);

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

    public void ShowLoadingNodeScreen(){
        nodeMenu.SetActive(true);
        cardMenu.SetActive(false);
        nodeMenuLoadingText.SetActive(true);
        nodeMenuDefaultPanel.SetActive(true);
        nodeMenuInfoPanel.SetActive(false);
    } 

    public void ShowNodeMenu(){
        nodeMenuBackButton.SetActive(true);
        cardMenu.SetActive(false);
    }

    public void SetLoadingNodeScreen(bool val){
        nodeMenuLoadingText.SetActive(val);
    } 

    public void SetDefaultNodeScreen(bool val){
        nodeMenuDefaultPanel.SetActive(val);
    } 

    public void SetInfoNodeScreen(bool val){
        nodeMenuInfoPanel.SetActive(val);
    } 

    public void UpdateNodeInfo(string _name, int _total, string _searchName){
        nodeMenuTotalText.text = _total.ToString();
        if(_searchName == "Research Keyword"){
            nodeMenuDetailText.text = "Publications of";
        }
        else{
            nodeMenuDetailText.text = "Researchers of";
        }
        nodeMenuNameText.text = _name;
    } 

     public void SetLoadingCardScreen(bool val){
        cardMenuLoadingText.SetActive(val);
    } 

    public void ShowCardMenu(string _name, int _total, string _searchName){
        cardMenu.SetActive(true);
        nodeMenuBackButton.SetActive(false);

        cardMenuTotalText.text = _total.ToString();
        if(_searchName == "Research Keyword"){
            cardMenuDetailText.text = "Publications of";
        }
        else{
            cardMenuDetailText.text = "Researchers of";
        }
        cardMenuNameText.text = _name;
    }

    public void UpdateCardInfo(string _name, int _total){
        cardMenuTotalText.text = _total.ToString();
        cardMenuDetailText.text = "Publications of";
        cardMenuNameText.text = _name;
    }

    public void UpdateDetailScreen(List<string> _researcherData){
        cardMenu.SetActive(false);
        detailMenu.SetActive(true);

        detailName.text = _researcherData[0];
        detailFaculty.text = _researcherData[1];
        detailDept.text = _researcherData[2];
        detailJournals.text = _researcherData[3];
        detailConferences.text = _researcherData[4];
        detailBooks.text = _researcherData[5];
        detailThesis.text = _researcherData[6];
        detailPatents.text = _researcherData[7];
        detailResearch.text = _researcherData[8];
    }

    public void OnTapBackToSearch(){
        Manager.instance.flushNode();
        nodeMenu.SetActive(false);
        searchMenu.SetActive(true);
    }

    public void OnTapCloseDetail(){
        detailMenu.SetActive(false);
        cardMenu.SetActive(true);
    }
}
