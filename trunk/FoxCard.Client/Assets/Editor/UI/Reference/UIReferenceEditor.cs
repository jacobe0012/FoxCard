using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
using System.IO;
using System.Text;

namespace XFramework.Editor
{
    [CustomEditor(typeof(UIReference))]
    public class UIReferenceEditor : ReferenceEditor
    {
        private enum ResourceType
        {
            Resources,
            AA,
        }

        private RectTransform rectTransform;

        private UIReferenceConfig config;

        ///// <summary>
        ///// 代码模板文件路径
        ///// </summary>
        //private const string TemplateFilePath = "./Template/UICodeTemplate.txt";

        ///// <summary>
        ///// UI集合的路径
        ///// </summary>
        //private const string SetFilePath = "Assets/FoxCard/Scripts/XFramework/Runtime/Module/UI/UISet.cs";

        ///// <summary>
        ///// UIType类的路径
        ///// </summary>
        //private const string UITypeFilePath = "Assets\FoxCard\Scripts\HotFix\UI\XFDemo\Game\UI\Types/UIType.cs";

        ///// <summary>
        ///// UI类的目录
        ///// </summary>
        //private const string ClassExploer = "Assets/FoxCard/Scripts/XFramework/Runtime/World/Game/UI";

        private const string DefaultNamespace = "XFramework";

        private const string TextDataName = "_textData";

        /// <summary>
        /// 配置文件路径
        /// </summary>
        private const string ConfigPath = "Assets/Editor/UI/Reference/Config/UIReferenceSetting.asset";

        /// <summary>
        /// UIType左边的部分
        /// </summary>
        private const string UITypePrefix = "public const string (className) =";

        /// <summary>
        /// UIType的值
        /// </summary>
        private const string UITypeValue = "nameof((NAMESPACE)(className));";

        private ResourceType resourceType = ResourceType.AA;

        /// <summary>
        /// 不带点的命名空间名称
        /// </summary>
        private string configNamespace;

        /// <summary>
        /// 带点的命名空间名称
        /// </summary>
        private string configNamespaceAndPoint;

        /// <summary>
        /// using
        /// </summary>
        private string usingContent;

        /// <summary>
        /// 折叠多语言对象
        /// </summary>
        private static bool foldoutMultiObject = true;

        private HashSet<string> multiObjectKeys = new HashSet<string>();

        protected override void OnEnable()
        {
            base.OnEnable();
            rectTransform = (target as UIReference).Get<RectTransform>();

            var configSetting = AssetDatabase.LoadAssetAtPath<UIReferenceSetting>(ConfigPath);
            if (configSetting != null)
                config = configSetting.Config;

            if (config != null)
            {
                configNamespace = config.Namespace.Trim();
                configNamespaceAndPoint = config.Namespace.Trim();
                if (!configNamespace.IsNullOrEmpty())
                    configNamespaceAndPoint += ".";

                if (configNamespace != DefaultNamespace)
                    usingContent = $"\nusing {DefaultNamespace};";
                else
                    usingContent = string.Empty;
            }                
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (config == null || !rectTransform)
                return;

            EditorGUILayout.Space();

            DisplaySingleElements("Multilingual Elements", TextDataName, ref foldoutMultiObject, multiObjectKeys);

            EditorGUILayout.Space();

            GUILayout.BeginHorizontal();

            if (GUILayout.Button("导出Key", GUILayout.MinHeight(45) /*"Export Keys"*/))
            {
                ExportKeys();
            }

            if (GUILayout.Button("刷新\n多语言对象", GUILayout.MinHeight(45)))
            {
                RefreshAllIMultilingual();
            }

            if (GUILayout.Button("导出代码\n(独立UI)", GUILayout.MinHeight(45) /*"Export All (AllowManagement)"*/))
            {
                ExportAll(true);
            }

            if (GUILayout.Button("导出代码\n(公共UI)", GUILayout.MinHeight(45) /*"Export All (Common)"*/))
            {
                ExportAll(false);
            }

            GUILayout.EndHorizontal();

            serializedObject.ApplyModifiedProperties();
            serializedObject.UpdateIfRequiredOrScript();
        }

        /// <summary>
        /// 导出所有的key
        /// </summary>
        private void ExportKeys()
        {
            string folderPath = GetFolderPath();
            if (folderPath.IsNullOrEmpty())
            {
                Debug.LogError("生成目录为空！");
                return;
            }

            string className = GetClassName();
            ExportKeys(className, folderPath);

            AssetDatabase.Refresh();
        }

