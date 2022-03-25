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
            Debug.Log("message from smartphone, sending to vr");
            //ServerSend.SendPhoneStatusToVR();
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
        Debug.Log("SERVER: " + Server.clients[_fromClient].tcp.socket.Client.RemoteEndPoint + " /Smartphone with id " + _fromClient + " ask for " + _command + ". Server will now send it to the other client");

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
        Debug.Log("SERVER: " + Server.clients[_fromClient].tcp.socket.Client.RemoteEndPoint + " /VR with id " + _fromClient + " send filter summary " + _name + ". Server will now send it to the other client");

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
        Debug.Log("SERVER: " + Server.clients[_fromClient].tcp.socket.Client.RemoteEndPoint + " /VR with id " + _fromClient + " send researcher id " + _id + ". Server will now send it to the other client");

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
        Debug.Log("SERVER: " + Server.clients[_fromClient].tcp.socket.Client.RemoteEndPoint + " /phone with id " + _fromClient + " request node " + _nodeId + ". Server will now send it to the other client");

        // send to VR
        ServerSend.SendNodeRequestToVR(_nodeId, _nodeId2, _tagName);
    }

    public static void SendErrorMessage(int _fromClient, PacketNetwork _packet){
        int _clientIdCheck = _packet.ReadInt();
        string _errorMsg = _packet.ReadString();

        if(_fromClient != _clientIdCheck){
            Debug.Log("SERVER: " + _fromClient + " has assumed wrong client id");
        }
        Debug.Log("SERVER: " + Server.clients[_fromClient].tcp.socket.Client.RemoteEndPoint + " /VR with id " + _fromClient + " send error message " + _errorMsg + ". Server will now send it to the other client");

        // send to smartphone
        ServerSend.SendErrorMessageToPhone(_errorMsg);
    }

    public static void SendOrientation(int _fromClient, PacketNetwork _packet){
        int _clientIdCheck = _packet.ReadInt();
        bool _isUp = _packet.ReadBool();

        if(_fromClient != _clientIdCheck){
            Debug.Log("SERVER: " + _fromClient + " has assumed wrong client id");
        }
        Debug.Log("SERVER: " + Server.clients[_fromClient].tcp.socket.Client.RemoteEndPoint + " /smartphone with id " + _fromClient + " send orientation " + _isUp + ". Server will now send it to the other client");

        // send to VR
        ServerSend.SendOrientationToVR(_isUp);
    }

    public static void SendNodeSize(int _fromClient, PacketNetwork _packet){
        int _clientIdCheck = _packet.ReadInt();
        float _nodeSize = _packet.ReadFloat();

        if(_fromClient != _clientIdCheck){
            Debug.Log("SERVER: " + _fromClient + " has assumed wrong client id");
        }
        Debug.Log("SERVER: " + Server.clients[_fromClient].tcp.socket.Client.RemoteEndPoint + " /smartphone with id " + _fromClient + " send node size " + _nodeSize + ". Server will now send it to the other client");

        // send to VR
        ServerSend.SendNodeSizeToVR(_nodeSize);
    }

    public static void SendPageType(int _fromClient, PacketNetwork _packet){
        int _clientIdCheck = _packet.ReadInt();
        string _pageType = _packet.ReadString();

        if(_fromClient != _clientIdCheck){
            Debug.Log("SERVER: " + _fromClient + " has assumed wrong client id");
        }
        Debug.Log("SERVER: " + Server.clients[_fromClient].tcp.socket.Client.RemoteEndPoint + " /smartphone with id " + _fromClient + " send page type " + _pageType + ". Server will now send it to the other client");

        // send to smartphone
        ServerSend.SendPageTypeToVR(_pageType);
    }
}
