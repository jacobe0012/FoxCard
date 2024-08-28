using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

namespace XFramework
{
    /// <summary>
    /// �����Ҫ�ڼ��س���ʱ��ʾ����������̳������
    /// <para>�������߼���UILoading.cs</para>
    /// </summary>
    public abstract class LoadingScene : Scene, ILoading
    {
        private struct OnResourcesLoaded : IWaitType
        {
            public int Error { get; set; }
        }

        public virtual void GetObjects(ICollection<string> objKeys)
        {
        }

        public float SceneProgress()
        {
            return SceneResManager.Progress(this.sceneObject);
        }

        protected sealed override async UniTask WaitForCompleted()
        {
            var tag = this.TagId;
            var ui = await UIHelper.CreateAsync<ILoading>(UIType.UILoading, this); // ��UILoading
            var ret = await ((IWaitObject)ui).Wait<OnResourcesLoaded>(); // ��ʾ���������ȴ���Դ�������
            if (tag != this.TagId)
                return;

            // ��� ret.Error == WaitTypeError.Destroy ��˵��UILoading���ͷŵ��ˣ�Ĭ�ϵ�������Դ������Ͼͻ��ͷŵ�UILoading
            if (ret.Error == WaitTypeError.Destroy)
            {
                this.isCompleted = true;
                this.OnCompleted();
            }
        }
    }
}