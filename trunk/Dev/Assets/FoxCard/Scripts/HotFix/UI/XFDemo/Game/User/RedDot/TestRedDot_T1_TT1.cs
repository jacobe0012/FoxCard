using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XFramework
{
    internal struct TestRefreshRed
    {

    }

    [RedDotName(RedDotConst.TestRedDot_T1_TT1)]
    internal class TestRedDot_T1_TT1 : RedDotNode, IEvent<TestRefreshRed>
    {
        private long timerId;

        protected override void Init()
        {
            base.Init();
            // 假设每秒执行一次刷新红点，这里只是用于测试
            timerId = TimerManager.Instance.StartRepeatedTimer(1000, Timer);   
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            TimerManager.Instance?.RemoveTimerId(ref timerId);
        }

        private void Timer()
        {
            // 正常逻辑应该是通过外部接口当有红点相关的数据发生改变才会执行刷新
            EventManager.Instance.Publish(new TestRefreshRed());
        }

        /// <summary>
        /// 从其他地方发消息，在这里接收后刷新红点
        /// </summary>
        /// <param name="args"></param>
        void IEvent<TestRefreshRed>.HandleEvent(TestRefreshRed args)
        {
            Refresh();
        }

        protected override void RefreshRedDot()
        {
            base.RefreshRedDot();
            // 这里刷新红点逻辑
            long time = TimeHelper.ClientNow();
            IsRed = time % 10 < 5;  //如果时间的个位数字小于5则显示红点
        }
    }
}
