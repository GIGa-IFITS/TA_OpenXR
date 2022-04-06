using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualSmartphone : MonoBehaviour
{
    public static VirtualSmartphone instance;
    
    public bool isOrientationUp = false;
    [SerializeField] private Transform defPhoneAnchor;
    [SerializeField] private Transform defPhoneParent;
    [SerializeField] private Transform largePhoneAnchor;
    [SerializeField] private Transform largePhoneParent;

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
            transform.localPosition = largePhoneAnchor.localPosition;
            transform.localRotation = largePhoneAnchor.localRotation;
            transform.localScale = largePhoneAnchor.localScale;
            transform.parent = largePhoneParent;
        }else{
            transform.parent = defPhoneParent;
            transform.localPosition = defPhoneAnchor.localPosition;
            transform.localRotation = defPhoneAnchor.localRotation;
            transform.localScale = defPhoneAnchor.localScale;
        }
    }
}
