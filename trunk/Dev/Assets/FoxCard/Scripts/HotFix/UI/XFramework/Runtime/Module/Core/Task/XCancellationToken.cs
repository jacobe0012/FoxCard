using System;
using System.Collections.Generic;

namespace XFramework
{
    public class XCancellationToken
    {
        private HashSet<Action> actions = new HashSet<Action>();

        public void Register(Action action)
        {
            actions.Add(action);
        }

        public void Remove(Action action)
        {
            actions?.Remove(action);
        }

        public bool IsCancel()
        {
            return actions == null;
        }

        public void Cancel()
        {
            if (actions == null)
                return;

            Invoke();
        }

        private void Invoke()
        {
            HashSet<Action> list = actions;
            actions = null;
            try
            {
                foreach (Action action in list)
                {
                    action?.Invoke();
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }
    }
}