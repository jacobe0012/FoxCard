namespace HotFix_UI
{
    /// <summary>
    /// Hearts: 红心
    /// Diamonds: 方块
    /// Clubs: 梅花
    /// Spades: 黑桃
    /// None:无花色 eg 石头牌
    /// any 任意花色
    /// </summary>
    public enum CardColor
    {
        None = 0,
        Hearts = 1,
        Diamonds = 2,
        Clubs = 3,
        Spades = 4,
        Any = 5
    }

    public enum Points
    {
        None = 0,
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10,
        Jack = 11,
        Queen = 12,
        King = 13,
        Ace = 14
    }

    /// <summary>
    /// 卡牌
    /// </summary>
    public struct Card
    {
        public int id;
        /// <summary>
        /// 卡牌点数
        /// </summary>
        public Points points;
        /// <summary>
        /// 卡牌颜色
        /// </summary>
        public CardColor color;
        /// <summary>
        /// 卡牌增益效果
        /// </summary>
        public CardGain cardGain;
        // public int points;
        // public int points;
        // public int points;
    }

    // public struct Card
    // {
    //     public int points;
    //     
    // }
    /// <summary>
    /// 卡牌增益效果
    /// </summary>
    public struct CardGain
    {
        //增强
        public int enhance;

        //版本
        public int version;

        //封蜡
        public int wax;

        // public int points;
        // public int points;
        // public int points;
    }
}