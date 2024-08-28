using TMPro.EditorUtilities;
using UnityEditor;

namespace XFramework.Editor
{
    [CustomEditor(typeof(XTextMeshPro))]
    public class XTextMeshProEditor : TMP_EditorPanel
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