        /// <summary>
        /// 生成UI代码
        /// </summary>
        private void ExportAll(bool allowManagement)
        {
            if (Application.isPlaying)
                return;

            string folderPath = GetFolderPath();
            if (folderPath.IsNullOrEmpty())
            {
                Debug.LogError("生成目录为空！");
                return;
            }

            string className = GetClassName();

            ExportCode(className, folderPath, allowManagement);
            ExportKeys(className, folderPath);
            ExportUIType(className);
            ExportUIPathSet(className);

            AssetDatabase.Refresh();
        }

        /// <summary>
        /// 导出代码
        /// </summary>
        /// <param name="className"></param>
        /// <param name="folderPath"></param>
        /// <param name="allowManagement"></param>
        private void ExportCode(string className, string folderPath, bool allowManagement)
        {
            string fileName = $"{className}.cs";
            string classFilePath = $"{folderPath}/{fileName}";

            if (File.Exists(classFilePath))
            {
                Debug.LogError($"{fileName}文件已经存在\n路径为{classFilePath}");
                return;
            }

            string templateFilePath = config.CodeTemplateFilePath;

            if (!File.Exists(templateFilePath))
            {
                Debug.LogError($"创建Code的临时文件不存在, 查找路径为{templateFilePath}");
                return;
            }

            string template = File.ReadAllText(templateFilePath);
            template = template.Replace("(className)", className)
                .Replace("(UITypeName)", className)
                .Replace("(NAMESPACE)", configNamespace)
                .Replace("(USING)", usingContent);

            if (allowManagement)
            {
                template = template.Replace("(IUILayer)", ", IUILayer").Replace("(AllowManagement)", allowManagement.ToString().ToLower());
                template = template.Replace("(IUILayerCode)", "public UILayer Layer => UILayer.Low;");
            }
            else
            {
                template = template.Replace("(IUILayer)", string.Empty).Replace("(AllowManagement)", allowManagement.ToString().ToLower());
                template = template.Replace("(IUILayerCode)", "// 此UI是不受UIManager管理的");
            }

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            File.WriteAllText(classFilePath, template);

            Debug.Log($"{fileName}生成成功\n路径为{classFilePath}");
        }

        /// <summary>
        /// 导出所有的Key
        /// </summary>
        private void ExportKeys(string className, string folderPath)
        {
            string templateFilePath = config.KeyTemplateFilePath;
            
            if (!File.Exists(templateFilePath))
            {
                Debug.LogError($"创建Key的临时文件不存在, 查找路径为{templateFilePath}");
                return;
            }

            string template = File.ReadAllText(templateFilePath);
            string prefix = "K";
            StringBuilder sb = new StringBuilder();
            int index = 0;

            foreach (var key in prefixKeys)
            {
                string content = $"\t\tpublic const string {prefix}{key} = \"{key}\";";
                if (index == 0)
                    sb.Append(content);
                else
                    sb.Append($"\n{content}");

                ++index;
            }
            foreach (var key in dragKeys)
            {
                string content = $"\t\tpublic const string {prefix}{key} = \"{key}\";";
                if (index == 0)
                    sb.Append(content);
                else
                    sb.Append($"\n{content}");

                ++index;
            }

            template = template.Replace("(className)", className).Replace("(content)", sb.ToString()).Replace("(NAMESPACE)", configNamespace);

            folderPath = $"{folderPath}/Keys";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string fileName = $"{className}Keys.cs";
            string classFilePath = $"{folderPath}/{fileName}";
            File.WriteAllText(classFilePath, template);

            Debug.Log($"{fileName}生成成功\n路径为{classFilePath}");
        }

        /// <summary>
        /// 导出到UIType
        /// </summary>
        /// <param name="className"></param>
        private void ExportUIType(string className)
        {
            string filePath = config.UITypeFilePath;

            if (!File.Exists(filePath))
            {
                Debug.LogError($"UIType.cs不存在，查找路径为{filePath}");
                return;
            }

            string flag = "#endregion 1";
            List<string> lines = new List<string>(File.ReadAllLines(filePath));
            string prefix = UITypePrefix.Replace("(className)", className);
            string content = $"\t\t{prefix} {UITypeValue.Replace("(className)", className).Replace("(NAMESPACE)", configNamespaceAndPoint)}";
            int nameIndex = lines.FindIndex(str => str.Contains(prefix));

            if (nameIndex >= 0)
            {
                lines[nameIndex] = content;
            }
            else
            {
                int flagIndex = lines.FindIndex(str => str.Trim() == flag);
                if (flagIndex >= 0)
                {
                    int inex = flagIndex;
                    lines.Insert(flagIndex, content);
                    lines.Insert(flagIndex + 1, string.Empty);
                }
                else
                {
                    Debug.LogError($"UIType.cs里没有找到标记{flag}");
                    return;
                }
            }

            File.WriteAllLines(filePath, lines);
            Debug.Log($"{className}已添加到UIType.cs里");
        }

