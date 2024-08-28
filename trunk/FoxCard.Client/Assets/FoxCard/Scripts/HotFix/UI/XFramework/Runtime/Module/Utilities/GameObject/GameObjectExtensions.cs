using System.Collections.Generic;
using UnityEngine;

namespace XFramework
{
    public static class GameObjectExtensions
    {
        #region grayed

        internal static HashSet<int> grayedList = new HashSet<int>();

        internal static void ChangeGrayedList(int instanceId, bool add)
        {
            if (add)
                grayedList.Add(instanceId);
            else
                grayedList.Remove(instanceId);
        }

        internal static bool ContainInGrayedList(int instanceId)
        {
            return grayedList.Contains(instanceId);
        }

        /// <summary>
        /// 是否为灰色
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool IsGrayed(this GameObject self)
        {
            bool grayed = ContainInGrayedList(self.GetInstanceID());
            if (!grayed)
            {
                grayed = self.GetComponent<IUIGrayed>()?.Grayed ?? false;
                if (grayed)
                    ChangeGrayedList(self.GetInstanceID(), true);
            }

            return grayed;
        }

        /// <summary>
        /// 设置所有的子对象为灰色
        /// </summary>
        /// <param name="self"></param>
        /// <param name="grayed"></param>
        public static void SetGrayed(this GameObject self, bool grayed)
        {
            if (grayed == self.IsGrayed())
                return;

            ChangeGrayedList(self.GetInstanceID(), grayed);

            using var list = XList<IUIGrayed>.Create();
            self.GetComponentsInChildren<IUIGrayed>(true, list);
            foreach (var ui in list)
            {
                if (ui is Component component)
                {
                    if (component.gameObject == self)
                        ui.SetGrayed(grayed);
                    else
                        ui.SetOverrideGrayed(grayed);
                }
                else
                {
                    ui.SetGrayed(grayed);
                }
            }
        }

        /// <summary>
        /// 重置所有子对象的灰色显示
        /// </summary>
        /// <param name="self"></param>
        public static void ResetGrayed(this GameObject self)
        {
            var grayed = self.IsGrayed();

            using var list = XList<IUIGrayed>.Create();
            self.GetComponentsInChildren<IUIGrayed>(true, list);
            foreach (var ui in list)
            {
                ui.ResetGrayed();
                if (ui is Component component)
                {
                    ChangeGrayedList(component.gameObject.GetInstanceID(), ui.Grayed);
                }
            }

            var newGrayed = self.IsGrayed();
            if (newGrayed != grayed)
            {
                ChangeGrayedList(self.GetInstanceID(), newGrayed);
            }
        }

        #endregion

        /// <summary>
        /// 获取对象里包含prefix前缀的所有对象，名称不能重复
        /// <para>result.key是去除前缀后的名称</para>
        /// </summary>
        /// <param name="self"></param>
        /// <param name="prefix">前缀</param>
        /// <param name="result">存储结果</param>
        public static void FindChildrenWithPrefix(this GameObject self, string prefix,
            Dictionary<string, GameObject> result)
        {
            FindWithPrefix(self, prefix, result);
        }

        /// <summary>
        /// 获取对象里包含prefix前缀的所有对象，名称不能重复
        /// <para>result.key是去除前缀后的名称</para>
        /// </summary>
        /// <param name="self"></param>
        /// <param name="prefix">前缀</param>
        /// <param name="result">存储结果</param>
        public static void FindChildrenWithPrefix(this Transform self, string prefix,
            Dictionary<string, GameObject> result)
        {
            self.gameObject.FindChildrenWithPrefix(prefix, result);
        }

        /// <summary>
        /// 查找一个对象的前缀
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="result"></param>
        private static void FindWithPrefix(GameObject obj, string prefix, Dictionary<string, GameObject> result)
        {
            if (prefix.IsNullOrEmpty())
                return;

            using var transforms = XList<Transform>.Create();
            obj.GetComponentsInChildren<Transform>(true, transforms);
            foreach (var trans in transforms)
            {
                if (prefix != trans.name && trans.name.StartsWith(prefix))
                {
                    string name = trans.name.Substring(prefix.Length);
                    if (!result.ContainsKey(name))
                        result.Add(name, trans.gameObject);
                }
            }
        }

        /// <summary>
        /// 对象引用
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static Reference Reference(this GameObject self)
        {
            return self.GetComponent<Reference>();
        }

        /// <summary>
        /// 对象引用
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static Reference Reference(this Transform self)
        {
            return self.GetComponent<Reference>();
        }

        /// <summary>
        /// 设置GameObject的隐藏与显示
        /// </summary>
        /// <param name="self"></param>
        /// <param name="active"></param>
        public static void SetViewActive(this GameObject self, bool active)
        {
            self?.SetActive(active);
            // if (self?.activeSelf != active)
            //     self?.SetActive(active);
        }

        /// <summary>
        /// 设置GameObject的隐藏与显示
        /// </summary>
        /// <param name="self"></param>
        /// <param name="active"></param>
        public static void SetViewActive(this Transform self, bool active)
        {
            self.gameObject.SetViewActive(active);
        }

        /// <summary>
        /// 设置CanvasGroup达到隐藏显示UI的效果
        /// </summary>
        /// <param name="self"></param>
        /// <param name="active"></param>
        public static void SetCanvasGroup(this GameObject self, bool active)
        {
            self.transform.SetCanvasGroup(active);
        }

        /// <summary>
        /// 设置CanvasGroup达到隐藏显示UI的效果
        /// </summary>
        /// <param name="self"></param>
        /// <param name="active"></param>
        public static void SetCanvasGroup(this Transform self, bool active)
        {
            if (!(self is RectTransform))
                return;

            CanvasGroup canvasGroup = self.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
                canvasGroup = self.gameObject.AddComponent<CanvasGroup>();

            canvasGroup.SetViewActive(active);
        }

        /// <summary>
        /// 设置CanvasGroup达到隐藏显示UI的效果
        /// </summary>
        /// <param name="self"></param>
        /// <param name="active"></param>
        public static void SetViewActive(this CanvasGroup self, bool active)
        {
            self.alpha = active ? 1 : 0;
            self.interactable = active;
            self.blocksRaycasts = active;
        }
    }
}