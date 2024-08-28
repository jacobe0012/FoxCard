using System;
using UnityEngine;

namespace XFramework
{
    public enum MiniLoopType
    {
        /// <summary>
        /// 不循环
        /// </summary>
        None,

        /// <summary>
        /// 重新开始
        /// </summary>
        Restart,

        /// <summary>
        /// A到B，B到A
        /// <para>A到B算一次，B到A也算一次</para>
        /// </summary>
        Yoyo,
    }

    /// <summary>
    /// 不要存储MiniTween的引用，应当存储MiniTween.InstanceId(表示MiniTween的唯一Id)
    /// <para>如果需要获取引用，可以通过管理器Get(id)拿出Tween</para>
    /// <para>如果存储MiniTween的引用，可能会产生灾难性后果</para>
    /// </summary>
    public abstract class MiniTween : XObject
    {
        protected XObject parentXObject;

        protected long tagId;

        public long InstanceId { get; private set; }

        /// <summary>
        /// 已经完成/取消
        /// </summary>
        public bool IsCancel { get; protected set; }

        public XFTask<bool> Task { get; protected set; }

        public bool Pause { get; set; }

        /// <summary>
        /// 延迟时间
        /// </summary>
        protected float delayTime;

        /// <summary>
        /// 已过时间
        /// </summary>
        protected float elapsedTime;

        /// <summary>
        /// 持续时间
        /// </summary>
        protected float duration;

        /// <summary>
        /// 执行次数
        /// </summary>
        protected int executeCount;

        /// <summary>
        /// 循环类型
        /// </summary>
        protected MiniLoopType loopType;

        /// <summary>
        /// tween模式
        /// </summary>
        protected MiniTweenMode tweenMode;

        /// <summary>
        /// tween提供者 (谁创建的tween)
        /// </summary>
        private MiniTweenProvider provider;

        /// <summary>
        /// 完成后的回调
        /// </summary>
        protected Action completed_Action { get; set; }

        /// <summary>
        /// 释放时的回调
        /// </summary>
        protected Action destroy_Action { get; set; }

        /// <summary>
        /// 当前进度
        /// </summary>
        public float Progress => this.duration > 0 ? Mathf.Clamp01(this.elapsedTime / this.duration) : -1f;

        /// <summary>
        /// 是否已完成
        /// </summary>
        public bool IsCompelted => (this.Progress >= 1f && this.executeCount == 0) || this.IsCancel;

        protected override void OnStart()
        {
            this.InstanceId = RandomHelper.GenerateInstanceId();
            this.Pause = false;
            this.IsCancel = false;
            this.executeCount = 1;
            this.loopType = MiniLoopType.None;
            this.delayTime = 0;
        }

        /// <summary>
        /// 设置提供者
        /// </summary>
        /// <param name="provider"></param>
        internal void SetProvider(MiniTweenProvider provider)
        {
            this.provider = provider;
        }

