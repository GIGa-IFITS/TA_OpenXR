using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualSmartphone : MonoBehaviour
{
    public static VirtualSmartphone instance;
    
    private bool isOrientationUp = false;
    [SerializeField] private Transform defPhoneAnchor;
    [SerializeField] private Transform largePhoneAnchor;
    [SerializeField] private GameObject playerRef;
    [Header("Threshold")]
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float minY;
    [SerializeField] private float maxY;
    [SerializeField] private float minZ;
    [SerializeField] private float maxZ;
    private Vector3 rotation;


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

    void Update(){
        //always face user
        //this.transform.LookAt(this.transform.position + playerRef.transform.rotation * Vector3.forward, playerRef.transform.rotation * Vector3.up);

        rotation = transform.eulerAngles;
        rotation.x = Mathf.Clamp(rotation.x, minX, maxX);
        rotation.y = Mathf.Clamp(rotation.y, minY, maxY);
        rotation.z = Mathf.Clamp(rotation.z, minZ, maxZ);

        transform.eulerAngles = rotation;
        //Debug.Log("euler angles " + transform.eulerAngles);
    }

    public void SetDeviceOrientation(bool _isUp){
        isOrientationUp = _isUp;
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
