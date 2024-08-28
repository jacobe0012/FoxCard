using System;
using System.Threading;
using UnityEngine;
using YooAsset;

namespace XFramework
{
    public class Init : MonoSingleton<Init>
    {
        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Log.ILog = new UnityLogger();

            // AppDomain.CurrentDomain.UnhandledException += (sender, e) => { Log.Error($"{e}"); };
            //
            // SynchronizationContext.SetSynchronizationContext(ThreadSynchronizationContext.Instance);

            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        // Start is called before the first frame update
        private void Start()
        {
            Game.Instance.Start();
        }

        // Update is called once per frame
        private void Update()
        {
            //ThreadSynchronizationContext.Instance.Update();
            Game.Instance.Update();
        }

        private void LateUpdate()
        {
            Game.Instance.LateUpdate();
        }

        private void FixedUpdate()
        {
            Game.Instance.FixedUpdate();
        }

        private void OnApplicationQuit()
        {
            Game.Instance.Dispose();
        }
    }
}