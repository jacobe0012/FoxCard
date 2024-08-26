using System;
using System.Drawing;
using Color = UnityEngine.Color;

namespace XFramework
{
    public static class UIExtensions
    {
        /// <summary>
        /// 获取一个UIComponent，如果没有则创建一个(前提是GameObject存在所需的Component)
        /// </summary>
        /// <typeparam name="UIComponentType"></typeparam>
        /// <typeparam name="UnityComponentType"></typeparam>
        /// <param name="inherit">继承查找</param>
        /// <returns></returns>
        public static UIComponentType TakeComponent<UIComponentType, UnityComponentType>(this UI self,
            bool inherit = true)
            where UIComponentType : UComponent, new()
            where UnityComponentType : UnityEngine.Component
        {
            var comp = self.GetUIComponent<UIComponentType>(inherit);
            if (comp != null)
                return comp;

            return CreateUIComponent(self, typeof(UIComponentType), typeof(UnityComponentType)) as UIComponentType;
        }

        /// <summary>
        /// 获取一个UIComponent，如果没有则创建一个(前提是GameObject存在所需的Component)
        /// </summary>
        /// <param name="self"></param>
        /// <param name="uiCompType"></param>
        /// <param name="unityComponentType"></param>
        /// <returns></returns>
        public static UIComponent TakeComponent(this UI self, Type uiCompType, Type unityComponentType)
        {
            var comp = self.GetUIComponent(uiCompType);
            if (comp != null)
                return comp;

            return CreateUIComponent(self, uiCompType, unityComponentType);
        }

        private static UIComponent CreateUIComponent(UI self, Type uiCompType, Type unityComponentType)
        {
            var unityComponent = self.GetComponent(unityComponentType);
            if (!unityComponent)
                return null;

            var comp = ObjectFactory.Create<UnityEngine.Component>(uiCompType, unityComponent, true) as UIComponent;
            self.AddUIComponent(comp);

            return comp;
        }

        /// <summary>
        /// 灰色
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool IsGrayed(this UI self)
        {
            return self.GameObject.IsGrayed();
        }

        /// <summary>
        /// 设置灰色
        /// </summary>
        /// <param name="self"></param>
        /// <param name="grayed"></param>
        public static void SetGrayed(this UI self, bool grayed)
        {
            self.GameObject.SetGrayed(grayed);
        }
    }
}