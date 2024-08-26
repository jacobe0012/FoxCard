using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XFramework
{
    public class CommonObject : XObject
    {
        protected virtual void Init()
        {

        }

        protected virtual void Destroy()
        {

        }

        protected sealed override void OnStart()
        {
            base.OnStart();
            Common.Instance.Add(this);
            Init();
        }

        protected sealed override void OnDestroy()
        {
            Destroy();
            Common.Instance.Remove(this);
        }
    }
}
