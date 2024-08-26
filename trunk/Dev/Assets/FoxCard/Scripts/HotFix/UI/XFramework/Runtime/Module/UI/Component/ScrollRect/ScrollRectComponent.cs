using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace XFramework
{
    public abstract class ScrollRectComponent<T> : UIBehaviourComponent<T> where T : ScrollRect
    {
        protected UI content;

        public UI Content => content;

        public UnityEvent<Vector2> OnValueChanged => this.Get().onValueChanged;

        protected override void SetParentAfter()
        {
            if (this.content is null)
            {
                if (this.Get() != null && this.Get().content != null)
                {
                    this.content = this.parent.AddChild("Content", this.Get().content.gameObject, true);
                }
            }
            else
            {
                this.parent.AddChild(this.content);
            }

            base.SetParentAfter();
        }

        protected override void Destroy()
        {
            this.OnValueChanged.RemoveAllListeners();
            this.content = null;
            base.Destroy();
        }

        public void SetVerticalNormalizedPosition(float value)
        {
            this.Get().verticalNormalizedPosition = value;
        }

        public void SetHorizontalNormalizedPosition(float value)
        {
            this.Get().horizontalNormalizedPosition = value;
        }

        /// <summary>
        /// 移动到child的位置
        /// </summary>
        /// <param name="child"></param>
        /// <param name="duration"></param>
        public void MoveToChild(Transform child, float duration = 0.5f)
        {
            this.Get().MoveToChild(child, duration);
        }

        /// <summary>
        /// 移动到所处childIndex的子对象的位置
        /// </summary>
        /// <param name="childIndex"></param>
        /// <param name="duration"></param>
        public void MoveToChild(int childIndex, float duration = 0.5f)
        {
            this.Get().MoveToChild(childIndex, duration);
        }
    }

    public class ScrollRectComponent : ScrollRectComponent<ScrollRect>
    {
        
    }

    public static class ScrollRectExtensions
    {
        public static ScrollRectComponent GetScrollRect(this UI self)
        {
            return self.TakeComponent<ScrollRectComponent, ScrollRect>(true);
        }

        public static ScrollRectComponent GetScrollRect(this UI self, string key)
        {
            var ui = self.GetFromKeyOrPath(key);
            return ui?.GetScrollRect();
        }

        public static void MoveToChild(this ScrollRect self, int childIndex, float duration = 0.5f)
        {
            var child = self.transform.GetChild(childIndex);
            self.MoveToChild(child, duration);
        }

        public static void MoveToChild(this ScrollRect self, Transform child, float duration = 0.5f)
        {
            RectTransform item = child as RectTransform;
            ScrollRect scrollRect = self;
            RectTransform viewport = scrollRect.viewport ?? self.transform as RectTransform;
            RectTransform content = scrollRect.content;

            if (viewport == null || content == null)
            {
                Log.Error("MoveToPosition viewport or content is null!");
                return;
            }

            RectTransform rectTransform = scrollRect.GetComponent<RectTransform>();
            Vector3 itemCurrentLocalPostion = rectTransform.InverseTransformVector(self.ConvertLocalPosToWorldPos(item));
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

            DG.Tweening.DOTween.To(() => scrollRect.normalizedPosition, x => scrollRect.normalizedPosition = x, newNormalizedPosition, duration);
        }

        private static Vector3 ConvertLocalPosToWorldPos(this ScrollRect self, RectTransform target)
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