        protected virtual void SetTweenMode(MiniTweenMode tweenMode, float arg)
        {
            this.tweenMode = tweenMode;
            switch (tweenMode)
            {
                case MiniTweenMode.Duration:
                    this.duration = arg;
                    break;
                case MiniTweenMode.Speed:
                    float distance = GetDiffValue();
                    this.duration = Math.Max(0.0001f, distance / Math.Abs(arg));
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 获取差值
        /// </summary>
        /// <returns></returns>
        protected abstract float GetDiffValue();

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="completed">立即完成</param>
        public abstract void Cancel(XObject parent, bool completed);

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="parent">因为Tween使用的类对象池，为了避免自己存储后不及时释放，所以要检验parent是否与内部的parent一致</param>
        public virtual void Cancel(XObject parent)
        {
            if (!this.CheckParentValid(parent))
                return;

            this.Dispose();
        }

        /// <summary>
        /// 设置一个延迟时间
        /// </summary>
        /// <param name="delay"></param>
        public void SetDelayTime(float delay)
        {
            this.delayTime = delay;
        }

        /// <summary>
        /// 取消/完成
        /// </summary>
        protected void SetResult(bool completed)
        {
            if (this.IsCancel)
                return;

            this.IsCancel = true;

            var tcs = this.Task;
            this.Task = null;
            tcs.SetResult(completed);
        }

        /// <summary>
        /// 增加已过时间之后
        /// </summary>
        protected abstract void AddElapsedTimeAfter();

        /// <summary>
        /// 重置
        /// </summary>
        protected abstract void Reset();

        /// <summary>
        /// 增加已过时间
        /// </summary>
        /// <param name="deltaTime"></param>
        internal void AddElapsedTime(float deltaTime)
        {
            if (!this.CheckIsValid())
                return;

            if (this.Pause || this.IsCompelted)
                return;

            this.delayTime -= deltaTime;
            if (this.delayTime > 0)
                return;

            this.elapsedTime += deltaTime;
            this.AddElapsedTimeAfter();
            this.CheckCompleted();
        }

        /// <summary>
        /// 设置已过时间
        /// </summary>
        /// <param name="elapsedTime"></param>
        protected void SetElapsedTime(float elapsedTime)
        {
            if (!this.CheckIsValid())
                return;

            if (this.Pause || this.IsCompelted)
                return;

            this.elapsedTime = elapsedTime;
            this.AddElapsedTimeAfter();
            this.CheckCompleted();
        }

        /// <summary>
        /// 检验是否有效
        /// </summary>
        /// <returns></returns>
        private bool CheckIsValid()
        {
            if (parentXObject != null)
            {
                if (parentXObject.IsDisposed || parentXObject.TagId != tagId)
                {
                    this.Dispose();
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 检验是否完成
        /// </summary>
        /// <returns></returns>
        protected void CheckCompleted()
        {
            if (this.Progress >= 1f)
            {
                if (this.executeCount > 0)
                    --this.executeCount;
                if (this.IsCompelted)
                {
                    var action = this.completed_Action;
                    try
                    {
                        action?.Invoke();
                    }
                    catch (Exception e)
                    {
                        Log.Error(e);
                    }
                    finally
                    {
                        this.Dispose();
                    }
                }
                else
                {
                    this.Reset();
                }
            }
        }

        /// <summary>
        /// 检验传进来的parent是否和tween的parent完全一致
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        public bool CheckParentValid(XObject parent)
        {
            return this.parentXObject == parent && this.tagId == parent.TagId;
        }

        /// <summary>
        /// 添加完成后的监听
        /// </summary>
        /// <param name="action"></param>
        public void AddOnCompleted(Action action)
        {
            this.completed_Action += action;
        }

        /// <summary>
        /// 移除完成后的监听
        /// </summary>
        /// <param name="action"></param>
        public void RemoveOnCompleted(Action action)
        {
            this.completed_Action -= action;
        }

        /// <summary>
        /// 移除所有完成后的监听
        /// </summary>
        public void RemoveAllOnCompleted()
        {
            this.completed_Action = null;
        }

        /// <summary>
        /// 添加释放时的监听
        /// </summary>
        /// <param name="action"></param>
        public void AddOnDestroy(Action action)
        {
            this.destroy_Action += action;
        }

        /// <summary>
        /// 移除释放时的监听
        /// </summary>
        /// <param name="action"></param>
        public void RemoveOnDestroy(Action action)
        {
            this.destroy_Action -= action;
        }

        /// <summary>
        /// 移除所有释放时的监听
        /// </summary>
        public void RemoveAllOnDestroy()
        {
            this.destroy_Action = null;
        }

        /// <summary>
        /// 设置循环执行
        /// </summary>
        /// <param name="loopType"></param>
        /// <param name="count">执行次数，小于0时为无限循环，0无效</param>
        public void SetLoop(MiniLoopType loopType, int count = 1)
        {
            if (count == 0)
            {
                Log.Error("MiniTween SetLoop error, count == 0");
                return;
            }

            this.loopType = loopType;
            switch (loopType)
            {
                case MiniLoopType.None:
                    this.executeCount = this.executeCount != 0 ? 1 : this.executeCount;
                    break;
                case MiniLoopType.Restart:
                case MiniLoopType.Yoyo:
                    this.executeCount = count;
                    break;
                default:
                    break;
            }
        }

        protected sealed override void OnDestroy()
        {
            provider?.Remove(this);
            provider = null;
            InstanceId = 0;
            bool completed =
                this.Progress >= 1f &&
                executeCount == 0; // this.Progress >= 0f && this.Progress < 1f || executeCount != 0;

            var action = this.destroy_Action;
            RemoveAllOnDestroy();

            try
            {
                action?.Invoke();
            }
            catch (Exception e)
            {
                Log.Error(e);
            }

            SetResult(completed);

            Destroy();
        }

        protected virtual void Destroy()
        {
            this.elapsedTime = 0;
            this.duration = 0;
            this.parentXObject = null;
            this.tagId = 0;
            this.RemoveAllOnCompleted();
        }
    }

    /// <summary>
    /// 不要存储MiniTween的引用，应当存储MiniTween.InstanceId(表示MiniTween的唯一Id)
    /// <para>如果需要获取引用，可以通过管理器Get(id)拿出Tween</para>
    /// <para>如果存储MiniTween的引用，可能会产生灾难性后果</para>
    /// </summary>
    public class MiniTween<T> : MiniTween, IAwake<XObject, T, T, float, MiniTweenMode>
    {
        /// <summary>
        /// 动态变化时的监听
        /// </summary>
        protected Action<T> setValue_Action;

        /// <summary>
        /// 起始值
        /// </summary>
        protected T startValue;

        /// <summary>
        /// 目标值
        /// </summary>
        protected T endValue;

        protected override void SetTweenMode(MiniTweenMode tweenMode, float arg)
        {
            base.SetTweenMode(tweenMode, arg);
            if (tweenMode == MiniTweenMode.Speed)
            {
                if (arg < 0)
                    ObjectUtils.Swap(ref startValue, ref endValue);
            }
        }

        protected override float GetDiffValue()
        {
            throw new NotImplementedException();
        }

        protected override void AddElapsedTimeAfter()
        {
            throw new NotImplementedException();
        }

        protected override void Reset()
        {
            base.elapsedTime = 0;
            switch (base.loopType)
            {
                case MiniLoopType.None:
                    break;
                case MiniLoopType.Restart:
                    break;
                case MiniLoopType.Yoyo:
                    (this.startValue, this.endValue) = (this.endValue, this.startValue);
                    break;
                default:
                    break;
            }
        }

        public override void Cancel(XObject parent)
        {
            this.Cancel(parent, false);
        }

        public override void Cancel(XObject parent, bool completed)
        {
            if (IsDisposed)
                return;

            if (!base.CheckParentValid(parent))
                return;

            Action<T> action1 = null;
            Action action2 = null;
            if (completed)
            {
                base.IsCancel = true;
                base.elapsedTime = base.duration;
                action1 = this.setValue_Action;
                action2 = this.completed_Action;
                //this.setValue_Action?.Invoke(this.endValue);
                //this.completed_Action?.Invoke();
            }

            try
            {
                action1?.Invoke(this.endValue);
                action2?.Invoke();
            }
            catch (Exception e)
            {
                Log.Error(e);
            }

            base.IsCancel = false;
            base.Dispose();
        }

        /// <summary>
        /// 添加动态监听，每变化一次调用一次监听
        /// </summary>
        /// <param name="action"></param>
        public void AddListener(Action<T> action)
        {
            this.setValue_Action += action;
            this.AddElapsedTimeAfter();
        }

        /// <summary>
        /// 移除动态监听
        /// </summary>
        /// <param name="action"></param>
        public void RemoveListener(Action<T> action)
        {
            this.setValue_Action -= action;
        }

        /// <summary>
        /// 移除所有的动态监听
        /// </summary>
        public void RemoveAllListeners()
        {
            this.setValue_Action = null;
        }

        protected override void Destroy()
        {
            base.Destroy();
            this.RemoveAllListeners();
        }

        public virtual void Initialize(XObject parent, T startValue, T endValue, float arg, MiniTweenMode tweenMode)
        {
            base.parentXObject = parent;
            base.tagId = parent.TagId;
            this.startValue = startValue;
            this.endValue = endValue;
            base.SetTweenMode(tweenMode, arg);
            SetTask();
            EndInit();
        }

        protected virtual void EndInit()
        {
        }

        private void SetTask()
        {
            base.Task = XFTask<bool>.Create();
        }
    }
}