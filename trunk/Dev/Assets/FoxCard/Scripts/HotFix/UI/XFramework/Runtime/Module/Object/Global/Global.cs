using System;
//using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

namespace XFramework
{
    public sealed class Global : CommonObject
    {
        public bool isStandAlone;
        public Transform GameRoot { get; private set; }

        public Transform UI { get; private set; }

        public Camera UICamera { get; private set; }

        public Camera MainCamera { get; private set; }

        //public CinemachineVirtualCamera VirtualCamera { get; private set; }
        public Transform CameraBounds { get; private set; }

        public GlobalGameObjects GameObjects = new GlobalGameObjects();

        protected override void Init()
        {
            GameRoot = GameObject.Find("/GameRoot(Clone)").transform;
            //Log.Debug($"{GameRoot.name}");
            UI = GameRoot.Find("UI");
            UICamera = UI.Find("UICamera")?.GetComponent<Camera>();
            MainCamera = GameRoot.Find("MainCamera")?.GetComponent<Camera>();
            //VirtualCamera = GameRoot.Find("Virtual Camera")?.GetComponent<CinemachineVirtualCamera>();
            CameraBounds = GameRoot.Find("CameraBounds")?.GetComponent<Transform>();


            GameObjects.Cover = UnityEngine.GameObject.Find("Cover");
            GameObjects.InitBackGround = UnityEngine.GameObject.Find("InitBackGround");
            GameObjects.Reporter = UnityEngine.GameObject.Find("Reporter");
            GameObjects.RollBackground = UnityEngine.GameObject.Find("RollBackground");
            GameObjects.Cover?.SetViewActive(true);
            GameObjects.InitBackGround.transform.Find("EventSystem")?.gameObject?.SetActive(false);
            //GameObjects.InitBackGround?.SetActive(false);
            GameObjects.Reporter?.SetActive(true);
            GameObjects.BagPos = new Vector3(Screen.width / 2f, 0, 0);
            SetResolution();
            InitUnitySetting();
        }


        public void SetResolution()
        {
            const float defualtResolutionX = 1170;
            const float defualtResolutionY = 2532;
            var componentsInChildren = UI.gameObject.GetComponentsInChildren<CanvasScaler>();
#if UNITY_STANDALONE
            foreach (var canvasScaler in componentsInChildren)
            {
                var referenceResolution = canvasScaler.referenceResolution;
                referenceResolution.x = defualtResolutionX;
                referenceResolution.y = defualtResolutionY;
                canvasScaler.referenceResolution = referenceResolution;
            }
#else
            foreach (var canvasScaler in componentsInChildren)
            {
                var referenceResolution = canvasScaler.referenceResolution;
                referenceResolution.x = Screen.width;
                referenceResolution.y = Screen.height;
                canvasScaler.referenceResolution = referenceResolution;
            }
#endif
        }

        void InitUnitySetting()
        {
            Input.multiTouchEnabled = false;
            Application.runInBackground = true;
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = -1;
#if UNITY_ANDROID || UNITY_IOS
            int maxRefreshRate = Screen.currentResolution.refreshRate;
            Application.targetFrameRate = maxRefreshRate;
#endif
        }

        protected override void Destroy()
        {
            GameRoot = null;
            UI = null;
            UICamera = null;
            GameObjects.Dispose();
            GameObjects = null;
        }

        public class GlobalGameObjects : IDisposable
        {
            public GameObject Cover;
            public GameObject InitBackGround;
            public GameObject Reporter;
            public GameObject RollBackground;
            public Vector3 BagPos;

            public void Dispose()
            {
                Cover = null;
                InitBackGround = null;
                Reporter = null;
                RollBackground = null;
            }
        }
    }
}