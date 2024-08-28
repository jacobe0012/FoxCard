using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace HotFix_UI
{
    public class InputFieldMono : MonoBehaviour, IPointerClickHandler
    {
        private TMP_InputField inputField;
        private bool isFirstTime;

        private void Start()
        {
            //var language = ConfigManager.Instance.Tables.Tblanguage;
            // 获取TMP_InputField组件的引用
            inputField = GetComponent<TMP_InputField>();

            // 设置默认文字
            inputField.text = "Login";
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!isFirstTime)
            {
                // 清除默认文字
                inputField.text = "";
                isFirstTime = true;
            }
        }
    }
}