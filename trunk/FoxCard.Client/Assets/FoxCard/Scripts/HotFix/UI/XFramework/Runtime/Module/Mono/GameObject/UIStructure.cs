using UnityEngine;

namespace XFramework
{
    public class UIStructure : MonoBehaviour
    {
#if UNITY_EDITOR
        private object uiObject;

        public object UIObject => uiObject;

        public void SetUIObject(object ui)
        {
            uiObject = ui;
        }
#endif
    }
}