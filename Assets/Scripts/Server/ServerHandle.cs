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

    }

    // received phone size
    public static void SendPhoneSize(int _fromClient, PacketNetwork _packet){
        int _clientIdCheck = _packet.ReadInt();
        float _screenWidth = _packet.ReadFloat();
        float _screenHeight = _packet.ReadFloat();

        if(_fromClient != _clientIdCheck){
            Debug.Log("SERVER: " + _fromClient + " has assumed wrong client id");
        }
        Debug.Log("SERVER: " + Server.clients[_fromClient].tcp.socket.Client.RemoteEndPoint + " /Smartphone with id " + _fromClient + " sent phone size with width " + _screenWidth + " and height " + _screenHeight + ". Server will now send it to the other client");

        // send phone size from server to VR
        ServerSend.SendPhoneSizeToVR(_screenWidth, _screenHeight);
    }
    
    // received texture from smartphone
    public static void SendTexture(int _fromClient, PacketNetwork _packet){
        int _clientIdCheck = _packet.ReadInt();
        byte[] _textureToSend2D = _packet.ReadBytes(_packet.UnreadLength());

        if(_fromClient != _clientIdCheck){
            Debug.Log("SERVER: " + _fromClient + " has assumed wrong client id");
        }
        Debug.Log("SERVER: " + Server.clients[_fromClient].tcp.socket.Client.RemoteEndPoint + " /Smartphone with id " + _fromClient + " sent texture2d " + _textureToSend2D + " server will now send it to the other client");

        // send texture 2D from server to VR
        ServerSend.SendTextureToVR(_textureToSend2D);
    }

    // received dashboard toggle data from smartphone
    public static void SendDashboardToggle(int _fromClient, PacketNetwork _packet){
        int _clientIdCheck = _packet.ReadInt();
        bool _toggleVal = _packet.ReadBool();

        if(_fromClient != _clientIdCheck){
            Debug.Log("SERVER: " + _fromClient + " has assumed wrong client id");
        }
        Debug.Log("SERVER: " + Server.clients[_fromClient].tcp.socket.Client.RemoteEndPoint + " /Smartphone with id " + _fromClient + " sent dashboard toggle " + _toggleVal + " server will now send it to the other client");

        // send to VR
        ServerSend.SendDashboardToggleToVR(_toggleVal);
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

    // public static void TextureRequested(int _fromClient, PacketNetwork _packet){
    //     int _clientIdCheck = _packet.ReadInt();

    //     if(_fromClient != _clientIdCheck){
    //         Debug.Log("SERVER: " + _fromClient + " has assumed wrong client id");
    //     }
    //     Debug.Log("SERVER: " + Server.clients[_fromClient].tcp.socket.Client.RemoteEndPoint + " /VR with id " + _fromClient + " request for texture");

    //     // request for texture 2D to smartphone
    //     ServerSend.RequestForTexture(_fromClient, "request texture");
    // }

    // // received dashboard data from VR
    // public static void SendDashboardData(int _fromClient, PacketNetwork _packet){
    //     int _clientIdCheck = _packet.ReadInt();

    //     int journals = _packet.ReadInt();
    //     int conferences = _packet.ReadInt();
    //     int books = _packet.ReadInt();
    //     int thesis = _packet.ReadInt();
    //     int patents = _packet.ReadInt();
    //     int research = _packet.ReadInt();

    //     if(_fromClient != _clientIdCheck){
    //         Debug.Log("SERVER: " + _fromClient + " has assumed wrong client id");
    //     }
    //     Debug.Log("SERVER: " + Server.clients[_fromClient].tcp.socket.Client.RemoteEndPoint + " /VR with id " + _fromClient + " sent dashboard data. Server will now send it to the other client");

    //     // send data from server to smartphone
    //     ServerSend.SendDashboardDataToSmartphone(journals, conferences, books, thesis, patents, research);
    // }
}
