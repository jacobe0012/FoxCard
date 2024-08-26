using System;
using System.Collections.Generic;

namespace XFramework
{
    internal enum TimerType
    {
        None,
        Wait,
        Once,
        Repeat,
    }

    internal class TimerTask : XObject, IAwake<long, long, TimerType, object>
    {
        public long Id { get; private set; }

        public object Callback { get; private set; }

        public long Time { get; private set; }

        public TimerType TimerType { get; private set; }

        public void Initialize(long id, long time, TimerType type, object callback)
        {
            this.Id = id;
            this.Time = time;
            this.TimerType = type;
            this.Callback = callback;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            this.Id = default;
            this.Time = default;
            this.TimerType = default;
            this.Callback = default;
        }
    }

    public sealed class TimerProvider : XObject, IAwake<ITimeNow>, IUpdate
    {
        private ITimeNow timeGetter;

        /// <summary>
        /// 任务id - 定时任务
        /// </summary>
        private Dictionary<long, TimerTask> timerTasks = new Dictionary<long, TimerTask>();

        /// <summary>
        /// 到达时间 - 任务id
        /// </summary>
        private SortedDictionary<long, List<long>> timerTaskIds = new SortedDictionary<long, List<long>>();

        /// <summary>
        /// 存储所有过期的到达时间
        /// </summary>
        private Queue<long> timerOutTimeIds = new Queue<long>();

        /// <summary>
        /// 存储所有过期的任务id
        /// </summary>
        private Queue<long> timerOutTimeTaskIds = new Queue<long>();

        /// <summary>
        /// 最小要到达的时间
        /// </summary>
        private long minTime = long.MaxValue;

        private long timerId = 0;

        public void Initialize(ITimeNow args)
        {
            this.timeGetter = args;
        }

        public void Update()
        {
            if (timerTaskIds.Count == 0)
                return;

            long curTime = GetCurTime();

            if (curTime < minTime)
                return;

            foreach (var dict in timerTaskIds)
            {
                long time = dict.Key;
                if (time > curTime)
                {
                    minTime = time;
                    break;
                }

                timerOutTimeIds.Enqueue(time);
            }

            while (timerOutTimeIds.Count > 0)
            {
                long time = timerOutTimeIds.Dequeue();

                foreach (var taskId in timerTaskIds[time])
                {
                    timerOutTimeTaskIds.Enqueue(taskId);
                }

                timerTaskIds.Remove(time);
            }

            while (timerOutTimeTaskIds.Count > 0)
            {
                long id = timerOutTimeTaskIds.Dequeue();

                TimerTask timerTask = GetChild(id);
                Run(timerTask);
            }
        }

        private TimerTask GetChild(long id)
        {
            timerTasks.TryGetValue(id, out var task);
            return task;
        }

        private TimerTask AddChild(long time, TimerType timerType, object callback)
        {
            long id = ++timerId;
            if (id == 0)
                id = ++timerId;

            var task = ObjectFactory.Create(typeof(TimerTask), id, time, timerType, callback, true) as TimerTask;
            timerTasks.Add(id, task);
            return task;
        }

        private long GetCurTime()
        {
            return this.timeGetter.GetTime();
        }

        private void Run(TimerTask task)
        {
            if (task == null)
                return;

            switch (task.TimerType)
            {
                case TimerType.None:
                    break;

                case TimerType.Wait:
                {
                    var tcs = task.Callback as XFTask<bool>;
                    Remove(task.Id);
                    tcs?.SetResult(true);
                    break;
                }

                case TimerType.Once:
                {
                    Action action = task.Callback as Action;
                    Remove(task.Id);
                    action?.Invoke();
                    break;
                }

                case TimerType.Repeat:
                {
                    Action action = task.Callback as Action;
                    long tillTime = task.Time + GetCurTime();
                    AddTimer(tillTime, task);
                    action?.Invoke();
                    break;
                }

                default:
                    break;
            }
        }

        private void AddTimer(long tillTime, TimerTask task)
        {
            List<long> list;
            if (!timerTaskIds.TryGetValue(tillTime, out list))
            {
                list = new List<long>();
                timerTaskIds.Add(tillTime, list);
            }

            list.Add(task.Id);

            if (tillTime < minTime)
                minTime = tillTime;
        }

