namespace HotFix_UI
{
    public static class CMD
    {
        //public const string LOGIN = "Login";

        public const string LOGIN = "Login";

        //0,1   登录
        //public const int LOGIN = (MainCmd.loginCmd << 16) + LoginCmd.loginVerify;

        //0, 2 切换账号
        public const int SWITCHACCOUNT = (MainCmd.loginCmd << 16) + LoginCmd.switchAccount;

        //1,1 
        public const int INITPLAYER = (MainCmd.playerCmd << 16) + PlayerCmd.initCacheRole;

        //10,1  查询背包      
        public const int OPENBAG = (MainCmd.bagCmd << 16) + BagCmd.findItem;

        //1,6
        public const int CHANGENAME = (MainCmd.playerCmd << 16) + PlayerCmd.rename;

        //1,5
        public const int CHANGESTATUS = (MainCmd.playerCmd << 16) + PlayerCmd.checkStatus;

        //1,8 查询当前关卡
        public const int QUERYLEVEL = (MainCmd.playerCmd << 16) + PlayerCmd.queryLevel;

        //2,3 请求战局id
        public const int REQUESTBATTLEID = (MainCmd.combatPowerCmd << 16) + CombatPowerCmd.requestBattleId;

        //2,5 验证对局是否可以开始
        public const int QUERYCANSTART = (MainCmd.combatPowerCmd << 16) + CombatPowerCmd.queryCanStart;

        //2,4 查询章节信息 精简
        public const int CHAPTERINFO = (MainCmd.combatPowerCmd << 16) + CombatPowerCmd.chapterInfo;

        //2,7 查询角色属性             
        public const int QUERYPROPERTY = (MainCmd.combatPowerCmd << 16) + CombatPowerCmd.playerProperty;

        //2,10查询解锁天赋
        public const int QUERYTALENT = (MainCmd.combatPowerCmd << 16) + CombatPowerCmd.queryTalent;

        //2,11 解锁天赋
        public const int LockTalent = (MainCmd.combatPowerCmd << 16) + TalentCmd.lockTatant;

        //2,12 
        public const int CHALLENGECLAIM = (MainCmd.combatPowerCmd << 16) + ChallengCmd.challengeClaim;

        //2,13
        public const int QUERYCHALLENGE = (MainCmd.combatPowerCmd << 16) + ChallengCmd.challengeQuery;

        //2,14 消耗复活币
        public const int CONSUMEREBIRTHCOIN = (MainCmd.combatPowerCmd << 16) + ChallengCmd.consumeRebirthCoin;

        //5,1   
        public const int QUERYMAIL = (MainCmd.mailSystemCmd << 16) + MailCmd.queryMail;

        //6,3
        public const int QUERYAUTOPATROL = (MainCmd.dropSystemCmd << 16) + PatrolCmd.queryAutoPatrol;

        //9,5查询所有装备
        public const int QUERYEQUIP = (MainCmd.equipCmd << 16) + EquipCmd.findEquip;

        //9,11查询所有已穿戴装备
        public const int QUERYWEAR = (MainCmd.equipCmd << 16) + EquipCmd.allwearEquip;

        //9,3升级 gameequip  gameequip
        public const int EQUIPUPGRADE = (MainCmd.equipCmd << 16) + EquipCmd.upgrade;

        //9,7一键升级 gameequip  gameequip
        public const int EQUIPALLUPGRADE = (MainCmd.equipCmd << 16) + EquipCmd.allUpgrade;

        //9,10穿戴  UID gameequip
        public const int EQUIPWEAR = (MainCmd.equipCmd << 16) + EquipCmd.wearEquip;

        //9,12卸下 UID gameequip
        public const int EQUIPUNWEAR = (MainCmd.equipCmd << 16) + EquipCmd.unwearEquip;

        //9,2降级 gameequip  gameequip
        public const int EQUIPDOWNGRADE = (MainCmd.equipCmd << 16) + EquipCmd.downgrade;

        //9,4降品 gameequip  ALLgameequip
        public const int EQUIPDOWNQUALITY = (MainCmd.equipCmd << 16) + EquipCmd.downQuality;

        //9,6合成  gameequipLIST  ALLgameequip
        public const int EQUIPCOMPOSE = (MainCmd.equipCmd << 16) + EquipCmd.compose;

