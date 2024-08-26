using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XFramework
{
    public class LanguageComponent : UComponent<UIReference>
    {
        protected override void EndInitialize()
        {
            base.EndInitialize();
            var reference = this.Get();
            foreach (var obj in reference.GetAllText())
            {
                if (obj is IMultilingual multilingual)
                    multilingual.RefreshText();
            }
        }
    }
}
