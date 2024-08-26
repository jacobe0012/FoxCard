using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XFramework
{
    public delegate void RedDotChanged(RedDotNode state);

    public class RedDotNode : IDisposable
    {
        /// <summary>
        /// 已释放
        /// </summary>
        public bool IsDisposed { get; private set; }

        /// <summary>
        /// 红点状态
        /// </summary>
        private bool _isRed;

        /// <summary>
        /// 脏标记，若为true则允许刷新红点
        /// </summary>
        private bool isDirty;

        /// <summary>
        /// 红点为true的子节点
        /// </summary>
        private HashSet<RedDotNode> redNodes;

        /// <summary>
        /// 红点状态改变后的回调
        /// </summary>
        private RedDotChanged changedAction;

        /// <summary>
        /// 叶子节点
        /// </summary>
        private bool leafNode => children.Count == 0;

        /// <summary>
        /// 节点名称
        /// </summary>
        private string name;

        /// <summary>
        /// 节点父对象
        /// </summary>
        protected RedDotNode parent;

        /// <summary>
        /// 子节点
        /// </summary>
        protected Dictionary<string, RedDotNode> children;

        /// <summary>
        /// 节点名称
        /// </summary>
        public string Name => name;

        /// <summary>
        /// 红点状态
        /// </summary>
        public bool IsRed 
        {
            get => leafNode ? _isRed : redNodes.Count > 0;
            set
            {
                if (leafNode)
                {
                    if (_isRed != value)
                    {
                        _isRed = value;
                        OnRedDotChanged();
                    }
                }
                else
                {
                    Log.Error("禁止设置非叶子节点的红点状态");
                }
            }
        }

        public RedDotNode()
        {
            IsDisposed = false;
            redNodes = new HashSet<RedDotNode>();
            children = new Dictionary<string, RedDotNode>();
            if (this is IEvent) EventManager.Instance.AddTarget(this);
            Init();
        }

        internal void Awake(string name, RedDotNode parent)
        {
            this.name = name;
            this.SetParent(parent);
        }

        internal void SetParent(RedDotNode parent)
        {
            if (this.parent == parent)
                return;

            if (parent == this)
                return;

            this.parent?.RemoveFromChildren(this);
            this.parent = parent;
            this.parent?.AddFromChildren(this);
        }

        /// <summary>
        /// 设置脏标记
        /// </summary>
        /// <param name="dirty"></param>
        private void SetDirty(bool dirty)
        {
            var parentDirty = false;

            // 如果设置为false，则检查父对象的脏标记
            // 父对象脏，子对象一定为脏
            // 子对象脏，父对象不一定脏
            if (!dirty)
            {
                var parentNode = this.parent;
                while (!parentDirty && parentNode != null)
                {
                    parentDirty = parentNode.isDirty;
                    parentNode = parentNode.parent;
                }
            }

            var newDirty = dirty || parentDirty || changedAction != null;
            if (newDirty == this.isDirty)
                return;

            this.isDirty = newDirty;
            if (newDirty)
                Refresh();

            foreach (var child in children.Values)
            {
                child.SetDirty(newDirty);
            }
        }

        /// <summary>
        /// 子节点状态改变
        /// </summary>
        /// <param name="childNode"></param>
        private void ChildStateChanged(RedDotNode childNode)
        {
            if (childNode.IsRed)
                AddFromRedNodes(childNode);
            else
                RemoveFromRedNodes(childNode);
        }

        /// <summary>
        /// 添加到红点为ture列表
        /// </summary>
        /// <param name="childNode"></param>
        private void AddFromRedNodes(RedDotNode childNode)
        {
            var red = IsRed;
            if (!redNodes.Add(childNode))
                return;

            var newRed = IsRed;
            if (red != newRed)
            {
                OnRedDotChanged();
            }
        }

        /// <summary>
        /// 从红点true列表移除
        /// </summary>
        /// <param name="childNode"></param>
        private void RemoveFromRedNodes(RedDotNode childNode)
        {
            var red = IsRed;
            if (!redNodes.Remove(childNode))
                return;

            var newRed = IsRed;
            if (red != newRed)
            {
                OnRedDotChanged();
            }
        }

        /// <summary>
        /// 添加到children
        /// </summary>
        /// <param name="childNode"></param>
        private void AddFromChildren(RedDotNode childNode)
        {
            children.Add(childNode.name, childNode);
            if (childNode.IsRed)
            {
                AddFromRedNodes(childNode);
            }
        }

        /// <summary>
        /// 从children里移除
        /// </summary>
        /// <param name="childNode"></param>
        private void RemoveFromChildren(RedDotNode childNode)
        {
            if (children.Remove(childNode.name))
            {
                if (!IsDisposed)
                {
                    RemoveFromRedNodes(childNode);
                }
            }
        }

        /// <summary>
        /// 刷新当下所有节点(包括自己)
        /// </summary>
        private void RefreshAll()
        {
            Refresh();
            foreach (var child in children.Values)
            {
                child.RefreshAll();
            }
        }

        protected virtual void Init()
        {

        }

        protected virtual void OnDestroy()
        {

        }

        /// <summary>
        /// 刷新方法
        /// </summary>
        protected void Refresh()
        {
            // 非叶子节点不用刷新
            // 子节点刷新就行了
            if (!leafNode)
                return;

            // 如果没有添加监听则先不刷新
            // 等添加监听时再刷新
            if (!isDirty)
                return;

            RefreshRedDot();
        }

        /// <summary>
        /// 刷新红点的具体实现
        /// </summary>
        protected virtual void RefreshRedDot() { }

        /// <summary>
        /// 红点状态改变时
        /// </summary>
        protected void OnRedDotChanged()
        {
            parent?.ChildStateChanged(this);
            changedAction?.Invoke(this);
        }

        /// <summary>
        /// 注册监听
        /// </summary>
        /// <param name="action"></param>
        public bool RegisterRedDotEvent(RedDotChanged action)
        {
            if (action is null)
                return false;

            changedAction += action;
            var red = IsRed;
            SetDirty(true);
            if (red == IsRed) // 如果两次red一致，说明红点没变化，调用回调初始化红点
                action?.Invoke(this);

            return true;
        }

        /// <summary>
        /// 移除监听
        /// </summary>
        /// <param name="action"></param>
        public bool UnregisterRedDotEvent(RedDotChanged action)
        {
            if (action is null || changedAction is null)
                return false;

            changedAction -= action;
            if (changedAction is null)
                SetDirty(false);

            return true;
        }

        /// <summary>
        /// 节点查找(只查找一次，不递归查找)
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public RedDotNode Find(string name)
        {
            if (children.TryGetValue(name, out var node))
                return node;

            //foreach (var childNode in children.Values)
            //{
            //    return childNode.Find(name);
            //}

            return null;
        }

        void IDisposable.Dispose()
        {
            if (IsDisposed)
                return;

            IsDisposed = true;
            _isRed = false;
            if (this is IEvent) 
                EventManager.Instance?.RemoveTarget(this);

            foreach (var child in children.Values)
            {
                (child as IDisposable)?.Dispose();
            }

            children.Clear();
            redNodes.Clear();

            OnDestroy();

            if (parent != null && !parent.IsDisposed)
            {
                parent.RemoveFromChildren(this);
            }

            isDirty = false;
            children = null;
            redNodes = null;
            name = null;
            parent = null;
            changedAction = null;
        }
    }
}
