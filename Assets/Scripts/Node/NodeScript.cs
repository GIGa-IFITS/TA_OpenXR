using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NodeScript : MonoBehaviour
{
    Renderer renderer;
    NodeVariable nodeVariable;
    public void OnHoverNode(){
        nodeVariable = GetComponent<NodeVariable>();

        // if 3d, change material
        if(nodeVariable.ukuran > 0){
            renderer = GetComponent<Renderer>();
            renderer.material = nodeVariable.hoverMaterial;
        }
        ScreenManager.instance.OnSelectNode(gameObject.GetComponent<NodeVariable>(), false, false);
    }

    public void OnExitHoverNode(){
        // always 3d, 2d already auto handled by button component
        renderer.material = GetComponent<NodeVariable>().defaultMaterial;
    }
    
    public void OnClickNode(){
        ScreenManager.instance.OnSelectNode(gameObject.GetComponent<NodeVariable>(), true, false);
    }
}
