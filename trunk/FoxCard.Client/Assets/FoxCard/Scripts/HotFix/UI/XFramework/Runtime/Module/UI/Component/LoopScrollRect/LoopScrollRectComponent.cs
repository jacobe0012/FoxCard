using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace XFramework
{
    [UIComponentFlag]
    public abstract class LoopScrollRectComponent : UIBehaviourComponent<LoopScrollRect>, ILoopScrollRectPrefabKey,
        LoopScrollDataSource, LoopScrollPrefabSource
    {
        private string key;

        public int TotalCount => this.Get().totalCount;

        public UI Content { get; private set; }

        public UIListComponent List { get; private set; }

        public UnityEvent<Vector2> OnValueChanged => this.Get().onValueChanged;


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
            this.key = null;
            this.Get().ClearCells();
            this.SetDataSource(null);
            this.SetPrefabSource(null);
            this.List = null;
            this.Content = null;
            //this.SetTotalCount(0);
            base.Destroy();
        }

        public void SetPrefabSource(LoopScrollPrefabSource prefabSource)
        {
            this.Get().prefabSource = prefabSource;
        }

        public void SetDataSource(LoopScrollDataSource dataSource)
        {
            this.Get().dataSource = dataSource;
        }

        public void SetDefaultValue()
        {
            this.SetDataSource(this);
            this.SetPrefabSource(this);
        }

        public void SetKey(string key)
        {
            this.key = key;
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

        #region Interface

        public string Key => key;

        void LoopScrollDataSource.ProvideData(Transform transform, int idx)
        {
            this.ProvideData(transform, idx);
        }

        GameObject LoopScrollPrefabSource.GetObject(int index)
        {
            if (this.Key.IsNullOrEmpty())
                throw new ArgumentNullException("LoopScrollPrefabSource.GetObject获取对象失败，未设置Key");

            return ResourcesManager.Instantiate(this, this.Key, this.Get().content, true);
        }

        void LoopScrollPrefabSource.ReturnObject(Transform trans)
        {
            this.ReturnObject(trans);
            GameObject obj = trans.gameObject;
            ResourcesManager.ReleaseInstance(obj);
        }

        #endregion
    }

    public class LoopScrollRectComponent<T> : LoopScrollRectComponent where T : UI, new()
    {
        protected ILoopScrollRectProvide<T> provideData;

        protected Dictionary<int, T> children = new Dictionary<int, T>();

        protected override void Destroy()
        {
            base.Destroy();
            this.children.Clear();
            this.provideData = null;
        }

        public void SetProvideData(ILoopScrollRectProvide<T> provideData)
        {
            this.provideData = provideData;
        }

        public void SetProvideData(string key, ILoopScrollRectProvide<T> provideData)
        {
            this.SetKey(key);
            this.SetProvideData(provideData);
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
            GameObject obj = transform.gameObject;
            int instanceId = obj.GetInstanceID();
            this.RemoveChild(instanceId);

            string name = instanceId.ToString();
            T child = UI.Create<T>(name, obj, true); //这里创建之后并没有执行Awake，如果有需要可以在接收方法里自己调用Awake
            //this.Parent.AddChild(child);
            var list = this.List;
            list.AddChild(child, false);
            this.children.Add(instanceId, child);
            this.provideData.ProvideData(child, idx);
        }

        protected override void ReturnObject(Transform trans)
        {
            GameObject obj = trans.gameObject;
            int instanceId = obj.GetInstanceID();
            this.RemoveChild(instanceId);
        }

        #endregion
    }

    public static class LoopScrollRectExtensions
    {
        public static LoopScrollRectComponent<T> GetLoopScrollRect<T>(this ILoopScrollRectProvide<T> self)
            where T : UI, new()
        {
            if (!(self is UI ui))
                return null;

            return self.GetLoopScrollRect(ui);
        }

        public static LoopScrollRectComponent<T> GetLoopScrollRect<T>(this ILoopScrollRectProvide<T> self, UI ui)
            where T : UI, new()
        {
            var comp = ui.GetLoopScrollRect<T>();
            return comp;
        }

        public static LoopScrollRectComponent<T> GetLoopScrollRect<T>(this UI self) where T : UI, new()
        {
            var comp = self.GetUIComponent<LoopScrollRectComponent<T>>();
            if (comp != null)
                return comp;

            var scrollRect = self.GetComponent<LoopScrollRect>();
            if (!scrollRect)
                return null;

            comp = ObjectFactory.Create<LoopScrollRectComponent<T>, UnityEngine.Component>(scrollRect, true);
            if (self is ILoopScrollRectProvide<T> provide)
                comp.SetProvideData(provide);

            self.AddUIComponent(comp);

            return comp;
        }

        public static LoopScrollRectComponent<T> GetLoopScrollRect<T>(this UI self, string key) where T : UI, new()
        {
            var ui = self.GetFromKeyOrPath(key);
            return ui?.GetLoopScrollRect<T>();
        }
    }
}