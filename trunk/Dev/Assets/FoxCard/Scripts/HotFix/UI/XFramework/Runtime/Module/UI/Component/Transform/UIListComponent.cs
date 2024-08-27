using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using HotFix_UI;
using UnityEngine;
using XFramework.UIEventType;

namespace XFramework
{
    /// <summary>
    /// 用一个列表管理一系列的子UI
    /// </summary>
    public class UIListComponent : RectTransformComponent, IEnumerable, IInternalEvent<OnChildSiblingChanged>,
        IInternalEvent<OnChildIdChanged>
    {
        /// <summary>
        /// 标记被UIList管理，受UIList管理的子UI添加这个类后，当这个类释放时同时也会从UIList里移除掉，如果不移除会有很大的潜在安全隐患
        /// </summary>
        private class UIListFlagComponent : UIComponent
        {
            protected override void OnDestroy()
            {
                UI me = this.Parent;
                UI parent = me.Parent;
                if (parent != null && !parent.IsDisposed)
                {
                    var list = parent.GetUIComponent<UIListComponent>();
                    if (list != null && !list.IsDisposed)
                    {
                        list.RemoveFromChildren(me);
                    }
                }

                base.OnDestroy();
            }
        }

        protected Transform content => this.Get();

        /// <summary>
        /// UI列表
        /// </summary>
        protected List<UI> children = new List<UI>();

        /// <summary>
        /// Id列表
        /// </summary>
        protected HashSet<IUID> uidChildren = new HashSet<IUID>();

        /// <summary>
        /// Id -> UI
        /// </summary>
        protected Dictionary<long, UI> childrenDict = new Dictionary<long, UI>();

        /// <summary>
        /// UI列表里的对象个数
        /// </summary>
        public int Count => this.children.Count;

        /// <summary>
        /// UI列表
        /// </summary>
        public List<UI> Children => this.children;

        protected override void Destroy()
        {
            Clear();
            base.Destroy();
        }

        /// <summary>
        /// 仅移除，不走释放逻辑，框架调用
        /// </summary>
        /// <param name="child"></param>
        private void RemoveFromChildren(UI child)
        {
            this.children.Remove(child);
            if (child is IUID childUID)
            {
                if (this.uidChildren.Remove(childUID))
                {
                    if (!this.childrenDict.Remove(childUID.Id))
                    {
                        long id = 0;
                        foreach (var item in this.childrenDict)
                        {
                            UI ui = item.Value;
                            if (ui == child)
                            {
                                id = item.Key;
                                break;
                            }
                        }

                        if (id != 0)
                            this.childrenDict.Remove(id);
                    }
                }
            }
        }

        /// <summary>
        /// 添加到管理列表
        /// </summary>
        /// <param name="child"></param>
        protected void AddToChildren(UI child)
        {
            this.children.Add(child);

            if (child is IUID childUID)
            {
                if (childUID.IsValid())
                    this.childrenDict.Add(childUID.Id, child);

                this.uidChildren.Add(childUID);
            }
        }

        /// <summary>
        /// 检查index是否有效
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        protected bool CheckIndex(int index)
        {
            int childCount = this.Count;
            if (index < 0 || index >= childCount)
            {
                Log.Error("UIListComponent GetChild超出索引范围, index is {0}", index);
                return false;
            }

            return true;
        }

        /// <summary>
        /// 设置一个子对象在list里的索引，并不会改变transform的索引
        /// </summary>
        /// <param name="curIndex"></param>
        /// <param name="newIndex"></param>
        private void SetChildIndex(int curIndex, int newIndex)
        {
            if (curIndex == newIndex)
                return;

            UI child = this.GetChildAt(curIndex);
            if (child is null)
                return;

            this.children.RemoveAt(curIndex);
            this.children.Insert(newIndex, child);
        }

        /// <summary>
        /// 改变id
        /// </summary>
        /// <param name="child"></param>
        /// <param name="beforeId"></param>
        /// <param name="curId"></param>
        private void ChangeUID(UI child, long beforeId, long curId)
        {
            if (child is IUID u)
            {
                if (uidChildren.Contains(u))
                {
                    childrenDict.Remove(beforeId);
                    childrenDict.Add(curId, child);
                }
            }
        }

        /// <summary>
        /// 添加标记，释放时同步在List里移除
        /// </summary>
        /// <param name="child"></param>
        private void AddFlag(UI child)
        {
            UIListFlagComponent flag = ObjectFactory.Create<UIListFlagComponent>(true);
            child.AddUIComponent(flag);
        }

        /// <summary>
        /// 尝试添加到字典用于方便获取Id
        /// </summary>
        /// <param name="child"></param>
        private void TryAddToDict(UI child)
        {
            if (child is IUID ui)
            {
                if (!this.uidChildren.Contains(ui))
                    return;

                if (ui.IsValid())
                {
                    if (!this.childrenDict.ContainsKey(ui.Id))
                        this.childrenDict.Add(ui.Id, child);
                }
            }
        }

        /// <summary>
        /// 添加UI到列表里
        /// </summary>
        /// <param name="child"></param>
        /// <param name="addToChildren">同时添加到父UI的Children里</param>
        public void AddChild(UI child, bool addToChildren)
        {
            if (this.parent == child.Parent)
            {
                Log.Error("child存在相同的父对象 childName is {0}", child.Name);
                return;
            }

            if (this.children.Contains(child))
            {
                Log.Error("UIList已存在child，无法重复添加, childName is {0}", child.Name);
                return;
            }

            child.SetParent(this.parent, addToChildren);
            this.AddChild(child.GameObject.transform, false);
            this.AddFlag(child);
            this.AddToChildren(child);
        }

        /// <summary>
        /// 根据索引移除一个UI
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool RemoveChildAt(int index)
        {
            if (!this.CheckIndex(index))
                return false;

            var child = this.GetChildAt(index);
            child.Dispose();

            return true;
        }

        /// <summary>
        /// 清空
        /// </summary>
        public void Clear()
        {
            using var list = XList<UI>.Create();
            list.AddRange(this.children);
            foreach (var ui in list)
            {
                //Log.Error("释放 {0}, disposed = {1}", ui.Name, ui.IsDisposed);
                ui.Dispose();
            }

            this.children.Clear();
            this.uidChildren.Clear();
            this.childrenDict.Clear();
        }

        #region Get

        /// <summary>
        /// 根据索引获取一个UI
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="index"></param>
        /// <returns></returns>
        public T GetChildAt<T>(int index) where T : UI
        {
            if (!this.CheckIndex(index))
                return null;

            return this.children[index] as T;
        }

        /// <summary>
        /// 根据索引获取一个UI
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public UI GetChildAt(int index)
        {
            return this.GetChildAt<UI>(index);
        }

        /// <summary>
        /// 根据Id获取一个UI, 获取的UI需继承IUID
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetChildById<T>(long id) where T : UI
        {
            if (!this.childrenDict.TryGetValue(id, out var child))
            {
                foreach (var ui in this.uidChildren)
                {
                    if (ui.Id == id)
                    {
                        child = ui as UI;
                        this.childrenDict.Add(id, child);
                        break;
                    }
                }
            }

            return child as T;
        }

        /// <summary>
        /// 根据Id获取一个UI, 获取的UI需继承IUID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UI GetChildById(long id)
        {
            return this.GetChildById<UI>(id);
        }

        #endregion

        #region Creator

        private UI InnerCreateWithUIType(string uiType, bool addToChildren)
        {
            UI child = UIHelper.Create(uiType, this.content, false);
            this.AddChild(child, addToChildren);

            return child;
        }

        private T InnerCreateWithKey<T>(string key, bool objFromPool, bool isFromPool) where T : UI, new()
        {
            T child = UI.Create<T>(isFromPool);
            GameObject obj = ResourcesManager.Instantiate(child, key, this.content, objFromPool);
            child.Bind(obj, this.parent.Name);
            this.AddChild(child, false);

            return child;
        }

        private T InnerCreate<T>(GameObject obj, bool isFromPool) where T : UI, new()
        {
            T child = UI.Create<T>(this.parent.Name, obj, isFromPool);
            this.AddChild(child, false);

            return child;
        }

        private async UniTask<UI> InnerCreateWithUITypeAsync(string uiType, bool addToChildren,
            CancellationToken cct = default)
        {
            UI child = await UIHelper.CreateAsync(this, uiType, this.content, false, cct);
            if (child is null)
                return null;

            this.AddChild(child, addToChildren);

            return child;
        }

        private async UniTask<UI> InnerCreateWithUITypeAsync(string uiType, bool addToChildren, Transform parent,
            CancellationToken cct = default)
        {
            UI child = await UIHelper.CreateAsync(this, uiType, parent, false, cct);
            if (child is null)
                return null;

            this.AddChild(child, addToChildren);

            return child;
        }

        private async UniTask<T> InnerCreateWithKeyAsync<T>(string key, bool objFromPool, bool isFromPool)
            where T : UI, new()
        {
            var tagId = this.TagId;
            T child = UI.Create<T>(isFromPool);
            GameObject obj = await ResourcesManager.InstantiateAsync(child, key, this.content, objFromPool);
            if (tagId != this.TagId) // 异步方法返回后会继续执行后面的逻辑，但是可能在返回前此类就释放掉了，为了避免这种情况，要用tag判断是否被释放过
            {
                child?.Dispose();
                return null;
            }

            child.Bind(obj, this.parent.Name);
            this.AddChild(child, false);

            return child;
        }

        /// <summary>
        /// 通过UIType创建一个UI, 此UI需实现AUIEvent
        /// </summary>
        /// <param name="uiType"></param>
        /// <param name="addToChildren">同时添加到父UI的Children里</param>
        /// <returns></returns>
        public UI CreateWithUIType(string uiType, bool addToChildren)
        {
            UI child = this.InnerCreateWithUIType(uiType, addToChildren);
            ObjectHelper.Awake(child);

            return child;
        }

        /// <summary>
        /// 通过UIType创建一个UI, 此UI需实现AUIEvent;
        /// 可以接受一个初始化参数, 如果一个参数不够，可以用struct
        /// </summary>
        /// <typeparam name="P1"></typeparam>
        /// <param name="uiType"></param>
        /// <param name="arg"></param>
        /// <param name="addToChildren">同时添加到父UI的Children里</param>
        /// <returns></returns>
        public UI CreateWithUIType<P1>(string uiType, P1 arg, bool addToChildren)
        {
            UI child = this.InnerCreateWithUIType(uiType, addToChildren);
            ObjectHelper.Awake(child, arg);

            return child;
        }

        /// <summary>
        /// 通过key创建一个UI, 无需实现AUIEvent
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="objFromPool"></param>
        /// <param name="isFromPool"></param>
        /// <returns></returns>
        public T CreateWithKey<T>(string key, bool objFromPool = false, bool isFromPool = false) where T : UI, new()
        {
            T child = this.InnerCreateWithKey<T>(key, objFromPool, isFromPool);
            ObjectHelper.Awake(child);

            return child;
        }

        /// <summary>
        /// 通过key创建一个UI, 无需实现AUIEvent;
        /// 可以接受一个初始化参数, 如果一个参数不够，可以用struct
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="P1"></typeparam>
        /// <param name="key"></param>
        /// <param name="arg"></param>
        /// <param name="objFromPool"></param>
        /// <param name="isFromPool"></param>
        /// <returns></returns>
        public T CreateWithKey<T, P1>(string key, P1 arg, bool objFromPool = false, bool isFromPool = false)
            where T : UI, IAwake<P1>, new()
        {
            T child = this.InnerCreateWithKey<T>(key, objFromPool, isFromPool);
            ObjectHelper.Awake(child, arg);

            return child;
        }

        /// <summary>
        /// 通过key创建一个UI, 无需实现AUIEvent
        /// </summary>
        /// <param name="key"></param>
        /// <param name="objFromPool"></param>
        /// <param name="isFromPool"></param>
        /// <returns></returns>
        public UI CreateWithKey(string key, bool objFromPool = false, bool isFromPool = false)
        {
            return this.CreateWithKey<UI>(key, objFromPool, isFromPool);
        }

        /// <summary>
        /// 传入已存在的obj并创建一个UI
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="isFromPool"></param>
        /// <returns></returns>
        public T Create<T>(GameObject obj, bool isFromPool = false) where T : UI, new()
        {
            T child = this.InnerCreate<T>(obj, isFromPool);
            ObjectHelper.Awake(child);

            return child;
        }

        /// <summary>
        /// 传入已存在的obj并创建一个UI;
        /// 可以接受一个初始化参数, 如果一个参数不够，可以用struct
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="P1"></typeparam>
        /// <param name="obj"></param>
        /// <param name="arg"></param>
        /// <param name="isFromPool"></param>
        /// <returns></returns>
        public T Create<T, P1>(GameObject obj, P1 arg, bool isFromPool = false) where T : UI, IAwake<P1>, new()
        {
            T child = this.InnerCreate<T>(obj, isFromPool);
            ObjectHelper.Awake(child, arg);

            return child;
        }

        /// <summary>
        /// 传入已存在的obj并创建一个UI
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="isFromPool"></param>
        /// <returns></returns>
        public UI Create(GameObject obj, bool isFromPool = false)
        {
            return this.Create<UI>(obj, isFromPool);
        }

        /// <summary>
        /// 通过UIType创建一个UI, 此UI需实现AUIEvent
        /// </summary>
        /// <param name="uiType"></param>
        /// <param name="addToChildren">同时添加到父UI的Children里</param>
        /// <returns></returns>
        public async UniTask<UI> CreateWithUITypeAsync(string uiType, bool addToChildren, Transform parent,
            CancellationToken cct = default)
        {
            UI child = await this.InnerCreateWithUITypeAsync(uiType, addToChildren, parent, cct);
            if (child is null)
                return null;

            ObjectHelper.Awake(child);

            return child;
        }

        /// <summary>
        /// 通过UIType创建一个UI, 此UI需实现AUIEvent;
        /// 可以接受一个初始化参数, 如果一个参数不够，可以用struct
        /// </summary>
        /// <typeparam name="P1"></typeparam>
        /// <param name="uiType"></param>
        /// <param name="arg"></param>
        /// <param name="addToChildren">同时添加到父UI的Children里</param>
        /// <returns></returns>
        public async UniTask<UI> CreateWithUITypeAsync<P1>(string uiType, P1 arg, bool addToChildren, Transform parent,
            CancellationToken cct = default)
        {
            UI child = await this.InnerCreateWithUITypeAsync(uiType, addToChildren, parent, cct);
            if (child is null)
                return null;

            ObjectHelper.Awake(child, arg);

            return child;
        }


        /// <summary>
        /// 通过UIType创建一个UI, 此UI需实现AUIEvent
        /// </summary>
        /// <param name="uiType"></param>
        /// <param name="addToChildren">同时添加到父UI的Children里</param>
        /// <returns></returns>
        public async UniTask<UI> CreateWithUITypeAsync(string uiType, bool addToChildren,
            CancellationToken cct = default)
        {
            UI child = await this.InnerCreateWithUITypeAsync(uiType, addToChildren, cct);
            if (child is null)
                return null;

            ObjectHelper.Awake(child);

            return child;
        }

        /// <summary>
        /// 通过UIType创建一个UI, 此UI需实现AUIEvent;
        /// 可以接受一个初始化参数, 如果一个参数不够，可以用struct
        /// </summary>
        /// <typeparam name="P1"></typeparam>
        /// <param name="uiType"></param>
        /// <param name="arg"></param>
        /// <param name="addToChildren">同时添加到父UI的Children里</param>
        /// <returns></returns>
        public async UniTask<UI> CreateWithUITypeAsync<P1>(string uiType, P1 arg, bool addToChildren,
            CancellationToken cct = default)
        {
            UI child = await this.InnerCreateWithUITypeAsync(uiType, addToChildren, cct);
            if (child is null)
                return null;

            ObjectHelper.Awake(child, arg);

            return child;
        }

        /// <summary>
        /// 通过UIType创建一个UI, 此UI需实现AUIEvent;
        /// 可以接受一个初始化参数, 如果一个参数不够，可以用struct
        /// </summary>
        /// <typeparam name="P1"></typeparam>
        /// <param name="uiType"></param>
        /// <param name="arg"></param>
        /// <param name="addToChildren">同时添加到父UI的Children里</param>
        /// <returns></returns>
        public async UniTask<UI> CreateWithUITypeAsync<P1, P2>(string uiType, P1 arg1, P2 arg2, bool addToChildren,
            CancellationToken cct = default)
        {
            UI child = await this.InnerCreateWithUITypeAsync(uiType, addToChildren, cct);
            if (child is null)
                return null;

            ObjectHelper.Awake(child, arg1, arg2);

            return child;
        }

        /// <summary>
        /// 通过key创建一个UI, 无需实现AUIEvent
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="objFromPool"></param>
        /// <param name="isFromPool"></param>
        /// <returns></returns>
        public async UniTask<T> CreateWithKeyAsync<T>(string key, bool objFromPool = false, bool isFromPool = false)
            where T : UI, new()
        {
            T child = await this.InnerCreateWithKeyAsync<T>(key, objFromPool, isFromPool);
            if (child is null)
                return null;

            ObjectHelper.Awake(child);

            return child;
        }

        /// <summary>
        /// 通过key创建一个UI, 无需实现AUIEvent;
        /// 可以接受一个初始化参数, 如果一个参数不够，可以用struct
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="P1"></typeparam>
        /// <param name="key"></param>
        /// <param name="arg"></param>
        /// <param name="objFromPool"></param>
        /// <param name="isFromPool"></param>
        /// <returns></returns>
        public async UniTask<T> CreateWithKeyAsync<T, P1>(string key, P1 arg, bool objFromPool = false,
            bool isFromPool = false) where T : UI, IAwake<P1>, new()
        {
            T child = await this.InnerCreateWithKeyAsync<T>(key, objFromPool, isFromPool);
            if (child is null)
                return null;

            ObjectHelper.Awake(child, arg);

            return child;
        }

        /// <summary>
        /// 通过key创建一个UI, 无需实现AUIEvent
        /// </summary>
        /// <param name="key"></param>
        /// <param name="objFromPool"></param>
        /// <param name="isFromPool"></param>
        /// <returns></returns>
        public async UniTask<UI> CreateWithKeyAsync(string key, bool objFromPool = false, bool isFromPool = false)
        {
            return await this.CreateWithKeyAsync<UI>(key, objFromPool, isFromPool);
        }

        #endregion

        /// <summary>
        /// 排序列表
        /// </summary>
        /// <param name="comparison"></param>
        public void Sort(Comparison<UI> comparison)
        {
            this.children.Sort(comparison);
            for (int i = 0; i < this.children.Count; i++)
            {
                this.children[i].GameObject.transform.SetSiblingIndex(i);
            }

            //TODO：自加的强制重新排列
            JiYuUIHelper.ForceRefreshLayout(this.Parent);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.children.GetEnumerator();
        }

        #region Events

        void IInternalEvent<OnChildSiblingChanged>.HandleEvent(OnChildSiblingChanged args)
        {
            int curIndex = args.BeforeIndex;
            var ui = this.GetChildAt(curIndex);
            if (ui == args.Target)
            {
                SetChildIndex(curIndex, args.AfterIndex);
            }
        }

        void IInternalEvent<OnChildIdChanged>.HandleEvent(OnChildIdChanged args)
        {
            if (args.BeforeId == 0)
            {
                TryAddToDict(args.Target);
                return;
            }

            ChangeUID(args.Target, args.BeforeId, args.AfterId);
        }

        #endregion
    }

    public static class UIListExtensions
    {
        public static UIListComponent GetList(this UI self)
        {
            return self.TakeComponent<UIListComponent, Transform>(true);
        }

        public static UIListComponent GetList(this UI self, string key)
        {
            var ui = self.GetFromKeyOrPath(key);
            return ui?.GetList();
        }
    }
}