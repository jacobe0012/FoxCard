using UnityEditor;
using UnityEditor.UI;
using UnityEngine;

namespace XFramework.Editor
{
    [CustomEditor(typeof(XText))]
    public class XTextEditor : UnityEditor.UI.TextEditor
    {
        protected GUIContent m_GrayedContent;

        protected SerializedProperty overrideGrayedProperty;

        protected SerializedProperty grayedProperty;

        protected bool m_Grayed;

        protected override void OnEnable()
        {
            base.OnEnable();
            m_GrayedContent = EditorGUIUtility.TrTextContent("Grayed", "灰色");
            overrideGrayedProperty = base.serializedObject.FindProperty("overrideGrayed");
            grayedProperty = base.serializedObject.FindProperty("grayed");
            m_Grayed = grayedProperty.boolValue;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            ShowLanguageKey(serializedObject);

            var txt = target as XText;

            GUI.enabled = false;
            EditorGUILayout.PropertyField(overrideGrayedProperty);
            GUI.enabled = true;

            var grayed = EditorGUILayout.Toggle(m_GrayedContent, grayedProperty.boolValue);
            if (m_Grayed != grayed)
            {
                m_Grayed = grayed;
                //grayedProperty.boolValue = grayed;
                XImageEditor.SetGameObjectGrayed(txt.gameObject, grayed || overrideGrayedProperty.boolValue);
                EditorUtility.SetDirty(txt.gameObject);
            }

            base.serializedObject.ApplyModifiedProperties();
            base.serializedObject.UpdateIfRequiredOrScript();

            base.OnInspectorGUI();
        }

        public static void ShowLanguageKey(SerializedObject serializedObject)
        {
            SerializedProperty ignoreProperty = serializedObject.FindProperty("m_IgnoreLanguage");
            if (ignoreProperty != null)
            {
                EditorGUILayout.PropertyField(ignoreProperty);

                if (!ignoreProperty.boolValue)
                {
                    SerializedProperty keyProperty = serializedObject.FindProperty("m_Key");
                    if (keyProperty != null)
                    {
                        EditorGUILayout.PropertyField(keyProperty);
                    }
                }
            }
        }
    }
}
