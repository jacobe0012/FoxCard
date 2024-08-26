using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using XFramework.UIEventType;

namespace XFramework
{
    public partial class RectTransformComponent : UComponent<RectTransform>
    {
        public int ChildCount => this.Get().childCount;
        private Vector3 originalLocalScale;

        protected override void EndInitialize()
        {
            base.EndInitialize();
            originalLocalScale = Get().localScale;
        }

        protected override void Destroy()
        {
            base.Destroy();
            Get().localScale = originalLocalScale;
        }

        public Transform GetChild(int index)
        {
            return this.Get().GetChild(index);
        }

        public Transform Find(string path)
        {
            return this.Get().Find(path);
        }

        public void SetParent(Transform parent)
        {
            this.Get().SetParent(parent);
        }

        public void SetParent(Transform parent, bool worldPositionStays)
        {
            this.Get().SetParent(parent, worldPositionStays);
        }

        public bool AddChild(Transform child)
        {
            if (child.parent != this.Get())
            {
                child.SetParent(this.Get());
                return true;
            }

            return false;
        }

        public bool AddChild(Transform child, bool worldPositionStays)
        {
            if (child.parent != this.Get())
            {
                child.SetParent(this.Get(), worldPositionStays);
                return true;
            }

            return false;
        }

        public void AddChildAt(Transform child, int index)
        {
            this.Get().AddChildAt(child, index);
        }

        public void AddChildAt(Transform child, int index, bool worldPositionStays)
        {
            this.Get().AddChildAt(child, index, worldPositionStays);
        }

        #region Child Controller

        private void OnSiblingChanged(int before, int after)
        {
            var me = this.parent;
            me.Parent?.Publish(new UIEventType.OnChildSiblingChanged
            {
                Target = me,
                BeforeIndex = before,
                AfterIndex = after
            });
        }

        public void SetSiblingIndex(int index)
        {
            var childCount = this.Get().parent.childCount;
            if (childCount == 0)
                return;

            index %= childCount;
            if (index < 0)
                index += childCount;

            int beforeIndex = this.GetSiblingIndex();
            if (beforeIndex == index)
                return;

            this.Get().SetSiblingIndex(index);
            OnSiblingChanged(beforeIndex, index);
        }

        public void SetAsLastSibling()
        {
            int index = this.Get().parent.childCount - 1;
            if (index < 0)
                return;

            int beforeIndex = this.GetSiblingIndex();
            if (beforeIndex == index)
                return;

            this.Get().SetAsLastSibling();
            OnSiblingChanged(beforeIndex, index);
        }

        public void SetAsFirstSibling()
        {
            int index = 0;
            int beforeIndex = this.GetSiblingIndex();
            if (beforeIndex == index)
                return;

            this.Get().SetAsFirstSibling();
            OnSiblingChanged(beforeIndex, index);
        }

        public int GetSiblingIndex()
        {
            return this.Get().GetSiblingIndex();
        }

        #endregion
    }

    public static class TransformExtensions
    {
        public static RectTransformComponent GetRectTransform(this UI self)
        {
            return self.TakeComponent<UIListComponent, Transform>(true);
        }

        public static RectTransformComponent GetRectTransform(this UI self, string key)
        {
            UI ui = self.GetFromKeyOrPath(key);
            return ui?.GetRectTransform();
        }

        public static int GetSiblingIndex(this UI self)
        {
            return self.GetRectTransform().GetSiblingIndex();
        }

        public static void SetSiblingIndex(this UI self, int index)
        {
            self.GetRectTransform().SetSiblingIndex(index);
        }

        public static void SetAsLastSibling(this UI self)
        {
            self.GetRectTransform().SetAsLastSibling();
        }

        public static void SetAsFirstSibling(this UI self)
        {
            self.GetRectTransform().SetAsFirstSibling();
        }
    }
}