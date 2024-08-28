using UnityEngine;
using UnityEngine.Events;

namespace XFramework
{
    public abstract class XScrollRectComponent<T> : ScrollRectComponent<T> where T : XScrollRect
    {
        public UnityEvent OnEndDrag => this.Get().m_OnEndDrag;

        public UnityEvent<Vector2> OnDrag => this.Get().m_OnDrag;

        public UnityEvent OnBeginDrag => this.Get().m_OnBeginDrag;

        protected override void Destroy()
        {
            this.OnEndDrag.RemoveAllListeners();
            this.OnDrag.RemoveAllListeners();
            this.OnBeginDrag.RemoveAllListeners();
            base.Destroy();
        }
    }

    public class XScrollRectComponent : XScrollRectComponent<XScrollRect>
    {
    }

    public static class XScrollRectExtensions
    {
        public static XScrollRectComponent GetXScrollRect(this UI self)
        {
            return self.TakeComponent<XScrollRectComponent, XScrollRect>(true);
        }

        public static XScrollRectComponent GetXScrollRect(this UI self, string key)
        {
            var ui = self.GetFromKeyOrPath(key);
            return ui?.GetXScrollRect();
        }

        public static void MoveToChild(this XScrollRect self, int childIndex, float duration = 0.5f)
        {
            var child = self.transform.GetChild(childIndex);
            self.MoveToChild(child, duration);
        }

        public static void MoveToChild(this XScrollRect self, Transform child, float duration = 0.5f)
        {
            RectTransform item = child as RectTransform;
            XScrollRect scrollRect = self;
            RectTransform viewport = scrollRect.viewport ?? self.transform as RectTransform;
            RectTransform content = scrollRect.content;

            if (viewport == null || content == null)
            {
                Log.Error("MoveToPosition viewport or content is null!");
                return;
            }

            RectTransform rectTransform = scrollRect.GetComponent<RectTransform>();
            Vector3 itemCurrentLocalPostion =
                rectTransform.InverseTransformVector(self.ConvertLocalPosToWorldPos(item));
            Vector3 itemTargetLocalPos = rectTransform.InverseTransformVector(self.ConvertLocalPosToWorldPos(viewport));

            Vector3 diff = itemTargetLocalPos - itemCurrentLocalPostion;
            diff.z = 0.0f;

            var newNormalizedPosition = new Vector2(
                diff.x / (content.rect.width - viewport.rect.width),
                diff.y / (content.rect.height - viewport.rect.height)
            );

            newNormalizedPosition = scrollRect.normalizedPosition - newNormalizedPosition;

            newNormalizedPosition.x = Mathf.Clamp01(newNormalizedPosition.x);
            newNormalizedPosition.y = Mathf.Clamp01(newNormalizedPosition.y);

            DG.Tweening.DOTween.To(() => scrollRect.normalizedPosition, x => scrollRect.normalizedPosition = x,
                newNormalizedPosition, duration);
        }

        private static Vector3 ConvertLocalPosToWorldPos(this XScrollRect self, RectTransform target)
        {
            var pivotOffset = new Vector3(
                (0.5f - target.pivot.x) * target.rect.size.x,
                (0.5f - target.pivot.y) * target.rect.size.y,
                0f);

            var localPosition = target.localPosition + pivotOffset;

            return target.parent.TransformPoint(localPosition);
        }
    }
}