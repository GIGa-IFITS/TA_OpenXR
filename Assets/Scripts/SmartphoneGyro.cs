using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartphoneGyro : MonoBehaviour
{
    public static SmartphoneGyro instance;
    private GameObject gyroControl;
    private Quaternion rot;
    private Quaternion correctionRot;
    public float correctionX;
    public float correctionY;
    public float correctionZ;


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

    private void Start(){
        gyroControl = transform.parent.gameObject;
        correctionRot = new Quaternion(0, 0, 1, 0);
    }

    private void LateUpdate() {
        gyroControl.transform.rotation = Quaternion.Euler(correctionX, correctionY, correctionZ);
    }

    public void SetPhoneRotation(float x, float y, float z, float w){
        rot.x = x;
        rot.y = y;
        rot.z = z;
        rot.w = w;

        //transform.rotation = Quaternion.Euler(correctionX, correctionY, correctionZ) * rot; 
        transform.localRotation = correctionRot * rot;
    }

}

