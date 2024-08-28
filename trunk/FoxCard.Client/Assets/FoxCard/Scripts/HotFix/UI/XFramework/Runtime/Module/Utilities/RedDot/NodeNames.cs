using HotFix_UI;

namespace XFramework
{
    /// <summary>
    /// 节点名
    /// </summary>
    public class NodeNames
    {
        public const string Root = "Root";

        /// <summary>
        /// 拿到所属模块的红点字符串
        /// </summary>
        /// <param name="funcId">模块id</param>
        /// <returns></returns>
        // public static string GetTagFuncRedDotName(int tagfuncId)
        // {
        //     var tbtag_func = ConfigManager.Instance.Tables.Tbtag_func;
        //     return $"{Root}|Tag{tbtag_func.Get(tagfuncId).tagId}|TagFunc{tagfuncId}";
        // }

        /// <summary>
        /// 拿到所属页签的红点字符串
        /// </summary>
        /// <param name="tagId">页签id</param>
        /// <returns></returns>
        public static string GetTagRedDotName(int tagId)
        {
            return $"{Root}|Tag{tagId}";
        }
    }
}