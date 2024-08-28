using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;
using XFramework;
using YooAsset;

namespace HotFix_UI
{
    [Serializable]
    public class PlayerSaveData
    {
        public long userId;
        public string nickName;
        public NoticeList noticesList;
        public int popLevelID;
    }

    [Serializable]
    public class NoticeList
    {
        public List<Notice> notices;
        public int version;
    }

    [Serializable]
    public class Notice
    {
        /**
         *  版本号
         */
        public int? version;

        /**
         * 公告id
         */
        public int id;

        /**
         * 公告内容（uri）
         */
        public string zhCnLink;

        public string enLink;

        public string deLink;

        public string frLink;

        public string esLink;

        public string ryLink;

        /**
         * 文本参数
         */
        public string para;

        /**
         *  公告图片
         */
        public string zhCnPic;

        public string enPic;

        public string dePic;

        public string frPic;

        public string esPic;

        public string ryPic;

        /**
         *  公告标题
         */
        public string zhCnTitle;

        public string enTitle;

        public string deTitle;

        public string frTitle;

        public string esTitle;

        public string ryTitle;

        /**
         *  公告内容
         */
        public string zhCnContent;

        public string enContent;

        public string deContent;

        public string frContent;

        public string esContent;

        public string ryContent;

        /**
         * 发件人
         */
        public string zhCnFromPerson;

        public string enFromPerson;

        public string deFromPerson;

        public string frFromPerson;

        public string esFromPerson;

        public string ryFromPerson;

        /**
         * 有效期（天数）
         */
        public int? valid;

        /**
         *  公告状态 0 失效 1有效
         */
        public int? noticeStatus;

        /**
         *  阅读状态 0 未读 1已读
         */
        public int? readStatus;

        /**
         * 初始化时间
         */
        public long? initTime;
    }
}