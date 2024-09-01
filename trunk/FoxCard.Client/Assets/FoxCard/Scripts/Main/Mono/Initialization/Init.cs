using System;
using System.Collections;
using System.Collections.Generic;
using Best.HTTP.Shared;
using Best.HTTP.Shared.Extensions;
using Best.SignalR;
using Best.SignalR.Authentication;
using Best.SignalR.Encoders;
using Best.SignalR.Messages;
using Best.WebSockets;
using UnityEngine;

public class Init : MonoBehaviour
{
    // Start is called before the first frame update
    private HubConnection hub;

    private string url1 = "https://192.168.28.112:7176/LoginHub";
    private string url2 = "http://192.168.28.112:5159/LoginHub";

    void Start()
    {
        Best.HTTP.Shared.HTTPManager.Logger.Level = Best.HTTP.Shared.Logger.Loglevels.All;

        // var signalR = new SignalR();
        // signalR.Init(url2);
        // signalR.Connect();
        // return;
        // signalR.ConnectionStarted += (a, b) =>
        // {
        //     Debug.Log($"ConnectionId:{b.ConnectionId}");
        //     ;
        // };
        // var websocket = new WebSocket(new Uri("ws://192.168.28.112:5159/LoginHub/"));
        //
        // websocket.Open();
        //
        //
        // 
        //var uri = new Uri(url1);

        //uri.GetRequestPathAndQueryURL()
        // int questionMarkIndex = uri.Query.IndexOf('?');
        //
        // if (questionMarkIndex != -1 && uri.Query[questionMarkIndex + 1] == '&')
        // {
        //     uri.Query = uri.Query.Remove(questionMarkIndex + 1, 1);
        // }

        hub = new HubConnection(new Uri(url1),
            new JsonProtocol(new LitJsonEncoder()));


        Debug.Log($"2222");
        //hub.ReconnectPolicy = new DefaultRetryPolicy();
        Debug.Log($"3333");
        hub.OnConnected += OnConnected;
        hub.OnReconnected += OnReConnected;
        hub.OnError += OnError;
        hub.OnClosed += OnClosed;
        hub.OnMessage += OnMessage;
        Debug.Log($"4444");
        hub.StartConnect();
        Debug.Log($"6666");
    }

    void OnConnected(HubConnection hub)
    {
        Debug.Log("OnConnected!");
        hub.SendAsync("Test");
    }

    bool OnMessage(HubConnection hub, Message msg)
    {
        bool processed = false;

        Debug.Log($"OnMessage! {msg.target}");

        return processed;
    }

    void OnClosed(HubConnection hub)
    {
        Debug.Log("OnClosed!");
    }

    void OnError(HubConnection hub, string msg)
    {
        Debug.Log($"OnError! msg:{msg}");
    }


    void OnReConnected(HubConnection hub)
    {
        Debug.Log("OnReConnected!");
    }

    // Update is called once per frame
    void Update()
    {
    }
}