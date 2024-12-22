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

        protected override void OnClose()
        {
            base.OnClose();
        }
    }
}