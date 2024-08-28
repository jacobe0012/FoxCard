using System;

namespace XFramework
{
    public interface ILoopScrollRectPrefabKey
    {
        string Key { get; }
    }

    /// <summary>
    /// 用于在循环滚动视图里面接收对象
    /// T类会使用对象池，请管理好字段
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ILoopScrollRectProvide<T> where T : UI, new()
    {
        void ProvideData(T ui, int index);
    }

    /// <summary>
    /// 用于在循环滚动视图里面接收对象
    /// T类会使用对象池，请管理好字段
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ILoopScrollRectMultiProvide
    {
        void ProvideData(UI ui, int index);
    }
}