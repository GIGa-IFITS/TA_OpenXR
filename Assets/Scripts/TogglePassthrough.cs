using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogglePassthrough : MonoBehaviour
{
    [SerializeField] private OVRPassthroughLayer ovrPassthroughLayer;

    public void setPassthroughVisibility(){
        ovrPassthroughLayer.hidden = !ovrPassthroughLayer.hidden;
    }
}
