using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XFramework
{
    public class RedDotNameAttribute : BaseAttribute
    {
        /// <summary>
        /// 短名
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 全名，中间用.间隔
        /// </summary>
        public string FullName { get; private set; }

        /// <summary>
        /// 红点名称
        /// </summary>
        /// <param name="fullName">全名，中间用.间隔</param>
        public RedDotNameAttribute(string fullName)
        {
            FullName = fullName.Trim();
            if (FullName.IsNullOrEmpty())
                return;

            var index = FullName.LastIndexOf('.');
            if (index >= 0 && index + 1 < fullName.Length)
                Name = FullName.Substring(index + 1);
            else
                Name = fullName;
        }
    }
}
