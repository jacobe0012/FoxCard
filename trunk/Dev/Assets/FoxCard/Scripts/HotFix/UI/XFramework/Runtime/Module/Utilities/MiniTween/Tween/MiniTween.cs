using System;
using UnityEngine;

namespace XFramework
{
    public enum MiniLoopType
    {
        /// <summary>
        /// ��ѭ��
        /// </summary>
        None,
        /// <summary>
        /// ���¿�ʼ
        /// </summary>
        Restart,
        /// <summary>
        /// A��B��B��A
        /// <para>A��B��һ�Σ�B��AҲ��һ��</para>
        /// </summary>
        Yoyo,
    }

    /// <summary>
    /// ��Ҫ�洢MiniTween�����ã�Ӧ���洢MiniTween.InstanceId(��ʾMiniTween��ΨһId)
    /// <para>�����Ҫ��ȡ���ã�����ͨ��������Get(id)�ó�Tween</para>
    /// <para>����洢MiniTween�����ã����ܻ���������Ժ��</para>
    /// </summary>
    public abstract class MiniTween : XObject
    {
        protected XObject parentXObject;

        protected long tagId;

        public long InstanceId { get; private set; }

        /// <summary>
        /// �Ѿ����/ȡ��
        /// </summary>
        public bool IsCancel { get; protected set; }

        public XFTask<bool> Task { get; protected set; }

        public bool Pause { get; set; }

        /// <summary>
        /// �ӳ�ʱ��
        /// </summary>
        protected float delayTime;

        /// <summary>
        /// �ѹ�ʱ��
        /// </summary>
        protected float elapsedTime;

        /// <summary>
        /// ����ʱ��
        /// </summary>
        protected float duration;

        /// <summary>
        /// ִ�д���
        /// </summary>
        protected int executeCount;

        /// <summary>
        /// ѭ������
        /// </summary>
        protected MiniLoopType loopType;

        /// <summary>
        /// tweenģʽ
        /// </summary>
        protected MiniTweenMode tweenMode;

        /// <summary>
        /// tween�ṩ�� (˭������tween)
        /// </summary>
        private MiniTweenProvider provider;

        /// <summary>
        /// ��ɺ�Ļص�
        /// </summary>
        protected Action completed_Action { get; set; }

        /// <summary>
        /// �ͷ�ʱ�Ļص�
        /// </summary>
        protected Action destroy_Action { get; set; }

        /// <summary>
        /// ��ǰ����
        /// </summary>
        public float Progress => this.duration > 0 ? Mathf.Clamp01(this.elapsedTime / this.duration) : -1f;

        /// <summary>
        /// �Ƿ������
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
        /// �����ṩ��
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
        /// ��ȡ��ֵ
        /// </summary>
        /// <returns></returns>
        protected abstract float GetDiffValue();

        /// <summary>
        /// ȡ��
        /// </summary>
        /// <param name="completed">�������</param>
        public abstract void Cancel(XObject parent, bool completed);

        /// <summary>
        /// ȡ��
        /// </summary>
        /// <param name="parent">��ΪTweenʹ�õ������أ�Ϊ�˱����Լ��洢�󲻼�ʱ�ͷţ�����Ҫ����parent�Ƿ����ڲ���parentһ��</param>
        public virtual void Cancel(XObject parent)
        {
            if (!this.CheckParentValid(parent))
                return;

            this.Dispose();
        }

        /// <summary>
        /// ����һ���ӳ�ʱ��
        /// </summary>
        /// <param name="delay"></param>
        public void SetDelayTime(float delay)
        {
            this.delayTime = delay;
        }

        /// <summary>
        /// ȡ��/���
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
        /// �����ѹ�ʱ��֮��
        /// </summary>
        protected abstract void AddElapsedTimeAfter();

        /// <summary>
        /// ����
        /// </summary>
        protected abstract void Reset();

        /// <summary>
        /// �����ѹ�ʱ��
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
        /// �����ѹ�ʱ��
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
        /// �����Ƿ���Ч
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
        /// �����Ƿ����
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
        /// ���鴫������parent�Ƿ��tween��parent��ȫһ��
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        public bool CheckParentValid(XObject parent)
        {
            return this.parentXObject == parent && this.tagId == parent.TagId;
        }

        /// <summary>
        /// �����ɺ�ļ���
        /// </summary>
        /// <param name="action"></param>
        public void AddOnCompleted(Action action)
        {
            this.completed_Action += action;
        }

        /// <summary>
        /// �Ƴ���ɺ�ļ���
        /// </summary>
        /// <param name="action"></param>
        public void RemoveOnCompleted(Action action)
        {
            this.completed_Action -= action;
        }

        /// <summary>
        /// �Ƴ�������ɺ�ļ���
        /// </summary>
        public void RemoveAllOnCompleted()
        {
            this.completed_Action = null;
        }

        /// <summary>
        /// ����ͷ�ʱ�ļ���
        /// </summary>
        /// <param name="action"></param>
        public void AddOnDestroy(Action action)
        {
            this.destroy_Action += action;
        }

        /// <summary>
        /// �Ƴ��ͷ�ʱ�ļ���
        /// </summary>
        /// <param name="action"></param>
        public void RemoveOnDestroy(Action action)
        {
            this.destroy_Action -= action;
        }

        /// <summary>
        /// �Ƴ������ͷ�ʱ�ļ���
        /// </summary>
        public void RemoveAllOnDestroy()
        {
            this.destroy_Action = null;
        }

        /// <summary>
        /// ����ѭ��ִ��
        /// </summary>
        /// <param name="loopType"></param>
        /// <param name="count">ִ�д�����С��0ʱΪ����ѭ����0��Ч</param>
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
            bool completed = this.Progress >= 1f && executeCount == 0;// this.Progress >= 0f && this.Progress < 1f || executeCount != 0;

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
    /// ��Ҫ�洢MiniTween�����ã�Ӧ���洢MiniTween.InstanceId(��ʾMiniTween��ΨһId)
    /// <para>�����Ҫ��ȡ���ã�����ͨ��������Get(id)�ó�Tween</para>
    /// <para>����洢MiniTween�����ã����ܻ���������Ժ��</para>
    /// </summary>
    public class MiniTween<T> : MiniTween, IAwake<XObject, T, T, float, MiniTweenMode>
    {
        /// <summary>
        /// ��̬�仯ʱ�ļ���
        /// </summary>
        protected Action<T> setValue_Action;

        /// <summary>
        /// ��ʼֵ
        /// </summary>
        protected T startValue;

        /// <summary>
        /// Ŀ��ֵ
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
        /// ��Ӷ�̬������ÿ�仯һ�ε���һ�μ���
        /// </summary>
        /// <param name="action"></param>
        public void AddListener(Action<T> action)
        {
            this.setValue_Action += action;
            this.AddElapsedTimeAfter();
        }

        /// <summary>
        /// �Ƴ���̬����
        /// </summary>
        /// <param name="action"></param>
        public void RemoveListener(Action<T> action)
        {
            this.setValue_Action -= action;
        }

        /// <summary>
        /// �Ƴ����еĶ�̬����
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
