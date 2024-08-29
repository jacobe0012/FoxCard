using System;
using System.Collections;
using System.Collections.Generic;
using Best.SignalR;
using Best.SignalR.Authentication;
using Best.SignalR.Encoders;
using Best.SignalR.Messages;
using UnityEngine;

public class Init : MonoBehaviour
{
    // Start is called before the first frame update
    private HubConnection hub;

    void Start()
    {
        hub = new HubConnection(new Uri($"https://192.168.28.112:7176/LoginHub"),
            new JsonProtocol(new LitJsonEncoder()));

        hub.AuthenticationProvider = new DefaultAccessTokenAuthenticator(hub);

        Debug.Log($"2222");
        hub.ReconnectPolicy = new DefaultRetryPolicy();
        Debug.Log($"3333");
        hub.OnConnected += OnConnected;
        hub.OnReconnected += OnReConnected;
        hub.OnError += OnError;
        hub.OnClosed += OnClosed;
        hub.OnMessage += OnMessage;
        Debug.Log($"4444");
        hub.StartConnect();
        Debug.Log($"5555");
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