using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SmartphoneScreen : MonoBehaviour
{
    public static SmartphoneScreen instance;
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
    private void Start() {
        // dashboard
        journals = dashboardMenu.transform.GetChild(2).GetChild(1).GetChild(1).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();
        conferences = dashboardMenu.transform.GetChild(2).GetChild(1).GetChild(1).GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>();
        books = dashboardMenu.transform.GetChild(2).GetChild(1).GetChild(1).GetChild(2).GetChild(2).GetComponent<TextMeshProUGUI>();
        thesis = dashboardMenu.transform.GetChild(2).GetChild(1).GetChild(1).GetChild(2).GetChild(3).GetComponent<TextMeshProUGUI>();
        patents = dashboardMenu.transform.GetChild(2).GetChild(1).GetChild(1).GetChild(2).GetChild(4).GetComponent<TextMeshProUGUI>();
        research = dashboardMenu.transform.GetChild(2).GetChild(1).GetChild(1).GetChild(2).GetChild(5).GetComponent<TextMeshProUGUI>();

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
        ClientSend.SendPageType("dashboardMenu");
      
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
        ClientSend.SendPageType("dashboardData");
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

    public void SetHoverButton(ButtonPress _hoveredButton){
        hoveredButton = _hoveredButton;
        HoverButtonReset(); 
    }

    IEnumerator HoverButtonReset(){
        // reset hovered button after 0.5f
        yield return new WaitForSeconds(0.5f);
        hoveredButton = null;
    }

    public void TouchButton(){
        Debug.Log("touch button called");
        // if there are any button hovered, touch that button
        if(hoveredButton != null){
            hoveredButton.ButtonPressed();
        }
    }          
}
