using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config
{
    private string URL = "127.0.0.1";
    private string port = "5000";
    private string webAPI;

    public void SetUrl(string URL)
    {
        this.URL = URL;
    }

    public string GetUrl()
    {
        return this.URL;
    }

    public void SetPort(string port)
    {
        this.port = port;
    }

    public string GetPort()
    {
        return this.port;
    }

    public string GetWebAPI()
    {
        string prefix = "https://";
        this.webAPI = prefix + URL + ":" + port;
        return this.webAPI;
    }
}