        private bool Remove(long timerId)
        {
            if (timerId == 0)
                return false;

            TimerTask timerTask = GetChild(timerId);
            if (timerTask == null)
                return false;

            timerTask.Dispose();
            timerTasks.Remove(timerId);

            return true;
        }

        /// <summary>
        /// 移除一个定时任务
        /// </summary>
        /// <param name="timerId"></param>
        public void RemoveTimerId(ref long timerId)
        {
            Remove(timerId);
            timerId = 0;
        }

        /// <summary>
        /// 等待一定时间
        /// </summary>
        /// <param name="waitTime">等待时间（毫秒）</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async XFTask<bool> WaitAsync(long waitTime, XCancellationToken cancellationToken = null)
        {
            long tillTime = GetCurTime() + waitTime;
            return await WaitTillAsync(tillTime, cancellationToken);
        }

        /// <summary>
        /// 等待一帧
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async XFTask<bool> WaitFrameAsync(XCancellationToken cancellationToken = null)
        {
            return await WaitAsync(1, cancellationToken);
        }

        /// <summary>
        /// 延迟一定时间后执行回调
        /// </summary>
        /// <param name="delayTime">延迟时间（毫秒）</param>
        /// <param name="action">回调</param>
        /// <returns></returns>
        public long StartOnceTimer(long delayTime, Action action)
        {
            long tillTime = GetCurTime() + delayTime;
            TimerTask timerTask = AddChild(delayTime, TimerType.Once, action);
            AddTimer(tillTime, timerTask);

            return timerTask.Id;
        }

        /// <summary>
        /// 开启一个重复执行的任务
        /// </summary>
        /// <param name="repeatTime">重复执行的时间（毫秒）</param>
        /// <param name="action">回调</param>
        /// <returns></returns>
        public long StartRepeatedTimer(long repeatTime, Action action)
        {
            //long tillTime = GetCurTime() + repeatTime;
            long tillTime = GetCurTime();
            TimerTask timerTask = AddChild(repeatTime, TimerType.Repeat, action);
            AddTimer(tillTime, timerTask);

            return timerTask.Id;
        }

        /// <summary>
        /// 每帧执行的任务
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public long RepeatedFrameTimer(Action action)
        {
            return this.StartRepeatedTimer(1, action);
        }

        /// <summary>
        /// 直到时间达到tillTime时执行action，一般用于不要求逻辑连贯的地方
        /// </summary>
        /// <param name="tillTime">直到时间，毫秒级</param>
        /// <param name="action"></param>
        /// <returns></returns>
        public long WaitTill(long tillTime, Action action)
        {
            if (tillTime <= GetCurTime())
            {
                Log.Warnning("WaitTillTimer tillTime less than current time");
            }

            TimerTask timerTask = AddChild(tillTime, TimerType.Once, action);
            AddTimer(tillTime, timerTask);

            return timerTask.Id;
        }

        /// <summary>
        /// 直到时间达到tillTime时返回true
        /// </summary>
        /// <param name="tillTime"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async XFTask<bool> WaitTillAsync(long tillTime, XCancellationToken cancellationToken = null)
        {
            if (tillTime <= GetCurTime())
                return true;

            XFTask<bool> tcs = XFTask<bool>.Create(true);

            TimerTask timerTask = AddChild(tillTime, TimerType.Wait, tcs);
            AddTimer(tillTime, timerTask);

            long timerId = timerTask.Id;

            void Function()
            {
                if (Remove(timerId))
                    tcs.SetResult(false);
            }

            bool result = false;
            try
            {
                cancellationToken?.Register(Function);
                result = await tcs;
            }
            finally
            {
                cancellationToken?.Remove(Function);
            }

            return result;
        }

        protected override void OnDestroy()
        {
            foreach (TimerTask task in timerTasks.Values)
            {
                task.Dispose();
            }

            timerTasks.Clear();
            timerTaskIds.Clear();
            timerOutTimeIds.Clear();
            timerOutTimeTaskIds.Clear();
            timeGetter = null;
        }
    }
}