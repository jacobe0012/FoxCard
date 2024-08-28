using System.Reflection;
using UnityEditor;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

namespace XFramework.Editor
{
    [CustomEditor(typeof(Image), true)]
    internal class NewImageEditor : ImageEditor
    {
        protected Sprite m_OverrideSprite;

        protected GUIContent m_OverrideSpriteContent;

        protected override void OnEnable()
        {
            base.OnEnable();
            m_OverrideSpriteContent = EditorGUIUtility.TrTextContent("Override Image");
            var overrideSpriteField = typeof(Image).GetField("m_OverrideSprite", BindingFlags.Instance | BindingFlags.NonPublic);
            m_OverrideSprite = (Sprite)overrideSpriteField.GetValue(target);
        }

        public override void OnInspectorGUI()
        {
            base.serializedObject.Update();
            if (Application.isPlaying)
            {
                GUI.enabled = false;
                EditorGUILayout.ObjectField(m_OverrideSpriteContent, m_OverrideSprite, typeof(Sprite), target);
                EditorGUILayout.Space();
                GUI.enabled = true;
            }
            base.OnInspectorGUI();
        }

        public override void OnPreviewGUI(Rect rect, GUIStyle background)
        {
            //base.OnPreviewGUI(rect, background);
            var img = target as Image;
            if (img != null)
            {
                if (img.overrideSprite != null)
                {
                    var texture = AssetPreview.GetAssetPreview(img.overrideSprite);
                    GUI.DrawTexture(rect, texture, ScaleMode.ScaleToFit);
                }
            }
        }
    }

    [CustomEditor(typeof(XImage), true)]
    internal class XImageEditor : ImageEditor
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
            base.serializedObject.Update();
            var img = target as XImage;

            GUI.enabled = false;
            EditorGUILayout.PropertyField(overrideGrayedProperty);
            GUI.enabled = true;

            var grayed = EditorGUILayout.Toggle(m_GrayedContent, grayedProperty.boolValue);
            if (m_Grayed != grayed)
            {
                m_Grayed = grayed;
                //grayedProperty.boolValue = grayed;
                SetGameObjectGrayed(img.gameObject, grayed || overrideGrayedProperty.boolValue);
                EditorUtility.SetDirty(img.gameObject);
            }

            base.serializedObject.ApplyModifiedProperties();
            base.serializedObject.UpdateIfRequiredOrScript();

            base.OnInspectorGUI();
        }

        public static void SetGameObjectGrayed(GameObject obj, bool grayed)
        {
            using var list = XList<IUIGrayed>.Create();
            obj.GetComponentsInChildren<IUIGrayed>(true, list);
            foreach (var ui in list)
            {
                if (ui is Component component)
                {
                    if (component.gameObject == obj)
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
    }
}
