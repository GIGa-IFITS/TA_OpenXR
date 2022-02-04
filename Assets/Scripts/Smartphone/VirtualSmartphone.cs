using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualSmartphone : MonoBehaviour
{
    public static VirtualSmartphone instance;
    private Renderer smartphoneScreenRenderer;
    private float widthInMeter;
    private float heightInMeter;
    public OVRMeshRenderer handReference;
    public bool isSimulator;
    private Texture2D textureReceived2D;
    private bool isOrientationUp = false;
    [SerializeField] private GameObject largeScreen;

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
        smartphoneScreenRenderer = this.GetComponent<Renderer>();
        textureReceived2D = new Texture2D(2048, 2048, TextureFormat.ARGB32, false);
    }

    private void Update(){
        if(!isSimulator){
            bool renderStatus = handReference.IsDataValid && handReference.IsDataHighConfidence;
            if(isOrientationUp){
                smartphoneScreenRenderer.enabled = false;
                largeScreen.SetActive(true);
            }else{
                smartphoneScreenRenderer.enabled = renderStatus;
                largeScreen.SetActive(false);
            }
        }
    }

    public void CopyTexture2DToRenderTexture(byte[] _texture2D){
        textureReceived2D.LoadImage(_texture2D);
        smartphoneScreenRenderer.material.SetTexture("_BaseMap", textureReceived2D);
    }

    public void UpdatePhoneSize(float _width, float _height){
        widthInMeter = 0.0254f * _width;
        heightInMeter = 0.0254f * _height;

        Manager.instance.SetDisconnectCanvasInactive();

        Debug.Log("width in meter: " + widthInMeter + " height in meter: " + heightInMeter);
    }

    public void PreviousNode(string _id, string _tagName){
        if(_tagName == "ListPenelitiDepartemen"){
            // show faculty list (institution unit filter)
            Manager.instance.getPenelitiDepartemenITS(_id);
        }else if(_tagName == "ListPublikasiKataKunci"){
            // show faculty list (research keyword filter)
            Manager.instance.getPublikasiKataKunci(_id);
        }
    }

    public void UpdateDeviceOrientation(bool _isUp){
        isOrientationUp = _isUp;
    }
}
