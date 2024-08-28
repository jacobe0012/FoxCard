using System;
using System.Collections.Generic;
using UnityEngine;

namespace XFramework
{
    public class RedDotNode : IDisposable
    {
        /// <summary>
        /// 节点名
        /// </summary>
        public string name;

        /// <summary>
        /// 节点被经过的次数
        /// </summary>
        public int passCnt = 0;

        /// <summary>
        /// 节点作为尾结点的次数
        /// </summary>
        public int endCnt = 0;

        /// <summary>
        /// 红点数
        /// </summary>
        public int redpoinCnt = 0;

        public Dictionary<string, RedDotNode> children = new Dictionary<string, RedDotNode>();

        public Action<int> changeCB;

        public RedDotNode parent;

        public RedDotNode(string name, RedDotNode parent)
        {
            this.name = name;
            this.passCnt = 0;
            this.endCnt = 0;
            this.redpoinCnt = 0;
            this.children = new Dictionary<string, RedDotNode>();
            this.parent = parent;
        }

        public void AddListener(Action<int> cb)
        {
            changeCB += cb;
        }

        public void RemoveListener(Action<int> cb)
        {
            changeCB -= cb;
        }

        public void RemoveAllListeners()
        {
            changeCB = null;
        }

        public void ClearChildrenListeners()
        {
            foreach (var item in children)
            {
                item.Value.ClearChildrenListeners();
            }

            RemoveAllListeners();
        }

        /// <summary>
        /// 打印一个节点及其所有子树节点
        /// </summary>
        /// <param name="indentLevel"></param>
        public void PrintTree(int indentLevel = 0)
        {
            string indent = new string('-', indentLevel); // 构建缩进字符串

            Log.Debug($"{indent}{name} redDotNum:{redpoinCnt}", Color.green); // 打印当前节点名称

            // 递归打印子节点
            foreach (var child in children.Values)
            {
                child.PrintTree(indentLevel + 1);
            }
        }

        public void Dispose()
        {
            foreach (var child in children)
            {
                child.Value.Dispose(); // 递归销毁子节点
            }

            RemoveAllListeners();
            children.Clear(); // 清空子节点列表
        }
    }
}