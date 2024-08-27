using System;
using System.Collections.Generic;
using HotFix_UI;
using UnityEngine;

namespace XFramework
{
    public sealed class RedDotManager : Singleton<RedDotManager>, IDisposable
    {
        private RedDotNode root;
        public char SplitChar { get; internal set; }

        private List<RedDotNode> dirtyList;

        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            SplitChar = '|';
            //创建根节点
            this.root = new RedDotNode("Root", null);
            dirtyList = new List<RedDotNode>();
            //CreateRedTreeTag();
            
        }

        public void CreateRedTreeTag()
        {
            // var tbtag = ConfigManager.Instance.Tables.Tbtag;
            // var tbtag_func = ConfigManager.Instance.Tables.Tbtag_func;
            // foreach (var item in tbtag.DataList)
            // {
            //     var str = $"{NodeNames.Root}{SplitChar}Tag{item.id}";
            //     this.InsterNode(str);
            // }
            //
            // foreach (var item in tbtag_func.DataList)
            // {
            //     var str = $"{NodeNames.Root}{SplitChar}Tag{item.tagId}{SplitChar}TagFunc{item.id}";
            //     this.InsterNode(str);
            // }
        }

        /// <summary>
        /// 插入节点
        /// </summary>
        /// <param name="name"></param>
        public void InsterNode(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return;
            }

            if (GetNode(name) != null)
            {
                // 如果已经存在，则不重复插入
                Debug.LogError("你已经插入了该节点 " + name);
                return;
            }

            // 从根节点出发
            RedDotNode node = root;
            node.passCnt += 1;

            int startIndex = 0;
            for (int i = 0; i < name.Length; i++)
            {
                if (name[i] == SplitChar)
                {
                    string path = name.Substring(startIndex, i - startIndex);
                    startIndex = i + 1;

                    if (!node.children.ContainsKey(path))
                    {
                        node.children.Add(path, new RedDotNode(path, node));
                    }

                    node = node.children[path];
                    node.passCnt = node.passCnt + 1;
                }
            }

            // 处理最后一个路径
            string lastPath = name.Substring(startIndex);
            if (!node.children.ContainsKey(lastPath))
            {
                node.children.Add(lastPath, new RedDotNode(lastPath, node));
            }