        //9,1  一键合成    ALLgameequip
        public const int EQUIPALLCOMPOUND = (MainCmd.equipCmd << 16) + EquipCmd.allcompose;

        //11,1 查询商店信息
        public const int QUERYSHOP = (MainCmd.shopCmd << 16) + ShopCmd.queryShop;

        //13,1 修改settings
        public const int CHANGESETTINGS = (MainCmd.settings << 16) + SettingsCmd.changeSettings;

        //13,2 查询settings
        public const int QUERYSETTINGS = (MainCmd.settings << 16) + SettingsCmd.querySettings;

        //13,5  发送兑换码
        public const int SENDGIFTCODE = (MainCmd.settings << 16) + SettingsCmd.sendGiftCode;

        //13,6  查询版本号
        public const int QUERYVERSION = (MainCmd.settings << 16) + SettingsCmd.queryVersion;

        //18,1 查询怪物图鉴
        public const int QUERYMONSTERCOLLECTION = (MainCmd.monsterCollection << 16) + MonsterCollection.query;

        //18,2 领取怪物图鉴奖励
        public const int RECEIVECOLLECTIONREWARD = (MainCmd.monsterCollection << 16) + MonsterCollection.receive;

        //19,1 服务器当前时间戳
        public const int SERVERTIME = (MainCmd.timeCmd << 16) + TimeCmd.time;

        //19,2 服务器每天更新时间
        public const int UPDATETIME = (MainCmd.timeCmd << 16) + TimeCmd.updateTime;

        //14,1 解锁通行证下一等级
        public const int UNLOCKNEXTPASSLEVEL = (MainCmd.passCmd << 16) + PassCmd.unlock;

        //14,2 查询通行证信息
        public const int QUERYPASS = (MainCmd.passCmd << 16) + PassCmd.queryPass;

        //14,3 领取通行证奖励
        public const int GETPASSREWARD = (MainCmd.passCmd << 16) + PassCmd.getReward;

        //14,5 解锁通行证令牌
        public const int GETPASSTOKEN = (MainCmd.passCmd << 16) + PassCmd.buyToken;

        //14,6 查询通行证解锁时间
        public const int PASSTIME = (MainCmd.passCmd << 16) + PassCmd.time;

        //8,1 查询活动相关信息
        public const int QUERYACTIVITY = (MainCmd.activeSystemCmd << 16) + ActivityCmd.query;

        //8,2 通过活动id查询单个活动信息
        public const int QUERTSINGLEACTIVITY = (MainCmd.activeSystemCmd << 16) + ActivityCmd.querySingleActivity;

        //8,3  购买骰子
        public const int BUYDICE = (MainCmd.activeSystemCmd << 16) + ActivityCmd.buyDice;

        //8,4 大富翁兑换活动物品
        public const int MONOEXCHANGE = (MainCmd.activeSystemCmd << 16) + ActivityCmd.exchange;

        //8,5 大富翁行动
        public const int MONOPOLYACTION = (MainCmd.activeSystemCmd << 16) + ActivityCmd.monopolyAction;

        //8,6 兑换体力商店
        public const int ENERGYSHOP = (MainCmd.activeSystemCmd << 16) + ActivityCmd.energyShop;

        //8,7 查询活动任务
        public const int QUERYACTIVITYTASK = (MainCmd.activeSystemCmd << 16) + ActivityCmd.queryActivityTask;

        //3,1 查询任务
        public const int QUERYTASK = (MainCmd.taskSystemCmd << 16) + TaskCmd.query;

        //3,2 领取任务奖励
        public const int GETTASKSCORE = (MainCmd.taskSystemCmd << 16) + TaskCmd.getTask;

        //3,3 领取任务宝箱奖励
        public const int GETTASKBOX = (MainCmd.taskSystemCmd << 16) + TaskCmd.getBox;

        //3,4 查询成就
        public const int QUERYACHIEVE = (MainCmd.taskSystemCmd << 16) + TaskCmd.queryAchieve;

        //99,1 接收邮件广播
        public const int BOARDCASTMAIL = (MainCmd.boardCastCmd << 16) + BoardCastCmd.mail;

        //99-7 定时任务拉取标记(东八时区6.00)
        public const int BOARDCASTUPDATETASK = (MainCmd.boardCastCmd << 16) + BoardCastCmd.updateTask;

