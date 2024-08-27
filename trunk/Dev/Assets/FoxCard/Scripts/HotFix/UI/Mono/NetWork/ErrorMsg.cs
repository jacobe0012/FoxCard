//
// using UnityEngine;
// using XFramework;
//
// namespace HotFix_UI
// {
//     public static class ErrorMsg
//     {
//         /// <summary>
//         /// 错误码处理
//         /// </summary>
//         /// <param name="MsgCode"></param>
//         public static async void LogErrorMsg(int MsgCode)
//         {
//             switch (MsgCode)
//             {
//                 case 1000:
//                     Log.Error("参数异常");
//                     break;
//                 case 1001:
//                     Log.Error("json解析异常");
//                     break;
//                 case 1002:
//                     Log.Error("mongodb异常");
//                     break;
//                 case 1003:
//                     Log.Error("逻辑服通讯失败");
//                     break;
//                 case 1004:
//                     Log.Error("redis异常");
//                     break;
//                 case 1005:
//                     Log.Error("本地配置缓存异常");
//                     break;
//                 case 1006:
//                     Log.Error("数据库数据异常");
//                     break;
//                 case 1007:
//                     Log.Error("初始化异常");
//                     break;
//                 case 1008:
//                     Log.Error("锁超时异常");
//                     break;
//                 case 1009:
//                     Log.Error("支付异常");
//                     break;
//                 case 1010:
//                     Log.Error("支付异常");
//                     break;
//                 case 1011:
//                     Log.Error("GM配置异常");
//                     break;
//                 case 170001:
//                     Log.Error("号已经在线上，不允许重复登录");
//                     break;
//                 case 170002:
//                     Log.Error("登录异常");
//                     break;
//                 case 170003:
//                     Log.Error("未登录");
//                     break;
//                 case 170004:
//                     Log.Error("账号已锁定");
//                     break;
//                 case 170005:
//                     Log.Error("账户不存在");
//                     break;
//                 case 170006:
//                     Log.Error("账号被封禁");
//                     break;
//                 case 170007:
//                     Log.Error("版本维护中");
//                     break;
//                 case 170008:
//                     Log.Error("快速登陆已超时");
//                     break;
//                 
//                 case 110001:
//                     Log.Error("创建角色失败");
//                     break;
//                 case 110002:
//                     Log.Error("绑定用户角色失败");
//                     break;
//                 case 110004:
//                     Log.Error("体力不足");
//                     WebMessageHandler.Instance.AddHandler(CMD.INITPLAYER, OnCheckResourceEnoughResponse1);
//                     NetWorkManager.Instance.SendMessage(CMD.INITPLAYER);
//                     break;
//                 case 110005:
//                     Log.Error("金币不足");
//                     WebMessageHandler.Instance.AddHandler(CMD.INITPLAYER, OnCheckResourceEnoughResponse3);
//                     NetWorkManager.Instance.SendMessage(CMD.INITPLAYER);
//                     break;
//                 case 110006:
//                     Log.Error("宝石不足");
//                     WebMessageHandler.Instance.AddHandler(CMD.INITPLAYER, OnCheckResourceEnoughResponse2);
//                     NetWorkManager.Instance.SendMessage(CMD.INITPLAYER);
//                     break;
//                 case 110003:
//                     Log.Error("角色操作失败");
//                     break;
//                 case 110007:
//                     Log.Error("昵称不能重复");
//                     await UIHelper.CreateAsync(UIType.UICommon_Resource,
//                         ConfigManager.Instance.Tables.Tblanguage.Get("user_info_name_change_repeated").current);
//                     break;
//                 case 910001:
//                     Log.Error("装备已到最顶级");
//                     break;
//                 case 910002:
//                     Log.Error("一键合成必须有主要合成材料");
//                     break;
//                 case 910003:
//                     Log.Error("合成材料不足");
//                     break;
//                 case 910004:
//                     Log.Error("装备生成缺少参数");
//                     break;
//                 case 910005:
//                     Log.Error("装备不能降级");
//                     break;
//                 case 910006:
//                     Log.Error("装备不存在");
//                     break;
//                 case 910007:
//                     Log.Error("不满足升品体条件");
//                     break;
//                 case 910008:
//                     Log.Error("装备已穿戴");
//                     break;
//                 case 1010008:
//                     Log.Error("物品不存在");
//                     break;
//                 case 1010009:
//                     Log.Error("物品数量不足");
//                     break;
//                 case 210001:
//                     Log.Error("章节不存在");
//                     break;
//                 case 210002:
//                     Log.Error("宝箱不存在");
//                     break;
//                 case 210003:
//                     Log.Error("章节未通关");
//                     break;
//                 case 210004:
//                     Log.Error("对局id不存在");
//                     break;
//                 case 210005:
//                     Log.Error("关卡未解锁");
//                     break;
//                 case 210006:
//                     Log.Error("关卡未通过");
//                     break;
//                 case 510001:
//                     Log.Error("邮件发送失败");
//                     break;
//                 case 510002:
//                     Log.Error("邮件删除失败");
//                     break;
//                 case 510003:
//                     Log.Error("邮件不存在");
//                     break;
//                 case 1010007:
//                     Log.Error("天赋不存在");
//                     break;
//                 case 1010006:
//                     Log.Error("天赋已解锁");
//                     break;
//                 case 1010005:
//                     Log.Error("天赋解锁条件不符合");
//                     break;
//                 case 610001:
//                     Log.Error("巡逻次数不足");
//                     break;
//                 case 610002:
//                     Log.Error("巡逻时间不足");
//                     break;
//                 case 1110001:
//                     Log.Error("广告cd中");
//                     break;
//                 case 1110002:
//                     Log.Error("礼包未解锁");
//                     break;
//                 case 1110003:
//                     Log.Error("礼包未解锁");
//                     break;
//                 case 1110004:
//                     Log.Error("礼包没有物品");
//                     break;
//                 case 1110005:
//                     Log.Error("礼包购买次数用完");
//                     break;
//                 case 1110006:
//                     Log.Error("商品未解锁");
//                     break;
//                 case 1110007:
//                     Log.Error("购买次数不足");
//                     break;
//                 case 310001:
//                     Log.Error("任务不存在");
//                     break;
//                 case 310002:
//                     Log.Error("任务宝箱未解锁");
//                     break;
//                 case 310003:
//                     Log.Error("任务宝箱不存在");
//                     break;
//                 case 310004:
//                     Log.Error("任务收益已领取");
//                     break;
//                 case 310005:
//                     Log.Error("任务宝箱已领取");
//                     break;
//                 case 1210001:
//                     Log.Error("抽奖次数不足");
//                     break;
//                 case 1210002:
//                     Log.Error("抽奖条件不满足");
//                     break;
//                 case 1210006:
//                     Log.Error("签到奖励已领取");
//                     break;
//                 case 1210007:
//                     Log.Error("签到宝箱奖励已领取");
//                     break;
//                 case -1000:
//                     Log.Error("系统其它错误");
//                     break;
//                 case -1001:
//                     Log.Error("参数验错误");
//                     break;
//                 case -1002:
//                     Log.Error("路由错误");
//                     break;
//                 case -1003:
//                     Log.Error("心跳超时相关");
//                     break;
//                 case -1004:
//                     Log.Error("请先登录");
//                     break;
//                 case -1005:
//                     Log.Error("class 不存在");
//                     break;
//                 case -1006:
//                     Log.Error("数据不存在");
//                     break;
//                 case -1007:
//                     Log.Error("强制玩家下线");
//                     break;
//                 default:
//                     Log.Error($"当前错误码未定义{MsgCode}");
//                     break;
//             }
//
//             void OnCheckResourceEnoughResponse1(object sender, WebMessageHandler.Execute e)
//             {
//                 var gameRole = new GameRole();
//                 gameRole.MergeFrom(e.data);
//                 if (e.data.IsEmpty)
//                 {
//                     Log.Debug("e.data.IsEmpty", Color.red);
//                     return;
//                 }
//
//                 var tbuserLevel = ConfigManager.Instance.Tables.Tbuser_level;
//                 ResourcesSingleton.Instance.playerResources.energy = gameRole.RoleAssets.Energy;
//                 ResourcesSingleton.Instance.playerResources.gems = gameRole.RoleAssets.Bitcoin;
//                 ResourcesSingleton.Instance.playerResources.gold = gameRole.RoleAssets.UsBill;
//                 var totalExp = gameRole.RoleAssets.Level > 1
//                     ? tbuserLevel.Get(gameRole.RoleAssets.Level - 1).exp + gameRole.RoleAssets.Exp
//                     : gameRole.RoleAssets.Exp;
//                 ResourcesSingleton.Instance.playerResources.playerTotalExp = totalExp;
//
//                 ResourcesSingleton.Instance.UpdateResourceUI();
//
//
//                 UIHelper.CreateAsync(UIType.UILack, 1);
//
//
//                 WebMessageHandler.Instance.RemoveHandler(CMD.INITPLAYER, OnCheckResourceEnoughResponse1);
//             }
//
//             void OnCheckResourceEnoughResponse2(object sender, WebMessageHandler.Execute e)
//             {
//                 var gameRole = new GameRole();
//                 gameRole.MergeFrom(e.data);
//                 if (e.data.IsEmpty)
//                 {
//                     Log.Debug("e.data.IsEmpty", Color.red);
//                     return;
//                 }
//
//                 var tbuserLevel = ConfigManager.Instance.Tables.Tbuser_level;
//                 ResourcesSingleton.Instance.playerResources.energy = gameRole.RoleAssets.Energy;
//                 ResourcesSingleton.Instance.playerResources.gems = gameRole.RoleAssets.Bitcoin;
//                 ResourcesSingleton.Instance.playerResources.gold = gameRole.RoleAssets.UsBill;
//                 var totalExp = gameRole.RoleAssets.Level > 1
//                     ? tbuserLevel.Get(gameRole.RoleAssets.Level - 1).exp + gameRole.RoleAssets.Exp
//                     : gameRole.RoleAssets.Exp;
//                 ResourcesSingleton.Instance.playerResources.playerTotalExp = totalExp;
//
//                 ResourcesSingleton.Instance.UpdateResourceUI();
//
//
//                 UIHelper.CreateAsync(UIType.UILack, 2);
//
//
//                 WebMessageHandler.Instance.RemoveHandler(CMD.INITPLAYER, OnCheckResourceEnoughResponse2);
//             }
//
//             void OnCheckResourceEnoughResponse3(object sender, WebMessageHandler.Execute e)
//             {
//                 var gameRole = new GameRole();
//                 gameRole.MergeFrom(e.data);
//                 if (e.data.IsEmpty)
//                 {
//                     Log.Debug("e.data.IsEmpty", Color.red);
//                     return;
//                 }
//
//                 var tbuserLevel = ConfigManager.Instance.Tables.Tbuser_level;
//                 ResourcesSingleton.Instance.playerResources.energy = gameRole.RoleAssets.Energy;
//                 ResourcesSingleton.Instance.playerResources.gems = gameRole.RoleAssets.Bitcoin;
//                 ResourcesSingleton.Instance.playerResources.gold = gameRole.RoleAssets.UsBill;
//                 var totalExp = gameRole.RoleAssets.Level > 1
//                     ? tbuserLevel.Get(gameRole.RoleAssets.Level - 1).exp + gameRole.RoleAssets.Exp
//                     : gameRole.RoleAssets.Exp;
//                 ResourcesSingleton.Instance.playerResources.playerTotalExp = totalExp;
//
//                 ResourcesSingleton.Instance.UpdateResourceUI();
//
//
//                 UIHelper.CreateAsync(UIType.UILack, 3);
//
//
//                 WebMessageHandler.Instance.RemoveHandler(CMD.INITPLAYER, OnCheckResourceEnoughResponse3);
//             }
//         }
//     }
// }