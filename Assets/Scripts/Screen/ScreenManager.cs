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
    public void OnTapUnit(){
        isSearching = true;
        smartphoneScreen.OnTapSearchUnit();
        desktopScreen.OnTapSearchUnit();
        ClientSend.SendPageType("unit");
        currSearch = "Institution Unit";
    }

    public void ShowDefaultNodeScreen(){
        smartphoneScreen.ShowDefaultNodeScreen();
        desktopScreen.ShowDefaultNodeScreen();
    }

    public void UpdateNodeInfo(string _name, int _total, string _searchName, bool _detail){
        smartphoneScreen.UpdateNodeInfo(_name, _total, _searchName, _detail);
        desktopScreen.UpdateNodeInfo(_name, _total, _searchName, _detail);
        
        // send to phone
    }

    public void UpdateDetailScreen(){
        smartphoneScreen.UpdateDetailScreen(researcherData);
        desktopScreen.UpdateDetailScreen(researcherData);
    }

    public void OnSelectNode(NodeVariable nodeObject, bool selected, bool swipe){
        if (nodeObject.CompareTag("ListPenelitiAbjad"))
        {

        }
        else if(nodeObject.CompareTag("ListPenelitiInisial"))
        {
            
        }
        
        else if(nodeObject.CompareTag("ListPenelitiFakultas"))
        {
            string nodeId = nodeObject.kode_peneliti;
            Debug.Log(nodeId + " <- fakultaspeneliti");

            string name = nodeObject.nama;
            int total = nodeObject.jumlah;

            // update screen                
            // if select node
            if(selected){
                // update node info
                UpdateNodeInfo(name, total, currSearch, false);

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
                UpdateNodeInfo(name, total, currSearch, false);
            }
        }
        else if(nodeObject.CompareTag("ListPenelitiDepartemen"))
        {
            string nodeId = nodeObject.kode_peneliti;
            Debug.Log(nodeId + " <- NodeVariable");

            string name = nodeObject.nama;
            int total = nodeObject.jumlah;

            // update screen                
            // if select node
            if(selected){
                // change screen to list of researchers


                // update node info
                UpdateNodeInfo(name, total, currSearch, false);

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
                UpdateNodeInfo(name, total, currSearch, false);
            }
        }
        else if(nodeObject.CompareTag("ListPenelitiDepartemenDetail"))
        {
            string nodeId = nodeObject.kode_peneliti;
            Debug.Log(nodeId + " <- NodeVariable");

            string name = nodeObject.nama;
            int total = nodeObject.jumlah;

            Manager.instance.getResearcherDetailData(nodeId);

            // update screen                
            // if select node
            if(selected){
                // show detail screen, update detail screen, nonactive node screen
                UpdateDetailScreen();
            }else{ // only hover
                UpdateNodeInfo(name, total, currSearch, true);
            }
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

            }
            else if(currSearch == "Institution Unit"){
                currNode = null;
                OnTapUnit();
            }
            else if(currSearch == "Academic Degree"){
                
            }
            else if(currSearch == "Research Keyword"){
                
            }
        }
        else{
            // back to search menu
            isSearching = false;
            smartphoneScreen.OnTapBackToSearch();
            desktopScreen.OnTapBackToSearch();
            ClientSend.SendPageType("searchMenu");
        } 
    }

    public void OnTapCloseDetail(){
        smartphoneScreen.OnTapCloseDetail();
        desktopScreen.OnTapCloseDetail();
    }

    public void CheckForNodeSpawn(){
        if(currNode == null){
            if(currSearch == "Researcher Name"){

            }
            else if(currSearch == "Institution Unit"){
                OnTapUnit();
            }
            else if(currSearch == "Academic Degree"){
                
            }
            else if(currSearch == "Research Keyword"){
                    
            }
        }else{
            OnSelectNode(currNode, true, true);
        }
    }

    public NodeVariable CopyNode(NodeVariable nodeObject){
        NodeVariable nodeCopy = new NodeVariable();
        nodeCopy = UnityEngine.Object.Instantiate(nodeObject);

        // 3d
        if(nodeCopy.ukuran > 0){
            nodeCopy.GetComponent<Renderer>().enabled = false;
        }else{
            nodeCopy.GetComponent<Image>().enabled = false;
        }
        nodeCopy.transform.GetChild(0).gameObject.SetActive(false);

        return nodeCopy;
    }

    public void SetScreenMode(string _swipeType){
        if(_swipeType == "up" && smartphoneScreen.gameObject.activeSelf){
            StartCoroutine(SwipeUp());
        }else if(_swipeType == "down" && desktopScreen.gameObject.activeSelf){
            StartCoroutine(SwipeDown());
        }
    }

    IEnumerator SwipeUp() {
        Debug.Log("screen mode VR");
        handTrackingUI.SetLaserOn();

        // swipe up when detail menu is active / node menu is nonactive, activate node menu first before flushing node
        if(smartphoneScreen.detailMenu.activeSelf){
            smartphoneScreen.nodeMenu.SetActive(true);
            yield return StartCoroutine(WaitForFlushNode());
            smartphoneScreen.nodeMenu.SetActive(false);
        }else{
            yield return StartCoroutine(WaitForFlushNode());
        }
        
        desktopScreen.gameObject.SetActive(true);
        smartphoneScreen.gameObject.SetActive(false);

        Vector3 offset = centerEyeAnchor.transform.forward;
        offset *= 30f;
        desktopScreen.transform.position = centerEyeAnchor.transform.position + offset;
        desktopScreen.transform.LookAt(desktopScreen.transform.position + centerEyeAnchor.transform.rotation * Vector3.forward, centerEyeAnchor.transform.rotation * Vector3.up);

        CheckIfSearching();
    }

    IEnumerator SwipeDown(){
        Debug.Log("screen mode smartphone");
        handTrackingUI.SetLaserOff();

        yield return StartCoroutine(WaitForFlushNode());

        desktopScreen.gameObject.SetActive(false);
        smartphoneScreen.gameObject.SetActive(true);
        CheckIfSearching();
    }

    IEnumerator WaitForFlushNode(){
        Manager.instance.flushNode();
        debugText.text += "node flushed!\n";
        yield return null;
    }

    public void CheckIfSearching(){
        if(ScreenManager.instance.isSearching){
            ScreenManager.instance.CheckForNodeSpawn();
        }
    }

    public void PrintDebug(string _msg){
        debugText.text += _msg;
    }
}
