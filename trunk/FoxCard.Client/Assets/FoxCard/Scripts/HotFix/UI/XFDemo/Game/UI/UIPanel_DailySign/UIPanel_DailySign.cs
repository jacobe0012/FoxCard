//---------------------------------------------------------------------
// Author: xxx
// Time: #CreateTime#
//---------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using HotFix_UI;
using MessagePack;
using UnityEngine;
using UnityEngine.UI;

namespace XFramework
{
	[UIEvent(UIType.UIPanel_DailySign)]
    internal sealed class UIPanel_DailySignEvent : AUIEvent, IUILayer
    {
	    public override string Key => UIPathSet.UIPanel_DailySign;

        public override bool IsFromPool => true;
		
		public override bool AllowManagement => true;
		
		public UILayer Layer => UILayer.Low;
		
        public override UI OnCreate()
        {
            return UI.Create<UIPanel_DailySign>();
        }
    }

    public partial class UIPanel_DailySign : UI, IAwake
	{	
		public void Initialize()
		{
			InitBtn();
			

		}

		private void InitBtn()
		{
			DoTweenEffect.DoScaleTweenOnClickAndLongPress(this.GetFromReference(KButton_Close),Close);
			this.GetButton(Kmask)?.OnClick.Add(()=>Close());
			DoTweenEffect.DoScaleTweenOnClickAndLongPress(this.GetFromReference(KButton_Get),GetReward);
		}

		private void GetReward()
		{
			WebMessageHandler.Instance.AddHandler(CMD.DAILYSIGN,OnResponseRewardGet);
			NetWorkManager.Instance.SendMessage(CMD.DAILYSIGN);
		}

		private async void OnResponseRewardGet(object sender, WebMessageHandler.Execute e)
		{
			WebMessageHandler.Instance.RemoveHandler(CMD.DAILYSIGN,OnResponseRewardGet);
			var message= MessagePackSerializer.Deserialize<Rewards>(e.data);
			if(message==null) return;

			var parentList= GetFromReference(KContainerReward).GetList();
			parentList.Clear();
			var rewards= message.rewards;
			for (int i = 0; i < rewards.Count; i++)
			{
				var reward=rewards[i];
				//var item= await parentList.CreateWithUITypeAsync(UIType.UICommonItem,false);
				Log.Debug(reward.ToString(),Color.cyan);
				//item.GetFromReference(UICommonItem.KItemIcon).get
			}
			
		}


		
		protected override void OnClose()
		{
			base.OnClose();
		}
	}
}
