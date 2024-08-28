using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEditor.Progress;

namespace XFramework.Editor
{
    [CustomEditor(typeof(UIStructure))]
    public class UIStructureEditor : UnityEditor.Editor
    {
        protected const string ChildrenName = "children";

        protected const string ComponentsName = "uiComponents";

        private static bool parentFoldout = false;

        private static bool childrenFoldout = false;

        private static bool componentFoldout = false;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var uiObject = (target as UIStructure).UIObject as UI;
            if (uiObject is null)
                return;

            DrawObject(uiObject);
            DrawParent(uiObject.Parent);
            DrawChildren(uiObject);
            DrawComponents(uiObject.UIComponents);
        }

        protected void DrawObject(UI ui)
        {
            EditorGUILayout.Space();
            GUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Script");
            EditorGUILayout.TextField(ui.GetType().Name, GUILayout.MaxWidth(250), GUILayout.MinWidth(100));
            GUILayout.EndHorizontal();
        }

        protected void DrawParent(UI parent)
        {
            if (parent != null)
            {
                EditorGUILayout.Space();
                parentFoldout = EditorGUILayout.Foldout(parentFoldout, "parent", true);
                if (parentFoldout)
                {
                    GUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField(parent.Name);
                    EditorGUILayout.ObjectField(parent.GameObject, typeof(GameObject), this);
                    GUILayout.EndHorizontal();
                }
            }
        }

        protected void DrawChildren(UI ui)
        {
            var children = ui.Children;
            var list = ui.GetUIComponent<UIListComponent>();
            if (children != null && children.Count > 0 || list != null && list.ChildCount > 0)
            {
                EditorGUILayout.Space();
                childrenFoldout = EditorGUILayout.Foldout(childrenFoldout, "children", true);
                if (childrenFoldout)
                {
                    foreach (var item in children)
                    {
                        GUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField($"{item.Key}, type = {item.Value.GetType().Name}");
                        EditorGUILayout.ObjectField(item.Value.GameObject, typeof(GameObject), this, GUILayout.MaxWidth(200), GUILayout.MinWidth(100));
                        GUILayout.EndHorizontal();
                    }

                    if (list != null)
                    {
                        EditorGUILayout.LabelField("List Children");
                        int index = 0;
                        foreach (UI child in list)
                        {
                            string key = $"index = {index++}";
                            if (child is IUID u)
                            {
                                if (u.IsValid())
                                    key += $", Id = {u.Id}";
                            }
                            key += $", type = {child.GetType().Name}";
                            GUILayout.BeginHorizontal();
                            EditorGUILayout.LabelField(key);
                            EditorGUILayout.ObjectField(child.GameObject, typeof(GameObject), this, GUILayout.MaxWidth(200), GUILayout.MinWidth(100));
                            GUILayout.EndHorizontal();
                        }
                    }
                }
            }
        }

        protected void DrawComponents(Dictionary<Type, UIComponent> components)
        {
            if (components != null && components.Count > 0)
            {
                EditorGUILayout.Space();
                componentFoldout = EditorGUILayout.Foldout(componentFoldout, "components", true);
                if (componentFoldout)
                {
                    foreach (var item in components)
                    {
                        GUILayout.BeginHorizontal();
                        var type = item.Value.GetType();
                        EditorGUILayout.LabelField(type.ToString().Remove(0, type.Namespace.Length + 1));
                        GUILayout.EndHorizontal();
                    }
                }
            }
        }
    }
}
