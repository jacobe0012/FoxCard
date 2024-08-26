using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XFramework
{
    public class UIEventAttribute : BaseAttribute
    {
        public string UIType { get; private set; }

        public UIEventAttribute(string uiType)
        {
            UIType = uiType;
        }
    }
}
