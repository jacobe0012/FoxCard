using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XFramework
{
    /// <summary>
    /// 红点管理(树状结构)
    /// </summary>
    public sealed class RedDotManager : CommonObject
    {
        private RedDotNode root;

        private Dictionary<string, RedDotNode> nodes = new Dictionary<string, RedDotNode>();

        protected override void Init()
        {
            base.Init();

            root = new RedDotNode();
            root.Awake("__InnerRoot", null);
        }

        protected override void Destroy()
        {
            base.Destroy();
            (root as IDisposable)?.Dispose();
            root = null;
            nodes.Clear();
        }

        /// <summary>
        /// 初始化所有红点(需在初始化数据之后手动调用)
        /// </summary>
        public void InitAllRedNodes()
        {
            var types = TypesManager.Instance.GetTypes(typeof(RedDotNameAttribute));
            foreach (var type in types)
            {
                var obj = Activator.CreateInstance(type);
                if (!(obj is RedDotNode curNode))
                    continue;

                var attris = type.GetCustomAttributes(typeof(RedDotNameAttribute), false);
                if (attris.Length == 0)
                    continue;

                foreach (RedDotNameAttribute attri in attris)
                {
                    var fullName = attri.FullName;
                    if (fullName.IsNullOrEmpty())
                        continue;

                    nodes.Add(fullName, curNode);

                    var nodeName = attri.Name;
                    var names = fullName.Split('.');
                    var parentNode = root;

                    // 创建树形结构的红点
                    foreach (var name in names)
                    {
                        var node = parentNode.Find(name);
                        if (node is null)
                        {
                            if (name == nodeName)
                                node = curNode;
                            else
                                node = new RedDotNode();

                            node.Awake(name, parentNode);
                        }

                        parentNode = node;
                    }
                }
            }
        }

        /// <summary>
        /// 查找红点节点
        /// </summary>
        /// <param name="redDotName"></param>
        /// <param name="alert"></param>
        /// <returns></returns>
        private RedDotNode Find(string redDotName, bool alert)
        {
            if (redDotName.IsNullOrEmpty())
                return null;

            if (nodes.TryGetValue(redDotName, out var redDotNode))
            {
                if (redDotNode.IsDisposed)
                {
                    if (alert)
                        Log.Error("名为{0}的红点节点已经被释放");

                    return null;
                }
            }                

            var names = redDotName.Split('.');
            var parentNode = root;
            bool exist = false;

            foreach (var name in names)
            {
                var childNode = parentNode.Find(name);
                if (childNode is null)
                {
                    if (alert)
                        Log.Error("未找到名为{0}的红点节点", name);

                    return null;
                }

                parentNode = childNode;
                exist = true;
            }

            return exist ? parentNode : null;
        }

        /// <summary>
        /// 注册红点变化监听
        /// </summary>
        /// <param name="redDotName"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public bool RegisterRedDotEvent(string redDotName, RedDotChanged action)
        {
            var node = Find(redDotName, true);
            if (node is null)
                return false;

            return node.RegisterRedDotEvent(action);
        }

        /// <summary>
        /// 移除红点变化监听
        /// </summary>
        /// <param name="redDotName"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public bool UnregisterRedDotEvent(string redDotName, RedDotChanged action)
        {
            var node = Find(redDotName, true);
            if (node is null)
                return false;

            return node.UnregisterRedDotEvent(action);
        }

        /// <summary>
        /// 获取某节点红点
        /// </summary>
        /// <param name="redDotName"></param>
        /// <returns></returns>
        public bool GetNodeRed(string redDotName)
        {
            return Find(redDotName, true)?.IsRed ?? false;
        }
    }
}
