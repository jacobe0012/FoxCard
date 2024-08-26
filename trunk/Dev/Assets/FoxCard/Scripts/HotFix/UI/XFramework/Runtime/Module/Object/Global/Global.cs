using System;
using System.Collections;
using System.Collections.Generic;
//using Cinemachine;
//using Common;
//using Google.Protobuf;
using HotFix_UI;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace XFramework
{
    public sealed class Global : CommonObject
    {
        public bool isStandAlone = true;
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
            GameObjects.InitBackGround?.SetActive(false);
            GameObjects.Reporter?.SetActive(true);

            SetResolution();
            InitUnitySetting();
        }
        public void SetResolution()
        {
            const float defualtResolutionX = 1080;
            const float defualtResolutionY = 1920;
            var componentsInChildren = UI.gameObject.GetComponentsInChildren<CanvasScaler>();
#if UNITY_EDITOR
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
        // public void SetCameraBounds(int mapType, float fov = 60)
        // {
        //     VirtualCamera.m_Lens.FieldOfView = fov;
        //     var cinemachineConfiner2D = VirtualCamera.GetComponent<CinemachineConfiner2D>();
        //     cinemachineConfiner2D.m_BoundingShape2D = CameraBounds.gameObject.GetComponent<Collider2D>();
        //     CinemachineFramingTransposer transposer =
        //         VirtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        //     Vector3 bounds = default;
        //     Vector3 pos = default;
        //     switch (mapType)
        //     {
        //         case 1:
        //             bounds = new Vector3(98, 1000, 1);
        //             transposer.m_TrackedObjectOffset = new Vector3(0, 0, -200);
        //             break;
        //         case 2:
        //             //TODO:
        //             bounds = new Vector3(1000, 98, 1);
        //             transposer.m_TrackedObjectOffset = new Vector3(0, 0, -200);
        //             break;
        //         case 3:
        //
        //             bounds = new Vector3(155, 170, 1);
        //             transposer.m_TrackedObjectOffset = new Vector3(0, 0, -200);
        //             pos = new Vector3(0, -10, 0);
        //             break;
        //         case 4:
        //           
        //             //bounds = new Vector3(float.MaxValue, float.MaxValue, 1);
        //             transposer.m_TrackedObjectOffset = new Vector3(0, -50, -200);
        //             cinemachineConfiner2D.m_BoundingShape2D = null;
        //             break;
        //     }
        //
        //     CameraBounds.SetPosition(pos);
        //     CameraBounds.SetScale(bounds);
        //     cinemachineConfiner2D.InvalidateCache();
        // }

        //         public void SetResolution()
        //         {
        //             const float defualtResolutionX = 1170;
        //             const float defualtResolutionY = 2532;
        //             var componentsInChildren = UI.gameObject.GetComponentsInChildren<CanvasScaler>();
        // #if UNITY_STANDALONE
        //             foreach (var canvasScaler in componentsInChildren)
        //             {
        //                 var referenceResolution = canvasScaler.referenceResolution;
        //                 referenceResolution.x = defualtResolutionX;
        //                 referenceResolution.y = defualtResolutionY;
        //                 canvasScaler.referenceResolution = referenceResolution;
        //             }
        // #else
        //             foreach (var canvasScaler in componentsInChildren)
        //             {
        //                 var referenceResolution = canvasScaler.referenceResolution;
        //                 referenceResolution.x = Screen.width;
        //                 referenceResolution.y = Screen.height;
        //                 canvasScaler.referenceResolution = referenceResolution;
        //             }
        // #endif
        //         }

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