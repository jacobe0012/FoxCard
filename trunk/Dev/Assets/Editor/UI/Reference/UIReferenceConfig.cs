using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

namespace XFramework.Editor
{
    [CreateAssetMenu(menuName = "UIReference/Config", fileName = "UIReferenceConfig")]
    public class UIReferenceConfig : ScriptableObject
    {
        /// <summary>
        /// 生成的类的目录
        /// </summary>
        [Header("生成的类的目录")] [TextArea] public string ClassExploer = "Assets/FoxCard/Scripts/HotFix/UI/XFDemo/Game/UI";

        /// <summary>
        /// 代码模板文件路径
        /// </summary>
        [Header("代码模板文件路径")] [TextArea] public string CodeTemplateFilePath = "./Template/UICodeTemplate.txt";

        /// <summary>
        /// key的模板文件路径
        /// </summary>
        [Header("key的模板文件路径")] [TextArea] public string KeyTemplateFilePath = "./Template/UIReferenceKeyTemplate.txt";

        /// <summary>
        /// UI路径集合类的路径
        /// </summary>
        [Header("UI路径集合类的路径")] [TextArea]
        public string UIPathSetFilePath = "Assets/FoxCard/Scripts/HotFix/UI/XFDemo/Game/UI/Types/UIPathSet.cs";

        /// <summary>
        /// UIType类的路径
        /// </summary>
        [Header("UIType类的路径")] [TextArea]
        public string UITypeFilePath = "Assets/FoxCard/Scripts/HotFix/UI/XFDemo/Game/UI/Types/UIType.cs";

        /// <summary>
        /// 命名空间
        /// </summary>
        [Header("命名空间")] public string Namespace = "XFramework";
    }
}
#endif