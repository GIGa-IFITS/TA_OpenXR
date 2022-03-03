using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VirtualSmartphoneCanvas : MonoBehaviour
{
    public static VirtualSmartphoneCanvas instance;
    [SerializeField] private GameObject dashboardMenu;
    [SerializeField] private GameObject filterMenu;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject dashboardPanel;
    [SerializeField] private GameObject dashboardErrorPanel;
    [SerializeField] private GameObject dashboardLoading;
    [SerializeField] private GameObject illustMenu;
    [SerializeField] private GameObject illustPanel;
    [SerializeField] private GameObject illustErrorPanel;
    [SerializeField] private GameObject summaryMenu;
    [SerializeField] private GameObject summaryPanel;
    [SerializeField] private GameObject summaryErrorPanel;
    [SerializeField] private GameObject detailMenu;
    private TextMeshProUGUI journals;
    private TextMeshProUGUI conferences;
    private TextMeshProUGUI books;
    private TextMeshProUGUI thesis;
    private TextMeshProUGUI patents;
    private TextMeshProUGUI research;
    private TextMeshProUGUI illustFilterText;
    private TextMeshProUGUI illustInstructionText;
    private TextMeshProUGUI summaryFilterText;
    private TextMeshProUGUI summaryNameText;
    private TextMeshProUGUI summaryTotalText;
    private TextMeshProUGUI summaryVariableText;
    private TextMeshProUGUI summaryInstructionText;
    private TextMeshProUGUI detFilterText;
    private TextMeshProUGUI detNameText;
    private TextMeshProUGUI detFacultyText;
    private TextMeshProUGUI detDeptText;
    private TextMeshProUGUI detJournalText;
    private TextMeshProUGUI detConferenceText;
    private TextMeshProUGUI detBookText;
    private TextMeshProUGUI detThesisText;
    private TextMeshProUGUI detPatentText;
    private TextMeshProUGUI detResearchText;
    [SerializeField] private GameObject detailPanel;
    [SerializeField] private GameObject detailErrorPanel;
    [SerializeField] private GameObject detailLoading;
    [SerializeField] private TextMeshProUGUI nodeSizeText;
    [SerializeField] private TextMeshProUGUI ipText;
    [SerializeField] private Button addNodeSizeBtn;
    [SerializeField] private Button subtractNodeSizeBtn;
    private string prevNodeName;
    private string prevNodeTotal;
    private string prevNodeId;
    private string currFilter;
    private string currTag;
    private string currId;
    private string currId2;
    private string currResearcherId;
    public bool isSimulator;
    private bool isOrientationUp = false;
    [SerializeField] private Transform defPhoneAnchor;
    [SerializeField] private Transform largePhoneAnchor;

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

        // filter illust page
        illustFilterText = illustMenu.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        illustInstructionText = illustMenu.transform.GetChild(3).GetChild(2).GetComponent<TextMeshProUGUI>();

        // summary page
        summaryFilterText = summaryMenu.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        summaryNameText = summaryMenu.transform.GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>();
        summaryTotalText = summaryMenu.transform.GetChild(3).GetChild(1).GetComponent<TextMeshProUGUI>();
        summaryVariableText = summaryMenu.transform.GetChild(3).GetChild(2).GetComponent<TextMeshProUGUI>();
        summaryInstructionText = summaryMenu.transform.GetChild(3).GetChild(3).GetComponent<TextMeshProUGUI>();

        // detail page
        detFilterText = detailMenu.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        detNameText = detailPanel.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>();
        detFacultyText = detailPanel.transform.GetChild(0).GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>();
        detDeptText = detailPanel.transform.GetChild(0).GetChild(3).GetChild(1).GetComponent<TextMeshProUGUI>();

        detJournalText = detailPanel.transform.GetChild(1).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();
        detConferenceText = detailPanel.transform.GetChild(1).GetChild(1).GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>();
        detBookText = detailPanel.transform.GetChild(1).GetChild(1).GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>();
        detThesisText = detailPanel.transform.GetChild(1).GetChild(1).GetChild(3).GetChild(1).GetComponent<TextMeshProUGUI>();
        detPatentText = detailPanel.transform.GetChild(1).GetChild(1).GetChild(4).GetChild(1).GetComponent<TextMeshProUGUI>();
        detResearchText = detailPanel.transform.GetChild(1).GetChild(1).GetChild(5).GetChild(1).GetComponent<TextMeshProUGUI>();
    
        ipText.text = "Connected to:" + "\n" + Client.instance.ip;
    }

    private void Update(){
        if(!isSimulator){
            if(isOrientationUp){
                this.transform.localPosition = largePhoneAnchor.localPosition;
                this.transform.localRotation = largePhoneAnchor.localRotation;
                this.transform.localScale = largePhoneAnchor.localScale;
            }else{
                this.transform.localPosition = defPhoneAnchor.localPosition;
                this.transform.localRotation = defPhoneAnchor.localRotation;
                this.transform.localScale = defPhoneAnchor.localScale;
            }
        }
    }

    public void ChangeMenuScreen(string _pageType){
        switch (_pageType) {
            case "dashboardMenu":
                dashboardLoading.SetActive(true);
                dashboardPanel.SetActive(false);
                dashboardErrorPanel.SetActive(false);
                dashboardMenu.SetActive(true);
                filterMenu.SetActive(false);
                settingsMenu.SetActive(false);
                break;

            case "dashboardData":
                Manager.instance.Dashboard();
                break;

            case "dashboardError":
                dashboardLoading.SetActive(false);
                dashboardPanel.SetActive(false);
                dashboardErrorPanel.SetActive(true);
                break;
    
            case "filterMenu":
                dashboardMenu.SetActive(false);
                filterMenu.SetActive(true);
                settingsMenu.SetActive(false);
                break;

            case "settingsMenu":
                dashboardMenu.SetActive(false);
                filterMenu.SetActive(false);
                settingsMenu.SetActive(true);
                break;

            case "backToSummaryMenu":
                detailMenu.SetActive(false);
                summaryMenu.SetActive(true);
                break;
            
            case "backToFilterMenu":
                illustMenu.SetActive(false);
                summaryMenu.SetActive(false);
                filterMenu.SetActive(true);
                break;

            case "backToPrevNode":
                // still in summary menu, send prev id, current tag
                if(currTag == "ListPenelitiDepartemen"){
                    currTag = "ListPenelitiFakultas";
                }
                else{
                    currTag = "ListPublikasiFakultas";
                }
                currId = prevNodeId;
                summaryNameText.text = prevNodeName;
                summaryTotalText.text = prevNodeTotal;
                break;

            case "retryFilter":
                // summary menu
                summaryPanel.SetActive(true);
                summaryErrorPanel.SetActive(false);
                break;

            case "researcherDetailData":
                detailLoading.SetActive(false);
                detailPanel.SetActive(true);
                detailErrorPanel.SetActive(false);
                break;

            case "retryDetail":
                detailLoading.SetActive(true);
                detailPanel.SetActive(false);
                detailErrorPanel.SetActive(false);
                Manager.instance.getResearcherDetailData(currResearcherId);
                break;

            case "errorDetail":
                detailLoading.SetActive(false);
                detailPanel.SetActive(false);
                detailErrorPanel.SetActive(true);
                break;
        }
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

    public void UpdateScreenFilter(string _filter){
        filterMenu.SetActive(false);
        summaryMenu.SetActive(false);
        illustMenu.SetActive(true);
        illustPanel.SetActive(true);
        illustErrorPanel.SetActive(false);

        currFilter = _filter;
        currTag = "none";

        if(_filter == "name"){
            illustFilterText.text = "Filtering By:" + "\n" + "Researcher Name";
            illustInstructionText.text = "Try knocking your phone to nearby node to view researchers of the same initial.";
        }
        else if(_filter == "unit"){
            illustFilterText.text = "Filtering By:" + "\n" + "Institution Unit";
            illustInstructionText.text = "Try knocking your phone to nearby node to view departements of the faculty.";
        }
        else if(_filter == "degree"){
            illustFilterText.text = "Filtering By:" + "\n" + "Academic Degree";
            illustInstructionText.text = "Try knocking your phone to nearby node to view researcher of the selected academic degree.";
        }
        else if(_filter == "keyword"){
            illustFilterText.text = "Filtering By:" + "\n" + "Research Keyword";
            illustInstructionText.text = "Try knocking your phone to nearby node to view research keywords of the faculty.";
        }
    }

    public void UpdateScreenSummary(string _name, int _total, string _tag, string _nodeId, string _filterName){
        illustMenu.SetActive(false);
        summaryMenu.SetActive(true);
        summaryPanel.SetActive(true);
        summaryErrorPanel.SetActive(false);

        summaryFilterText.text = "Filtering By:" + "\n" + _filterName;
        summaryNameText.text = _name;
        summaryTotalText.text = _total.ToString();

        if(_filterName == "Research Keyword"){
            summaryVariableText.text = "Publications";
        }else{
            summaryVariableText.text = "Researchers";
        }

        if(_tag == "ListPenelitiFakultas"){
            summaryInstructionText.text = "Try knocking your phone to nearby node to view researchers of the departement.";
            prevNodeName = _name;
            prevNodeId = _nodeId;
            prevNodeTotal = _total.ToString();
        }
        else if(_tag == "ListPublikasiFakultas"){
            summaryInstructionText.text = "Try knocking your phone to nearby node to view researchers with research keyword.";
            prevNodeName = _name;
            prevNodeId = _nodeId;
            prevNodeTotal = _total.ToString();
        }
        else{
            summaryInstructionText.text = "Try knocking your phone to nearby node to view detail of the researcher.";
        }

        currId = _nodeId;
        currTag = _tag;

        if(_tag == "ListPublikasiKataKunci"){
            currId2 = _name;
        }else{
            currId2 = "0";
        }
    }

    public void UpdateScreenResearcherDetail(string _id, string _filterName){
        summaryMenu.SetActive(false);
        detailMenu.SetActive(true);
        detailLoading.SetActive(true);
        detailPanel.SetActive(false);
        detailErrorPanel.SetActive(false);

        detFilterText.text = "Filtering By:" + "\n" + _filterName;
        currResearcherId = _id;

        Manager.instance.getResearcherDetailData(_id);
    }

    public void ShowResearcherDetail(RawData rawdata){
        detailPanel.SetActive(true);
        detNameText.text = rawdata.data[0].detail_peneliti[0].nama;
        detFacultyText.text = rawdata.data[0].detail_peneliti[0].fakultas;
        detDeptText.text = rawdata.data[0].detail_peneliti[0].departemen;

        detJournalText.text = rawdata.data[0].detail_peneliti[0].jurnal.ToString();
        detConferenceText.text =  rawdata.data[0].detail_peneliti[0].konferensi.ToString();
        detBookText.text = rawdata.data[0].detail_peneliti[0].buku.ToString();
        detThesisText.text = rawdata.data[0].detail_peneliti[0].tesis.ToString();
        detPatentText.text = rawdata.data[0].detail_peneliti[0].paten.ToString();
        detResearchText.text = rawdata.data[0].detail_peneliti[0].penelitian.ToString();
        detailPanel.SetActive(false);
    }

    public void ShowErrorResearcherDetail(){
        detailLoading.SetActive(false);
        detailPanel.SetActive(false);
        detailErrorPanel.SetActive(true);
    }

    public void UpdateScreenSettingsNode(float _nodeSize){
        nodeSizeText.text = _nodeSize.ToString();

        if(_nodeSize == 1){
            subtractNodeSizeBtn.interactable = false;
        }else if(_nodeSize == 5){
            addNodeSizeBtn.interactable = false;
        }else{
            subtractNodeSizeBtn.interactable = true;
            addNodeSizeBtn.interactable = true;
        }
    }

    public void ShowErrorScreen(){
        if(illustMenu.activeSelf){
            illustErrorPanel.SetActive(true);
            illustPanel.SetActive(false);
        }else{
            summaryErrorPanel.SetActive(true);
            summaryPanel.SetActive(false);
        }
    }

    public void UpdateDeviceOrientation(bool _isUp){
        isOrientationUp = _isUp;
    }
}
