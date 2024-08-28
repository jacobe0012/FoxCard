//---------------------------------------------------------------------
// Author: xxx
// Time: #CreateTime#
//---------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Cysharp.Threading.Tasks;
using HotFix_UI;
using UnityEngine;
using UnityEngine.UI;

namespace XFramework
{
	[UIEvent(UIType.UICard)]
	internal sealed class UICardEvent : AUIEvent
	{
		public override string Key => UIPathSet.UICard;

		public override bool IsFromPool => true;

		public override bool AllowManagement => false;

		// 此UI是不受UIManager管理的

		public override UI OnCreate()
		{
			return UI.Create<UICard>();
		}
	}

	public partial class UICard : UI, IAwake<Card>
	{
		public Card card;
		public bool isSelected;
		public void Initialize(Card card)
		{
			this.card = card;
			var KBtn_Card = GetFromReference(UICard.KBtn_Card);
			var KBg_Mask = GetFromReference(UICard.KBg_Mask);
			var KText_Points = GetFromReference(UICard.KText_Points);
			var KText_Points1 = GetFromReference(UICard.KText_Points1);
			var KImg_Top = GetFromReference(UICard.KImg_Top);
			var KImg_Top1 = GetFromReference(UICard.KImg_Top1);
			var KImg_Mid = GetFromReference(UICard.KImg_Mid);
			switch (card.color)
			{
				case CardColor.None:
					break;
				case CardColor.Hearts:
					KImg_Top.GetImage().SetSpriteAsync("icon_redheart", false).Forget();
					KImg_Top1.GetImage().SetSpriteAsync("icon_redheart", false).Forget();
					break;
				case CardColor.Diamonds:
					KImg_Top.GetImage().SetSpriteAsync("icon_fangpian", false).Forget();
					KImg_Top1.GetImage().SetSpriteAsync("icon_fangpian", false).Forget();
					break;
				case CardColor.Clubs:
					KImg_Top.GetImage().SetSpriteAsync("icon_meihua", false).Forget();
					KImg_Top1.GetImage().SetSpriteAsync("icon_meihua", false).Forget();
					break;
				case CardColor.Spades:
					KImg_Top.GetImage().SetSpriteAsync("icon_blackheart", false).Forget();
					KImg_Top1.GetImage().SetSpriteAsync("icon_blackheart", false).Forget();
					break;
				case CardColor.Any:
					break;
				default:
					break;

			}
			var point = ((int)card.points).ToString();
			switch (card.points)
			{
				case Points.Jack:
					point = "J";
					break;
				case Points.Queen:
					point = "Q";
					break;
				case Points.King:
					point = "K";
					break;
				case Points.Ace:
					point = "A";
					break;
			}
			KText_Points.GetTextMeshPro().SetTMPText(point);
			KText_Points1.GetTextMeshPro().SetTMPText(point);


			//DoTweenEffect.DoScaleTweenOnClickAndLongPress(KBtn_Card, () => OnBtnCardClick(card));
		}

		private void OnBtnCardClick(Card card)
		{
			if (JiYuUIHelper.TryGetUI(UIType.UIPanelInGame, out UI ui))
			{
				var panelInGame = ui as UIPanelInGame;
				panelInGame.currentSelectedCards.Add(card);
			}
		}


		protected override void OnClose()
		{
			base.OnClose();
		}
	}
}
