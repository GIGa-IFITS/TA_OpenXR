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
    public MeshRenderer smartphoneRenderer;
    public bool isSimulator;

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
        //smartphoneRenderer = this.GetComponentInParent<MeshRenderer>();
    }

    private void Update(){
        if(!isSimulator){
            bool renderStatus = handReference.IsDataValid && handReference.IsDataHighConfidence;
            smartphoneRenderer.enabled = renderStatus;
            smartphoneScreenRenderer.enabled = renderStatus;
        }
    }

    public void CopyTexture2DToRenderTexture(byte[] _texture2D){
        Texture2D textureReceived2D = new Texture2D(256, 256, TextureFormat.ARGB32, false);
        textureReceived2D.LoadImage(_texture2D);

        // RenderTexture textureReceived = new RenderTexture(textureReceived2D.width, textureReceived2D.height, 16, RenderTextureFormat.ARGB32);

        // // copy from texture 2d to render texture
        // Graphics.Blit(textureReceived2D, textureReceived);

        //smartphoneScreenRenderer.material.mainTexture = textureReceived2D;
        smartphoneScreenRenderer.material.SetTexture("_BaseMap", textureReceived2D);
    }

    public void UpdatePhoneSize(float _width, float _height){
        widthInMeter = 0.0254f * _width;
        heightInMeter = 0.0254f * _height;

        Debug.Log("width in meter: " + widthInMeter + " height in meter: " + heightInMeter);

        //this.transform.parent.transform.localScale = new Vector3(widthInMeter * 40, heightInMeter * 40, 0.3f);
        //this.transform.parent.transform.localScale = new Vector3(widthInMeter, heightInMeter, 0.0084f);
        //this.transform.parent.transform.localRotation = Quaternion.Euler(90, -90, 0);
        //this.transform.parent.transform.Rotate(new Vector3(0, 0, -100));
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
}
