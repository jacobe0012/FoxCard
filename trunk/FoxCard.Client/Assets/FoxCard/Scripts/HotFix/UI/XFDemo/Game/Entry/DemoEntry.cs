using cfg;
using cfg.config;
using Cysharp.Threading.Tasks;
using HotFix_UI;
using SimpleJSON;
using UnityEngine;
using WeChatWASM;
using YooAsset;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using UnityEngine.Networking;


namespace XFramework
{
    public class DemoEntry : XFEntry
    {
        public override void Dispose()
        {
            base.Dispose();
            ConfigManager.Instance.Dispose();
            //WebMessageHandler.Instance.Dispose();
            NetWorkManager.Instance.Dispose();
            ResourcesSingleton.Instance.Dispose();
            JsonManager.Instance.Dispose();
            RedDotManager.Instance.Dispose();
            PlayerSingleton.Instance.Dispose();
        }

        public override void Start()
        {
            Log.Debug($"DemoEntry");

            LoadAsync();
        }


        private async void LoadAsync()
        {
            var tables = new Tables();
            await tables.LoadAsync(Loader);
            ConfigManager.Instance.InitTables(tables);
            ConfigManager.Instance.SwitchLanguages(Tblanguage.L10N.zh_cn);

            base.Init();
            Startup.Initialize();
            ResourcesSingleton.Instance.Init();
            JsonManager.Instance.Init();
            RedDotManager.Instance.Init();
            PlayerSingleton.Instance.Init();
            NetWorkManager.Instance.Init();

            //await UniTask.Delay(3000);

            // NetWorkManager.Instance.SendMessage(CMD.LOGIN, new PlayerData
            // {
            //     Id = 2,
            //     LoginType = 2,
            //     NickName = "2",
            //     LocationData = new LocationData
            //     {
            //         Addr = "locationInfo.addr",
            //         Country = "locationInfo.addr",
            //         Province = "locationInfo.addr",
            //         City = "locationInfo.addr",
            //     },
            //     OtherData = new OtherData
            //     {
            //         code = "locationInfo.addr",
            //     }
            // });

            await UniTask.Delay(4000);
            WXBase.InitSDK(async (a) =>
            {
                Log.Debug($"WXBase.InitSDK");
                WX.Login(new LoginOption
                {
                    complete = null,
                    fail = (fail) => { Log.Debug($"LoginFail:{fail}"); },
                    success = async (success) =>
                    {
                        Log.Debug($"1success.code:{success.code}");
                        var locationInfo = await GetLocationInfoNew();

                        Log.Debug($"2success.code:{success.code}");
                        NetWorkManager.Instance.SendMessage(CMD.LOGIN, new PlayerData
                        {
                            ThirdId = "",
                            LoginType = 2,
                            NickName = "",
                            LocationData = new LocationData
                            {
                                Addr = locationInfo.addr,
                                Country = locationInfo.country,
                                Province = locationInfo.province,
                                City = locationInfo.city,
                            },
                            OtherData = new OtherData
                            {
                                code = success.code
                            }
                        });
                        Log.Debug($"3success.code:{success.code}");
                    },
                    timeout = 10000
                });
                // var sceneController = Common.Instance.Get<SceneController>(); // 场景控制
                // var sceneObj = sceneController.LoadSceneAsync<Login>(SceneName.Login);
                // await SceneResManager.WaitForCompleted(sceneObj).ToCoroutine(); // 等待场景加载完毕 
            });


            // WX.CheckSession(new CheckSessionOption
            // {
            //     complete = null,
            //     fail = (a) =>
            //     {
            //         
            //     },
            //     success = (success) => { }
            // });
            //
            // WX.GetSetting(new GetSettingOption
            // {
            //     complete = null,
            //     fail = (a) =>
            //     {
            //         var wxUserInfoButton =
            //             WXBase.CreateUserInfoButton(0, 0, Screen.width, Screen.height, "zh_CN", false);
            //         wxUserInfoButton.OnTap((a) =>
            //         {
            //             // var iv = a.iv;
            //             // var encryptedData = a.encryptedData;
            //             // var signature = a.signature;
            //             // var userInfoRaw = a.userInfoRaw;
            //             // //var sessionKey = ResourcesSingleton.Instance.session_key;
            //             // string decryptedData = Decrypt(encryptedData, sessionKey, iv);
            //             // Log.Debug($"Decrypt:{decryptedData}", Color.green);
            //         });
            //     },
            //     success = (a) => { },
            //     withSubscriptions = null
            // });


            /// wx.login({
            /// success (res) {
            /// if (res.code) {
            /// //发起网络请求
            /// wx.request({
            /// url: 'https://example.com/onLogin',
            /// data: {
            /// code: res.code
            /// }
            /// })
            /// } else {
            /// console.log('登录失败！' + res.errMsg)
            /// }
            /// }
            /// })
            //JiYuUIHelper.LoadSceneAsyncYoo(SceneName.Login).Forget();
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
            Debug.Log($"address:{resData.data.addr}|province:{resData.data.province}|city:{resData.data.city}");

            resData.data.country = Regex.Replace(resData.data.country, @"\s+", "");
            resData.data.city = Regex.Replace(resData.data.city, @"\s+", "");
            resData.data.province = Regex.Replace(resData.data.province, @"\s+", "");
            resData.data.addr = Regex.Replace(resData.data.addr, @"\s+", "");

            return resData.data;
        }

        #region 地址json

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

        public static string Decrypt(string encryptedData, string sessionKey, string iv)
        {
            // 将Base64字符串解码为字节数组
            byte[] encryptedDataBytes = Convert.FromBase64String(encryptedData);
            byte[] sessionKeyBytes = Convert.FromBase64String(sessionKey);
            byte[] ivBytes = Convert.FromBase64String(iv);

            // 创建AES解密器
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = sessionKeyBytes;
                aesAlg.IV = ivBytes;
                aesAlg.Mode = CipherMode.CBC;
                aesAlg.Padding = PaddingMode.PKCS7;

                // 创建解密器转换流
                using (ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV))
                {
                    string result;
                    using (System.IO.MemoryStream msDecrypt = new System.IO.MemoryStream(encryptedDataBytes))
                    {
                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (System.IO.StreamReader srDecrypt = new System.IO.StreamReader(csDecrypt))
                            {
                                result = srDecrypt.ReadToEnd();
                            }
                        }
                    }

                    return result;
                }
            }
        }


        private async UniTask<JSONNode> Loader(string name)
        {
            var package = YooAssets.GetPackage("DefaultPackage");

            AssetHandle handle =
                package.LoadAssetAsync<TextAsset>($"Assets/{Application.productName}/ConfigJsonData/" + name + ".json");

            await handle.ToUniTask();
            var textAsset = handle.AssetObject as TextAsset;

            string fileText = textAsset.text;


            return JSON.Parse(fileText);
        }
    }
}