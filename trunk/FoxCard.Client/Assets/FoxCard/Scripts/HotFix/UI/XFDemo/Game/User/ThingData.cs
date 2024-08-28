using System;
using Newtonsoft.Json;


// demo
namespace XFramework
{
    namespace UIEventType
    {
        /// <summary>
        /// 当有物品改变时
        /// </summary>
        public struct OnThingChanged
        {
            public int ThingId { get; set; }

            public int Count { get; set; }
        }
    }

    /// <summary>
    /// 物品数据，一般不会向外部暴露这个类
    /// </summary>
    public class Thing : UserData, IAwake<int>
    {
        /// <summary>
        /// 物品id
        /// </summary>
        [JsonProperty]
        public int ThingId { get; private set; }

        /// <summary>
        /// 物品个数
        /// </summary>
        [JsonProperty]
        public int Count { get; private set; }

        public void Initialize(int thingId)
        {
            this.ThingId = thingId;
        }

        public void AddCount(int add)
        {
            if (add == 0)
                return;

            Count = Math.Max(0, Count + add);
            OnCountChanged();
        }

        public void SetCount(int count)
        {
            if (count == this.Count)
                return;

            Count = Math.Max(0, count);
            OnCountChanged();
        }

        /// <summary>
        /// 发送消息告诉UI物品数量改变，UI接收消息后更新显示
        /// </summary>
        public void OnCountChanged()
        {
            EventManager.Instance.Publish(new UIEventType.OnThingChanged
                { ThingId = this.ThingId, Count = this.Count });
        }
    }

    /// <summary>
    /// 物品数据集合，这是Demo，提供思路
    /// </summary>
    public class ThingData : UserData
    {
        /// <summary>
        /// 添加物品，count可以为负数，这样添加和移除都可用这个接口
        /// </summary>
        /// <param name="thingId"></param>
        /// <param name="count"></param>
        public void AddThing(int thingId, int count)
        {
            if (count == 0)
                return;

            var thing = this.GetThing(thingId);
            if (thing is null)
            {
                thing = this.CreateThing(thingId);
            }

            thing.AddCount(count);
            if (thing.Count == 0)
                this.RemoveData(thing);
        }

        /// <summary>
        /// 物品个数是否满足
        /// </summary>
        /// <param name="thingId"></param>
        /// <param name="needCount"></param>
        /// <returns></returns>
        public bool ThingIsEnough(int thingId, int needCount)
        {
            return this.GetThingCount(thingId) >= needCount;
        }

        /// <summary>
        /// 获取物品个数
        /// </summary>
        /// <param name="thingId"></param>
        /// <returns></returns>
        public int GetThingCount(int thingId)
        {
            return this.GetThing(thingId)?.Count ?? 0;
        }

        private Thing GetThing(int thingId)
        {
            return this.GetListData<Thing>(thingId);
        }

        private Thing CreateThing(int thingId)
        {
            var thing = UserData.CreateWithId<Thing, int>(thingId, thingId);
            this.AddListData(thing);

            return thing;
        }
    }
}