        //99,8 属性变更时广播
        public const int BOARDCASTUPDATEPROPERTY = (MainCmd.boardCastCmd << 16) + BoardCastCmd.updateProperty;

        private static class MainCmd
        {
            //登录模块

            public const int loginCmd = 0;


            //玩家资产模块

            public const int playerCmd = 1;


            //战力培养模块

            public const int combatPowerCmd = 2;


            //任务模块

            public const int taskSystemCmd = 3;


            //聊天模块

            public const int chatSystemCmd = 4;


            //邮件模块

            public const int mailSystemCmd = 5;


            //掉落模块

            public const int dropSystemCmd = 6;


            //外部系统模块

            public const int externalSystemCmd = 7;


            //活动系统模块

            public const int activeSystemCmd = 8;


            // 装备系统模块

            public const int equipCmd = 9;


            //充值系统接口

            public const int paymentSystem = 701;


            //背包模块

            public const int bagCmd = 10;

            public const int settings = 13;

            //怪物图鉴
            public const int monsterCollection = 18;

            //商店模块
            public const int shopCmd = 11;

            //时间模块
            public const int timeCmd = 19;

            //广播模块
            public const int boardCastCmd = 99;

            //通行证模块
            public const int passCmd = 14;
        }


        //子路由模块

        #region SubRoute

        private static class MonsterCollection
        {
            public const int query = 1;
            public const int receive = 2;
        }

        private static class SettingsCmd
        {
            public const int querySettings = 2;
            public const int changeSettings = 1;

            public const int sendGiftCode = 5;
            public const int queryVersion = 6;
        }

        private static class BoardCastCmd
        {
            public const int mail = 1;
            public const int updateTask = 7;
            public const int updateProperty = 7;
        }

        private static class CombatPowerCmd
        {
            public const int chapterInfo = 4;
            public const int playerProperty = 7;
            public const int queryTalent = 10;

            public const int requestBattleId = 3;
            public const int queryCanStart = 5;
        }

        private static class BagCmd
        {
            public const int findItem = 1;

            public const int saveItem = 2;

            public const int deleteItem = 3;
        }

        private static class EquipCmd
        {
            public const int allcompose = 1;

            public const int downgrade = 2;

            public const int upgrade = 3;

            public const int downQuality = 4;

            public const int findEquip = 5;

            public const int compose = 6;

            public const int allUpgrade = 7;

            public const int insertEquip = 9;

            public const int wearEquip = 10;

            public const int allwearEquip = 11;

            public const int unwearEquip = 12;
        }

        //登录验证
        private static class LoginCmd
        {
            public const int loginVerify = 1;
            public const int switchAccount = 2;
        }

        private static class PlayerCmd
        {
            public const int initCacheRole = 1;
            public const int editCacheRole = 2;
            public const int findCacheRole = 3;
            public const int checkStatus = 5;
            public const int queryLevel = 8;
            public const int rename = 6;
        }


        private static class TalentCmd
        {
            public const int lockTatant = 11;
        }

        private static class ChallengCmd
        {
            public const int challengeClaim = 12;
            public const int challengeQuery = 13;
            public const int consumeRebirthCoin = 14;
        }

        private static class MailCmd
        {
            public const int queryMail = 1;
        }

        private static class PatrolCmd
        {
            public const int queryAutoPatrol = 3;
        }

        private static class ShopCmd
        {
            public const int queryShop = 10;
        }

        private static class TimeCmd
        {
            public const int time = 1;
            public const int updateTime = 2;
        }

        private static class PassCmd
        {
            public const int unlock = 1;
            public const int queryPass = 2;
            public const int getReward = 3;
            public const int buyToken = 5;
            public const int time = 6;
        }

        private static class ActivityCmd
        {
            public const int query = 1;
            public const int querySingleActivity = 2;
            public const int buyDice = 3;
            public const int exchange = 4;
            public const int monopolyAction = 5;
            public const int energyShop = 6;
            public const int queryActivityTask = 7;
        }

        private static class TaskCmd
        {
            public const int query = 1;
            public const int getTask = 2;
            public const int getBox = 3;
            public const int queryAchieve = 4;
        }

        #endregion
    }
}