            node = node.children[lastPath];
            node.endCnt = node.endCnt + 1;
        }

        /// <summary>
        /// 添加回调(在退出时一般需要移除回调，否则会多重回调)
        /// </summary>
        /// <param name="path">全路径</param>
        /// <param name="cb">回调</param>
        /// <returns>红点节点</returns>
        public RedDotNode AddListener(string path, Action<int> cb)
        {
            if (cb == null)
                return null;

            RedDotNode node = GetNode(path);
            if (node == null)
            {
                return null;
            }

            //TODO:增加Debug回调 后面删掉
            cb += (a) =>
            {
                //Log.Debug($"path:{path} num:{a}");
            };
            node.AddListener(cb);
            return node;
        }

        /// <summary>
        /// 移除回调(在退出时一般需要移除回调，否则会多重回调)
        /// </summary>
        /// <param name="path"></param>
        /// <param name="cb"></param>
        public void RemoveListener(string path, Action<int> cb)
        {
            if (cb == null)
                return;

            RedDotNode node = GetNode(path);
            if (node == null)
            {
                return;
            }

            node.RemoveListener(cb);
        }

        /// <summary>
        /// 移除该节点所有的回调
        /// </summary>
        /// <param name="path"></param>
        public void RemoveAllListeners(string path)
        {
            RedDotNode node = GetNode(path);
            if (node == null)
            {
                return;
            }

            node.RemoveAllListeners();
        }

        /// <summary>
        /// 移除该节点下的所有子节点回调
        /// </summary>
        /// <param name="path"></param>
        public void ClearChildrenListeners(string path)
        {
            RedDotNode node = GetNode(path);
            if (node == null)
            {
                return;
            }

            foreach (var item in node.children)
            {
                item.Value.ClearChildrenListeners();
            }
        }

        /// <summary>
        /// 移除该节点及其所有子节点回调
        /// </summary>
        /// <param name="path"></param>
        public void ClearAllListeners(string path)
        {
            RedDotNode node = GetNode(path);
            if (node == null)
            {
                return;
            }

            node.ClearChildrenListeners();
        }

        /// <summary>
        /// 查询节点是否在树中并返回节点
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public RedDotNode GetNode(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }

            if (name == root.name)
            {
                return root;
            }

            RedDotNode node = this.root;


            int startIndex = 0;
            for (int i = 0; i < name.Length; i++)
            {
                if (name[i] == SplitChar)
                {
                    string path = name.Substring(startIndex, i - startIndex);
                    startIndex = i + 1;

                    if (!node.children.ContainsKey(path))
                    {
                        return null;
                    }

                    node = node.children[path];
                }
            }

            // 处理最后一个路径
            string lastPath = name.Substring(startIndex);
            if (!node.children.ContainsKey(lastPath))
            {
                return null;
            }

            RedDotNode lastNode = node.children[lastPath];
            if (lastNode.endCnt > 0)
            {
                return lastNode;
            }

            return null;
        }

        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="name"></param>
        public void DeleteNode(string name)
        {
            if (GetNode(name) == null)
            {
                return;
            }

            RedDotNode node = this.root;
            node.passCnt = node.passCnt - 1;

            int startIndex = 0;
            for (int i = 0; i < name.Length; i++)
            {
                if (name[i] == SplitChar)
                {
                    string path = name.Substring(startIndex, i - startIndex);
                    startIndex = i + 1;

                    RedDotNode childNode = node.children[path];
                    childNode.passCnt = childNode.passCnt - 1;

                    if (childNode.passCnt == 0)
                    {
                        // 如果该节点没有任何孩子，则直接删除
                        node.children.Remove(path);
                        return;
                    }

                    node = childNode;
                }
            }

            // 处理最后一个路径
            string lastPath = name.Substring(startIndex);
            RedDotNode lastChildNode = node.children[lastPath];
            lastChildNode.passCnt = lastChildNode.passCnt - 1;

            if (lastChildNode.passCnt == 0)
            {
                // 如果最后一个节点没有任何孩子，则直接删除
                node.children.Remove(lastPath);
            }

            node.endCnt = node.endCnt - 1;
        }

        public void HandleDirtyList()
        {
            Debug.LogError($"Count:{dirtyList.Count}");
            foreach (var VARIABLE in dirtyList)
            {
                Debug.LogError($"{VARIABLE.name}");
            }
        }

        /// <summary>
        /// 修改节点的和点数
        /// </summary>
        /// <param name="name"></param>
        /// <param name="delta"></param>
        async public void ChangeRedPointCnt(string name, int delta)
        {
            RedDotNode targetNode = GetNode(name);
            if (targetNode == null)
            {
                return;
            }

            // dirtyList.Add(targetNode);
            // await UniTask.Yield();
            // HandleDirtyList();
            return;
            //如果是减红点 并且和点数不够减了 则调整delta 使其不减为0
            if (delta < 0 && targetNode.redpoinCnt + delta < 0)
            {
                delta = -targetNode.redpoinCnt;
            }

            RedDotNode node = this.root;

            string[] pathList = name.Split('|');
            foreach (var path in pathList)
            {
                RedDotNode childNode = node.children[path];
                childNode.redpoinCnt = childNode.redpoinCnt + delta;
                node = childNode;
                //调用回调函数
                // foreach (var cb in node.updateCb.Values)
                // {
                //     cb?.Invoke(node.redpoinCnt);
                // }
            }
        }

        /// <summary>
        /// 修改节点的和点数(增量)
        /// </summary>
        /// <param name="name"></param>
        /// <param name="delta"></param>
        public void AddRedPointCnt(string name, int delta)
        {
            if (delta == 0)
            {
                return;
            }

            RedDotNode targetNode = GetNode(name);
            if (targetNode == null)
            {
                return;
            }

            // 如果是减红点并且点数不够减，则调整 delta 使其不减为0


            RedDotNode node = this.root;
            int startIndex = 0;
            for (int i = 0; i < name.Length; i++)
            {
                if (name[i] == SplitChar)
                {
                    string path = name.Substring(startIndex, i - startIndex);
                    startIndex = i + 1;

                    RedDotNode childNode = node.children[path];
                    childNode.redpoinCnt = childNode.redpoinCnt + delta;
                    childNode.redpoinCnt = Math.Max(0, childNode.redpoinCnt);
                    node = childNode;
                    node.changeCB?.Invoke(node.redpoinCnt);
                }
            }

            // 处理最后一个路径
            string lastPath = name.Substring(startIndex);
            RedDotNode lastChildNode = node.children[lastPath];
            lastChildNode.redpoinCnt = lastChildNode.redpoinCnt + delta;
            lastChildNode.redpoinCnt = Math.Max(0, lastChildNode.redpoinCnt);
            lastChildNode.changeCB?.Invoke(lastChildNode.redpoinCnt);
        }


        /// <summary>
        /// 设置节点的和点数
        /// </summary>
        /// <param name="name"></param>
        /// <param name="delta"></param>
        public void SetRedPointCnt(string name, int value)
        {
            RedDotNode targetNode = GetNode(name);
            if (targetNode == null)
            {
                return;
            }

            var delta = value - targetNode.redpoinCnt;
            // 如果是减红点并且点数不够减，则调整 delta 使其不减为0
            if (delta == 0)
            {
                return;
            }

            RedDotNode node = this.root;
            int startIndex = 0;
            for (int i = 0; i < name.Length; i++)
            {
                if (name[i] == SplitChar)
                {
                    string path = name.Substring(startIndex, i - startIndex);
                    startIndex = i + 1;

                    RedDotNode childNode = node.children[path];
                    childNode.redpoinCnt = childNode.redpoinCnt + delta;
                    childNode.redpoinCnt = Math.Max(0, childNode.redpoinCnt);
                    node = childNode;
                    node.changeCB?.Invoke(node.redpoinCnt);
                }
            }

            // 处理最后一个路径
            string lastPath = name.Substring(startIndex);
            RedDotNode lastChildNode = node.children[lastPath];
            lastChildNode.redpoinCnt = lastChildNode.redpoinCnt + delta;
            lastChildNode.redpoinCnt = Math.Max(0, lastChildNode.redpoinCnt);
            lastChildNode.changeCB?.Invoke(lastChildNode.redpoinCnt);
        }

        /// <summary>
        /// 查询节点红点数
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int GetRedPointCnt(string name)
        {
            RedDotNode node = GetNode(name);

            if (node == null)
            {
                return 0;
            }

            return node.redpoinCnt;
        }

        /// <summary>
        /// 清空
        /// </summary>
        public void Clear()
        {
            if (root == null || root.children == null)
                return;

            foreach (var item in root.children)
            {
                item.Value.Dispose();
            }
        }

        public void Dispose()
        {
            root.Dispose();
            root = null;
            Instance = null;
        }
    }
}