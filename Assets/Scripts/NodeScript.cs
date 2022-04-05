using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeScript : MonoBehaviour
{
    public void OnHoverNode(){
        ScreenManager.instance.OnSelectNode(gameObject.GetComponent<NodeVariable>(), false);
    }
    
    public void OnPinchNode(){
        ScreenManager.instance.OnSelectNode(gameObject.GetComponent<NodeVariable>(), true);
    }
}
