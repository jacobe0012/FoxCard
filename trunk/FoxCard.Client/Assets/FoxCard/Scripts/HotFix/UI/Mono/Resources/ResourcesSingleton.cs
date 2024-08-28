using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Main;
using Unity.Mathematics;
using UnityEngine;
using XFramework;
using YooAsset;

namespace HotFix_UI
{
    /// <summary>
    /// 前端缓存的所有数据单例
    /// </summary>
    public sealed class ResourcesSingleton : Singleton<ResourcesSingleton>, IDisposable
    {
        public uint seed;


        public void Init()
        {
            InitSeed();
        }

        public void InitSeed()
        {
            //uint newseed = (uint)DateTime.Now.Ticks;
            uint newseed = (uint)Stopwatch.GetTimestamp() + (uint)DateTime.Now.Ticks;

            seed = newseed;
        }


        public void Dispose()
        {
            Instance = null;
        }
    }

    public enum GraphicQuality
    {
        HD,
        Normal,
        Flow
    }

    public struct SettingsData
    {
        //
        public GraphicQuality quality;
        public bool enableFx;
        public bool enableBgm;
        public bool enableShock;
        public bool enableWeakEffect;
        public bool enableShowStick;
        public string version;
        public bool isShared;

        //public Tblanguage.L10N currentL10N;
    }
}