using TMPro.EditorUtilities;
using UnityEditor;

namespace XFramework.Editor
{
    [CustomEditor(typeof(XTextMeshProUGUI))]
    public class XTextMeshProUGUIEditor : TMP_EditorPanelUI
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            XTextEditor.ShowLanguageKey(serializedObject);

            serializedObject.ApplyModifiedProperties();

            base.OnInspectorGUI();
        }
    }
}
