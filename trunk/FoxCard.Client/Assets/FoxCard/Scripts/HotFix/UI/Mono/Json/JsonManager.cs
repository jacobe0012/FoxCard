using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using XFramework;
using YooAsset;

namespace HotFix_UI
{
    public sealed class JsonManager : Singleton<JsonManager>, IDisposable
    {
        /// <summary>
        /// 用户数据
        /// </summary>
        public PlayerSaveData user = new PlayerSaveData();


#if UNITY_EDITOR
        private string savePath = "Assets/Resources/Local";
#else
        private  string savePath = Application.persistentDataPath + "/Local";
#endif
        public void Init()
        {
            // string savePath = "Assets/Resources/notice.json";
            // string jsonData = File.ReadAllText(savePath);
            // var data = JsonConvert.DeserializeObject<List<Notice>>(jsonData);
            //
            // foreach (var VARIABLE in data)
            // {
            //     Log.Error($"zhCnContent {VARIABLE.zhCnContent}");
            //     Log.Error($"version {VARIABLE.version}");
            // }
        }

        /// <summary>
        /// 保存玩家存档数据
        /// </summary>
        /// <param name="data">玩家存档数据</param>
        /// <param name="userId">userID</param>
        public async UniTask SavePlayerData(PlayerSaveData data)
        {
            if (data == null)
            {
                Log.Error($"data is null");
                return;
            }

            user = data;

            string folderPath = savePath; // 指定存档文件夹的路径
            string filePath = Path.Combine(folderPath, user.userId + ".json"); // 使用玩家账号名拼接文件名
            string jsonData = JsonConvert.SerializeObject(data); // 将数据转换为JSON格式的字符串
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // 将JSON字符串写入文件
            await File.WriteAllTextAsync(filePath, jsonData).AsUniTask();
        }

        /// <summary>
        /// 根据userID读取玩家存档数据
        /// </summary>
        /// <param name="userId">userId</param>
        /// <returns>玩家存档数据</returns>
        public async UniTask<PlayerSaveData> LoadPlayerData(long userId = default, string nickName = default)
        {
            if (userId == default)
            {
                userId = user.userId;
            }

            if (nickName == default)
            {
                nickName = user.nickName;
            }

            string folderPath = savePath; // 指定存档文件夹的路径
            string filePath = Path.Combine(folderPath, userId + ".json"); // 使用玩家账号名拼接文件名
            //PlayerSaveData data = new PlayerSaveData();
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            if (File.Exists(filePath))
            {
                string jsonData = await File.ReadAllTextAsync(filePath).AsUniTask();
                user = JsonConvert.DeserializeObject<PlayerSaveData>(jsonData);
            }
            else
            {
                user.userId = userId;
                user.popLevelID = 10002;
                user.nickName = nickName;

                string json = JsonConvert.SerializeObject(user);
                await File.WriteAllTextAsync(filePath, json).AsUniTask();
            }


            return user;
        }


        public void Dispose()
        {
        }
    }
}