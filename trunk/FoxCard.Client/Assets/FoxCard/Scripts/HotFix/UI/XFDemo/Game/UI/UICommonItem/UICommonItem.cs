//---------------------------------------------------------------------
// Author: xxx
// Time: #CreateTime#
//---------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace XFramework
{
	[UIEvent(UIType.UICommonItem)]
    internal sealed class UICommonItemEvent : AUIEvent
    {
	    public override string Key => UIPathSet.UICommonItem;

        public override bool IsFromPool => true;
		
		public override bool AllowManagement => false;
		
		// 此UI是不受UIManager管理的
		
        public override UI OnCreate()
        {
            return UI.Create<UICommonItem>();
        }
    }

    public partial class UICommonItem : UI, IAwake
	{	
		public void Initialize()
		{
			
		}
		
		protected override void OnClose()
		{
			base.OnClose();
		}
	}
}
