using System.Collections.Generic;


namespace HotFix_UI
{
    //所有黑桃+50筹码
    public class Buff_100 : Buff
    {
        public override void OnBuyCard()
        {
        }


        public override void OnDiscardCard(List<Card> cards)
        {
            base.OnDiscardCard(cards);
        }


        public override void OnPlayCard(List<Card> cards)
        {
            base.OnPlayCard(cards);
            foreach (var item in cards)
            {
                if (item.color == CardColor.Spades)
                {
                    PlayerSingleton.Instance.currentScoreAdd += 50;
                    //TODO:playerAnimation
                }
            }
        }

        public override void OnRefreshShop()
        {
        }


    }
}