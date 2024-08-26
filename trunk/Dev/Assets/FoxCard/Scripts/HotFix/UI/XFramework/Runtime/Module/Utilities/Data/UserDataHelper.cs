using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XFramework
{
    public static class UserDataHelper
    {
        private static bool GetUserMgr(out UserDataManager userMgr)
        {
            userMgr = Common.Instance.Get<UserDataManager>();
            return userMgr != null; 
        }

        /// <summary>
        /// 加载存档(异步)
        /// </summary>
        /// <returns></returns>
        public async static XFTask LoadAsync()
        {
            if (!GetUserMgr(out var mgr))
                return;

            await mgr.LoadAsync();
        }

        /// <summary>
        /// 保存存档(同步)
        /// </summary>
        public static void Save()
        {
            if (!GetUserMgr(out var mgr))
                return;

            mgr.Save();
        }

        /// <summary>
        /// 保存存档(异步)
        /// </summary>
        /// <returns></returns>
        public async static XFTask SaveAsync()
        {
            if (!GetUserMgr(out var mgr))
                return;

            await mgr.SaveAsync();
        }

        /// <summary>
        /// 设置一个创建User的委托，如果没有存档则会调用这个
        /// </summary>
        /// <param name="createUser"></param>
        public static void SetCreateUser(Func<User> createUser)
        {
            if (!GetUserMgr(out var mgr))
                return;

            mgr.SetCreateUser(createUser);
        }

        /// <summary>
        /// 设置自动保存间隔（毫秒）
        /// </summary>
        /// <param name="interval"></param>
        public static void SetSaveInterval(long interval)
        {
            if (!GetUserMgr(out var mgr))
                return;

            mgr.SetSaveInterval(interval);
        }
    }
}
