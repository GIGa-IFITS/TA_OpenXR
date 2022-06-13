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

    public static void SendTouchToVR(string _touch){
        using(PacketNetwork _packet = new PacketNetwork((int)ServerPackets.sendTouch)){
            _packet.Write(_touch);

            SendTCPData(1, _packet);
        }
    }

    public static void SendSwipeToVR(string _swipeType){
        using(PacketNetwork _packet = new PacketNetwork((int)ServerPackets.sendSwipe)){
            _packet.Write(_swipeType);

            SendTCPData(1, _packet);
        }
    }

    public static void SendScrollSpeedToVR(float _scrollSpeed){
        using(PacketNetwork _packet = new PacketNetwork((int)ServerPackets.sendScrollSpeed)){
            _packet.Write(_scrollSpeed);

            SendTCPData(1, _packet);
        }
    }

    public static void SendRotationToVR(float _x, float _y, float _z, float _w){
        using(PacketNetwork _packet = new PacketNetwork((int)ServerPackets.sendRotation)){
            _packet.Write(_x);
            _packet.Write(_y);
            _packet.Write(_z);
            _packet.Write(_w);

            SendTCPData(1, _packet);
        }
    }
    
    #endregion
}
