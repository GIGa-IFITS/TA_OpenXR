using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class WriteDebugFile : MonoBehaviour
{
    string filename = "";

    private void OnEnable() {
        Application.logMessageReceived += Log;
    }

    private void OnDisable() {
        Application.logMessageReceived -= Log;
    }

    private void Start() {
        filename = Application.persistentDataPath + "/LogFile.txt";
    }

    public void Log(string logString, string stackTrace, LogType type){
        TextWriter tw = new StreamWriter(filename, true);

        tw.WriteLine("[" + System.DateTime.Now + "]" + logString);

        tw.Close();
    }
}
