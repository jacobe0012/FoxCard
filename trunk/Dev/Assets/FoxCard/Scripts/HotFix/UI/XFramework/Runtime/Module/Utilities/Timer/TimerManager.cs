using System;

namespace XFramework
{
    public sealed class TimerManager : CommonObject, IUpdate
    {
        private static TimerManager _instance;

        private TimerProvider timerProvider;

        public static TimerManager Instance => _instance;

        protected override void Init()
        {
            base.Init();
            _instance = this;
            this.timerProvider = ObjectFactory.Create<TimerProvider, ITimeNow>(new ActualTime(), true);
        }

        protected override void Destroy()
        {
            this.timerProvider?.Dispose();
            this.timerProvider = null;
            _instance = null;
            base.Destroy();
        }

        public void Update()
        {
            this.timerProvider.Update();
        }

        /// <summary>
        /// �Ƴ�һ����ʱ����
        /// </summary>
        /// <param name="timerId"></param>
        public void RemoveTimerId(ref long timerId)
        {
            this.timerProvider.RemoveTimerId(ref timerId);
        }

        /// <summary>
        /// �ȴ�һ��ʱ��
        /// </summary>
        /// <param name="waitTime">�ȴ�ʱ�䣨���룩</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async XFTask<bool> WaitAsync(long waitTime, XCancellationToken cancellationToken = null)
        {
            return await this.timerProvider.WaitAsync(waitTime, cancellationToken);
        }

        /// <summary>
        /// �ȴ�һ֡
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async XFTask<bool> WaitFrameAsync(XCancellationToken cancellationToken = null)
        {
            return await this.timerProvider.WaitFrameAsync(cancellationToken);
        }

        /// <summary>
        /// �ӳ�һ��ʱ���ִ�лص�
        /// </summary>
        /// <param name="delayTime">�ӳ�ʱ�䣨���룩</param>
        /// <param name="action">�ص�</param>
        /// <returns></returns>
        public long StartOnceTimer(long delayTime, Action action)
        {
            return this.timerProvider.StartOnceTimer(delayTime, action);
        }

        /// <summary>
        /// ����һ���ظ�ִ�е�����
        /// </summary>
        /// <param name="repeatTime">�ظ�ִ�е�ʱ�䣨���룩</param>
        /// <param name="action">�ص�</param>
        /// <returns></returns>
        public long StartRepeatedTimer(long repeatTime, Action action)
        {
            return this.timerProvider.StartRepeatedTimer(repeatTime, action);
        }

        /// <summary>
        /// ÿִ֡�е�����
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public long RepeatedFrameTimer(Action action)
        {
            return this.timerProvider.RepeatedFrameTimer(action);
        }

        /// <summary>
        /// ֱ��ʱ��ﵽtillTimeʱִ��action��һ�����ڲ�Ҫ���߼�����ĵط�
        /// </summary>
        /// <param name="tillTime">ֱ��ʱ�䣬���뼶</param>
        /// <param name="action"></param>
        /// <returns></returns>
        public long WaitTill(long tillTime, Action action)
        {
            return this.timerProvider.WaitTill(tillTime, action);
        }

        /// <summary>
        /// ֱ��ʱ��ﵽtillTimeʱ����true
        /// </summary>
        /// <param name="tillTime"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async XFTask<bool> WaitTillAsync(long tillTime, XCancellationToken cancellationToken = null)
        {
            return await this.timerProvider.WaitTillAsync(tillTime, cancellationToken);
        }
    }
}
