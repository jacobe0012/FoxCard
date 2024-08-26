using System.Collections.Generic;
using XFramework;

namespace HotFix_UI
{
    public class Buff : IBuff
    {
        public void OnAddCard(List<Card> cards)
        {
            throw new System.NotImplementedException();
        }

        public void OnAfterTriggerScoringCard(List<Card> cards)
        {
            throw new System.NotImplementedException();
        }

        public virtual void OnBuyCard()
        {
            Log.Debug($"Base OnBuyCard");
        }

        public void OnCalTotalScore(List<Card> cards)
        {
            throw new System.NotImplementedException();
        }

        public virtual void OnDiscardCard(List<Card> cards)
        {
            Log.Debug($"Base OnDiscardCard");
        }

        public virtual void OnPlayCard(List<Card> cards)
        {
            Log.Debug($"Base OnPlayCard");
        }


        public virtual void OnRefreshShop()
        {
            Log.Debug($"Base OnRefreshShop");
        }

        public void OnRefreshShop(List<Card> cards)
        {
            throw new System.NotImplementedException();
        }

        public void OnRoundStart()
        {
            throw new System.NotImplementedException();
        }

        public void OnSellCard(List<Card> cards)
        {
            throw new System.NotImplementedException();
        }

        public void OnTriggerHandCard(List<Card> cards)
        {
            throw new System.NotImplementedException();
        }

        public void OnTriggerScoringCard(List<Card> cards)
        {
            throw new System.NotImplementedException();
        }
    }
}