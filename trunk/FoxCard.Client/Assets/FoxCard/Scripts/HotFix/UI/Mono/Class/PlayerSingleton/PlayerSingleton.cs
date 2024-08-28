using System;
using System.Collections.Generic;
using cfg.config;
using XFramework;

namespace HotFix_UI
{
    public sealed class PlayerSingleton : Singleton<PlayerSingleton>, IDisposable
    {
        //玩家所持所有牌组
        public List<Card> allCards = new List<Card>();

        //玩家所持所有buff牌
        public List<Buff> allBuffs = new List<Buff>();

        //玩家所持所有牌型状态
        public Dictionary<CardGroupType, CardGroup> allCardGroups = new Dictionary<CardGroupType, CardGroup>();

        /// <summary>
        /// 手牌容量
        /// </summary>
        public int handSize;

        /// <summary>
        /// 同花顺子判断数量
        /// </summary>
        public int flushSize = 5;

        /// <summary>
        /// 顺子判断间隔 1：只有1  2：可1可2
        /// </summary>
        public int straightInternal = 1;

        public float currentScoreAdd;

        public float currentScoreMul;

        public void Init()
        {
            Tbcard_group tbcard_Group = ConfigManager.Instance.Tables.Tbcard_group;
            handSize = 8;

            foreach (Points point in Enum.GetValues(typeof(Points)))
            {
                if (point == Points.None)
                    continue;
                foreach (CardColor color in Enum.GetValues(typeof(CardColor)))
                {
                    if (color == CardColor.None || color == CardColor.Any)
                        continue;
                    allCards.Add(new Card()
                    {
                        points = point,
                        color = color,
                        cardGain = default
                    });
                }
            }

            for (int i = 0; i < allCards.Count; i++)
            {
                var temp = allCards[i];
                temp.id = i + 1;
                allCards[i] = temp;
            }


            for (int i = 0; i < tbcard_Group.DataList.Count; i++)
            {
                var cardGroup = tbcard_Group.DataList[i];
                allCardGroups.Add((CardGroupType)cardGroup.id, new CardGroup()
                {
                    type = (CardGroupType)cardGroup.id,
                    level = 1,
                    scoreAdd = cardGroup.initAdd,
                    scoreMul = cardGroup.initMul,
                });
            }


            allBuffs.Add(new Buff_100());
            allBuffs.Add(new Buff_101());
        }

        public void Dispose()
        {
            Instance = null;
        }
    }
}