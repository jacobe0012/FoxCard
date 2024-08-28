using System;
using System.Collections;
using System.Collections.Generic;
//using cfg.blobstruct;
using Cysharp.Threading.Tasks;
//using Google.Protobuf;
using HotFix_UI;
using Main;
using Unity.Collections;
//using Unity.Entities;
using UnityEngine;
using UnityEngine.SceneManagement;
using YooAsset;


namespace XFramework
{
    public class Login : LoadingScene
    {
        public override void GetObjects(ICollection<string> objKeys)
        {
        }

        protected override void OnCompleted()
        {
            UIHelper.CreateAsync(UIType.UILogin).Forget();
            //LoadDllMono.
            Log.Debug($"Login");
        }


        protected override void OnDestroy()
        {
            base.OnDestroy();
        }
    }
}