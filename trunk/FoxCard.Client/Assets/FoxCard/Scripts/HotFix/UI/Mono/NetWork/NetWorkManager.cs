//---------------------------------------------------------------------
// JiYuStudio
// Author: 格伦
// Time: 2023-08-31 10:47:42
//---------------------------------------------------------------------

using System;
using System.IO;
using Best.SignalR;
using Best.SignalR.Encoders;
using Best.SignalR.Messages;
//using Best.WebSockets;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using MessagePack;
using Newtonsoft.Json;
using UnityEngine;
using UnityWebSocket;
using WeChatWASM;
using XFramework;


namespace HotFix_UI
{
    public sealed class NetWorkManager : Singleton<NetWorkManager>, IDisposable
    {
        public static int wsUrl0 = 29;
        public static int wsUrl1 = 22;
        public static string emptyUrl = "ws://192.168.2.{0}:10100/websocket";

        private Color debugColor = Color.green;

#if UNITY_EDITOR
        public static string savePath = "Assets/Resources/WsUrl.json";
#else
        public static string savePath = Application.persistentDataPath + "/WsUrl.json";
#endif
        private long timerId;

        private const float HeartbeatInterval = 10; // 心跳间隔时间

        private float timer; // 计时器

        // 定义数据结构
        [Serializable]
        public class WebUrlData
        {
            [JsonProperty("webUrl")] public string webUrl;
        }

        private HubConnection hub;
        private WebSocket websocket;

        private static readonly MessagePackSerializerOptions options =
            MessagePackSerializerOptions.Standard.WithCompression(MessagePackCompression.Lz4Block);

        private void CreateSampleData()
        {
            // 存储数据
            string wsUrl = string.Format(emptyUrl, wsUrl0);
            WebUrlData data = new WebUrlData { webUrl = wsUrl };
            string json = JsonConvert.SerializeObject(data);
            File.WriteAllText(savePath, json);
        }


        public void Init()
        {
            // if (!File.Exists(savePath))
            // {
            //     // 如果 JSON 文件不存在，创建一个示例 JSON 数据
            //     CreateSampleData();
            // }
            //
            // // 读取数据
            // string json = File.ReadAllText(savePath);
            // WebUrlData data = JsonConvert.DeserializeObject<WebUrlData>(json);
            // string url = data.webUrl;
            websocket = new WebSocket($"ws://{MyUrl.urlipv4}/ws");

            websocket.OnOpen += async (a, b) =>
            {
                Log.Debug($"OnOpen", debugColor);

                SendMessage(CMD.LOGIN, new PlayerData
                {
                    ThirdId = null,
                    LoginType = 0,
                    NickName = null,
                    LocationData = default,
                    OtherData = new OtherData
                    {
                        Code = "aa1",
                        UnionidId = null
                    }
                });
            };
            websocket.OnMessage += OnMessage;
            websocket.ConnectAsync();


            return;


            debugColor = Color.cyan;
            Log.Debug($"1111", debugColor);
            // hub = new HubConnection(new Uri($"https://{DeviceTool.GetLocalIp()}:7176/LoginHub"),
            //     new JsonProtocol(new LitJsonEncoder()));

            hub = new HubConnection(new Uri($"http://{MyUrl.urlipv4}/LoginHub/"),
                new JsonProtocol(new LitJsonEncoder()));
            hub.ReconnectPolicy = new DefaultRetryPolicy();


            hub.OnConnected += OnConnected;
            hub.OnReconnected += OnReConnected;
            hub.OnError += OnError;
            hub.OnClosed += OnClosed;
            hub.OnMessage += OnMessage;
            //Log.Debug($"4444", debugColor);
            hub.StartConnect();
            //var s=await hub.ConnectAsync();


            //Log.Debug($"5555", debugColor);
        }

        void OnMessage(object a, MessageEventArgs b)
        {
            if (b.RawData == null)
            {
                Log.Debug($"empty message", debugColor);
                return;
            }

            var message = MessagePackSerializer.Deserialize<MyMessage>(b.RawData, options);
            if (message.ErrorCode != 0)
            {
                Log.Debug($"ErrorCode{message.ErrorCode}", debugColor);
            }

            //var playerData = MessagePackSerializer.Deserialize<PlayerData>(message.Content);

            if (message.MethodName == CMD.LOGIN)
            {
                SendMessage(CMD.QUERYRESOURCE);
            }

            Log.Debug($"Onmsg methodName:{message.MethodName} content:{message.Content}", debugColor);
        }


        bool OnMessage(HubConnection hub, Message msg)
        {
            bool processed = false;

            Log.Debug($"OnMessage! {msg.target}", debugColor);

            return processed;
        }

        void OnClosed(HubConnection hub)
        {
            Log.Error("OnClosed!");
        }

        void OnError(HubConnection hub, string msg)
        {
            Log.Error($"OnError! msg:{msg}");
        }

        void OnConnected(HubConnection hub)
        {
            Log.Error("OnConnected!");
        }

        void OnReConnected(HubConnection hub)
        {
            Log.Error("OnReConnected!");
        }


        /// <summary>
        /// 开启定时器
        /// </summary>
        // public void StartTimer()
        // {
        //     //开启一个每帧执行的任务，相当于Update
        //     var timerMgr = TimerManager.Instance;
        //     timerId = timerMgr.RepeatedFrameTimer(this.Update);
        // }
        //
        // /// <summary>
        // /// 移除定时器
        // /// </summary>
        // public void RemoveTimer()
        // {
        //     var timerMgr = TimerManager.Instance;
        //     timerMgr?.RemoveTimerId(ref this.timerId);
        //     this.timerId = 0;
        // }

