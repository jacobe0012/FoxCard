using System;
using System.Collections.Generic;
using UnityEngine;

namespace XFramework
{
    public class UI : EventObject
    {
        private string name;

        /// <summary>
        /// 根UI的Key
        /// </summary>
        private string rootUIType;

        private GameObject gameObject;

        private UILayer layer;

        private UI parent;

        private UI rootUI;

        protected Reference reference;

        /// <summary>
        /// 子UI
        /// </summary>
        protected Dictionary<string, UI> children;

        /// <summary>
        /// 此UI身上挂的UI组件
        /// </summary>
        protected Dictionary<Type, UIComponent> uiComponents;

        public GameObject GameObject => gameObject;

        public string Name => name;

        /// <summary>
        /// 层级
        /// </summary>
        public UILayer Layer => layer;

        public UI Parent => parent;

        /// <summary>
        /// 子UI
        /// </summary>
        public Dictionary<string, UI> Children => children;

        /// <summary>
        /// 此UI身上挂的UI组件
        /// </summary>
        public Dictionary<Type, UIComponent> UIComponents => uiComponents;

        #region Create

        /// <summary>
        /// 创建一个UI类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="isFromPool"></param>
        /// <returns></returns>
        public static T Create<T>(bool isFromPool = false) where T : UI, new()
        {
            return ObjectFactory.CreateNoInit<T>(isFromPool);
        }

        /// <summary>
        /// 创建一个UI类并绑定GameObject
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <param name="isFromPool"></param>
        /// <returns></returns>
        public static T Create<T>(string key, GameObject obj, bool isFromPool) where T : UI, new()
        {
            T ui = UI.Create<T>(isFromPool);
            ui.Bind(obj, key);
            return ui;
        }

        /// <summary>
        /// 创建一个UI类并绑定GameObject
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <param name="isFromPool"></param>
        /// <returns></returns>
        public static UI Create(Type type, string key, GameObject obj, bool isFromPool)
        {
            UI ui = UI.Create(type, isFromPool);
            if (ui is null)
                return null;

            ui.Bind(obj, key);
            return ui;
        }

        /// <summary>
        /// 创建一个UI类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <param name="isFromPool"></param>
        /// <returns></returns>
        public static UI Create(Type type, bool isFromPool = false)
        {
            return ObjectFactory.CreateNoInit(type, isFromPool) as UI;
        }

        #endregion

        protected sealed override void OnStart()
        {
            children = ObjectPool.Instance.Fetch<Dictionary<string, UI>>();
            uiComponents = ObjectPool.Instance.Fetch<Dictionary<Type, UIComponent>>();
        }

        /// <summary>
        /// 设置UI的父类
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="addToChildren"></param>
        internal void SetParent(UI parent, bool addToChildren)
        {
            if (parent == null)
            {
                Log.Error("UI不能主动设置parent为null");
                return;
            }

            if (parent == this)
            {
                Log.Error("UI不能设置parent为this");
                return;
            }

            if (parent == this.parent)
            {
                Log.Error("UI设置相同的parent");
                return;
            }

            this.parent?.RemoveFromChildren(this);
            this.parent = parent;
            this.SetRootUIType(parent.RootUIType());
            this.SetLayer(parent.Layer);

            // 递归重置子UI的属性
            foreach (var child in this.children.Values)
            {
                //先置空，不然无法重复设置parent
                child.parent = null;
                child.SetParent(this, false);
            }

            if (addToChildren)
                parent.children.Add(this.Name, this);

            this.SetParentAfter();
        }

        internal void RemoveUIComponent(UIComponent component)
        {
            if (component == null)
                return;

            this.uiComponents.Remove(component.GetType());
        }

        private void RemoveFromChildren(UI child)
        {
            if (child == null)
                return;

            this.children.Remove(child.Name);
        }

        private void SetRootUIType(string rootUIType)
        {
            if (this.rootUIType == rootUIType)
                return;

            this.rootUIType = rootUIType;
            this.rootUI = null;
            this.RootUI();
        }

        /// <summary>
        /// 获取引用组件里的对象
        /// </summary>
        /// <param name="key"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        protected UI GetFromReference(string key, bool error)
        {
            UI ui = this.GetChild(key);
            if (ui != null)
                return ui;

            ui = this.AddChildFromReference<UI>(key, true, error);
            return ui;
        }

        /// <summary>
        /// 根据路径获取子对象
        /// </summary>
        /// <param name="path"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        protected UI GetFromPath(string path, bool error)
        {
            UI ui = this.GetChild(path);
            if (ui != null)
                return ui;

            ui = this.AddChildFromPath<UI>(path, true, error);
            return ui;
        }

