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
        /// 移除一个定时任务
        /// </summary>
        /// <param name="timerId"></param>
        public void RemoveTimerId(ref long timerId)
        {
            this.timerProvider.RemoveTimerId(ref timerId);
        }

        /// <summary>
        /// 等待一定时间
        /// </summary>
        /// <param name="waitTime">等待时间（毫秒）</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async XFTask<bool> WaitAsync(long waitTime, XCancellationToken cancellationToken = null)
        {
            return await this.timerProvider.WaitAsync(waitTime, cancellationToken);
        }

        /// <summary>
        /// 等待一帧
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async XFTask<bool> WaitFrameAsync(XCancellationToken cancellationToken = null)
        {
            return await this.timerProvider.WaitFrameAsync(cancellationToken);
        }

        /// <summary>
        /// 延迟一定时间后执行回调
        /// </summary>
        /// <param name="delayTime">延迟时间（毫秒）</param>
        /// <param name="action">回调</param>
        /// <returns></returns>
        public long StartOnceTimer(long delayTime, Action action)
        {
            return this.timerProvider.StartOnceTimer(delayTime, action);
        }

        /// <summary>
        /// 开启一个重复执行的任务
        /// </summary>
        /// <param name="repeatTime">重复执行的时间（毫秒）</param>
        /// <param name="action">回调</param>
        /// <returns></returns>
        public long StartRepeatedTimer(long repeatTime, Action action)
        {
            return this.timerProvider.StartRepeatedTimer(repeatTime, action);
        }

        /// <summary>
        /// 每帧执行的任务
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public long RepeatedFrameTimer(Action action)
        {
            return this.timerProvider.RepeatedFrameTimer(action);
        }

        /// <summary>
        /// 直到时间达到tillTime时执行action，一般用于不要求逻辑连贯的地方
        /// </summary>
        /// <param name="tillTime">直到时间，毫秒级</param>
        /// <param name="action"></param>
        /// <returns></returns>
        public long WaitTill(long tillTime, Action action)
        {
            return this.timerProvider.WaitTill(tillTime, action);
        }

        /// <summary>
        /// 直到时间达到tillTime时返回true
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
