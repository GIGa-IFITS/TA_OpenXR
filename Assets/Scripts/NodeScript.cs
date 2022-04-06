using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeScript : MonoBehaviour
{
    public void OnHoverNode(){
        Debug.Log("node hover");
        ScreenManager.instance.OnSelectNode(gameObject.GetComponent<NodeVariable>(), false);
    }
    
    public void OnPinchNode(){
        Debug.Log("node pinch");
        ScreenManager.instance.OnSelectNode(gameObject.GetComponent<NodeVariable>(), true);
    }
}
