using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XFramework
{
    public class RedDotComponent : UIComponent
    {
        /// <summary>
        /// 监听
        /// </summary>
        private RedDotChanged listener;

        /// <summary>
        /// 红点节点名称
        /// </summary>
        private string redDotName;

        protected override void OnDestroy()
        {
            UnregisterRedDotEvent(listener);
            redDotName = null;
            listener = null;
            base.OnDestroy();
        }

        private RedDotManager GetRedDotManager()
        {
            return Common.Instance.Get<RedDotManager>();
        }

        /// <summary>
        /// 默认方式设置红点显示/隐藏
        /// </summary>
        /// <param name="node"></param>
        private void OnRedDotChanged(RedDotNode node)
        {
            this.parent.SetActive(node.IsRed);
        }

        /// <summary>
        /// 注册红点变化监听
        /// </summary>
        /// <param name="redDotName"></param>
        /// <param name="action"></param>
        public void RegisterRedDotEvent(string redDotName, RedDotChanged action)
        {
            if (this.redDotName != null)
            {
                Log.Error("请勿重复设置红点监听");
                return;
            }

            if (action is null)
                return;

            if (GetRedDotManager().RegisterRedDotEvent(redDotName, action))
            {
                this.redDotName = redDotName;
                listener = action;
            }
        }

        /// <summary>
        /// 移除红点变化监听
        /// </summary>
        /// <param name="redDotName"></param>
        /// <param name="action"></param>
        public void UnregisterRedDotEvent(RedDotChanged action)
        {
            if (redDotName is null)
                return;

            if (action is null)
                return;

            if (GetRedDotManager()?.UnregisterRedDotEvent(redDotName, action) ?? false)
            {
                redDotName = null;
                listener = null;
            }
        }

        /// <summary>
        /// 默认方式红点变化监听
        /// </summary>
        /// <param name="redDotName"></param>
        public void RegisterRedDotEvent(string redDotName)
        {
            RegisterRedDotEvent(redDotName, OnRedDotChanged);
        }

        /// <summary>
        /// 移除默认方式设置的红点监听
        /// </summary>
        /// <param name="redDotName"></param>
        public void UnregisterRedDotEvent()
        {
            UnregisterRedDotEvent(OnRedDotChanged);
        }
    }

    public static class RedDotComponentExtensions
    {
        /// <summary>
        /// 红点组件
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static RedDotComponent GetRedDot(this UI self)
        {
            var comp = self.GetUIComponent<RedDotComponent>();
            if (comp != null)
                return comp;

            comp = ObjectFactory.Create<RedDotComponent>(true);
            self.AddUIComponent(comp);

            return comp;
        }

        /// <summary>
        /// 红点组件
        /// </summary>
        /// <param name="self"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static RedDotComponent GetRedDot(this UI self, string key)
        {
            var ui = self.GetFromKeyOrPath(key);
            return ui?.GetRedDot();
        }
    }
}