        /// <summary>
        /// 关闭链接
        /// </summary>
        public void Close()
        {
            //RemoveTimer();
            // hub.OnConnected -= OnConnected;
            // hub.OnReconnected -= OnReConnected;
            // hub.OnError -= OnError;
            // hub.OnClosed -= OnClosed;
            // hub.OnMessage -= OnMessage;
            // hub.StartClose();
        }

        // public void OnOpen(object o, OpenEventArgs args)
        // {
        //     //TODO:开启心跳
        //     StartTimer();
        //     Log.Debug($"OnOpen", debugColor);
        // }
        //
        // public void OnClose(object o, CloseEventArgs args)
        // {
        //     RemoveTimer();
        //     Log.Debug($"OnClose", debugColor);
        // }

        // void SendHeartbeat()
        // {
        //     //Log.Debug($"SendHeartbeat()", debugColor);
        //     var message = new GameMail();
        //     var myExternalMessage = new MyExternalMessage
        //     {
        //         CmdCode = 0,
        //         ProtocolSwitch = 0,
        //         CmdMerge = 0,
        //         ResponseStatus = 0,
        //         ValidMsg = "0",
        //         DataContent = message.ToByteString()
        //     };
        //
        //     socket.SendAsync(myExternalMessage.ToByteArray());
        // }

        // void Update()
        // {
        //     timer += Time.unscaledDeltaTime;
        //     // 当计时器超过心跳间隔时发送心跳消息
        //     if (timer >= HeartbeatInterval)
        //     {
        //         timer = 0f; // 重置计时器
        //         SendHeartbeat();
        //     }
        // }


        /// <summary>
        /// 向服务器发送proto消息
        /// </summary>
        /// <param name="cmd">业务主路由</param>
        /// <param name="subCmd">业务子路由</param>
        /// <param name="protoMessage">发送的proto消息类</param>
        /// <typeparam name="T"></typeparam>
        // public void SendMessage<T>(string serverFunc, T classobj) where T : IMessagePack, new()
        // {
        //     Log.Debug($"SendMessage: {serverFunc}", debugColor);
        //     hub.SendAsync(serverFunc, classobj);
        // }
        // public void SendMessage<T>(string serverFunc, T classobj) where T : IMessagePack, new()
        // {
        //     Log.Debug($"SendMessage: {serverFunc}", debugColor);
        //     hub.SendAsync(serverFunc, classobj);
        // }
        /// <summary>
        /// 向服务器发送proto消息
        /// </summary>
        /// <param name="cmd">业务主路由</param>
        /// <param name="subCmd">业务子路由</param>
        /// <param name="protoMessage">发送的proto消息类</param>
        /// <typeparam name="T"></typeparam>
        // public void SendMessage(int cmd, int subCmd, ByteString byteString)
        // {
        //     var myExternalMessage = new MyExternalMessage
        //     {
        //         CmdMerge = CmdHelper.GetMergeCmd(cmd, subCmd),
        //         DataContent = byteString,
        //         ProtocolSwitch = 0,
        //         CmdCode = 1
        //     };
        //
        //     socket.SendAsync(myExternalMessage.ToByteArray());
        // }

        /// <summary>
        /// 向服务器发送proto消息
        /// </summary>
        /// <param name="cmd">业务主路由</param>
        /// <param name="subCmd">业务子路由</param>
        /// <param name="protoMessage">发送的proto消息类</param>
        /// <typeparam name="T"></typeparam>
        public void SendMessage(string serverMethodName)
        {
            var myExternalMessage = new MyMessage
            {
                MethodName = serverMethodName,
                ErrorCode = 0,
            };

            websocket.SendAsync(MessagePackSerializer.Serialize(myExternalMessage, options));
        }

        /// <summary>
        /// 向服务器发送proto消息
        /// </summary>
        /// <param name="cmd">业务主路由</param>
        /// <param name="subCmd">业务子路由</param>
        /// <param name="protoMessage">发送的proto消息类</param>
        /// <typeparam name="T"></typeparam>
        public void SendMessage<T>(string serverMethodName, T protoMessage) where T : IMessagePack
        {
            var myExternalMessage = new MyMessage
            {
                MethodName = serverMethodName,
                Content = MessagePackSerializer.Serialize(protoMessage, options),
                ErrorCode = 0,
            };

            websocket.SendAsync(MessagePackSerializer.Serialize(myExternalMessage, options));
        }

        /// <summary>
        /// 向服务器发送路由消息
        /// </summary>
        /// <param name="cmd">业务主路由</param>
        /// <param name="subCmd">业务子路由</param>
        // public void SendMessage(int cmd, int subCmd)
        // {
        //     var myExternalMessage = new MyMessage()
        //     {
        //         CmdMerge = CmdHelper.GetMergeCmd(cmd, subCmd),
        //         ProtocolSwitch = 0,
        //         CmdCode = 1
        //     };
        //
        //     websocket.SendAsync(myExternalMessage.ToByteArray());
        // }

        /// <summary>
        /// 向服务器发送路由消息
        /// </summary>
        /// <param name="mergeCmd">合并路由</param>
        // public void SendMessage(int mergeCmd)
        // {
        //     //Log.Debug($"SendMessageSendMessageSendMessageSendMessage",Color.green);
        //     var myExternalMessage = new MyExternalMessage
        //     {
        //         CmdMerge = mergeCmd,
        //         ProtocolSwitch = 0,
        //         CmdCode = 1
        //     };
        //     //myExternalMessage.
        //     socket.SendAsync(myExternalMessage.ToByteArray());
        // }
        public void Dispose()
        {
            Close();
            Instance = null;
        }
    }
}