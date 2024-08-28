using System;
using System.Collections.Generic;
using HotFix_UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace XFramework
{
    [UIComponentFlag]
    public abstract class LoopScrollRectMultiComponent : UIBehaviourComponent<LoopScrollRectMulti>,
        LoopScrollMultiDataSource, LoopScrollPrefabSource
    {
        public int TotalCount => this.Get().totalCount;

        public UI Content { get; private set; }

        public UIListComponent List { get; private set; }

        public UnityEvent<Vector2> OnValueChanged => this.Get().onValueChanged;

        // public UnityEvent OnEndDrag => this.Get().m_OnEndDrag;
        //
        // public UnityEvent<Vector2> OnDrag => this.Get().m_OnDrag;
        //
        // public UnityEvent OnBeginDrag => this.Get().m_OnBeginDrag;

        public Func<int, string> OnGetObject;

        protected override void EndInitialize()
        {
            base.EndInitialize();
            this.SetDefaultValue();
        }

        protected override void SetParentAfter()
        {
            base.SetParentAfter();

            if (this.Content is null)
            {
                if (this.Get() != null && this.Get().content != null)
                {
                    Content = this.parent.AddChild("Content", this.Get().content.gameObject, true);
                }
            }
            else
            {
                this.parent.AddChild(this.Content);
            }

            List = Content.GetList();
        }

        protected override void Destroy()
        {
            this.OnValueChanged.RemoveAllListeners();

            this.Get().ClearCells();
            this.SetDataSource(null);
            this.SetPrefabSource(null);
            this.List = null;
            this.Content = null;
            this.OnGetObject = null;
            //this.SetTotalCount(0);
            base.Destroy();
        }

        public void SetPrefabSource(LoopScrollPrefabSource prefabSource)
        {
            this.Get().prefabSource = prefabSource;
        }

        public void SetDataSource(LoopScrollMultiDataSource dataSource)
        {
            this.Get().dataSource = dataSource;
        }

        public void SetDefaultValue()
        {
            this.SetDataSource(this);
            this.SetPrefabSource(this);
        }


        public void SetTotalCount(int totalCount)
        {
            this.Get().totalCount = totalCount;
        }

        public void RefillCells(int startItem = 0, float contentOffset = 0)
        {
            this.Get().RefillCells(startItem, contentOffset);
        }

        public void RefillCellsFromEnd(int endItem = 0, bool alignStart = false)
        {
            this.Get().RefillCellsFromEnd(endItem, alignStart);
        }

        public void RefreshCells()
        {
            this.Get().RefreshCells();
        }

        public void SetVerticalNormalizedPosition(float value)
        {
            this.Get().verticalNormalizedPosition = value;
        }

        public void SetHorizontalNormalizedPosition(float value)
        {
            this.Get().horizontalNormalizedPosition = value;
        }

        protected abstract void ProvideData(Transform transform, int index);

        protected abstract void ReturnObject(Transform transform);

        //protected abstract GameObject GetObject(int index);

        #region Interface

        void LoopScrollMultiDataSource.ProvideData(Transform transform, int idx)
        {
            this.ProvideData(transform, idx);
        }

        GameObject LoopScrollPrefabSource.GetObject(int index)
        {
            //Log.Debug($"GetObject");
            var str = OnGetObject?.Invoke(index);
            if (str.IsNullOrEmpty())
                throw new ArgumentNullException("LoopScrollPrefabSource.GetObject获取对象失败，未设置Key");

            return ResourcesManager.Instantiate(this, str, this.Get().content, true);

            // if (this.Key.IsNullOrEmpty())
            //     throw new ArgumentNullException("LoopScrollPrefabSource.GetObject获取对象失败，未设置Key");
            //
            // return ResourcesManager.Instantiate(this, this.Key, this.Get().content, true);
        }

        void LoopScrollPrefabSource.ReturnObject(Transform trans)
        {
            this.ReturnObject(trans);
            GameObject obj = trans.gameObject;
            ResourcesManager.ReleaseInstance(obj);
        }

        #endregion
    }

    public class LoopScrollRectMultiComponent<T> : LoopScrollRectMultiComponent where T : UI, new()
    {
        protected ILoopScrollRectMultiProvide provideData;

        protected Dictionary<int, UI> children = new Dictionary<int, UI>();


        protected override void Destroy()
        {
            base.Destroy();
            this.children.Clear();
            this.provideData = null;
            //this.OnGetObject = null;
        }

        public void SetProvideData(ILoopScrollRectMultiProvide provideData)
        {
            this.provideData = provideData;
        }

        protected void RemoveChild(int instanceId)
        {
            if (this.children.TryRemove(instanceId, out var child))
            {
                child.Dispose();
            }
        }

        #region override

        protected override void ProvideData(Transform transform, int idx)
        {
            //Log.Debug($"ProvideData");
            GameObject obj = transform.gameObject;
            int instanceId = obj.GetInstanceID();
            //Log.Debug($"go:{obj.name}");
            var str = $"UI{obj.name}";
            string noCloneStr = str.Replace("(Clone)", "").Trim();

            this.RemoveChild(instanceId);

            //uia.GameObject.
            string name = instanceId.ToString();


            var uiEventManager = UIHelper.TryGetUIEventManager();

            var uiEvent = uiEventManager?.Get(noCloneStr);

            UI child = uiEvent.OnCreate();
            child.Bind(obj, name);

            //T child = UI.Create<T>(name, obj, true); //这里创建之后并没有执行Awake，如果有需要可以在接收方法里自己调用Awake
            //this.Parent.AddChild(child);
            var list = this.List;

            list.AddChild(child, false);
            this.children.Add(instanceId, child);
            this.provideData.ProvideData(child, idx);
        }

        protected override void ReturnObject(Transform trans)
        {
            //Log.Debug($"ReturnObject");
            GameObject obj = trans.gameObject;
            int instanceId = obj.GetInstanceID();
            this.RemoveChild(instanceId);
        }

        // protected override GameObject GetObject(int index)
        // {
        //     var go = OnGetObject?.Invoke(index);
        //
        //     return go;
        // }

        #endregion
    }

    public static class LoopScrollRectMultiExtensions
    {
        // public static LoopScrollRectMultiComponent<T> GetLoopScrollRectMulti<T>(this ILoopScrollRectProvide<T> self)
        //     where T : UI, new()
        // {
        //     if (!(self is UI ui))
        //         return null;
        //
        //     return self.GetLoopScrollRectMulti(ui);
        // }
        //
        // public static LoopScrollRectMultiComponent<T> GetLoopScrollRectMulti<T>(this ILoopScrollRectProvide<T> self,
        //     UI ui)
        //     where T : UI, new()
        // {
        //     var comp = ui.GetLoopScrollRectMulti<T>();
        //     return comp;
        // }

        public static LoopScrollRectMultiComponent<T> GetLoopScrollRectMulti<T>(this UI self) where T : UI, new()
        {
            var comp = self.GetUIComponent<LoopScrollRectMultiComponent<T>>();
            if (comp != null)
                return comp;

            var scrollRect = self.GetComponent<LoopScrollRectMulti>();
            if (!scrollRect)
                return null;

            comp = ObjectFactory.Create<LoopScrollRectMultiComponent<T>, UnityEngine.Component>(scrollRect, true);
            if (self is ILoopScrollRectMultiProvide provide)
                comp.SetProvideData(provide);

            self.AddUIComponent(comp);

            return comp;
        }

        public static LoopScrollRectMultiComponent<T> GetLoopScrollRectMulti<T>(this UI self, string key)
            where T : UI, new()
        {
            var ui = self.GetFromKeyOrPath(key);
            return ui?.GetLoopScrollRectMulti<T>();
        }
    }
}