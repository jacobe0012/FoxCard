using System;

namespace XFramework
{
    public sealed class UserDataManager : CommonObject
    {
        /// <summary>
        /// 存档的Key
        /// </summary>
        private const string UserArchiveKey = "USERARCHIVES";

        private User user;

        /// <summary>
        /// 创建User类的委托
        /// </summary>
        private Func<User> createUser;

        /// <summary>
        /// 自动保存数据间隔时间(毫秒)
        /// </summary>
        private long saveInterval = TimeHelper.Minute;

        private long timerId;

        /// <summary>
        /// 存档号
        /// </summary>
        private int archiveId = 0;

        /// <summary>
        /// 用户数据
        /// </summary>
        public User MyUser => user;

        /// <summary>
        /// 自动保存数据间隔时间(毫秒)
        /// </summary>
        public long SaveInterval => saveInterval;

        protected override void Init()
        {
            base.Init();
            createUser = InnerCreateUser;
        }

        protected override void Destroy()
        {
            InnerSave();

            base.Destroy();
            var timerMgr = TimerManager.Instance;
            if (timerMgr != null && timerId > 0)
            {
                timerMgr.RemoveTimerId(ref timerId);
            }

            user?.Dispose();
            user = null;
        }

        private void SetString(int archiveId, string str)
        {
            // 检查存档id是否有变化，有变化代表已经有最新的，不给存
            if (archiveId != this.archiveId)
                return;

            ++this.archiveId;
            PlayerPrefsHelper.SetString(UserArchiveKey, str, true);
            //Log.Error($"存档\n{str}");
        }

        /// <summary>
        /// 创建User类以及关联数据类
        /// </summary>
        /// <returns></returns>
        private User InnerCreateUser()
        {
            var user = UserData.Create<User>();

            return user;
        }

        /// <summary>
        /// 保存存档(异步)
        /// </summary>
        private async XFTask InnerSaveAsync()
        {
            if (user is null)
                return;

            int archiveId = this.archiveId;
            string json = await System.Threading.Tasks.Task.Run(() => { return JsonHelper.ToJson(user); });
            SetString(archiveId, json);
        }

        /// <summary>
        /// 自动存档方法
        /// </summary>
        private void InnerSaveTimer()
        {
            SaveAsync().Coroutine();
        }

        /// <summary>
        /// 保存存档(同步)
        /// </summary>
        private void InnerSave()
        {
            if (user is null)
                return;

            int archiveId = this.archiveId;
            string json = JsonHelper.ToJson(user);
            SetString(archiveId, json);
        }

        /// <summary>
        /// 自动存档 重新开始计时
        /// </summary>
        private void RestartTimer()
        {
            var timerMgr = TimerManager.Instance;
            if (timerId > 0)
            {
                timerMgr.RemoveTimerId(ref timerId);
            }

            timerId = timerMgr.StartRepeatedTimer(saveInterval, InnerSaveTimer);
        }

        /// <summary>
        /// 异步加载存档
        /// </summary>
        /// <returns></returns>
        public async XFTask LoadAsync()
        {
            if (PlayerPrefsHelper.TryGetString(UserArchiveKey, out var json))
            {
                //Log.Error($"读取存档\n{json}");
                var task = System.Threading.Tasks.Task.Run(() =>
                {
                    var obj = JsonHelper.ToObject<User>(json);
                    return obj;
                });

                var obj = await task;
                user = obj;
                ObjectHelper.Deserialize(obj);
            }
            else
            {
                user = createUser();
            }

            RestartTimer();
        }

        /// <summary>
        /// 设置一个创建User的委托，如果没有存档则会调用这个
        /// </summary>
        /// <param name="createUser"></param>
        public void SetCreateUser(Func<User> createUser)
        {
            this.createUser = createUser;
        }

        /// <summary>
        /// 设置自动保存间隔（毫秒）
        /// </summary>
        /// <param name="interval"></param>
        public void SetSaveInterval(long interval)
        {
            this.saveInterval = interval;
            RestartTimer();
        }

        /// <summary>
        /// 保存当前存档(同步)
        /// </summary>
        public void Save()
        {
            RestartTimer();
            InnerSave();
        }

        /// <summary>
        /// 保存当前存档(异步)
        /// </summary>
        /// <returns></returns>
        public async XFTask SaveAsync()
        {
            RestartTimer();
            await InnerSaveAsync();
        }
    }
}