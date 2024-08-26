namespace XFramework
{
    public class UIChild : UI, IUID
    {
        /// <summary>
        /// 这个id一旦赋值不为0会提供给UIList使用，然后可以使用<see cref="UIListComponent.GetChildById(long)"/>获取到
        /// </summary>
        private long m_Id = 0;

        public long Id => m_Id;

        protected void SetId(long id)
        {
            if (id == 0)
            {
                Log.Error("UIChild不能设置id为0");
                return;
            }

            if (this.m_Id == id)
            {
                return;
            }

            long beforeId = this.m_Id;
            this.m_Id = id;

            this.Parent?.Publish(new UIEventType.OnChildIdChanged
            {
                Target = this,
                BeforeId = beforeId,
                AfterId = this.m_Id
            });
        }

        protected override void OnClose()
        {
            this.m_Id = 0;
            base.OnClose();
        }
    }
}