        /// <summary>
        /// 导出到UIPathSet
        /// </summary>
        /// <param name="className"></param>
        private void ExportUIPathSet(string className)
        {
            string filePath = config.UIPathSetFilePath;

            if (!File.Exists(filePath))
            {
                Debug.LogError($"UIPathSet.cs不存在，查找路径为{filePath}");
                return;
            }

            string flag = "#endregion 1";
            string name = rectTransform.name;
            string assetPath = GetAssetPath();
            List<string> lines = new List<string>(File.ReadAllLines(filePath));
            string prefix = $"public const string {className} =";
            string content = $"\t\t{prefix} \"{name}\";";
            int nameIndex = lines.FindIndex(str => str.Contains(prefix));
          
            if (nameIndex >= 0)
            {
                lines[nameIndex] = content;
            }
            else
            {
                int flagIndex = lines.FindIndex(str => str.Trim() == flag);
                if (flagIndex >= 0)
                {
                    int inex = flagIndex;
                    lines.Insert(flagIndex, content);
                    lines.Insert(flagIndex + 1, string.Empty);
                }
                else
                {
                    Debug.LogError($"UIPathSet.cs里没有找到标记{flag}");
                    return;
                }
            }

            File.WriteAllLines(filePath, lines);
            Debug.Log($"{className}已添加到UIPathSet.cs里");
        }

        /// <summary>
        /// 获取类名
        /// </summary>
        /// <returns></returns>
        private string GetClassName()
        {
            string name = rectTransform.name;
            string className = name;

            if (className.EndsWith("panel", true, null))
                className = className.Remove(className.Length - 5);

            if (className.EndsWith("ui", true, null))
                className = className.Remove(className.Length - 2);

            if (className.StartsWith("ui", true, null))
                className = className.Remove(0, 2);

            className = $"UI{className}";

            return className;
        }

        /// <summary>
        /// 目录路径
        /// </summary>
        /// <returns></returns>
        private string GetFolderPath()
        {
            string path = config?.ClassExploer;
            if (path.IsNullOrEmpty())
                return null;

            string className = GetClassName();
            path = $"{path}/{className}";
            return path;
        }

        /// <summary>
        /// 资源路径
        /// </summary>
        /// <returns></returns>
        private string GetAssetPath()
        {
            GameObject obj = rectTransform?.gameObject;
            if (!obj)
                return string.Empty;

            string path = AssetDatabase.GetAssetPath(obj);
            if (path.IsNullOrEmpty())
                return string.Empty;

            switch (resourceType)
            {
                case ResourceType.Resources:
                    {
                        if (path.StartsWith("Assets/", true, null))
                        {
                            path = path.Substring(7);
                        }

                        if (path.StartsWith("Resources/"))
                        {
                            path = path.Substring(10);
                        }

                        if (path.EndsWith(".prefab", true, null))
                        {
                            path = path.Substring(0, path.Length - 7);
                        }

                        return path;
                    }
                case ResourceType.AA:
                    {
                        path = Path.GetFileNameWithoutExtension(path);
                        return path;
                    }
                default:
                    return path;
            }
        }

        #region Language

        /// <summary>
        /// 刷新该对象内所有的继承了IMultilingual的组件并添加到UIReference内
        /// </summary>
        private void RefreshAllIMultilingual()
        {
            UnityEditor.SerializedProperty dataProperty = serializedObject.FindProperty(TextDataName);
            dataProperty.ClearArray();

            List<IMultilingual> texts = new List<IMultilingual>();
            rectTransform.GetComponentsInChildren<IMultilingual>(true, texts);
            Dictionary<string, int> keys = new Dictionary<string, int>();
            foreach (IMultilingual txt in texts)
            {
                if (!(txt is Object obj))
                    continue;

                if (txt.IgnoreLanguage)
                    continue;

                string key = obj.name;
                string newKey = key;

                keys.TryGetValue(key, out int count);
                if (count > 0)
                    newKey += count.ToString();

                keys[key] = ++count;

                AddReference(dataProperty, newKey, obj);
            }

            UnityEditor.EditorUtility.SetDirty(this);
            serializedObject.ApplyModifiedProperties();
            serializedObject.UpdateIfRequiredOrScript();
        }

        /// <summary>
        /// 添加到引用里
        /// </summary>
        /// <param name="dataProperty"></param>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        private void AddReference(SerializedProperty dataProperty, string key, Object obj)
        {
            int index = dataProperty.arraySize;
            dataProperty.InsertArrayElementAtIndex(index);
            var element = dataProperty.GetArrayElementAtIndex(index);
            element.FindPropertyRelative("Key").stringValue = key;
            element.FindPropertyRelative("Object").objectReferenceValue = obj;
        }

        #endregion
    }
}
#endif