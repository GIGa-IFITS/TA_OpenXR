using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GestureDetector))]
public class GestureDetectorEditor : Editor
{
    public override void OnInspectorGUI(){
        DrawDefaultInspector();

        GestureDetector gestureDetector = (GestureDetector)target;
        if(GUILayout.Button("Save")){
            gestureDetector.Save();
        }
    }
}
