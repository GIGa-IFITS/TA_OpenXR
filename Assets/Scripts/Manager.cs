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
        Client.instance.ConnectToServer();
    }

    public void Disconnected(){
        GameObject playerRef = GameObject.Find("Stage Object/OVRCameraRig");
        if(playerRef == null){
            playerRef = GameObject.Find("Stage Object/[VRSimulator_CameraRig]");
        }

        disconnectCanvas.SetActive(true);
        Vector3 offset = playerRef.transform.forward;
        offset *= 2f;
        disconnectCanvas.transform.position = playerRef.transform.position + offset;
        flushNode();

        // ada tombol yang ngarah ke fungsi StartConnection
        // kalau ip server ganti --> apakah perlu restart server atau otomatis?
    }

    public void SetDisconnectCanvasInactive(){
        disconnectCanvas.SetActive(false);
    }
}
