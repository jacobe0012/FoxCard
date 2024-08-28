using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace XFramework
{
    public abstract class UIComponent : XObject
    {
        protected UI parent;

        public UI Parent => parent;

        public T GetParent<T>() where T : UI
        {
            return parent as T;
        }

        protected sealed override void OnStart()
        {
            base.OnStart();
        }

        internal void SetParent(Type fixedType, UI parent)
        {
            if (parent == null)
            {
                Log.Error("UIComponent不能设置parent为null");
                return;
            }

            if (parent == this.parent)
            {
                Log.Error("UIComponent设置相同的parent");
                return;
            }

            this.parent?.RemoveUIComponent(this);
            this.parent = parent;
            this.parent.UIComponents.Add(fixedType, this);
            this.parent.AddTarget(this);
            this.SetParentAfter();
        }

        internal void SetParent(UI parent)
        {
            this.SetParent(this.GetType(), parent);
        }

        protected virtual void SetParentAfter()
        {
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            if (this.parent != null && !this.parent.IsDisposed)
            {
                this.parent.RemoveTarget(this);
                this.parent.RemoveUIComponent(this);
            }

            this.parent = null;
        }
    }

    public abstract class UComponent : UIComponent, IAwake<UnityEngine.Component>
    {
        public abstract void Initialize(Component comp);
    }

    [UIComponentFlag]
    public abstract class UComponent<T> : UComponent where T : UnityEngine.Component
    {
        protected T unityComponent;

        public sealed override void Initialize(Component comp)
        {
            this.unityComponent = comp as T;
            this.EndInitialize();
        }

        protected virtual void EndInitialize()
        {
        }

        public T Get() => this.unityComponent;

        protected sealed override void OnDestroy()
        {
            this.Destroy();
            this.unityComponent = null;
            base.OnDestroy();
        }

        protected virtual void Destroy()
        {
        }
    }

    public abstract class UIBehaviourComponent<T> : UComponent<T> where T : UIBehaviour
    {
        public RectTransformComponent RectTransform => parent?.GetRectTransform();

        public void SetEnabled(bool enabled)
        {
            unityComponent.enabled = enabled;
        }

        public bool GetEnabled()
        {
            return unityComponent.enabled;
        }

        public bool IsActiveAndEnabled()
        {
            return unityComponent.isActiveAndEnabled;
        }
    }
}