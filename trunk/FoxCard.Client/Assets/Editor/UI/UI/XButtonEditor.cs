using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UI;

namespace XFramework.Editor
{
    [CustomEditor(typeof(XButton))]
    internal class XButtonEditor : ButtonEditor
    {
        private List<string> normalPropertyName = new List<string>();

        private List<string> groupPropertyName = new List<string>();

        private static bool foldout;

        protected override void OnEnable()
        {
            base.OnEnable();

            if (normalPropertyName.Count == 0)
            {
                normalPropertyName.Add("m_LongPressInterval");
                normalPropertyName.Add("m_MaxLongPressCount");
                normalPropertyName.Add("m_OnLongPress");
                normalPropertyName.Add("m_OnLongPressEnd");
            }

            if (groupPropertyName.Count == 0)
            {
                groupPropertyName.Add("m_IsOn");
            }
        }

        public override void OnInspectorGUI()
        {
            //serializedObject.Update();

            base.OnInspectorGUI();

            foldout = EditorGUILayout.Foldout(foldout, "Extensions", true);
            if (foldout)
            {
                foreach (var name in normalPropertyName)
                {
                    PropertyField(name);
                }
            }

            serializedObject.ApplyModifiedProperties();
            serializedObject.UpdateIfRequiredOrScript();
        }

        private void PropertyField(string propertyPath)
        {
            EditorGUILayout.Separator();
            SerializedProperty property = GetProperty(propertyPath);
            if (property != null)
            {
                EditorGUILayout.PropertyField(property);
            }
        }

        private SerializedProperty GetProperty(string propertyPath)
        {
            SerializedProperty property = serializedObject.FindProperty(propertyPath);
            return property;
        }
    }
}