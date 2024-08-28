namespace XFramework
{
    /// <summary>
    /// 多语言
    /// </summary>
    public interface IMultilingual
    {
        /// <summary>
        /// 多语言key
        /// </summary>
        string Key { get; }

        /// <summary>
        /// 多语言参数
        /// </summary>
        object[] Args { get; }

        /// <summary>
        /// 忽略多语言
        /// </summary>
        bool IgnoreLanguage { get; }

        void SetKey(string key, params object[] args);

        /// <summary>
        /// 设置text
        /// </summary>
        /// <param name="content"></param>
        void SetContent(string content);
    }
}