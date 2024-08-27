using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace XFramework
{
    /// <summary>
    /// 数据类，所有的数据类要继承这个
    /// <para>需要存档的字段请用 [JsonProperty] 标记</para>
    /// <para>不需要存档的字段请用 [JsonIgnore] 标记</para>
    /// </summary>
    public class UserData : XObject, IUID, IDeserialize
    {
        /// <summary>
        /// 唯一的id，如果存到listData里，则需通过此id获取
        /// </summary>
        [JsonProperty(Order = 10000)] private long id;

        /// <summary>
        /// 用于序列化dictData
        /// </summary>
        [JsonProperty("dictData", Order = 10001, NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(HashSetUserDataJsonConverter))]
        private HashSet<UserData> _dictArchives;

        /// <summary>
        /// 用于序列化listData
        /// </summary>
        [JsonProperty("listData", Order = 10002, NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(HashSetUserDataJsonConverter))]
        private HashSet<UserData> _listArchives;

        /// <summary>
        /// 存储不能重复的数据类型
        /// </summary>
        [JsonIgnore] protected Dictionary<Type, UserData> dictData;

        /// <summary>
        /// 存储可以重复的数据类型
        /// </summary>
        [JsonIgnore] protected Dictionary<long, UserData> listData;

        /// <summary>
        /// 依附的父数据类
        /// </summary>
        [JsonIgnore] private UserData parent;

        /// <summary>
        /// 来自dictData字典
        /// </summary>
        [JsonIgnore] private bool fromDict;

        /// <summary>
        /// 类型名称
        /// </summary>
        [JsonProperty] private string typeName;

        /// <summary>
        /// 程序集名称
        /// </summary>
        [JsonProperty] private string assemblyName;

        /// <summary>
        /// 唯一的id，如果存到listData里，则需通过此id获取
        /// </summary>
        [JsonIgnore]
        public long Id => id;

        /// <summary>
        /// 依附的父数据类
        /// </summary>
        [JsonIgnore]
        public UserData Parent => parent;

        public T GetParent<T>() where T : UserData
        {
            return parent as T;
        }

        public static T Create<T>() where T : UserData, new()
        {
            return CreateWithId<T>(RandomHelper.GenerateId());
        }

        public static T Create<T, P1>(P1 p1) where T : UserData, new()
        {
            return CreateWithId<T, P1>(RandomHelper.GenerateId(), p1);
        }

        public static T CreateWithId<T>(long id) where T : UserData, new()
        {
            var data = ObjectFactory.CreateNoInit<T>();
            data.id = id;
            ObjectHelper.Awake(data);
            return data;
        }

        public static T CreateWithId<T, P1>(long id, P1 p1) where T : UserData, new()
        {
            var data = ObjectFactory.CreateNoInit<T>();
            data.id = id;
            ObjectHelper.Awake(data, p1);
            return data;
        }

        protected UserData()
        {
        }

        protected sealed override void OnStart()
        {
            base.OnStart();
            dictData ??= ObjectPool.Instance.Fetch<Dictionary<Type, UserData>>();
            listData ??= ObjectPool.Instance.Fetch<Dictionary<long, UserData>>();
            typeName ??= this.GetType().FullName;
            assemblyName ??= this.GetType().Assembly.GetName().Name;
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        void IDeserialize.Deserialize()
        {
            if (_dictArchives != null)
            {
                foreach (var data in _dictArchives)
                {
                    this.AddData(data);
                }
            }

            if (_listArchives != null)
            {
                foreach (var data in _listArchives)
                {
                    this.AddListData(data);
                }
            }

            OnDeserialize();

            if (_dictArchives != null)
            {
                foreach (var data in _dictArchives)
                {
                    ObjectHelper.Deserialize(data);
                }
            }

            if (_listArchives != null)
            {
                foreach (var data in _listArchives)
                {
                    ObjectHelper.Deserialize(data);
                }
            }
        }

        /// <summary>
        /// 反序列化时
        /// </summary>
        protected virtual void OnDeserialize()
        {
        }

        /// <summary>
        /// 设置父对象之后
        /// </summary>
        protected virtual void SetParentAfter()
        {
        }

        private void SetParent(UserData parent, bool isFromDict)
        {
            if (parent == null)
            {
                Log.Error($"{GetType()}设置parent为null");
                return;
            }

            if (parent == this)
            {
                Log.Error($"{GetType()}设置parent为this");
                return;
            }

            if (parent == this.parent)
            {
                Log.Error($"{GetType()}设置parent为this.parent");
                return;
            }

            if (this.parent != null && !this.parent.IsDisposed)
            {
                this.parent.InnerRemoveData(this);
            }

            this.parent = parent;
            this.fromDict = isFromDict;
            parent.InnerAddData(this);

            this.SetParentAfter();
        }

        /// <summary>
        /// 从dictData移除，不会执行Dispose
        /// </summary>
        /// <param name="data"></param>
        private void RemoveFromDict(UserData data)
        {
            this.dictData.Remove(data.GetType());
            if (this._dictArchives != null)
            {
                this._dictArchives.Remove(data);
                if (this._dictArchives.Count == 0)
                {
                    ObjectPool.Instance.Recycle(this._dictArchives);
                    this._dictArchives = null;
                }
            }
        }

        /// <summary>
        /// 从listData移除，不会执行Dispose
        /// </summary>
        /// <param name="data"></param>
        private void RemoveFromList(UserData data)
        {
            this.listData.Remove(data.id);
            if (this._listArchives != null)
            {
                this._listArchives.Remove(data);
                if (this._listArchives.Count == 0)
                {
                    ObjectPool.Instance.Recycle(_listArchives);
                    this._listArchives = null;
                }
            }
        }

        /// <summary>
        /// 添加到dictData
        /// </summary>
        /// <param name="data"></param>
        private void AddToDict(UserData data)
        {
            this.dictData.Add(data.GetType(), data);
            this._dictArchives ??= ObjectPool.Instance.Fetch<HashSet<UserData>>();
            this._dictArchives.Add(data);
        }

        /// <summary>
        /// 添加到listData
        /// </summary>
        /// <param name="data"></param>
        private void AddToList(UserData data)
        {
            this.listData.Add(data.Id, data);
            this._listArchives ??= ObjectPool.Instance.Fetch<HashSet<UserData>>();
            this._listArchives.Add(data);
        }

        /// <summary>
        /// 移除数据
        /// </summary>
        /// <param name="data"></param>
        private void InnerRemoveData(UserData data)
        {
            if (data.fromDict)
                this.RemoveFromDict(data);
            else
                this.RemoveFromList(data);
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="data"></param>
        private void InnerAddData(UserData data)
        {
            if (data.fromDict)
                this.AddToDict(data);
            else
                this.AddToList(data);
        }

        /// <summary>
        /// 移除数据
        /// </summary>
        /// <param name="data"></param>
        public void RemoveData(UserData data)
        {
            data.Dispose();
        }

        #region DictData

        /// <summary>
        /// 添加一个不会重复的数据类型
        /// </summary>
        /// <param name="data"></param>
        public void AddData(UserData data)
        {
            var type = data.GetType();
            if (GetData(type) != null)
            {
                Log.Error($"{GetType()} 重复添加UserData, Type = {type}");
                return;
            }

            data.SetParent(this, true);
        }

        /// <summary>
        /// 获取一个数据
        /// </summary>
        /// <param name="dataType"></param>
        /// <returns></returns>
        public UserData GetData(Type dataType)
        {
            if (dictData.TryGetValue(dataType, out var data))
                return data;

            return null;
        }

        /// <summary>
        /// 获取一个数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetData<T>() where T : UserData
        {
            return GetData(typeof(T)) as T;
        }

        #endregion

        #region ListData

        /// <summary>
        /// 添加一个可重复类型
        /// </summary>
        /// <param name="data"></param>
        public void AddListData(UserData data)
        {
            if (GetListData(data.id) != null)
            {
                Log.Error($"{GetType()} 重复添加UserData, Id = {data.id}");
                return;
            }

            data.SetParent(this, false);
        }

        /// <summary>
        /// 获取一个数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserData GetListData(long id)
        {
            if (listData.TryGetValue(id, out var data))
                return data;

            return null;
        }

        /// <summary>
        /// 获取一个数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetListData<T>(long id) where T : UserData
        {
            return GetListData(id) as T;
        }

        #endregion

        protected virtual void Destroy()
        {
        }

        protected sealed override void OnDestroy()
        {
            base.OnDestroy();

            foreach (var data in dictData.Values)
            {
                data.Dispose();
            }

            dictData.Clear();
            ObjectPool.Instance.Recycle(dictData);
            dictData = null;

            foreach (var data in listData.Values)
            {
                data.Dispose();
            }

            listData.Clear();
            ObjectPool.Instance.Recycle(listData);
            listData = null;

            if (_dictArchives != null)
            {
                _dictArchives.Clear();
                ObjectPool.Instance.Recycle(_dictArchives);
                _dictArchives = null;
            }

            if (_listArchives != null)
            {
                _listArchives.Clear();
                ObjectPool.Instance.Recycle(_listArchives);
                _listArchives = null;
            }

            Destroy();

            if (parent != null && !parent.IsDisposed)
            {
                parent.InnerRemoveData(this);
            }

            parent = null;
            fromDict = default;
            id = 0;
            typeName = null;
        }
    }
}