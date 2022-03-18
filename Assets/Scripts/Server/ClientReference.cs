using System;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientReference
{
    public static int dataBufferSize = 4096;

    public int id;
    public TCP tcp;
    public ClientReference(int _clientId){
        id = _clientId;
        tcp = new TCP(id);
    }

    public class TCP{
        public TcpClient socket;
        private readonly int id;
        private NetworkStream stream;
        private PacketNetwork receivedData;
        private byte[] receiveBuffer;

        public TCP (int _id){
            id = _id;
        }

        public void Connect(TcpClient _socket){
            socket = _socket;
            socket.ReceiveBufferSize = dataBufferSize;
            socket.SendBufferSize = dataBufferSize;

            stream = socket.GetStream();

            receivedData = new PacketNetwork();
            receiveBuffer = new byte[dataBufferSize];

            stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallback, null);

            Debug.Log("SERVER: send message to id: " + id);

            if(Manager.instance.ipAddress == ""){
                ServerSend.Welcome(id, "Welcome to the server");
            }else{
                ServerSend.Welcome(id, Manager.instance.URL);
            }
        }

        public void SendData(PacketNetwork _packet){
            try{
                if(socket != null){
                    stream.BeginWrite(_packet.ToArray(), 0, _packet.Length(), null, null);
                }
            }
            catch(Exception _ex){
                Debug.Log("SERVER: Error sending data to client " + id + " via TCP: " + _ex);
            }
        }

        private void ReceiveCallback(IAsyncResult _result){
            try{
                int _byteLength = stream.EndRead(_result);
                if(_byteLength <= 0){
                    Server.clients[id].tcp.Disconnect();
                    return;
                }

                byte[] _data = new byte[_byteLength];
                Array.Copy(receiveBuffer, _data, _byteLength);

                //handle data
                receivedData.Reset(HandleData(_data));
                stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallback, null);

            }
            catch (Exception _ex){
                Debug.Log("SERVER: Error receiving TCP data: " + _ex);
                Server.clients[id].tcp.Disconnect();
            }
        }

        private bool HandleData(byte[] _data){
            int _packetLength = 0;
            
            receivedData.SetBytes(_data);

            // int = 4 bytes, first int is the length of the packet
            if(receivedData.UnreadLength() >= 4){
                _packetLength = receivedData.ReadInt();
                if(_packetLength <= 0){
                    return true; // reset received data
                }
            }

            // if packet still have continuation
            while(_packetLength > 0 && _packetLength <= receivedData.UnreadLength()){
                byte[] _packetBytes = receivedData.ReadBytes(_packetLength);
                ThreadManager.ExecuteOnMainThread(() => {
                    using (PacketNetwork _packet = new PacketNetwork(_packetBytes)){
                        int _packetId = _packet.ReadInt();
                        Server.packetHandlers[_packetId](id, _packet);
                    }
                });

                _packetLength = 0;
                if(receivedData.UnreadLength() >= 4){
                    _packetLength = receivedData.ReadInt();
                    if(_packetLength <= 0){
                        return true; // reset received data
                    }
                }
            }

            if(_packetLength <= 1){
                return true;
            }

            return false;
        }

        public void Disconnect(){
            socket.Close();
            stream = null;
            receivedData = null;
            receiveBuffer = null;
            socket = null;

            ThreadManager.ExecuteOnMainThread(() => {
                Manager.instance.Disconnected();
            });
        }
    }
}
