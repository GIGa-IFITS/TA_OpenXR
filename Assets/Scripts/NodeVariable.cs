using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeVariable : MonoBehaviour
{
    public string kode_alternate;
    public string kode_peneliti;
    public string nama;
    public int jumlah;
    public float ukuran;
    public Vector3 ukuran2;

    private void OnCollisionEnter(Collision other) {
        Debug.Log("collide!" +  other.gameObject.name);
        if(other.gameObject.name == "SmartphoneScreen"){
            other.gameObject.GetComponent<VirtualSmartphoneCollision>().CollideWithNode(this);
        }    
    }
}
