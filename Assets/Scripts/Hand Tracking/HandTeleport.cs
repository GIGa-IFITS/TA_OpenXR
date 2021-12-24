using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandTeleport : MonoBehaviour
{
    public GameObject cam;
    public void teleportCameraRig(Transform target){
        cam.transform.position = new Vector3(target.position.x, cam.transform.position.y, target.position.z);
        cam.transform.rotation = this.gameObject.transform.rotation;
    }
}
