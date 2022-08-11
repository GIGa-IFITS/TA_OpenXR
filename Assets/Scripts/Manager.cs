using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.Android;
using UnityEngine.XR;

public class Manager : EventHandler
{

    [Header("Koneksi dari unity ke database - Manager")]
    public InputField ip;
    public InputField port;
    public string ipAddress = "";
    public string portServer = "";
    private string param;
    public static Manager instance;
    [Header("Hand, Virtual Smartphone dan Desktop Screen - Manager")]
    [SerializeField] private HandPoseSwitch handPoseSwitch;
    [SerializeField] private GameObject virtualSmartphone;
    [SerializeField] private GameObject desktopScreen;
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
        XRSettings.eyeTextureResolutionScale = 2f;
    }

    public void SubmitIP(string ip){
        ipAddress = ip;
        StartConnection();
    }
    
    public void StartConnection()
    {
        Config config = new Config();
        Debug.Log("Manager -> " +ipAddress +":"+ portServer);
        config.SetUrl(ipAddress);
        config.SetPort(portServer);
        ApplyURL(config);

        // start server
        Server.Start(6000);
    }

    public void Disconnected(){
        disconnectCanvas.SetActive(true);
        Vector3 offset = centerEyeAnchor.transform.forward;
        offset *= 2f;
        disconnectCanvas.transform.position = centerEyeAnchor.transform.position + offset;
        disconnectCanvas.transform.LookAt(disconnectCanvas.transform.position + centerEyeAnchor.transform.rotation * Vector3.forward, centerEyeAnchor.transform.rotation * Vector3.up);
        flushNode();
        virtualSmartphone.SetActive(false);
        desktopScreen.SetActive(false);
        handPoseSwitch.SetStaticHandStatus(false);
    }

    public void SetVirtualSmartphoneCanvasActive(){
        Debug.Log("Set virtual smartphone active");
        virtualSmartphone.SetActive(true);
        disconnectCanvas.SetActive(false);
        handPoseSwitch.SetStaticHandStatus(true);

        ScreenManager.instance.OnTapDashboard();
    }
}
