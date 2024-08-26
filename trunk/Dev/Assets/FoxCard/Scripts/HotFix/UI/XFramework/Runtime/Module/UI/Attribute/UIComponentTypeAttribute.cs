using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XFramework
{
    /// <summary>
    /// 给UIComponent类加上这个特性，才能正确的获取到Untiy组件
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class UIComponentFlagAttribute : BaseAttribute
    {

        public UIComponentFlagAttribute()
        {
            
        }
    }
}
