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
        if(_fromClient == 2){
            Manager.instance.SetVirtualSmartphoneCanvasActive();
        }
    }

    // send filter type to spawn first level node
    public static void SendTouch(int _fromClient, PacketNetwork _packet){
        int _clientIdCheck = _packet.ReadInt();
        string _touch = _packet.ReadString();

        if(_fromClient != _clientIdCheck){
            Debug.Log("SERVER: " + _fromClient + " has assumed wrong client id");
        }
        // send to VR
        ServerSend.SendTouchToVR(_touch);
    }

    public static void SendSwipe(int _fromClient, PacketNetwork _packet){
        int _clientIdCheck = _packet.ReadInt();
        string _swipeType = _packet.ReadString();

        if(_fromClient != _clientIdCheck){
            Debug.Log("SERVER: " + _fromClient + " has assumed wrong client id");
        }
        // send to smartphone
        ServerSend.SendSwipeToVR(_swipeType);
    }

    public static void SendScrollSpeed(int _fromClient, PacketNetwork _packet){
        int _clientIdCheck = _packet.ReadInt();
        float _scrollSpeed = _packet.ReadFloat();

        if(_fromClient != _clientIdCheck){
            Debug.Log("SERVER: " + _fromClient + " has assumed wrong client id");
        }
        // send to VR
        ServerSend.SendScrollSpeedToVR(_scrollSpeed);
    }

    public static void SendRotation(int _fromClient, PacketNetwork _packet){
        int _clientIdCheck = _packet.ReadInt();
        float _x = _packet.ReadFloat();
        float _y = _packet.ReadFloat();
        float _z = _packet.ReadFloat();
        float _w = _packet.ReadFloat();

        if(_fromClient != _clientIdCheck){
            Debug.Log("SERVER: " + _fromClient + " has assumed wrong client id");
        }
        // send to VR
        ServerSend.SendRotationToVR(_x, _y, _z, _w);
    }    
}
