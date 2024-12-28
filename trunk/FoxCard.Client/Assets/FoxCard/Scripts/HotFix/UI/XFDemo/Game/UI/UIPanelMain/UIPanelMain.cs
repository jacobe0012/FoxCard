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
	[UIEvent(UIType.UIPanelMain)]
    internal sealed class UIPanelMainEvent : AUIEvent, IUILayer
    {
	    public override string Key => UIPathSet.UIPanelMain;

        public override bool IsFromPool => true;
		
		public override bool AllowManagement => true;
		
		public UILayer Layer => UILayer.Low;
		
        public override UI OnCreate()
        {
            return UI.Create<UIPanelMain>();
        }
    }

    public partial class UIPanelMain : UI, IAwake
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
