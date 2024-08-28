// //---------------------------------------------------------------------
// // JiYuStudio
// // Author: xxx
// // Time: 2023-08-31 14:13:13
// //---------------------------------------------------------------------
//
// using System;
// using System.Collections.Generic;
// using UnityEngine;
// using XFramework;
//
// namespace HotFix_UI
// {
//     public sealed class WebMessageHandler : Singleton<WebMessageHandler>, IDisposable
//     {
//         private Dictionary<int, Dictionary<string, EventHandler<Execute>>> handlers =
//             new Dictionary<int, Dictionary<string, EventHandler<Execute>>>();
//
//         private Dictionary<int, int> tagPoolsNum = new Dictionary<int, int>();
//
//         private Dictionary<int, EventHandler> tagEvent = new Dictionary<int, EventHandler>();
//
//         // 事件参数
//         public class Execute : EventArgs
//         {
//             //public ByteString data;
//
//             // internal Execute(ByteString data)
//             // {
//             //     this.data = data;
//             // }
//         }
//
//         /// <summary>
//         /// 注册消息回调
//         /// </summary>
//         /// <param name="cmd">主路由</param>
//         /// <param name="subCmd">功能子路由</param>
//         /// <param name="IHandler">回调事件</param>
//         public void AddTagEvnetHandler(int tagId, EventHandler IHandler)
//         {
//             tagEvent.TryAdd(tagId, IHandler);
//         }
//
//         /// <summary>
//         /// 注册消息回调
//         /// </summary>
//         /// <param name="cmd">主路由</param>
//         /// <param name="subCmd">功能子路由</param>
//         /// <param name="IHandler">回调事件</param>
//         public void AddHandler(int cmd, int subCmd, EventHandler<Execute> IHandler, int tagId = 0)
//         {
//             var mergeCmd = CmdHelper.GetMergeCmd(cmd, subCmd);
//             var name = IHandler.Method.Name;
//             if (tagId != 0)
//             {
//                 if (tagPoolsNum.ContainsKey(tagId))
//                 {
//                     tagPoolsNum[tagId]++;
//                 }
//                 else
//                 {
//                     tagPoolsNum.Add(tagId, 1);
//                 }
//
//                 IHandler += (sender, e) =>
//                 {
//                     //Log.Debug($"{name}11111111111", Color.green);
//                     if (tagPoolsNum.ContainsKey(tagId))
//                     {
//                         tagPoolsNum[tagId]--;
//                         if (tagPoolsNum[tagId] <= 0)
//                         {
//                             Log.Debug($"完成了初始数据层:{tagId}", Color.green);
//                             if (tagEvent.TryGetValue(tagId, out var eventHandler))
//                             {
//                                 eventHandler.Invoke(eventHandler, EventArgs.Empty);
//                                 tagEvent.TryRemove(tagId, out var f1);
//
//                                 tagPoolsNum.TryRemove(tagId, out var f);
//                                 //eventHandler = null;
//                                 // tagPoolsNum.Clear();
//                                 // //eventHandler = null;
//                                 // tagEvent.Clear();
//                             }
//                         }
//                     }
//                 };
//             }
//
//
//             if (handlers.ContainsKey(mergeCmd))
//             {
//                 if (handlers[mergeCmd].ContainsKey(name))
//                 {
//                     Log.Debug($"IHandler:{name} is already exists!", Color.red);
//                     return;
//                 }
//
//                 handlers[mergeCmd].Add(name, IHandler);
//                 return;
//             }
//
//             handlers[mergeCmd] = new Dictionary<string, EventHandler<Execute>>();
//             handlers[mergeCmd].Add(name, IHandler);
//         }
//
//
//         /// <summary>
//         /// 注册消息回调
//         /// </summary>
//         /// <param name="mergeCmd">合并路由</param>
//         /// <param name="IHandler">回调事件</param>
//         public void AddHandler(int mergeCmd, EventHandler<Execute> IHandler, int tagId = 0)
//         {
//             var name = IHandler.Method.Name;
//             if (tagId != 0)
//             {
//                 if (tagPoolsNum.ContainsKey(tagId))
//                 {
//                     tagPoolsNum[tagId]++;
//                 }
//                 else
//                 {
//                     tagPoolsNum.Add(tagId, 1);
//                 }
//
//                 IHandler += (sender, e) =>
//                 {
//                     //Log.Debug($"{name}11111111111", Color.green);
//                     if (tagPoolsNum.ContainsKey(tagId))
//                     {
//                         tagPoolsNum[tagId]--;
//                         if (tagPoolsNum[tagId] <= 0)
//                         {
//                             Log.Debug($"完成了初始数据层:{tagId}", Color.green);
//                             if (tagEvent.TryGetValue(tagId, out var eventHandler))
//                             {
//                                 eventHandler.Invoke(eventHandler, EventArgs.Empty);
//                                 tagEvent.TryRemove(tagId, out var f1);
//
//                                 tagPoolsNum.TryRemove(tagId, out var f);
//                                 //eventHandler = null;
//                                 // tagPoolsNum.Clear();
//                                 // //eventHandler = null;
//                                 // tagEvent.Clear();
//                             }
//                         }
//                     }
//                 };
//             }
//
//             if (handlers.ContainsKey(mergeCmd))
//             {
//                 if (handlers[mergeCmd].ContainsKey(name))
//                 {
//                     Log.Debug($"IHandler:{name} is already exists!", Color.red);
//                     return;
//                 }
//
//                 handlers[mergeCmd].Add(name, IHandler);
//                 return;
//             }
//
//             handlers[mergeCmd] = new Dictionary<string, EventHandler<Execute>>();
//             handlers[mergeCmd].Add(name, IHandler);
//         }
//
//         /// <summary>
//         /// 销毁消息回调
//         /// </summary>
//         /// <param name="mergeCmd">合并路由</param>
//         /// <param name="IHandler">回调事件</param>
//         public void RemoveHandler(int mergeCmd, EventHandler<Execute> IHandler)
//         {
//             var name = IHandler.Method.Name;
//             if (!handlers.ContainsKey(mergeCmd))
//             {
//                 Log.Debug($"mergeCmd:{mergeCmd} is not exists!");
//                 return;
//             }
//
//             if (handlers[mergeCmd].ContainsKey(name))
//             {
//                 handlers[mergeCmd][name] = null;
//                 handlers[mergeCmd].Remove(name);
//                 return;
//             }
//             else
//             {
//                 Log.Debug($"IHandler:{name} is not exists!");
//             }
//         }
//
//         /// <summary>
//         /// 销毁消息回调
//         /// </summary>
//         /// <param name="cmd"></param>
//         /// <param name="subCmd"></param>
//         /// <param name="IHandler"></param>
//         public void RemoveHandler(int cmd, int subCmd, EventHandler<Execute> IHandler)
//         {
//             var mergeCmd = CmdHelper.GetMergeCmd(cmd, subCmd);
//             var name = IHandler.Method.Name;
//             if (!handlers.ContainsKey(mergeCmd))
//             {
//                 Log.Debug($"mergeCmd:{mergeCmd} is not exists!", Color.red);
//                 return;
//             }
//
//             if (handlers[mergeCmd].ContainsKey(name))
//             {
//                 handlers[mergeCmd][name] = null;
//                 handlers[mergeCmd].Remove(name);
//                 return;
//             }
//             else
//             {
//                 Log.Debug($"IHandler:{name} is not exists!", Color.red);
//             }
//         }
//
//         /// <summary>
//         /// 消息分发
//         /// </summary>
//         /// <param name="cmd"></param>
//         /// <param name="subCmd"></param>
//         /// <param name="data"></param>
//         public void PackageHandler(int cmd, int subCmd, ByteString data)
//         {
//             var mergeCmd = CmdHelper.GetMergeCmd(cmd, subCmd);
//             if (!handlers.ContainsKey(mergeCmd))
//             {
//                 Log.Debug($"cmd:{cmd},{subCmd} is not exists,please check out!", Color.red);
//                 return;
//             }
//
//             if (data.IsEmpty)
//             {
//                 Log.Debug($"cmd:{cmd},{subCmd} 接收到空消息", Color.red);
//             }
//
//             var handlerDic = handlers[mergeCmd];
//             var tempList = new List<EventHandler<Execute>>(handlerDic.Values);
//
//             foreach (var handler in tempList)
//             {
//                 handler.Invoke(handler, new Execute(data));
//             }
//
//             tempList.Clear();
//             tempList = null;
//         }
//
//         //消息分发
//         public void PackageHandler(int mergeCmd, ByteString data)
//         {
//             int cmd = CmdHelper.GetCmd(mergeCmd);
//             int subCmd = CmdHelper.GetSubCmd(mergeCmd);
//             if (!handlers.ContainsKey(mergeCmd))
//             {
//                 //TODO:
//                 if (cmd != 0 && subCmd != 0)
//                 {
//                     Log.Debug($"cmd:{cmd},{subCmd} is not exists,please check out!", Color.red);
//                 }
//                 else
//                 {
//                     Log.Debug($"ReceiveHeartbeat", Color.cyan);
//                 }
//
//
//                 return;
//             }
//
//             if (data.IsEmpty)
//             {
//                 Log.Debug($"cmd:{cmd},{subCmd} 接收到空消息", Color.red);
//             }
//
//             var handlerDic = handlers[mergeCmd];
//             var tempList = new List<EventHandler<Execute>>(handlerDic.Values);
//
//             foreach (var handler in tempList)
//             {
//                 handler.Invoke(handler, new Execute(data));
//             }
//
//             tempList.Clear();
//             tempList = null;
//         }
//
//         public void Dispose()
//         {
//             foreach (var VARIABLE in handlers)
//             {
//                 VARIABLE.Value.Clear();
//             }
//
//             tagPoolsNum.Clear();
//             tagEvent.Clear();
//             handlers.Clear();
//             Instance = null;
//         }
//     }
// }