        public void Bind(GameObject obj, string key)
        {
            if (this.gameObject)
            {
                Log.Error($"请勿重复绑定GameObject, name = {key}");
                return;
            }

            this.name = key;
            this.gameObject = obj;
            this.reference = obj.Reference();
            if (this.reference is UIReference uiReference)
            {
                var comp = ObjectFactory.Create<LanguageComponent, UnityEngine.Component>(uiReference, true);
                this.AddUIComponent(comp);
            }

            base.AddEvent();

#if UNITY_EDITOR
            // 添加结构信息组件，可以在Inspector面板里看到UI内的子对象和组件信息
            if (!gameObject.TryGetComponent<UIStructure>(out var uiStructureComp))
                uiStructureComp = GameObject.AddComponent<UIStructure>();

            uiStructureComp.SetUIObject(this);
#endif
        }

        /// <summary>
        /// 设置层级
        /// </summary>
        /// <param name="layer"></param>
        public void SetLayer(UILayer layer)
        {
            this.layer = layer;
        }

        /// <summary>
        /// 根UI类型
        /// </summary>
        /// <returns></returns>
        public string RootUIType()
        {
            if (this.rootUIType.IsNullOrEmpty())
            {
                if (parent is null)
                    this.SetRootUIType(this.Name);
                else
                    this.SetRootUIType(parent.RootUIType());
            }

            return this.rootUIType;
        }

        /// <summary>
        /// 根UI
        /// </summary>
        /// <returns></returns>
        public UI RootUI()
        {
            if (this.rootUI != null)
                return this.rootUI;

            string rootType = this.RootUIType();
            if (rootType.IsNullOrEmpty())
                return null;

            var uiMgr = Common.Instance.Get<UIManager>();
            this.rootUI = uiMgr?.Get(rootType);

            return this.rootUI;
        }

        /// <summary>
        /// 根UI
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T RootUI<T>() where T : UI
        {
            return this.RootUI() as T;
        }

        /// <summary>
        /// 父对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetParent<T>() where T : UI
        {
            return this.parent as T;
        }

        /// <summary>
        /// 获取引用组件里的对象
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public UI GetFromReference(string key)
        {
            return this.GetFromReference(key, true);
        }

        /// <summary>
        /// 根据路径获取子对象
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public UI GetFromPath(string path)
        {
            return this.GetFromPath(path, true);
        }

        /// <summary>
        /// 通过引用组件里的Key或者Path获取对象
        /// </summary>
        /// <param name="keyOrPath"></param>
        /// <returns></returns>
        public UI GetFromKeyOrPath(string keyOrPath)
        {
            return this.GetFromReference(keyOrPath, false) ?? this.GetFromPath(keyOrPath, false);
        }

        #region UIComponent

        /// <summary>
        /// 添加一个UI组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="component"></param>
        public void AddUIComponent(UIComponent component)
        {
            this.AddUIComponent(component.GetType(), component);
        }

        /// <summary>
        /// 添加一个UI组件，并使用固定类型作为Key
        /// </summary>
        /// <param name="fixedType"></param>
        /// <param name="component"></param>
        private void AddUIComponent(Type fixedType, UIComponent component)
        {
            Type type = fixedType;
            if (!uiComponents.ContainsKey(type))
            {
                component.SetParent(type, this);
            }
            else
            {
                component?.Dispose();
                Log.Error($"重复添加UIComponent, Name is {this.Name}, ComponentType is {type}");
            }
        }

        /// <summary>
        /// 获取一个UI组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inherit">继承</param>
        /// <returns></returns>
        public T GetUIComponent<T>(bool inherit = true) where T : UIComponent
        {
            if (!uiComponents.TryGetValue(typeof(T), out var comp))
            {
                if (inherit && comp is null)
                {
                    foreach (var component in uiComponents.Values)
                    {
                        if (component is T)
                        {
                            comp = component;
                            break;
                        }
                    }
                }
            }

            return comp as T;
        }

        /// <summary>
        /// 获取一个UI组件
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public UIComponent GetUIComponent(Type type)
        {
            if (uiComponents.TryGetValue(type, out var comp))
            {
                // 判断相等或继承关系
                var compType = comp.GetType();
                if (compType == type || compType.IsSubclassOf(type))
                    return comp;
            }

            return null;
        }

        /// <summary>
        /// 移除一个UI组件
        /// </summary>
        /// <param name="type"></param>
        public void RemoveUIComponent(Type type)
        {
            this.GetUIComponent(type)?.Dispose();
        }

