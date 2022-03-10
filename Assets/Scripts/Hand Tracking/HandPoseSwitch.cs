using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPoseSwitch : MonoBehaviour
{
    private SkinnedMeshRenderer skinnedMeshRenderer;
    [SerializeField] private GameObject handPose;
    void Start()
    {
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
    }

    public void SetStaticHandStatus(bool val){
        handPose.SetActive(val);
        skinnedMeshRenderer.enabled = !val;
    }
}
