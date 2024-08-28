namespace XFramework
{
    public interface ILanguageLoader
    {
        /// <summary>
        /// 通过键值获取value
        /// </summary>
        /// <param name="languageType"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        string GetValue(int languageType, string key);

        /// <summary>
        /// 默认语言类型
        /// </summary>
        /// <returns></returns>
        int GetDefaultLanguageType();
    }

    /// <summary>
    /// 多语言管理
    /// </summary>
    public sealed class LanguageManager : CommonObject
    {
        private const string LanguageKey = "LANGUAGE";

        private int _languageType = int.MinValue;

        private ILanguageLoader _loader;

        public int Language_Type => (int)_languageType;

        protected override void Destroy()
        {
            _loader = null;
            _languageType = int.MinValue;
            base.Destroy();
        }

        public void SetLoader(ILanguageLoader loader)
        {
            this._loader = loader;
            if (_loader != null)
            {
                if (!PlayerPrefsHelper.TryGetInt(LanguageKey, out var type))
                    _languageType = loader.GetDefaultLanguageType();
                else
                    _languageType = type;
            }
        }

        /// <summary>
        /// 获取多语言值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetValue(string key)
        {
            if (_loader is null)
                return key;

            return _loader.GetValue(this._languageType, key);
        }

        /// <summary>
        /// 设置多语言类型
        /// </summary>
        /// <param name="type"></param>
        public void SetLanguageType(int type)
        {
            if (_languageType == type)
                return;

            _languageType = type;
            PlayerPrefsHelper.SetInt(LanguageKey, type, true);

            foreach (var obj in UIReference.TextList())
            {
                if (obj is IMultilingual multilingual)
                {
                    if (multilingual.Key.IsNullOrEmpty())
                        continue;

                    multilingual.RefreshText();
                }
            }
        }
    }
}