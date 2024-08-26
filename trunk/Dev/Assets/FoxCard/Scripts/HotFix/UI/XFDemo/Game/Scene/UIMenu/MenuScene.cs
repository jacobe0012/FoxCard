using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using HotFix_UI;
using Main;
using Unity.Mathematics;
using UnityEngine;


namespace XFramework
{
    public class MenuScene : LoadingScene, IEvent<int>, IEvent<float>
    {
        public override void GetObjects(ICollection<string> objKeys)
        {
        }
        
        protected override async void OnCompleted()
        {
            var global = Common.Instance.Get<Global>();

            Log.Debug($"MenuScene");
            AudioManager.Instance.PlayFModAudio("UIBGM");
            await UIHelper.CreateAsync(UIType.UIPanelInGame);
            global.GameObjects.Cover.SetActive(false);
        }

        public void HandleEvent(int args)
        {
            //Log.Debug($"TestEvent {args}");
        }

        public void HandleEvent(float args)
        {
        }

        protected override void OnDestroy()
        {
            Log.Debug("离开Menu场景");

            base.OnDestroy();
        }
    }
}