using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualSmartphone : MonoBehaviour
{
    public static VirtualSmartphone instance;
    
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
