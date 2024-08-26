using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XFramework
{
    public abstract class AUIEvent
    {
        /// <summary>
        /// 可以加载出预制的路径
        /// </summary>
        public abstract string Key { get; }

        /// <summary>
        /// 生成的GameObject是否来自对象池
        /// </summary>
        public abstract bool IsFromPool { get; }

        /// <summary>
        /// 允许UIManager管理，注意->公共UI一般填false
        /// </summary>
        public abstract bool AllowManagement { get; }

        public abstract UI OnCreate();

        public virtual void OnRemove(UI ui)
        {

        }
    }

    public interface IUILayer
    {
        /// <summary>
        /// UI默认层级
        /// </summary>
        UILayer Layer { get; }
    }
}
