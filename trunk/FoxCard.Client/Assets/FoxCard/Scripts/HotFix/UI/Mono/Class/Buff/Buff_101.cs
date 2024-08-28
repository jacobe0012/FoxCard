using System.Collections.Generic;


namespace HotFix_UI
{
    //所有卡牌都计入分数
    public class Buff_101 : Buff
    {
        public override void OnBuyCard()
        {
            base.OnBuyCard();
        }


        public override void OnDiscardCard(List<Card> cards)
        {
            base.OnDiscardCard(cards);
        }


        public override void OnPlayCard(List<Card> cards)
        {
            base.OnPlayCard(cards);
        }

        public override void OnRefreshShop()
        {
            base.OnRefreshShop();
        }


    }
}