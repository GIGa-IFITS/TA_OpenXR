using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.Android;

public class Manager : EventHandler
{

    [Header("Koneksi dari unity ke database - Manager")]
    public InputField ip;
    public InputField port;
    public string ipAddress = "";
    public string portServer = "";
    private string param;
    public static Manager instance;
    public GameObject vrKeys;

    private void Awake() {
        if (instance == null){
            Debug.Log("client instantiated");
            instance = this;
        }    
        else if(instance != this){
            Debug.Log("Instance already exists, destroying object");
            Destroy(this);
        }
    }

    void Start()
    {
        StartConnection();
    }

    public void SubmitIP(string ip){
        ipAddress = ip;
        StartConnection();
        vrKeys.SetActive(false);
    }
    
    public void StartConnection()
    {
        Config config = new Config();
        Debug.Log("Manager -> " +ipAddress +":"+ portServer);
        config.SetUrl(ipAddress);
        config.SetPort(portServer);
        ApplyURL(config);
        configPanel.SetActive(false);

        // start server
        Server.Start(6000);

        // connect to smartphone
        Client.instance.ConnectToServer();
    }

    public void DashboardToggle()
    {
        dashboardStatus = !dashboardStatus;
        DashboardBar.SetActive(dashboardStatus);
        DashboardBar2.SetActive(dashboardStatus);
        DashboardBar3.SetActive(dashboardStatus);
        DashboardBar4.SetActive(dashboardStatus);

        Debug.Log("dashboard button pressed <- manager");
    }

    public void PenelitianToggle()
    {
        detPenelitiStatus = !detPenelitiStatus;
        DetailPenelitiBar.SetActive(detPenelitiStatus);

        Debug.Log("peneliti button pressed <- eventHandler");
    }
    public void OptionToggle()
    {
        detOptionStatus = !detOptionStatus;
        OptionBar.SetActive(detOptionStatus);

        Debug.Log("option button pressed <- eventHandler");

    }
    public void OpenResearcherDetail()
    {
        DetailPenelitiBar.SetActive(true);
    }

    public void CloseResearcherDetail()
    {
        DetailPenelitiBar.SetActive(false);
    }
}
