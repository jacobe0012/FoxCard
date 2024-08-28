using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace XFramework
{
    public static class UIHelper
    {
        #region Create Sync

        /// <summary>
        /// 创建一个不受UIManager管理的UI，通常是作为某UI的子UI(公共UI)
        /// </summary>
        /// <param name="uiType"></param>
        /// <param name="parentObj"></param>
        /// <param name="awake">立即初始化</param>
        /// <returns></returns>
        public static UI Create(string uiType, Transform parentObj, bool awake = true)
        {
            UI ui = Common.Instance.Get<UIManager>()?.Create(uiType, parentObj, awake);
            return ui;
        }

        /// <summary>
        /// 创建一个不受UIManager管理的UI，通常是作为某UI的子UI，带一个初始化参数，如果需要更多的参数，可以自行扩展或者使用struct
        /// </summary>
        /// <typeparam name="P1"></typeparam>
        /// <param name="uiType"></param>
        /// <param name="p1"></param>
        /// <param name="parentObj"></param>
        /// <returns></returns>
        public static UI Create<P1>(string uiType, P1 p1, Transform parentObj)
        {
            UI ui = Common.Instance.Get<UIManager>()?.Create(uiType, p1, parentObj);
            return ui;
        }

        /// <summary>
        /// 创建一个受UIManager管理的UI，通常是作为独立UI
        /// </summary>
        /// <param name="uiType"></param>
        /// <param name="layer"></param>
        /// <returns></returns>
        public static UI Create(string uiType, UILayer layer)
        {
            UI ui = Common.Instance.Get<UIManager>()?.Create(uiType, layer);
            return ui;
        }

        /// <summary>
        /// 创建一个受UIManager管理的UI，通常是作为独立UI，带一个初始化参数，如果需要更多的参数，可以自行扩展或者使用struct
        /// </summary>
        /// <typeparam name="P1"></typeparam>
        /// <param name="uiType"></param>
        /// <param name="p1"></param>
        /// <param name="layer"></param>
        /// <returns></returns>
        public static UI Create<P1>(string uiType, P1 p1, UILayer layer)
        {
            UI ui = Common.Instance.Get<UIManager>()?.Create(uiType, p1, layer);
            return ui;
        }

        /// <summary>
        /// 创建一个受UIManager管理的UI，通常是作为独立UI
        /// <para>层级为默认配置层级</para>
        /// </summary>
        /// <param name="uiType"></param>
        /// <returns></returns>
        public static UI Create(string uiType)
        {
            UI ui = Common.Instance.Get<UIManager>()?.Create(uiType);
            return ui;
        }

        /// <summary>
        /// 创建一个受UIManager管理的UI，通常是作为独立UI，带一个初始化参数，如果需要更多的参数，可以自行扩展或者使用struct
        /// <para>层级为默认配置层级</para>
        /// </summary>
        /// <typeparam name="P1"></typeparam>
        /// <param name="uiType"></param>
        /// <param name="p1"></param>
        /// <returns></returns>
        public static UI Create<P1>(string uiType, P1 p1)
        {
            UI ui = Common.Instance.Get<UIManager>()?.Create(uiType, p1);
            return ui;
        }

        #endregion


        #region Create Async

        /// <summary>
        ///  格伦新增
        /// 异步创建一个带父节点参数的受UIManager管理的UI，通常是作为独立UI
        /// </summary>
        /// <param name="source"></param>
        /// <param name="uiType"></param>
        /// <param name="p1"></param>
        /// <param name="parentObj"></param>
        /// <typeparam name="P1"></typeparam>
        /// <returns></returns>
        public static async UniTask<UI> CreateAsyncNew<P1>(XObject source, string uiType, P1 p1, Transform parentObj,
            CancellationToken cct = default)
        {
            var tag = source.TagId;

            UI ui = await TryGetUIManager().CreateAsyncNew(uiType, p1, parentObj, cct);
            if (tag != source.TagId) // 异步方法返回后会继续执行后面的逻辑，但是可能在返回前此类就释放掉了，为了避免这种情况，要用tag判断是否被释放过
            {
                ui?.Dispose();
                return null;
            }

            return ui;
        }

        /// <summary>
        /// 格伦新增
        /// 异步创建一个带父节点参数的受UIManager管理的UI，通常是作为独立UI
        /// </summary>
        /// <param name="source"></param>
        /// <param name="uiType"></param>
        /// <param name="parentObj"></param>
        /// <returns></returns>
        public static async UniTask<UI> CreateAsyncNew(XObject source, string uiType, Transform parentObj,
            CancellationToken cct = default)
        {
            var tag = source.TagId;
            UI ui = await TryGetUIManager().CreateAsyncNew(uiType, parentObj, cct);
            if (tag != source.TagId) // 异步方法返回后会继续执行后面的逻辑，但是可能在返回前此类就释放掉了，为了避免这种情况，要用tag判断是否被释放过
            {
                ui?.Dispose();
                return null;
            }

            return ui;
        }


        /// <summary>
        /// 异步创建一个不受UIManager管理的UI，通常是作为某UI的子UI(公共UI)
        /// </summary>
        /// <param name="source"></param>
        /// <param name="uiType"></param>
        /// <param name="parentObj"></param>
        /// <param name="awake">立即初始化</param>
        /// <returns></returns>
        public static async UniTask<UI> CreateAsync(XObject source, string uiType, Transform parentObj,
            bool awake = true,
            CancellationToken cct = default)
        {
            var tag = source.TagId;
            UI ui = await TryGetUIManager().CreateAsync(uiType, parentObj, awake, cct);
            if (tag != source.TagId) // 异步方法返回后会继续执行后面的逻辑，但是可能在返回前此类就释放掉了，为了避免这种情况，要用tag判断是否被释放过
            {
                ui?.Dispose();
                return null;
            }

            return ui;
        }


        /// <summary>
        /// 异步创建一个不受UIManager管理的UI，通常是作为某UI的子UI，带一个初始化参数，如果需要更多的参数，可以自行扩展或者使用struct
        /// </summary>
        /// <typeparam name="P1"></typeparam>
        /// <param name="source"></param>
        /// <param name="uiType"></param>
        /// <param name="p1"></param>
        /// <param name="parentObj"></param>
        /// <returns></returns>
        public static async UniTask<UI> CreateAsync<P1>(XObject source, string uiType, P1 p1, Transform parentObj,
            CancellationToken cct = default)
        {
            var tag = source.TagId;
            UI ui = await TryGetUIManager().CreateAsync(uiType, p1, parentObj, cct);
            if (tag != source.TagId) // 异步方法返回后会继续执行后面的逻辑，但是可能在返回前此类就释放掉了，为了避免这种情况，要用tag判断是否被释放过
            {
                ui?.Dispose();
                return null;
            }

            return ui;
        }

        /// <summary>
        /// 异步创建一个不受UIManager管理的UI，通常是作为某UI的子UI，带一个初始化参数，如果需要更多的参数，可以自行扩展或者使用struct
        /// </summary>
        /// <typeparam name="P1"></typeparam>
        /// <param name="source"></param>
        /// <param name="uiType"></param>
        /// <param name="p1"></param>
        /// <param name="parentObj"></param>
        /// <returns></returns>
        public static async UniTask<UI> CreateAsync<P1, P2>(XObject source, string uiType, P1 p1, P2 p2,
            Transform parentObj,
            CancellationToken cct = default)
        {
            var tag = source.TagId;
            UI ui = await TryGetUIManager().CreateAsync(uiType, p1, p2, parentObj, cct);
            if (tag != source.TagId) // 异步方法返回后会继续执行后面的逻辑，但是可能在返回前此类就释放掉了，为了避免这种情况，要用tag判断是否被释放过
            {
                ui?.Dispose();
                return null;
            }

            return ui;
        }

        /// <summary>
        /// 创建一个受UIManager管理的UI，通常是作为独立UI
        /// </summary>
        /// <param name="uiType"></param>
        /// <param name="layer"></param>
        /// <returns></returns>
        public static async UniTask<UI> CreateAsync(string uiType, UILayer layer,
            CancellationToken cct = default)
        {
            UI ui = await TryGetUIManager().CreateAsync(uiType, layer, cct);
            return ui;
        }

        /// <summary>
        /// 异步创建一个受UIManager管理的UI，通常是作为独立UI，带一个初始化参数，如果需要更多的参数，可以自行扩展或者使用struct
        /// </summary>
        /// <typeparam name="P1"></typeparam>
        /// <param name="uiType"></param>
        /// <param name="p1"></param>
        /// <param name="layer"></param>
        /// <returns></returns>
        public static async UniTask<UI> CreateAsync<P1>(string uiType, P1 p1, UILayer layer,
            CancellationToken cct = default)
        {
            UI ui = await TryGetUIManager().CreateAsync(uiType, p1, layer, cct);
            return ui;
        }

        /// <summary>
        /// 异步创建一个受UIManager管理的UI，通常是作为独立UI，带一个初始化参数，如果需要更多的参数，可以自行扩展或者使用struct
        /// </summary>
        /// <typeparam name="P1"></typeparam>
        /// <param name="uiType"></param>
        /// <param name="p1"></param>
        /// <param name="layer"></param>
        /// <returns></returns>
        public static async UniTask<UI> CreateAsync<P1, P2>(string uiType, P1 p1, P2 p2, UILayer layer,
            CancellationToken cct = default)
        {
            UI ui = await TryGetUIManager().CreateAsync(uiType, p1, p2, layer, cct);
            return ui;
        }

        /// <summary>
        /// 异步创建一个受UIManager管理的UI，通常是作为独立UI
        /// <para>层级为默认配置层级</para>
        /// </summary>
        /// <param name="uiType"></param>
        /// <returns></returns>
        public static async UniTask<UI> CreateAsync(string uiType,
            CancellationToken cct = default)
        {
            UI ui = await TryGetUIManager().CreateAsync(uiType, cct);
            return ui;
        }

        /// <summary>
        /// 异步创建一个受UIManager管理的UI，通常是作为独立UI，带一个初始化参数，如果需要更多的参数，可以自行扩展或者使用struct
        /// <para>层级为默认配置层级</para>
        /// </summary>
        /// <typeparam name="P1"></typeparam>
        /// <param name="uiType"></param>
        /// <param name="p1"></param>
        /// <returns></returns>
        public static async UniTask<UI> CreateAsync<P1>(string uiType, P1 p1,
            CancellationToken cct = default)
        {
            UI ui = await TryGetUIManager().CreateAsync(uiType, p1, cct);
            return ui;
        }

        /// <summary>
        /// 异步创建一个受UIManager管理的UI，通常是作为独立UI，带一个初始化参数，如果需要更多的参数，可以自行扩展或者使用struct
        /// <para>层级为默认配置层级</para>
        /// </summary>
        /// <typeparam name="P1"></typeparam>
        /// <param name="uiType"></param>
        /// <param name="p1"></param>
        /// <returns></returns>
        public static async UniTask<UI> CreateAsync<P1, P2>(string uiType, P1 p1, P2 p2,
            CancellationToken cct = default)
        {
            UI ui = await TryGetUIManager().CreateAsync(uiType, p1, p2, cct);
            return ui;
        }

        #endregion

        #region CreateOverLayTips

        ///生成overlay层上面的tips类型UI
        /// <summary>
        /// 异步创建一个不受UIManager管理的UI，通常是作为某UI的子UI，带一个初始化参数，如果需要更多的参数，可以自行扩展或者使用struct
        /// </summary>
        /// <typeparam name="P1"></typeparam>
        /// <param name="source"></param>
        /// <param name="uiType"></param>
        /// <param name="p1"></param>
        /// <param name="parentObj"></param>
        /// <returns></returns>
        public static async UniTask<UI> CreateOverLayTipsAsync<P1>(XObject source, string uiType, P1 p1,
            CancellationToken cct = default)
        {
            var overLayer = UIHelper.TryGetUIManager().GetUILayer(UILayer.Overlay);

            var ui = await UIHelper.CreateAsync(source, uiType, p1, overLayer);
            return ui;
        }

        #endregion

        public static void Remove(string uiType)
        {
            Common.Instance.Get<UIManager>()?.Remove(uiType);
        }

        public static UIManager TryGetUIManager()
        {
            var uiManager = Common.Instance.Get<UIManager>();
            if (uiManager == null)
            {
                Log.Error("UIManager is null");
                return null;
            }

            return uiManager;
        }

        public static UIEventManager TryGetUIEventManager()
        {
            var uiManager = Common.Instance.Get<UIEventManager>();
            if (uiManager == null)
            {
                Log.Error("UIManager is null");
                return null;
            }

            return uiManager;
        }
        
        public static void Clear()
        {
            Common.Instance.Get<UIManager>()?.Clear();
        }
    }
}