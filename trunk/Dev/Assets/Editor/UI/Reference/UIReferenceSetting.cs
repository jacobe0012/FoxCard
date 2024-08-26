using UnityEngine;

namespace XFramework.Editor
{
    [CreateAssetMenu(menuName = "UIReference/Setting", fileName = "UIReferenceSetting")]
    public class UIReferenceSetting : ScriptableObject
    {
        [SerializeField]
        [Header("配置项，自行创建后拖拽进来")]
        private UIReferenceConfig config;

        public UIReferenceConfig Config { get => config; set => config = value; }
    }
}