        /// <summary>
        /// 移除一个UI组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inherit">继承</param>
        public void RemoveUIComponent<T>(bool inherit = false) where T : UIComponent
        {
            this.GetUIComponent<T>(inherit)?.Dispose();
        }

        #endregion

        #region Children Controller

        /// <summary>
        /// 获取子UI
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public UI GetChild(string key)
        {
            children.TryGetValue(key, out UI child);
            return child;
        }

        /// <summary>
        /// 获取子UI
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T GetChild<T>(string key) where T : UI
        {
            return this.GetChild(key) as T;
        }

        /// <summary>
        /// 包含子UI
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ContainChild(string key)
        {
            return children.ContainsKey(key);
        }

        /// <summary>
        /// 移除一个子UI
        /// </summary>
        /// <param name="uiType"></param>
        public void RemoveChild(string uiType)
        {
            if (children.TryGetValue(uiType, out UI ui))
            {
                children.Remove(ui.Name);
                ui.Dispose();
            }
        }

        #endregion

        #region AddChild

        /// <summary>
        /// 通过key找到gameObject，然后用类包起来后加到Children里
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <param name="key"></param>
        /// <param name="isFromPool"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        private UI AddChildFromReference(Type type, string key, bool isFromPool, bool error)
        {
            GameObject obj = this.reference?.GetChild(key);
            if (!obj)
            {
                if (error)
                    Log.Error($"未找到key为{key}的对象");

                return null;
            }

            UI child = UI.Create(type, key, obj, isFromPool);
            if (child is null)
                return null;

            if (this.AddChild(child))
                return child;

            return null;
        }

        /// <summary>
        /// 通过key找到gameObject，然后用类包起来后加到Children里
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="isFromPool"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        private T AddChildFromReference<T>(string key, bool isFromPool, bool error) where T : UI, new()
        {
            return this.AddChildFromReference(typeof(T), key, isFromPool, error) as T;
        }

        /// <summary>
        /// 通过Path找到gameObject，然后用类包起来后加到Children里
        /// </summary>
        /// <param name="type"></param>
        /// <param name="path"></param>
        /// <param name="isFromPool"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        private UI AddChildFromPath(Type type, string path, bool isFromPool, bool error)
        {
            GameObject obj = this.gameObject.transform.Find(path)?.gameObject;
            if (!obj)
            {
                if (error)
                    Log.Error($"未找到path为{path}的对象");

                return null;
            }

            UI child = UI.Create(type, path, obj, isFromPool);
            if (child is null)
                return null;

            if (this.AddChild(child))
                return child;

            return null;
        }

        /// <summary>
        /// 通过Path找到gameObject，然后用类包起来后加到Children里
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <param name="isFromPool"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        private T AddChildFromPath<T>(string path, bool isFromPool, bool error) where T : UI, new()
        {
            return this.AddChildFromPath(typeof(T), path, isFromPool, error) as T;
        }

        /// <summary>
        /// 添加obj到UI类，用类包装起来
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <param name="isFromPool"></param>
        /// <returns></returns>
        public UI AddChild(string key, GameObject obj, bool isFromPool = false)
        {
            if (this.ContainChild(key))
            {
                Log.Error($"已存在name为{key}的子UI，请勿重复添加");
                return null;
            }

            UI child = UI.Create<UI>(key, obj, isFromPool);
            if (this.AddChild(child))
            {
                return child;
            }

            return null;
        }

        /// <summary>
        /// 添加一个子UI
        /// </summary>
        /// <param name="ui"></param>
        public bool AddChild(UI ui)
        {
            if (ui == this)
                return false;

            if (!ContainChild(ui.Name))
            {
                ui.SetParent(this, true);
                return true;
            }
            else
            {
                Log.Error($"重复添加ChildUI, name = {ui.Name}, parent = {ui}");
                ui?.Dispose();
                return false;
            }
        }

        /// <summary>
        /// 添加一个子UI，用不同的类
        /// </summary>
        /// <param name="type"></param>
        /// <param name="keyOrPath"></param>
        /// <param name="isFromPool"></param>
        /// <returns></returns>
        public UI AddChild(Type type, string keyOrPath, bool isFromPool = false)
        {
            if (this.ContainChild(keyOrPath))
            {
                Log.Error($"重复添加UI，keyOrPath = {keyOrPath}, name = {this.Name}, type = {this}");
                return null;
            }

            UI child = this.AddChildFromReference(type, keyOrPath, isFromPool, false) ??
                       this.AddChildFromPath(type, keyOrPath, isFromPool, true);
            ObjectHelper.Awake(child);
            return child;
        }

