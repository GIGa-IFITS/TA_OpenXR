using System;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Server
{
    public static int MaxClients = 2;
    public static int Port { get; private set;}
    public static Dictionary<int, ClientReference> clients = new Dictionary<int, ClientReference>();
    public delegate void PacketHandler(int _fromClient, PacketNetwork _packet);
    public static Dictionary<int, PacketHandler> packetHandlers;
    private static TcpListener tcpListener;

    public static void Start(int _port){
        Port = _port;

        InitializeServerData();

        tcpListener = new TcpListener(IPAddress.Any, Port);
        tcpListener.Start();
        tcpListener.BeginAcceptTcpClient(new AsyncCallback(TCPConnectCallback), null);
        
        Debug.Log("SERVER: started on port " + Port);
    }

    private static void TCPConnectCallback(IAsyncResult _result){
        TcpClient _client = tcpListener.EndAcceptTcpClient(_result);
        tcpListener.BeginAcceptTcpClient(new AsyncCallback(TCPConnectCallback), null);
        Debug.Log("SERVER: Incoming connection from " + _client.Client.RemoteEndPoint);

        for(int i=1; i<= MaxClients; i++){
            if(clients[i].tcp.socket == null){
                clients[i].tcp.Connect(_client);
                Debug.Log("SERVER: Connection from " + _client.Client.RemoteEndPoint + " will have id " + i);
                return;
            }
        }

        Debug.Log("SERVER: " + _client.Client.RemoteEndPoint + " failed to connect: Server full");
    }

    private static void InitializeServerData(){
        for(int i=1; i <= MaxClients;i++){
            clients.Add(i, new ClientReference(i));
        }

        packetHandlers = new Dictionary<int, PacketHandler>(){
            { (int)ClientPackets.welcomeReceived, ServerHandle.WelcomeReceived },
            { (int)ClientPackets.sendCommand, ServerHandle.SendCommand },
            { (int)ClientPackets.sendFilterSummary, ServerHandle.SendFilterSummary },
            { (int)ClientPackets.sendResearcherId, ServerHandle.SendResearcherId },
            { (int)ClientPackets.sendNodeRequest, ServerHandle.SendNodeRequest },
            { (int)ClientPackets.sendErrorMessage, ServerHandle.SendErrorMessage },
            { (int)ClientPackets.sendOrientation, ServerHandle.SendOrientation },
            { (int)ClientPackets.sendNodeSize, ServerHandle.SendNodeSize },
            { (int)ClientPackets.sendPageType, ServerHandle.SendPageType },
            { (int)ClientPackets.sendSwipe, ServerHandle.SendSwipe },
            { (int)ClientPackets.sendScrollSpeed, ServerHandle.SendScrollSpeed }
        };
    }
}
