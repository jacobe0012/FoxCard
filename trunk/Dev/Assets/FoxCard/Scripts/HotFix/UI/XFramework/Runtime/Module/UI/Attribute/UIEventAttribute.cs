namespace XFramework
{
    public class UIEventAttribute : BaseAttribute
    {
        public string UIType { get; private set; }

        public UIEventAttribute(string uiType)
        {
            UIType = uiType;
        }
    }
}