        /// <summary>
        /// 添加一个子UI，用不同的类
        /// </summary>
        /// <typeparam name="P1"></typeparam>
        /// <param name="type"></param>
        /// <param name="keyOrPath"></param>
        /// <param name="args"></param>
        /// <param name="isFromPool"></param>
        /// <returns></returns>
        public UI AddChild<P1>(Type type, string keyOrPath, P1 args, bool isFromPool = false)
        {
            if (this.ContainChild(keyOrPath))
            {
                Log.Error($"重复添加UI，keyOrPath = {keyOrPath}, name = {this.Name}, type = {this}");
                return null;
            }

            UI child = this.AddChildFromReference(type, keyOrPath, isFromPool, false) ??
                       this.AddChildFromPath(type, keyOrPath, isFromPool, true);
            ObjectHelper.Awake(child, args);
            return child;
        }

        /// <summary>
        /// 添加一个子UI，用不同的类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keyOrPath"></param>
        /// <param name="isFromPool"></param>
        /// <returns></returns>
        public T AddChild<T>(string keyOrPath, bool isFromPool = false) where T : UI, new()
        {
            return this.AddChild(typeof(T), keyOrPath, isFromPool) as T;
        }

        /// <summary>
        /// 添加一个子UI，带一个参数，用不同的类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="P1"></typeparam>
        /// <param name="keyOrPath"></param>
        /// <param name="args"></param>
        /// <param name="isFromPool"></param>
        /// <returns></returns>
        public T AddChild<T, P1>(string keyOrPath, P1 args, bool isFromPool = false) where T : UI, IAwake<P1>, new()
        {
            return this.AddChild(typeof(T), keyOrPath, args, isFromPool) as T;
        }

        #endregion

        /// <summary>
        /// 获取自身的组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetComponent<T>() where T : UnityEngine.Component
        {
            return this.GetComponent(typeof(T)) as T;
        }

        /// <summary>
        /// 获取自身的组件
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public UnityEngine.Component GetComponent(Type type)
        {
            if (this.reference)
                return this.reference.Get(type);

            return this.gameObject.GetComponent(type);
        }

        public void SetActive(bool active)
        {
            gameObject?.SetActive(active);
        }

        public void SetCanvasGroup(bool active)
        {
            this.gameObject.SetCanvasGroup(active);
        }

        /// <summary>
        /// 关闭本界面
        /// </summary>
        protected void Close()
        {
            //if (parent is null)
            //    UIHelper.Remove(Name);
            //else
            //    Dispose();

            this.Dispose();
        }

        /// <summary>
        /// 设置父对象后
        /// </summary>
        protected virtual void SetParentAfter()
        {
        }

        /// <summary>
        /// 获得焦点，在关闭一个UI后，位于最上层的UI会执行此方法，仅限于UIManager管理的类，子UI请自行管理
        /// </summary>
        public virtual void OnFocus()
        {
            //Log.Error($" OnFocus {name}");
        }

        /// <summary>
        /// 失去焦点，在打开一个UI前，位于最上层的UI会执行此方法，仅限于UIManager管理的类，子UI请自行管理
        /// </summary>
        public virtual void OnBlur()
        {
            //TODO:
            // if (this.name != UIType.UIPanel_JiyuGame)
            // {
            //     
            // }


            //Log.Error($"  OnBlur {name}");
        }

        /// <summary>
        /// 当前顶层UI在PC端按了Esc/手机端按了返回后执行，仅限于UIManager管理的类，子UI请自行管理
        /// </summary>
        public virtual void OnCancel()
        {
        }

        /// <summary>
        /// 关闭时调用
        /// </summary>
        protected virtual void OnClose()
        {
        }

        protected sealed override void OnDestroy()
        {
#if UNITY_EDITOR
            if (gameObject.TryGetComponent<UIStructure>(out var uiStructureComp))
                uiStructureComp.SetUIObject(null);
#endif

            //if (parent is null)

            UIHelper.Remove(this.Name);

            foreach (UIComponent comp in uiComponents.Values)
            {
                comp.Dispose();
            }

            uiComponents.Clear();
            ObjectPool.Instance.Recycle(uiComponents);
            uiComponents = null;

            foreach (UI child in children.Values)
            {
                child.Dispose();
            }

            children.Clear();
            ObjectPool.Instance.Recycle(children);
            children = null;

            OnClose();

            if (parent != null && !parent.IsDisposed)
            {
                parent.RemoveFromChildren(this);
            }

            parent = null;
            name = null;
            rootUIType = null;
            rootUI = null;
            reference = null;
            gameObject = null;
        }
    }
}