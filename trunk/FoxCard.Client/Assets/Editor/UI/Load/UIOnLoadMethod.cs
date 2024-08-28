using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;

namespace XFramework.Editor
{
    internal class UIOnLoadMethod
    {
        [InitializeOnLoadMethod]
        private static void InitializeCompnent()
        {
            UnityEditor.ObjectFactory.componentWasAdded += ComponentWasAdded;
        }

        /// <summary>
        /// 创建组件后执行的方法
        /// </summary>
        /// <param name="component"></param>
        private static void ComponentWasAdded(Component component)
        {
            if (component is Button && !(component is XButton))
            {
                GameObject obj = component.gameObject;
                Object.DestroyImmediate(component, true);
                obj.AddComponent<XButton>();
                Image image = obj.GetComponent<Image>();
                if (image)
                    image.raycastTarget = true;
            }
            else if (component is Image img)
            {
                bool notExist = !img.GetComponent<Button>() && !img.GetComponentInParent<Toggle>();
                img.raycastTarget = !notExist;

                if (component is XImage)
                    return;

                GameObject obj = component.gameObject;
                Object.DestroyImmediate(component, true);
                obj.AddComponent<XImage>();
            }
            else if (component is Text && !(component is XText))
            {
                GameObject obj = component.gameObject;
                Object.DestroyImmediate(component, true);
                XText xt = obj.AddComponent<XText>();
                if (!xt.GetComponentInParent<InputField>())
                    xt.raycastTarget = false;
            }
            else if (component is Toggle && !(component is XToggle))
            {
                GameObject obj = component.gameObject;
                Object.DestroyImmediate(component, true);
                obj.AddComponent<XToggle>();
            }
            else if (component is InputField input)
            {
                if (!(input is XInputField))
                {
                    GameObject obj = component.gameObject;
                    Object.DestroyImmediate(component, true);
                    obj.AddComponent<XInputField>();
                    return;
                }

                Image image = input.GetComponent<Image>();
                if (image)
                    image.raycastTarget = true;
            }
            else if (component is ScrollRect scroll)
            {
                if (!(scroll is XScrollRect))
                {
                    GameObject obj = component.gameObject;
                    Object.DestroyImmediate(component, true);
                    obj.AddComponent<XScrollRect>();
                    return;
                }

                Image image = scroll.GetComponent<Image>();
                if (image)
                    image.raycastTarget = true;
            }
            else if (component is Slider slider)
            {
                if (component is XSlider)
                    return;

                GameObject obj = component.gameObject;
                Object.DestroyImmediate(component, true);
                obj.AddComponent<XSlider>();
            }
            else if (component is Dropdown dropdown)
            {
                if (!(component is XDropdown))
                {
                    GameObject obj = component.gameObject;
                    Object.DestroyImmediate(component, true);
                    obj.AddComponent<XDropdown>();
                    return;
                }

                Image image = dropdown.GetComponent<Image>();
                if (image)
                    image.raycastTarget = true;
            }
            else if (component is TextMeshPro tmp)
            {
                if (!(component is XTextMeshPro))
                {
                    GameObject obj = component.gameObject;
                    Object.DestroyImmediate(component, true);
                    obj.AddComponent<XTextMeshPro>();
                    return;
                }
            }
            else if (component is TextMeshProUGUI tmpUGUI)
            {
                if (!(component is XTextMeshProUGUI))
                {
                    GameObject obj = component.gameObject;
                    Object.DestroyImmediate(component, true);
                    obj.AddComponent<XTextMeshProUGUI>();
                    return;
                }
            }
        }
    }
}
#endif
