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
	[UIEvent(UIType.UIPanel_Sign)]
    internal sealed class UIPanel_SignEvent : AUIEvent, IUILayer
    {
	    public override string Key => UIPathSet.UIPanel_Sign;

        public override bool IsFromPool => true;
		
		public override bool AllowManagement => true;
		
		public UILayer Layer => UILayer.Low;
		
        public override UI OnCreate()
        {
            return UI.Create<UIPanel_Sign>();
        }
    }

    public partial class UIPanel_Sign : UI, IAwake
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
