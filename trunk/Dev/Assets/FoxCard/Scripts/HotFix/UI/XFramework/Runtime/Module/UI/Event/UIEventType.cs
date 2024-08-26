using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XFramework.UIEventType
{
    /// <summary>
    /// 当子对象的索引改变
    /// </summary>
    public struct OnChildSiblingChanged
    {
        public UI Target { get; set; }

        public int BeforeIndex { get; set; }

        public int AfterIndex { get; set; }
    }

    /// <summary>
    /// 当子对象的唯一id改变
    /// </summary>
    public struct OnChildIdChanged
    {
        public UI Target { get; set; }

        public long BeforeId { get; set; }

        public long AfterId { get; set; }
    }
}
