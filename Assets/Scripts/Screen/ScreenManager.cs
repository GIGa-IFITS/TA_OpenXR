using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScreenManager : MonoBehaviour
{
    public static ScreenManager instance;
    public SmartphoneScreen smartphoneScreen;
    public DesktopScreen desktopScreen;
    private ButtonPress hoveredButton;
    private NodeVariable currNode = null;
    private NodeVariable prevNode = null;
    private string currTag;
    private string currSearch;
    public TextMeshProUGUI debugText;
    private List<string> researcherData = new List<string>();
    public bool isSearching = false;
    [SerializeField] private GameObject centerEyeAnchor;
    [SerializeField] private HandTrackingUI handTrackingUI;
    public float offset = 30f;
    private bool isSelected;
    private string selectedName;
    private int selectedTotal;
    private string selectedDetail;
    private NodeVariable currentHoverNode;
    private bool isOnCardPage;

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
        currentHoverNode = hoveredButton.gameObject.GetComponent<NodeVariable>();
        if(currentHoverNode == null){
            ResetInfo();
        }
    }

    public void ResetHoverButton(ButtonPress _hoveredButton){
        hoveredButton = null;
    }

    public void ResetInfo(){
        if(isOnCardPage){
            ResetHoverCard();
        }else{
            ResetHoverNode();
        }
    }

    public void TouchButton(){
        Debug.Log("touch detected");
        // if there are any button hovered, touch that button
        if(hoveredButton != null){
            hoveredButton.ButtonPressed();
        }
    }  

    public void OnTapDashboard(){
        smartphoneScreen.OnTapDashboard();
        desktopScreen.OnTapDashboard();
        Manager.instance.Dashboard();
    }

    public void ShowDashboardData(RawData rawdata){
        smartphoneScreen.ShowDashboardData(rawdata);
        desktopScreen.ShowDashboardData(rawdata);
    }

    public void ShowDashboardError(){
        smartphoneScreen.ShowDashboardError();
        desktopScreen.ShowDashboardError();
    }

    public void OnTapSearch(){
        smartphoneScreen.OnTapSearch();
        desktopScreen.OnTapSearch();
    }

    public void OnTapName(){
        isSearching = true;
        smartphoneScreen.OnTapSearchName();
        desktopScreen.OnTapSearchName();
        currSearch = "Researcher Name";
    }

    public void OnTapUnit(){
        isSearching = true;
        smartphoneScreen.OnTapSearchUnit();
        desktopScreen.OnTapSearchUnit();
        currSearch = "Institution Unit";
    }

    public void OnTapDegree(){
        isSearching = true;
        smartphoneScreen.OnTapSearchDegree();
        desktopScreen.OnTapSearchDegree();
        currSearch = "Academic Degree";
    }

    public void OnTapKeyword(){
        isSearching = true;
        smartphoneScreen.OnTapSearchKeyword();
        desktopScreen.OnTapSearchKeyword();
        currSearch = "Research Keyword";
    }

    public void ShowNodeMenu(){
        smartphoneScreen.ShowNodeMenu();
        desktopScreen.ShowNodeMenu();
    }

    public void SetLoadingNodeScreen(bool val){
        smartphoneScreen.SetLoadingNodeScreen(val);
        desktopScreen.SetLoadingNodeScreen(val);
    }

    public void SetDefaultNodeScreen(bool val){
        smartphoneScreen.SetDefaultNodeScreen(val);
        desktopScreen.SetDefaultNodeScreen(val);
    }

    public void SetInfoNodeScreen(bool val){
        smartphoneScreen.SetInfoNodeScreen(val);
        desktopScreen.SetInfoNodeScreen(val);
    }

    public void UpdateNodeInfo(string name, int total, string detail){
        smartphoneScreen.UpdateNodeInfo(name, total, detail);
        desktopScreen.UpdateNodeInfo(name, total, detail);        
    }

    public void SetLoadingCardScreen(bool val){
        smartphoneScreen.SetLoadingCardScreen(val);
        desktopScreen.SetLoadingCardScreen(val);
    }

    public void ShowCardMenu(string name, int total, string detail){
        smartphoneScreen.ShowCardMenu(name, total, detail);
        desktopScreen.ShowCardMenu(name, total, detail);        
    }

    public void UpdateCardInfo(string name, int total, string detail){
        smartphoneScreen.UpdateCardInfo(name, total, detail);
        desktopScreen.UpdateCardInfo(name, total, detail);  
    }

    public void UpdateDetailScreen(){
        smartphoneScreen.UpdateDetailScreen(researcherData);
        desktopScreen.UpdateDetailScreen(researcherData);
    }

    private void ChangeNodeHoverBg(bool val){
        smartphoneScreen.ChangeNodeHoverBg(val);
        desktopScreen.ChangeNodeHoverBg(val);
    }

    private void ChangeCardHoverBg(bool val){
        smartphoneScreen.ChangeCardHoverBg(val);
        desktopScreen.ChangeCardHoverBg(val);
    }

    private void ResetHoverNode(){
        if(isSelected){
            UpdateNodeInfo(selectedName, selectedTotal, selectedDetail);
            ChangeNodeHoverBg(false);
        }else{
            SetDefaultNodeScreen(true);
            SetInfoNodeScreen(false);
            ChangeNodeHoverBg(false);
        }
    }

    private void ResetHoverCard(){
        UpdateCardInfo(selectedName, selectedTotal, selectedDetail);
        ChangeCardHoverBg(false);
    }

    public void OnSelectNode(NodeVariable nodeObject, bool selected, bool swipe){
        if (nodeObject.CompareTag("ListPenelitiAbjad"))
        {
            string nodeId = nodeObject.kode_peneliti;
            string name = nodeObject.nama;
            int total = nodeObject.jumlah;

            // update screen                
            // if select node
            if(selected){
                // update selected value
                selectedName = name;
                selectedTotal = total;
                selectedDetail = "Researchers starting with the alphabet of";
                isSelected = true;
                isOnCardPage = true;
                ChangeCardHoverBg(false);

                // change screen to list of researchers
                ShowCardMenu(selectedName, selectedTotal, selectedDetail);
                SetLoadingCardScreen(true);

                // spawn node
                if(desktopScreen.gameObject.activeSelf){
                    Manager.instance.getPenelitiInisialITS(nodeId);
                }else{
                    Manager.instance.getPenelitiInisialITS2D(nodeId);
                }

                // update currNode and prevNode if not swipe
                if(!swipe){
                    prevNode = currNode;
                    currNode = CopyNode(nodeObject);
                }
            }else{ // only hover
                SetDefaultNodeScreen(false);
                SetInfoNodeScreen(true);
                UpdateNodeInfo(name, total, "Researchers starting with the alphabet of");
                ChangeNodeHoverBg(true);
            }
        }
        else if(nodeObject.CompareTag("ListPenelitiInisial"))
        {
            string nodeId = nodeObject.kode_peneliti;
            string name = nodeObject.nama;
            int total = nodeObject.jumlah;

            Manager.instance.getResearcherDetailData(nodeId);

            // update screen                
            // if select node
            if(selected){
                // show detail screen, update detail screen, nonactive card screen
                UpdateDetailScreen();
                isSearching = false;
            }else{ // only hover
                UpdateCardInfo(name, total, "Publications of");
                ChangeCardHoverBg(true);
            }
        }
        
        else if(nodeObject.CompareTag("ListPenelitiFakultas"))
        {
            string nodeId = nodeObject.kode_peneliti;
            string name = nodeObject.nama;
            int total = nodeObject.jumlah;

            // update screen                
            // if select node
            if(selected){
                // update selected value
                selectedName = name;
                selectedTotal = total;
                selectedDetail = "Researchers in the faculty of";
                isSelected = true;
                isOnCardPage = false;

                ShowNodeMenu();
                SetInfoNodeScreen(false);
                SetLoadingNodeScreen(true);
                UpdateNodeInfo(selectedName, selectedTotal, selectedDetail);
                ChangeNodeHoverBg(false);

                // spawn node
                if(desktopScreen.gameObject.activeSelf){
                    Manager.instance.getPenelitiDepartemenITS(nodeId);
                }else{
                    Manager.instance.getPenelitiDepartemenITS2D(nodeId);
                }

                // update curr node if not swipe
                if(!swipe){
                    currNode = CopyNode(nodeObject);
                }
            }else{ // only hover
                SetDefaultNodeScreen(false);
                SetInfoNodeScreen(true);
                UpdateNodeInfo(name, total,  "Researchers in the faculty of");
                ChangeNodeHoverBg(true);
            }
        }
        else if(nodeObject.CompareTag("ListPenelitiDepartemen"))
        {
            string nodeId = nodeObject.kode_peneliti;
            string name = nodeObject.nama;
            int total = nodeObject.jumlah;

            // update screen                
            // if select node
            if(selected){
                // update selected value
                selectedName = name;
                selectedTotal = total;
                selectedDetail = "Researchers in the department of";
                isSelected = true;
                isOnCardPage = true;
                ChangeCardHoverBg(false);

                // change screen to list of researchers
                ShowCardMenu(selectedName, selectedTotal, selectedDetail);
                SetLoadingCardScreen(true);

                // spawn node
                if(desktopScreen.gameObject.activeSelf){
                    Manager.instance.getPenelitiDepartemenDetailITS(nodeId);
                }else{
                    Manager.instance.getPenelitiDepartemenDetailITS2D(nodeId);
                }

                // update currNode and prevNode if not swipe
                if(!swipe){
                    prevNode = currNode;
                    currNode = CopyNode(nodeObject);
                }
            }else{ // only hover
                SetDefaultNodeScreen(false);
                SetInfoNodeScreen(true);
                UpdateNodeInfo(name, total, "Researchers in the department of");
                ChangeNodeHoverBg(true);
            }
        }
        else if(nodeObject.CompareTag("ListPenelitiDepartemenDetail"))
        {
            string nodeId = nodeObject.kode_peneliti;
            string name = nodeObject.nama;
            int total = nodeObject.jumlah;

            Manager.instance.getResearcherDetailData(nodeId);

            // update screen                
            // if select node
            if(selected){
                // show detail screen, update detail screen, nonactive card screen
                UpdateDetailScreen();
                isSearching = false;
            }else{ // only hover
                UpdateCardInfo(name, total, "Publications of");
                ChangeCardHoverBg(true);
            }
        }
        
        else if(nodeObject.CompareTag("ListGelar"))
        {
            string nodeId = nodeObject.kode_peneliti;
            string name = nodeObject.nama;
            int total = nodeObject.jumlah;

            // update screen                
            // if select node
            if(selected){
                // update selected value
                selectedName = name;
                selectedTotal = total;
                selectedDetail = "Researchers with the academic degree of";
                isSelected = true;
                isOnCardPage = true;
                ChangeCardHoverBg(false);

                // change screen to list of researchers
                ShowCardMenu(selectedName, selectedTotal, selectedDetail);
                SetLoadingCardScreen(true);

                // spawn node
                if(desktopScreen.gameObject.activeSelf){
                    Manager.instance.getGelarPenelitiDetail(nodeId);
                }else{
                    Manager.instance.getGelarPenelitiDetail2D(nodeId);
                }

                // update currNode and prevNode if not swipe
                if(!swipe){
                    prevNode = currNode;
                    currNode = CopyNode(nodeObject);
                }
            }else{ // only hover
                SetDefaultNodeScreen(false);
                SetInfoNodeScreen(true);
                UpdateNodeInfo(name, total, "Researchers with the academic degree of");
                ChangeNodeHoverBg(true);
            }
        }
        else if (nodeObject.CompareTag("ListGelarDetail"))
        {
            string nodeId = nodeObject.kode_peneliti;
            string name = nodeObject.nama;
            int total = nodeObject.jumlah;

            Manager.instance.getResearcherDetailData(nodeId);

            // update screen                
            // if select node
            if(selected){
                // show detail screen, update detail screen, nonactive card screen
                UpdateDetailScreen();
                isSearching = false;
            }else{ // only hover
                UpdateCardInfo(name, total, "Publications of");
                ChangeCardHoverBg(true);
            }
        }


        else if(nodeObject.CompareTag("ListPublikasiFakultas"))
        {
            string nodeId = nodeObject.kode_peneliti;
            string name = nodeObject.nama;
            int total = nodeObject.jumlah;

            // update screen                
            // if select node
            if(selected){
                // update selected value
                selectedName = name;
                selectedTotal = total;
                selectedDetail = "Publications in the faculty of";
                isSelected = true;
                isOnCardPage = false;

                ShowNodeMenu();
                SetInfoNodeScreen(false);
                SetLoadingNodeScreen(true);
                UpdateNodeInfo(selectedName, selectedTotal, selectedDetail);
                ChangeNodeHoverBg(false);

                // spawn node
                if(desktopScreen.gameObject.activeSelf){
                    Manager.instance.getPublikasiKataKunci(nodeId);
                }else{
                    Manager.instance.getPublikasiKataKunci2D(nodeId);
                }

                // update curr node if not swipe
                if(!swipe){
                    currNode = CopyNode(nodeObject);
                }
            }else{ // only hover
                SetDefaultNodeScreen(false);
                SetInfoNodeScreen(true);
                UpdateNodeInfo(name, total, "Publications in the faculty of");
                ChangeNodeHoverBg(true);
            }
        }
        else if (nodeObject.CompareTag("ListPublikasiKataKunci"))
        {
            string nodeId = nodeObject.kode_peneliti;
            string name = nodeObject.nama;
            int total = nodeObject.jumlah;

            // update screen                
            // if select node
            if(selected){
                // update selected value
                selectedName = name;
                selectedTotal = total;
                selectedDetail = "Publications with the keyword";
                isSelected = true;
                isOnCardPage = true;
                ChangeCardHoverBg(false);

                // change screen to list of researchers
                ShowCardMenu(selectedName, selectedTotal, selectedDetail);
                SetLoadingCardScreen(true);

                // spawn node
                if(desktopScreen.gameObject.activeSelf){
                    Manager.instance.getKataKunciPeneliti(nodeId, name);
                }else{
                    Manager.instance.getKataKunciPeneliti2D(nodeId, name);
                }

                // update currNode and prevNode if not swipe
                if(!swipe){
                    prevNode = currNode;
                    currNode = CopyNode(nodeObject);
                }
            }else{ // only hover
                SetDefaultNodeScreen(false);
                SetInfoNodeScreen(true);
                UpdateNodeInfo(name, total, "Publications with the keyword");
                ChangeNodeHoverBg(true);
            }
        }
        else if (nodeObject.CompareTag("ListKataKunciPeneliti"))
        {
            string nodeId = nodeObject.kode_peneliti;
            Debug.Log(nodeId + " <- IdPeneliti");

            string name = nodeObject.nama;
            int total = nodeObject.jumlah;

            Manager.instance.getResearcherDetailData(nodeId);

            // update screen                
            // if select node
            if(selected){
                // show detail screen, update detail screen, nonactive card screen
                UpdateDetailScreen();
                isSearching = false;
            }else{ // only hover
                UpdateCardInfo(name, total, "Publications of");
                ChangeCardHoverBg(true);
            } 
        }
    }

    public void UpdateResearcherDetailData(RawData rawdata){
        researcherData.Clear();
        researcherData.Add(rawdata.data[0].detail_peneliti[0].nama);
        researcherData.Add(rawdata.data[0].detail_peneliti[0].fakultas);
        researcherData.Add(rawdata.data[0].detail_peneliti[0].departemen);

        researcherData.Add(rawdata.data[0].detail_peneliti[0].jurnal.ToString());
        researcherData.Add(rawdata.data[0].detail_peneliti[0].konferensi.ToString());
        researcherData.Add(rawdata.data[0].detail_peneliti[0].buku.ToString());
        researcherData.Add(rawdata.data[0].detail_peneliti[0].tesis.ToString());
        researcherData.Add(rawdata.data[0].detail_peneliti[0].paten.ToString());
        researcherData.Add(rawdata.data[0].detail_peneliti[0].penelitian.ToString());
    }

    public void OnTapBackToSearch(){
        // check if there's a previous node
        if(currNode){
            if(prevNode){
                currNode = prevNode;
                prevNode = null;
                OnSelectNode(currNode, true, false);
            }else if(currSearch == "Researcher Name"){
                currNode = null;
                isSelected = false;
                OnTapName();
            }
            else if(currSearch == "Institution Unit"){
                currNode = null;
                isSelected = false;
                OnTapUnit();
            }
            else if(currSearch == "Academic Degree"){
                currNode = null;
                isSelected = false;
                OnTapDegree();
            }
            else if(currSearch == "Research Keyword"){
                currNode = null;
                isSelected = false;
                OnTapKeyword();
            }
            isOnCardPage = false;
            ResetInfo();
        }
        else{
            // back to search menu
            isSearching = false;
            smartphoneScreen.OnTapBackToSearch();
            desktopScreen.OnTapBackToSearch();
            //ClientSend.SendPageType("searchMenu");
        } 
    }

    public void OnTapCloseDetail(){
        smartphoneScreen.OnTapCloseDetail();
        desktopScreen.OnTapCloseDetail();
        isSearching = true;
        ResetInfo();

        // cek list peneliti null or not, kalau null berarti tadi nge swipe, ganti on select currnode
        if(Manager.instance.IsListPenelitiEmpty()){
            OnSelectNode(currNode, true, true);
        }
        
    }

    public void CheckForNodeSpawn(){
        if(currNode == null){
            if(currSearch == "Researcher Name"){
                OnTapName();
            }
            else if(currSearch == "Institution Unit"){
                OnTapUnit();
            }
            else if(currSearch == "Academic Degree"){
                OnTapDegree();
            }
            else if(currSearch == "Research Keyword"){
                OnTapKeyword();
            }
        }else{
            OnSelectNode(currNode, true, true);
        }
    }

    public NodeVariable CopyNode(NodeVariable nodeObject){
        NodeVariable nodeCopy;
        nodeCopy = UnityEngine.Object.Instantiate(nodeObject);

        // 3d
        if(nodeCopy.ukuran > 0){
            nodeCopy.gameObject.GetComponent<Renderer>().enabled = false;
            nodeCopy.gameObject.GetComponent<SphereCollider>().enabled = false;
        }else{
            nodeCopy.gameObject.GetComponent<Image>().enabled = false;
            nodeCopy.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
        nodeCopy.transform.GetChild(0).gameObject.SetActive(false);

        return nodeCopy;
    }

    public void SetScroll(float scrollSpeed){
        smartphoneScreen.SetScroll(scrollSpeed);
    }

    public void SetScreenMode(string _swipeType){
        if(_swipeType == "up" && smartphoneScreen.gameObject.activeSelf){
            handTrackingUI.SetLaserOn();
            StartCoroutine(SwipeUp());
        }else if(_swipeType == "down" && desktopScreen.gameObject.activeSelf){
            handTrackingUI.SetLaserOff();
            StartCoroutine(SwipeDown());
        }
    }

    IEnumerator SwipeUp() {
        Debug.Log("screen mode desktop");

        // swipe up when detail menu is active, activate first before flushing node
        if(smartphoneScreen.detailMenu.activeSelf){
            smartphoneScreen.cardMenu.SetActive(true);
            yield return StartCoroutine(WaitForFlushNode());
            smartphoneScreen.cardMenu.SetActive(false);
        }else{
            yield return StartCoroutine(WaitForFlushNode());
        }
        
        desktopScreen.gameObject.SetActive(true);
        smartphoneScreen.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        
        //desktopScreen.transform.position = centerEyeAnchor.transform.position + centerEyeAnchor.transform.forward * offset;
        //desktopScreen.transform.LookAt(desktopScreen.transform.position + centerEyeAnchor.transform.rotation * Vector3.forward, centerEyeAnchor.transform.rotation * Vector3.up);

        CheckIfSearching();
    }

    IEnumerator SwipeDown(){
        Debug.Log("screen mode smartphone");

        if(desktopScreen.detailMenu.activeSelf){
            desktopScreen.cardMenu.SetActive(true);
            yield return StartCoroutine(WaitForFlushNode());
            desktopScreen.cardMenu.SetActive(false);
        }else{
            yield return StartCoroutine(WaitForFlushNode());
        }

        desktopScreen.gameObject.SetActive(false);
        smartphoneScreen.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        CheckIfSearching();
    }

    IEnumerator WaitForFlushNode(){
        Manager.instance.flushNode();
        yield return null;
    }

    public void CheckIfSearching(){
        if(isSearching){
            CheckForNodeSpawn();
        }
    }

    public GameObject GetCenterEyeAnchor(){
        return centerEyeAnchor;
    }

    public void PrintDebug(string _msg){
        debugText.text += _msg;
    }

    public void PrintSpatialButtonLog(GameObject currButton){
        Debug.Log("click on " + currButton.name + ",spatial mode");
    }
}
