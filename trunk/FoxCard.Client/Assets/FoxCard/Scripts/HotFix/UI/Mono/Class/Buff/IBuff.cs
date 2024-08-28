using System.Collections.Generic;

namespace HotFix_UI
{
    //回合开始时,每张卡牌计分时,重新触发打出的牌时,重新触发留在手牌中的牌时,计算总分时,点击弃牌时,售出牌时,选择盲注时,添加卡牌时,
    interface IBuff
    {
        /// <summary>
        /// 回合开始时
        /// </summary>

        void OnRoundStart();

        /// <summary>
        /// 每张卡牌计分时
        /// </summary>
        /// <param name="cards"></param>
        void OnTriggerScoringCard(List<Card> cards);

        /// <summary>
        /// 重新触发打出的牌时
        /// </summary>
        /// <param name="cards"></param>
        void OnAfterTriggerScoringCard(List<Card> cards);

        /// <summary>
        /// 重新触发留在手牌中的牌时
        /// </summary>
        /// <param name="cards"></param>
        void OnTriggerHandCard(List<Card> cards);


        /// <summary>
        /// 计算总分时
        /// </summary>
        /// <param name="cards"></param>
        void OnCalTotalScore(List<Card> cards);



        /// <summary>
        /// 弃牌时
        /// </summary>
        /// <param name="cards"></param>
        void OnDiscardCard(List<Card> cards);


        /// <summary>
        /// 售出牌时
        /// </summary>
        /// <param name="cards"></param>
        void OnSellCard(List<Card> cards);

        /// <summary>
        /// 添加卡牌时
        /// </summary>
        /// <param name="cards"></param>
        void OnAddCard(List<Card> cards);

        /// <summary>
        /// 刷新商店时
        /// </summary>
        /// <param name="cards"></param>
        void OnRefreshShop(List<Card> cards);



    }
}