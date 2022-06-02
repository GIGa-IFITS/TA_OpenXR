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
        int i = 0;
        while(File.Exists(filename)){
            i++;
            filename = Application.persistentDataPath + "/LogFile" + i.ToString() + ".csv";
        }
        TextWriter tw = new StreamWriter(filename, true);
        tw.WriteLine("timestamp,event,mode");
        tw.Close();
    }

    public void Log(string logString, string stackTrace, LogType type){
        TextWriter tw = new StreamWriter(filename, true);

        tw.WriteLine(System.DateTime.Now + "," + logString);

        tw.Close();
    }
}
