using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerSend
{
    private static void SendTCPData(int _toClient, PacketNetwork _packet){
        _packet.WriteLength();
        Server.clients[_toClient].tcp.SendData(_packet);
    }

    #region Packets
    public static void Welcome(int _toClient, string _msg){
        using (PacketNetwork _packet = new PacketNetwork((int)ServerPackets.welcome)){
            _packet.Write(_msg);
            _packet.Write(_toClient);

            SendTCPData(_toClient, _packet);
        }
    }

    public static void SendPhoneSizeToVR(float _screenWidth, float _screenHeight){
        using (PacketNetwork _packet = new PacketNetwork((int)ServerPackets.sendPhoneSize)){
            _packet.Write(_screenWidth);
            _packet.Write(_screenHeight);

            SendTCPData(1, _packet);
        }
    }

    public static void SendTextureToVR(byte[] _textureToSend2D){
        using(PacketNetwork _packet = new PacketNetwork((int)ServerPackets.sendTexture)){
            _packet.Write(_textureToSend2D);

            SendTCPData(1, _packet);
        }
    }

    public static void SendDashboardToggleToVR(bool _on){
        using(PacketNetwork _packet = new PacketNetwork((int)ServerPackets.sendDashboardToggle)){
            _packet.Write(_on);

            SendTCPData(1, _packet);
        }
    }

    public static void SendCommandToVR(string _command){
        using(PacketNetwork _packet = new PacketNetwork((int)ServerPackets.sendCommand)){
            _packet.Write(_command);

            SendTCPData(1, _packet);
        }
    }

    public static void SendFilterToPhone(string _name, int _total, string _tag, string _nodeId, string _filterName){
        using(PacketNetwork _packet = new PacketNetwork((int)ServerPackets.sendFilterSummary)){
            _packet.Write(_name);
            _packet.Write(_total);
            _packet.Write(_tag);
            _packet.Write(_nodeId);
            _packet.Write(_filterName);

            SendTCPData(2, _packet);
        }
    }

    public static void SendResearcherIdToPhone(string _id, string _filterName){
        using(PacketNetwork _packet = new PacketNetwork((int)ServerPackets.sendResearcherId)){
            _packet.Write(_id);
            _packet.Write(_filterName);

            SendTCPData(2, _packet);
        }
    }

    public static void SendNodeRequestToVR(string _nodeId, string _nodeId2, string _tagName){
        using(PacketNetwork _packet = new PacketNetwork((int)ServerPackets.sendNodeRequest)){
            _packet.Write(_nodeId);
            _packet.Write(_nodeId2);
            _packet.Write(_tagName);

            SendTCPData(1, _packet);
        }
    }

    public static void SendErrorMessageToPhone(string _errorMsg){
        using(PacketNetwork _packet = new PacketNetwork((int)ServerPackets.sendErrorMessage)){
            _packet.Write(_errorMsg);

            SendTCPData(2, _packet);
        }
    }

    public static void SendOrientationToVR(bool _isUp){
        using(PacketNetwork _packet = new PacketNetwork((int)ServerPackets.sendOrientation)){
            _packet.Write(_isUp);

            SendTCPData(1, _packet);
        }
    }

    public static void SendNodeSizeToVR(float _nodeSize){
        using(PacketNetwork _packet = new PacketNetwork((int)ServerPackets.sendNodeSize)){
            _packet.Write(_nodeSize);

            SendTCPData(1, _packet);
        }
    }

    // public static void RequestForTexture(int _fromClient, string _msg){
    //     using (PacketNetwork _packet = new PacketNetwork((int)ServerPackets.textureRequest)){
    //         _packet.Write(_msg);

    //         SendTCPDataToOtherClient(_fromClient, _packet);
    //     }
    // }

    // public static void SendDashboardDataToSmartphone(int journals, int conferences, int books, int thesis, int patents, int research){
    //     using(PacketNetwork _packet = new PacketNetwork((int)ServerPackets.sendDashboardData)){
    //         _packet.Write(journals);
    //         _packet.Write(conferences);
    //         _packet.Write(books);
    //         _packet.Write(thesis);
    //         _packet.Write(patents);
    //         _packet.Write(research);

    //         SendTCPData(2, _packet);
    //     }
    // }
    
    #endregion
}
