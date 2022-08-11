 using UnityEngine;
 using UnityEditor;
 
 public class EditModeFunctions : EditorWindow
 {
    public string _swipeType = "up";
    [MenuItem("Window/Edit Mode Functions")]
    public static void ShowWindow()
    {
        GetWindow<EditModeFunctions>("Edit Mode Functions");
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Run Function"))
        {
            ScreenManager.instance.SetScreenMode(_swipeType);
        }
    }
}