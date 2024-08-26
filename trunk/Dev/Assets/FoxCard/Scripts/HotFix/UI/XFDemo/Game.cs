using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//using cfg.config;
using HotFix_UI;

namespace XFramework
{
    public class Game : Singleton<Game>, IDisposable
    {
        private IEntry entry;

        public void Start()
        {
            entry = new DemoEntry(); //
            entry.Start();
        }

        public void Update()
        {
            entry?.Update();
        }

        public void LateUpdate()
        {
            entry?.LateUpdate();
        }

        public void FixedUpdate()
        {
            entry?.FixedUpdate();
        }

        public void Restart()
        {
            // entry?.Dispose();
            // Start();
          

            //var global=Common.Instance.Get<Global>();
            //global.SetResolution();
            
            
            //ResourcesSingleton.Instance.Clear();
            //RedPointMgr.instance.Dispose();
        }

        public void Dispose()
        {
            entry?.Dispose();
            Instance = null;
        }
    }
}