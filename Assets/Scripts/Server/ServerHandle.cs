using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerHandle
{
    public static void WelcomeReceived(int _fromClient, PacketNetwork _packet){
        int _clientIdCheck = _packet.ReadInt();

        if(_fromClient != _clientIdCheck){
            Debug.Log("SERVER: " + _fromClient + " has assumed wrong client id");
        }
        Debug.Log("SERVER: " + Server.clients[_fromClient].tcp.socket.Client.RemoteEndPoint +  " connected succesfuly");

        // if message is from smartphone, send message to VR
        Manager.instance.SetVirtualSmartphoneCanvasActive();
    }

    public static void TouchReceived(int _fromClient, PacketNetwork _packet){
        string _touch = _packet.ReadString();

        if (_touch == "touch"){
            Debug.Log("touch detected");
            ScreenManager.instance.TouchButton();
        }
    }

    public static void SwipeReceived(int _fromClient, PacketNetwork _packet){
        string _swipeType = _packet.ReadString();
        Debug.Log("receive swipe type " + _swipeType);
        ScreenManager.instance.SetScreenMode(_swipeType);
    }

    public static void ScrollSpeedReceived(int _fromClient, PacketNetwork _packet){
        float _scrollSpeed = _packet.ReadFloat();
        ScreenManager.instance.SetScroll(_scrollSpeed);
    }

    public static void RotationReceived(int _fromClient, PacketNetwork _packet){
        float _x = _packet.ReadFloat();
        float _y = _packet.ReadFloat();
        float _z = _packet.ReadFloat();
        float _w = _packet.ReadFloat();
        SmartphoneGyro.instance.SetPhoneRotation(_x, _y, _z, _w);
    }    
}
