﻿using System.Collections;
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
    public static void SendCommand(int _fromClient, PacketNetwork _packet){
        int _clientIdCheck = _packet.ReadInt();
        string _command = _packet.ReadString();

        if(_fromClient != _clientIdCheck){
            Debug.Log("SERVER: " + _fromClient + " has assumed wrong client id");
        }
        // send to VR
        ServerSend.SendCommandToVR(_command);
    }


    public static void SendFilterSummary(int _fromClient, PacketNetwork _packet){
        int _clientIdCheck = _packet.ReadInt();
        string _name = _packet.ReadString();
        int _total = _packet.ReadInt();
        string _tag = _packet.ReadString();
        string _nodeId = _packet.ReadString();
        string _filterName = _packet.ReadString();

        if(_fromClient != _clientIdCheck){
            Debug.Log("SERVER: " + _fromClient + " has assumed wrong client id");
        }
        // send to smartphone
        ServerSend.SendFilterToPhone(_name, _total, _tag, _nodeId, _filterName);
    }

    public static void SendResearcherId(int _fromClient, PacketNetwork _packet){
        int _clientIdCheck = _packet.ReadInt();
        string _id = _packet.ReadString();
        string _filterName = _packet.ReadString();

        if(_fromClient != _clientIdCheck){
            Debug.Log("SERVER: " + _fromClient + " has assumed wrong client id");
        }
        // send to smartphone
        ServerSend.SendResearcherIdToPhone(_id, _filterName);
    }

    public static void SendNodeRequest(int _fromClient, PacketNetwork _packet){
        int _clientIdCheck = _packet.ReadInt();
        string _nodeId = _packet.ReadString();
        string _nodeId2 = _packet.ReadString();
        string _tagName = _packet.ReadString();

        if(_fromClient != _clientIdCheck){
            Debug.Log("SERVER: " + _fromClient + " has assumed wrong client id");
        }
        // send to VR
        ServerSend.SendNodeRequestToVR(_nodeId, _nodeId2, _tagName);
    }

    public static void SendErrorMessage(int _fromClient, PacketNetwork _packet){
        int _clientIdCheck = _packet.ReadInt();
        string _errorMsg = _packet.ReadString();

        if(_fromClient != _clientIdCheck){
            Debug.Log("SERVER: " + _fromClient + " has assumed wrong client id");
        }
        // send to smartphone
        ServerSend.SendErrorMessageToPhone(_errorMsg);
    }

    public static void SendOrientation(int _fromClient, PacketNetwork _packet){
        int _clientIdCheck = _packet.ReadInt();
        bool _isUp = _packet.ReadBool();

        if(_fromClient != _clientIdCheck){
            Debug.Log("SERVER: " + _fromClient + " has assumed wrong client id");
        }
        // send to VR
        ServerSend.SendOrientationToVR(_isUp);
    }

    public static void SendNodeSize(int _fromClient, PacketNetwork _packet){
        int _clientIdCheck = _packet.ReadInt();
        float _nodeSize = _packet.ReadFloat();

        if(_fromClient != _clientIdCheck){
            Debug.Log("SERVER: " + _fromClient + " has assumed wrong client id");
        }
        // send to VR
        ServerSend.SendNodeSizeToVR(_nodeSize);
    }

    public static void SendPageType(int _fromClient, PacketNetwork _packet){
        int _clientIdCheck = _packet.ReadInt();
        string _pageType = _packet.ReadString();

        if(_fromClient != _clientIdCheck){
            Debug.Log("SERVER: " + _fromClient + " has assumed wrong client id");
        }
        // send to smartphone
        ServerSend.SendPageTypeToPhone(_pageType);
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
