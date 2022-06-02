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
            Debug.Log("hover on node " + gameObject.name + ",spatial mode");
        }else{
            Debug.Log("hover on node " + gameObject.name + ",smartphone mode");
        } 
        ScreenManager.instance.OnSelectNode(gameObject.GetComponent<NodeVariable>(), false, false);
    }

    public void OnExitHoverNode(){
        if(nodeVariable.ukuran > 0){
            Debug.Log("stop hover on node " + gameObject.name + ",spatial mode");
            renderer.material = nodeVariable.defaultMaterial;
        }else{
            Debug.Log("stop hover on node " + gameObject.name + ",smartphone mode");
        }
    }
    
    public void OnClickNode(){
        if(nodeVariable.ukuran > 0){
            Debug.Log("click on node " + gameObject.name + ",spatial mode");
        }else{
            Debug.Log("click on node " + gameObject.name + ",smartphone mode");
        }
        ScreenManager.instance.OnSelectNode(gameObject.GetComponent<NodeVariable>(), true, false);
    }
}
