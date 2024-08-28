//---------------------------------------------------------------------
// JiYuStudio
// Author: xxx
// Time: #CreateTime#
//---------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using HotFix_UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using Main;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

namespace XFramework
{
    [UIEvent(UIType.UILogin)]
    internal sealed class UILoginEvent : AUIEvent, IUILayer
    {
        public override string Key => UIPathSet.UILogin;

        public override bool IsFromPool => true;

        public override bool AllowManagement => true;

        public UILayer Layer => UILayer.Mid;

        // 此UI是不受UIManager管理的

        public override UI OnCreate()
        {
            return UI.Create<UILogin>();
        }
    }

    public partial class UILogin : UI, IAwake
    {
        async public void Initialize()
        {
            var global = Common.Instance.Get<Global>();


            //Game.Instance.Restart();


            var inputtext = this.GetFromReference(UILogin.KInputText);

            var btnText = this.GetFromReference(UILogin.KBtnText);
            btnText.GetTextMeshPro().SetTMPText("");

            var loginBtn = this.GetFromReference(UILogin.KLoginBtn);

            var KIsStandAlone = this.GetFromReference(UILogin.KIsStandAlone);
            KIsStandAlone.GetToggle().OnValueChanged.Add((isOn) => { global.isStandAlone = isOn; });

            var locationInfo = await GetLocationInfoNew();
            NetWorkManager.Instance.SendMessage(CMD.LOGIN, new PlayerData
            {
                Id = 2,
                LoginType = 2,
                NickName = "2",
                LocationData = new LocationData
                {
                    Addr = locationInfo.addr,
                    Country = locationInfo.country,
                    Province = locationInfo.province,
                    City = locationInfo.city,
                }
            });

            DoTweenEffect.DoScaleTweenOnClickAndLongPress(loginBtn,
                () =>
                {
                    // var userName = Regex.Replace("dgsdg", @"\p{C}+", "");
                    // var data = JsonManager.Instance.LoadPlayerData(111, userName);
                    Log.Debug($"DoScaleClick");
                    var sceneController = Common.Instance.Get<SceneController>(); // 场景控制
                    var sceneObj = sceneController.LoadSceneAsync<MenuScene>(SceneName.UIMenu);
                    SceneResManager.WaitForCompleted(sceneObj).ToCoroutine(); // 等待场景加载完毕
                });
        }

        /// <summary>
        /// 利用bilibili的接口通过ip直接获取城市信息
        /// </summary>
        public async UniTask<ResponseData> GetLocationInfoNew()
        {
            //UnityWebRequest publicIpReq = UnityWebRequest.Get(@"https://api.live.bilibili.com/client/v1/Ip/getInfoNew");

            var publicIpReq = new UnityWebRequest("https://api.live.bilibili.com/client/v1/Ip/getInfoNew",
                UnityWebRequest.kHttpVerbGET);
            publicIpReq.downloadHandler = new DownloadHandlerBuffer();

            await publicIpReq.SendWebRequest().ToUniTask();

            if (!string.IsNullOrEmpty(publicIpReq.error))
            {
                Debug.Log($"获取城市信息失败：{publicIpReq.error}");
                //yield break;
            }

            var info = publicIpReq.downloadHandler.text;
            //Debug.Log(info);

            //将json解析为object
            var resData = JsonUtility.FromJson<ResponseRootData>(info);

            //Debug.Log($"address:{resData.data.addr}|province:{resData.data.province}|city:{resData.data.city}");
            return resData.data;
        }

        #region 用于接收返回值json的反序列化数据

        [System.Serializable]
        public class ResponseRootData
        {
            public int code;
            public string message;
            public ResponseData data;
        }

        [System.Serializable]
        public class ResponseData
        {
            public string addr;
            public string country;
            public string province;
            public string city;
            public string isp;
            public string latitude;
            public string longitude;
        }

        #endregion

        protected override void OnClose()
        {
            base.OnClose();
        }
    }
}