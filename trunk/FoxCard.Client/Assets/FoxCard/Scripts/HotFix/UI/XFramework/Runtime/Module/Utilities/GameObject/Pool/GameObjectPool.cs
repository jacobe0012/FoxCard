using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace XFramework
{
    /// <summary>
    /// GameObject对象池
    /// </summary>
    public class GameObjectPool : Singleton<GameObjectPool>, IDisposable
    {
        private Dictionary<string, Queue<GameObject>> pool = new Dictionary<string, Queue<GameObject>>();

        private Dictionary<string, GameObject> poolObjParentDict = new Dictionary<string, GameObject>();

        private Dictionary<string, MyRect> poolObjParentRecDict = new Dictionary<string, MyRect>();


        /// <summary>
        /// UI对象池位置
        /// </summary>
        private Transform uiHidden = null;

        /// <summary>
        /// 非UI对象池位置
        /// </summary>
        private Transform notUIHideen = null;

        public void Init()
        {
        }

        private struct MyRect
        {
            public Vector2 anchoredPosition;
            public Vector2 anchorMin;
            public Vector2 anchorMax;
            public Vector2 offsetMin;
            public Vector2 offsetMax;
            public Vector2 pivot;
            public Vector2 sizeDelta;
            public Vector3 localScale;
            public Quaternion rotation;
        }

        public void SetOriginalRect(string key, GameObject obj)
        {
            if (poolObjParentRecDict.TryGetValue(key, out var rec0))
            {
                if (obj.TryGetComponent<RectTransform>(out var rec))
                {
                    rec.anchoredPosition = rec0.anchoredPosition;
                    rec.anchorMin = rec0.anchorMin;
                    rec.anchorMax = rec0.anchorMax;
                    rec.offsetMin = rec0.offsetMin;
                    rec.offsetMax = rec0.offsetMax;
                    rec.pivot = rec0.pivot;
                    rec.sizeDelta = rec0.sizeDelta;
                    rec.localScale = rec0.localScale;
                    rec.rotation = rec0.rotation;
                }
            }
        }

        /// <summary>
        /// 取出
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public GameObject Fetch(string key)
        {
            GameObject obj = Dequeue(key);

            if (obj)
            {
                //TODO:格伦新增
                //SetOriginalRect(key, obj);

                //
                obj?.SetActive(true);
                //obj.SetViewActive(true);
            }

            return obj;
        }

        // void SetActiveRecursively(GameObject parentObject, bool active)
        // {
        //     parentObject.SetViewActive(active);
        //
        //     foreach (Transform child in parentObject.transform)
        //     {
        //         SetActiveRecursively(child.gameObject, active);
        //     }
        // }

        /// <summary>
        /// 回收
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        public void Recycle(string key, GameObject obj)
        {
            if (!obj)
                return;

            bool isUI = obj.transform as RectTransform;

            if (isUI)
                RecycleUI(key, obj);
            else
                RecycleNotUI(key, obj);

            Enqueue(key, obj);
        }

        private void RecycleUI(string key, GameObject obj)
        {
            try
            {
                if (uiHidden == null)
                {
                    //uiHidden = Common.Instance?.Get<Global>().UI.Find("Hidden");
                    uiHidden = GameObject.Find("UIPool")?.transform;
                    if (uiHidden == null)
                    {
                        Transform root = Common.Instance?.Get<Global>().GameRoot;
                        uiHidden = new GameObject("UIPool").transform;
                        uiHidden.gameObject.layer = LayerMask.NameToLayer("UI");
                        //uiHidden.SetViewActive(false);
                        uiHidden.SetParent(root, false);
                    }
                }

                if (!this.poolObjParentDict.TryGetValue(key, out var parentObj))
                {
                    parentObj = new GameObject(key);
                    parentObj.transform.SetParent(uiHidden, false);
                    this.poolObjParentDict.Add(key, parentObj);
                    //TODO:格伦新增
                    var gameObject = ResourcesManager.Instance.Loader.Instantiate(key, parentObj.transform);
                    var rectTransform = gameObject.GetComponent<RectTransform>();
                    var copiedRect = new MyRect()
                    {
                        rotation = rectTransform.rotation,
                        localScale = rectTransform.localScale,
                        anchorMin = rectTransform.anchorMin,
                        anchorMax = rectTransform.anchorMax,
                        anchoredPosition = rectTransform.anchoredPosition,
                        sizeDelta = rectTransform.sizeDelta,
                        pivot = rectTransform.pivot,
                        offsetMin = rectTransform.offsetMin,
                        offsetMax = rectTransform.offsetMax,
                    };
                    Enqueue(key, gameObject);
                    gameObject.transform.SetParent(parentObj.transform, false);
                    poolObjParentRecDict.TryAdd(key, copiedRect);
                    //
                }

                obj.transform.SetParent(parentObj.transform, false);
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        private void RecycleNotUI(string key, GameObject obj)
        {
            try
            {
                if (notUIHideen == null)
                {
                    Transform root = Common.Instance?.Get<Global>().GameRoot;
                    notUIHideen = root.Find("Pool");

                    if (notUIHideen == null)
                    {
                        GameObject pool = new GameObject("Pool");
                        pool.SetViewActive(false);
                        pool.transform.SetParent(root, false);
                        notUIHideen = pool.transform;
                    }
                }

                if (!this.poolObjParentDict.TryGetValue(key, out var parentObj))
                {
                    parentObj = new GameObject(key);
                    parentObj.transform.SetParent(notUIHideen, false);
                    this.poolObjParentDict.Add(key, parentObj);
                }

                obj.transform.SetParent(parentObj.transform, false);
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        private GameObject Dequeue(string key)
        {
            if (pool.TryGetValue(key, out Queue<GameObject> queue))
            {
                if (queue.Count > 0)
                {
                    GameObject obj = queue.Dequeue();
                    if (queue.Count == 0)
                    {
                        ObjectPool.Instance.Recycle(queue);
                        pool.Remove(key);

                        //TODO:格伦新增
                        // if (poolObjParentRecDict.TryRemove(key, out var rec))
                        // {
                        // }
                    }

                    return obj;
                }
            }

            return null;
        }

        private void Enqueue(string key, GameObject obj)
        {
            if (!pool.TryGetValue(key, out Queue<GameObject> queue))
            {
                queue = ObjectPool.Instance.Fetch<Queue<GameObject>>();
                pool.Add(key, queue);
            }

            obj.SetActive(false);
            queue.Enqueue(obj);
        }

        public void RemovePool(string key)
        {
            if (pool.TryRemove(key, out var queue))
            {
                while (queue.Count > 0)
                {
                    GameObject obj = queue.Dequeue();
                    ResourcesManager.Instance?.Loader?.ReleaseInstance(obj);
                }

                if (this.poolObjParentDict.TryRemove(key, out var parentObj))
                {
                    Object.Destroy(parentObj);
                }

                //TODO:格伦新增
                // if (this.poolObjParentRecDict.TryRemove(key, out var rect))
                // {
                // }
            }
        }

        public void Clear()
        {
            using var keys = XList<string>.Create();
            keys.AddRange(pool.Keys);

            foreach (var key in keys)
            {
                RemovePool(key);
            }
        }

        public void Dispose()
        {
            Clear();
            poolObjParentRecDict.Clear();
            Instance = null;
        }
    }
}