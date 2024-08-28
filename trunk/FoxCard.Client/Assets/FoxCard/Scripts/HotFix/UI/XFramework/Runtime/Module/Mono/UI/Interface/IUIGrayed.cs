namespace XFramework
{
    public interface IUIGrayed
    {
        /// <summary>
        /// 覆盖的灰色
        /// </summary>
        bool OverrideGrayed { get; set; }

        /// <summary>
        /// 灰色
        /// </summary>
        bool Grayed { get; set; }

        /// <summary>
        /// 设置灰色
        /// </summary>
        /// <param name="grayed"></param>
        void SetGrayed(bool grayed);

        /// <summary>
        /// 设置覆盖的灰色
        /// </summary>
        /// <param name="overrideGrayed"></param>
        void SetOverrideGrayed(bool overrideGrayed);

        /// <summary>
        /// 还原为默认状态
        /// </summary>
        void ResetGrayed();
    }
}