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

    private void Awake() {
        filename = Application.persistentDataPath + "/LogFile.csv";
        TextWriter tw = new StreamWriter(filename, true);
        tw.WriteLine("timestamp,event,p1,p2,p3");
        tw.Close();
    }

    public void Log(string logString, string stackTrace, LogType type){
        TextWriter tw = new StreamWriter(filename, true);

        tw.WriteLine(System.DateTime.Now + "," + logString);

        tw.Close();
    }
}
