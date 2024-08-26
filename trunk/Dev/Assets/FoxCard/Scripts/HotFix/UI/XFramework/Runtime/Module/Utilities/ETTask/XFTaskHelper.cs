using System;
using System.Collections.Generic;

namespace XFramework
{
    public static class XFTaskHelper
    {
        public static bool IsCancel(this XFCancellationToken self)
        {
            if (self == null)
            {
                return false;
            }

            return self.IsDispose();
        }

        private class CoroutineBlocker
        {
            private int count;

            private XFTask tcs;

            public CoroutineBlocker(int count)
            {
                this.count = count;
            }

            public async XFTask RunSubCoroutineAsync(XFTask task)
            {
                try
                {
                    await task;
                }
                finally
                {
                    --this.count;

                    if (this.count <= 0 && this.tcs != null)
                    {
                        XFTask t = this.tcs;
                        this.tcs = null;
                        t.SetResult();
                    }
                }
            }

            public async XFTask WaitAsync()
            {
                if (this.count <= 0)
                {
                    return;
                }

                this.tcs = XFTask.Create(true);
                await tcs;
            }
        }

        public static async XFTask WaitAny(List<XFTask> tasks)
        {
            if (tasks.Count == 0)
            {
                return;
            }

            CoroutineBlocker coroutineBlocker = new CoroutineBlocker(1);

            foreach (XFTask task in tasks)
            {
                coroutineBlocker.RunSubCoroutineAsync(task).Coroutine();
            }

            await coroutineBlocker.WaitAsync();
        }

        public static async XFTask WaitAny(XFTask[] tasks)
        {
            if (tasks.Length == 0)
            {
                return;
            }

            CoroutineBlocker coroutineBlocker = new CoroutineBlocker(1);

            foreach (XFTask task in tasks)
            {
                coroutineBlocker.RunSubCoroutineAsync(task).Coroutine();
            }

            await coroutineBlocker.WaitAsync();
        }

        public static async XFTask WaitAll(XFTask[] tasks)
        {
            if (tasks.Length == 0)
            {
                return;
            }

            CoroutineBlocker coroutineBlocker = new CoroutineBlocker(tasks.Length);

            foreach (XFTask task in tasks)
            {
                coroutineBlocker.RunSubCoroutineAsync(task).Coroutine();
            }

            await coroutineBlocker.WaitAsync();
        }

        public static async XFTask WaitAll(List<XFTask> tasks)
        {
            if (tasks.Count == 0)
            {
                return;
            }

            CoroutineBlocker coroutineBlocker = new CoroutineBlocker(tasks.Count);

            foreach (XFTask task in tasks)
            {
                coroutineBlocker.RunSubCoroutineAsync(task).Coroutine();
            }

            await coroutineBlocker.WaitAsync();
        }
    }
}