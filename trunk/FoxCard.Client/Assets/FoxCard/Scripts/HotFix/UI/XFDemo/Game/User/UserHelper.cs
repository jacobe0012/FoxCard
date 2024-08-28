using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// demo
namespace XFramework
{
    public static class UserHelper
    {
        internal static User CreateUser()
        {
            var user = UserData.Create<User>();

            //获取的时候可以用User.Instance.GetData<ThingData>();
            //但是这样获取并不好，因为存档里可能没有这个类，导致反序列化后也没有这个类
            //所以我写了个帮助类，UserHelper，调用UserHelper.GetThingData();即可
            var thingData = UserData.Create<ThingData>();
            user.AddData(thingData);
            thingData.AddThing(10000, 9999);    //这里代表开局就给9999个id为10000物品，至于这个id 10000的物品是啥，去配置表随意配置即可
            thingData.AddThing(10001, 1);    //这里代表开局就给1个id为10001物品，至于这个id 10000的物品是啥，去配置表随意配置即可

            return user;
        }

        /// <summary>
        /// 获取物品数据集合，如没有，则创建
        /// <para>有一种情况是，可能这个是后加的类，存档里没有，所以要写没有则创建</para>
        /// </summary>
        /// <returns></returns>
        public static ThingData GetThingData(this User self)
        {
            var thingData = self.GetData<ThingData>();
            if (thingData is null)
            {
                thingData = UserData.Create<ThingData>();
                self.AddData(thingData);
            }

            return thingData;
        }
    }
}
