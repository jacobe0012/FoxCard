using System;
using System.Collections.Generic;
using System.Linq;
using XFramework;

namespace HotFix_UI
{
    //高牌，一对，两对，三条，顺子，同花，四条，同花顺，皇家同花顺，葫芦。五条,同花五条,同花葫芦

    //High card, One pair, Two pair, Three of a kind, Straight, Flush, Four of a kind, Straight Flush, Royal Flush, Full House. 


    public enum CardGroupType
    {
        /// <summary>
        /// //高牌
        /// </summary>
        HighCard,


        /// <summary>
        ///  //一对
        /// </summary>
        OnePair,

        /// <summary>
        ///   //两对
        /// </summary>
        TwoPair,

        /// <summary>
        /// 三条
        /// </summary>
        ThreeOfAKind,

        /// <summary>
        ///  //顺子
        /// </summary>
        Straight,

        /// <summary>
        ///  //同花
        /// </summary>
        Flush,

        /// <summary>
        ///   //葫芦
        /// </summary>
        FullHouse,

        /// <summary>
        ///   //四条
        /// </summary>
        FourOfAKind,

        /// <summary>
        ///   //同花顺
        /// </summary>
        StraightFlush,

        /// <summary>
        ///   //皇家同花顺
        /// </summary>
        RoyalFlush,

        /// <summary>
        /// //五条
        /// </summary>
        FiveOfAKind,

        /// <summary>
        /// //同花五条
        /// </summary>
        FiveOfAKindFlush,

        /// <summary>
        /// //同花葫芦
        /// </summary>
        FullHouseFlush,
    }

    /// <summary>
    /// 牌组
    /// </summary>
    public struct CardGroup
    {
        public CardGroupType type;
        public int level;

        public int scoreAdd;
        public int scoreMul;
    }
}