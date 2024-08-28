using System;
using System.Collections.Generic;
using UnityEngine;

namespace XFramework
{
    public partial class RectTransformComponent
    {
        #region Transform

        public Vector2 Scale2()
        {
            return this.Get().Scale2();
        }

        public Vector3 Scale()
        {
            return this.Get().Scale();
        }

        public Vector2 LocalPosition2()
        {
            return this.Get().LocalPosition2();
        }

        public Vector3 LocalPosition()
        {
            return this.Get().LocalPosition();
        }

        public Vector2 Position2()
        {
            return this.Get().Position2();
        }

        public Vector3 Position()
        {
            return this.Get().Position();
        }

        public Quaternion Rotation()
        {
            return this.Get().Rotation();
        }

        public Quaternion LocalRotation()
        {
            return this.Get().LocalRotation();
        }

        public void SetScaleX(float x)
        {
            this.Get().SetScaleX(x);
        }

        public void SetScaleY(float y)
        {
            this.Get().SetScaleY(y);
        }

        public void SetScaleZ(float z)
        {
            this.Get().SetScaleZ(z);
        }

        public void SetScale2(float scale)
        {
            this.Get().SetScale2(scale);
        }

        public void SetScale3(float scale)
        {
            this.Get().SetScale3(scale);
        }

        public void SetScale(Vector2 scale)
        {
            this.Get().SetScale(scale);
        }

        public void SetScale(Vector3 scale)
        {
            this.Get().SetScale(scale);
        }

        public void SetLocalPosition(Vector3 local)
        {
            this.Get().SetLocalPosition(local);
        }

        public void SetLocalPositionX(float x)
        {
            this.Get().SetLocalPositionX(x);
        }

        public void SetLocalPositionY(float y)
        {
            this.Get().SetLocalPositionY(y);
        }

        public void SetLocalPositionZ(float z)
        {
            this.Get().SetLocalPositionZ(z);
        }

        public void SetPosition(Vector3 pos)
        {
            this.Get().SetPosition(pos);
        }

        public void SetPositionX(float x)
        {
            this.Get().SetPositionX(x);
        }

        public void SetPositionY(float y)
        {
            this.Get().SetPositionY(y);
        }

        public void SetPositionZ(float z)
        {
            this.Get().SetPositionZ(z);
        }

        public void SetRotation(Quaternion rotation)
        {
            this.Get().SetRotation(rotation);
        }

        public void SetLocalRotation(Quaternion rotation)
        {
            this.Get().SetLocalRotation(rotation);
        }

        public void SetEulerAnglesX(float x)
        {
            this.Get().SetEulerAnglesX(x);
        }

        public void SetEulerAnglesY(float y)
        {
            this.Get().SetEulerAnglesY(y);
        }

        public void SetEulerAnglesZ(float z)
        {
            this.Get().SetEulerAnglesZ(z);
        }

        public void SetLocalEulerAnglesX(float x)
        {
            this.Get().SetLocalEulerAnglesX(x);
        }

        public void SetLocalEulerAnglesY(float y)
        {
            this.Get().SetLocalEulerAnglesY(y);
        }

        public void SetLocalEulerAnglesZ(float z)
        {
            this.Get().SetLocalEulerAnglesZ(z);
        }

        #endregion

        #region RectTransform

        public Vector2 AnchoredPosition()
        {
            return this.Get().AnchoredPosition();
        }

        public Vector3 AnchoredPosition3D()
        {
            return this.Get().AnchoredPosition3D();
        }

        public Vector2 AnchorMin()
        {
            return this.Get().AnchorMin();
        }

        public Vector2 AnchorMax()
        {
            return this.Get().AnchorMax();
        }

        public Vector2 Pivot()
        {
            return this.Get().Pivot();
        }

        public Vector2 OffsetMin()
        {
            return this.Get().OffsetMin();
        }

        public Vector2 OffsetMax()
        {
            return this.Get().OffsetMax();
        }

        public float Width()
        {
            return this.Get().Width();
        }

        public float Height()
        {
            return this.Get().Height();
        }

        public void GetLocalCorners(List<Vector3> fourCornersArray)
        {
            this.Get().GetLocalCorners(fourCornersArray);
        }

        public void GetWorldCorners(List<Vector3> fourCornersArray)
        {
            this.Get().GetWorldCorners(fourCornersArray);
        }

        public void GetLocalCorners(Vector3[] fourCornersArray)
        {
            this.Get().GetLocalCorners(fourCornersArray);
        }

        public void GetWorldCorners(Vector3[] fourCornersArray)
        {
            this.Get().GetWorldCorners(fourCornersArray);
        }

        public void SetSize(Vector2 size)
        {
            this.Get().SetSize(size);
        }

        public void SetSize(float width, float height)
        {
            this.Get().SetSize(width, height);
        }

        public void SetWidth(float width)
        {
            this.Get().SetWidth(width);
        }

        public void SetHeight(float height)
        {
            this.Get().SetHeight(height);
        }

        public void SetAnchoredPosition(Vector2 pos)
        {
            this.Get().SetAnchoredPosition(pos);
        }

        public void SetAnchoredPosition3D(Vector2 pos)
        {
            this.Get().SetAnchoredPosition3D(pos);
        }

        public void SetAnchoredPosition3D(Vector3 pos)
        {
            this.Get().SetAnchoredPosition3D(pos);
        }

        public void SetAnchoredPositionZToZero()
        {
            this.Get().SetAnchoredPositionZToZero();
        }

        public void SetAnchoredPosition(float x, float y)
        {
            this.Get().SetAnchoredPosition(x, y);
        }

        public void SetAnchoredPosition3D(float x, float y, float z)
        {
            this.Get().SetAnchoredPosition3D(x, y, z);
        }

        public void SetAnchoredPositionX(float x)
        {
            this.Get().SetAnchoredPositionX(x);
        }

        public void SetAnchoredPositionY(float y)
        {
            this.Get().SetAnchoredPositionY(y);
        }

        public void SetAnchoredPositionZ(float z)
        {
            this.Get().SetAnchoredPositionZ(z);
        }

        public void SetAnchorMin(Vector2 anchorMin)
        {
            this.Get().SetAnchorMin(anchorMin);
        }

        public void SetAnchorMax(Vector2 anchorMax)
        {
            this.Get().SetAnchorMax(anchorMax);
        }

        public void SetAnchorX(Vector2 anchor)
        {
            this.Get().SetAnchorX(anchor);
        }

        public void SetAnchorY(Vector2 anchor)
        {
            this.Get().SetAnchorY(anchor);
        }

        public void SetAnchorX(float min, float max)
        {
            this.Get().SetAnchorX(min, max);
        }

        public void SetAnchorY(float min, float max)
        {
            this.Get().SetAnchorY(min, max);
        }

        public void SetPivot(Vector2 pivot)
        {
            this.Get().SetPivot(pivot);
        }

        public void SetOffsetMin(Vector2 offsetMin)
        {
            this.Get().SetOffsetMin(offsetMin);
        }

        public void SetOffsetMax(Vector2 offsetMax)
        {
            this.Get().SetOffsetMin(offsetMax);
        }

        public void SetOffset(float left, float right, float top, float bottom)
        {
            this.Get().SetOffset(left, right, top, bottom);
        }

        public void SetOffsetWithLeft(float left)
        {
            this.Get().SetOffsetWithLeft(left);
        }

        public void SetOffsetWithBottom(float buttom)
        {
            this.Get().SetOffsetWithBottom(buttom);
        }

        public void SetOffsetWithRight(float right)
        {
            this.Get().SetOffsetWithRight(right);
        }

        public void SetOffsetWithTop(float top)
        {
            this.Get().SetOffsetWithTop(top);
        }

        #endregion

        #region MiniTween

        public MiniTween DoMove(Vector3 endValue, float duration)
        {
            return this.Get().DoMove(this, endValue, duration);
        }

        public MiniTween DoMove(Vector3 startValue, Vector3 endValue, float duration)
        {
            return this.Get().DoMove(this, startValue, endValue, duration);
        }

        public MiniTween DoMoveX(float endValue, float duration)
        {
            return this.Get().DoMoveX(this, endValue, duration);
        }

        public MiniTween DoMoveX(float startValue, float endValue, float duration)
        {
            return this.Get().DoMoveX(this, startValue, endValue, duration);
        }

        public MiniTween DoMoveY(float endValue, float duration)
        {
            return this.Get().DoMoveY(this, endValue, duration);
        }

        public MiniTween DoMoveY(float startValue, float endValue, float duration)
        {
            return this.Get().DoMoveY(this, startValue, endValue, duration);
        }

        public MiniTween DoMoveZ(float endValue, float duration)
        {
            return this.Get().DoMoveZ(this, endValue, duration);
        }

        public MiniTween DoMoveZ(float startValue, float endValue, float duration)
        {
            return this.Get().DoMoveZ(this, startValue, endValue, duration);
        }

        /// <summary>
        /// 增量动画
        /// </summary>
        /// <param name="increaseValue"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        public MiniTween DoMoveIncremental(Vector3 increaseValue, float duration)
        {
            return this.Get().DoMoveIncremental(this, increaseValue, duration);
        }

        /// <summary>
        /// 增量动画，X轴，世界坐标
        /// </summary>
        /// <param name="increaseValue"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        public MiniTween DoMoveIncrementalX(float increaseValue, float duration)
        {
            return this.Get().DoMoveIncrementalX(this, increaseValue, duration);
        }

        /// <summary>
        /// 增量动画，Y轴，世界坐标
        /// </summary>
        /// <param name="increaseValue"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        public MiniTween DoMoveIncrementalY(float increaseValue, float duration)
        {
            return this.Get().DoMoveIncrementalY(this, increaseValue, duration);
        }

        /// <summary>
        /// 增量动画，Z轴，世界坐标
        /// </summary>
        /// <param name="increaseValue"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        public MiniTween DoMoveIncrementalZ(float increaseValue, float duration)
        {
            return this.Get().DoMoveIncrementalZ(this, increaseValue, duration);
        }

        public MiniTween DoLocalMove(Vector3 endValue, float duration)
        {
            return this.Get().DoLocalMove(this, endValue, duration);
        }

        public MiniTween DoLocalMove(Vector3 startValue, Vector3 endValue, float duration)
        {
            return this.Get().DoLocalMove(this, startValue, endValue, duration);
        }

        public MiniTween DoLocalMoveX(float endValue, float duration)
        {
            return this.Get().DoLocalMoveX(this, endValue, duration);
        }

        public MiniTween DoLocalMoveX(float startValue, float endValue, float duration)
        {
            return this.Get().DoLocalMoveX(this, startValue, endValue, duration);
        }

        public MiniTween DoLocalMoveY(float endValue, float duration)
        {
            return this.Get().DoLocalMoveY(this, endValue, duration);
        }

        public MiniTween DoLocalMoveY(float startValue, float endValue, float duration)
        {
            return this.Get().DoLocalMoveY(this, startValue, endValue, duration);
        }

        public MiniTween DoLocalMoveZ(float endValue, float duration)
        {
            return this.Get().DoLocalMoveZ(this, endValue, duration);
        }

        public MiniTween DoLocalMoveZ(float startValue, float endValue, float duration)
        {
            return this.Get().DoLocalMoveZ(this, startValue, endValue, duration);
        }

        /// <summary>
        /// 增量动画
        /// </summary>
        /// <param name="increaseValue"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        public MiniTween DoLocalMoveIncremental(Vector3 increaseValue, float duration)
        {
            return this.Get().DoLocalMoveIncremental(this, increaseValue, duration);
        }

        /// <summary>
        /// 增量动画，X轴，局部坐标
        /// </summary>
        /// <param name="increaseValue"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        public MiniTween DoLocalMoveIncrementalX(float increaseValue, float duration)
        {
            return this.Get().DoLocalMoveIncrementalX(this, increaseValue, duration);
        }

        /// <summary>
        /// 增量动画，Y轴，局部坐标
        /// </summary>
        /// <param name="increaseValue"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        public MiniTween DoLocalMoveIncrementalY(float increaseValue, float duration)
        {
            return this.Get().DoLocalMoveIncrementalY(this, increaseValue, duration);
        }

        /// <summary>
        /// 增量动画，Z轴，局部坐标
        /// </summary>
        /// <param name="increaseValue"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        public MiniTween DoLocalMoveIncrementalZ(float increaseValue, float duration)
        {
            return this.Get().DoLocalMoveIncrementalZ(this, increaseValue, duration);
        }

        public MiniTween DoScale(Vector3 endValue, float duration)
        {
            return this.Get().DoScale(this, endValue, duration);
        }

        public MiniTween DoScale(Vector3 startValue, Vector3 endValue, float duration)
        {
            return this.Get().DoScale(this, startValue, endValue, duration);
        }

        public MiniTween DoScale2(Vector2 endValue, float duration)
        {
            return this.Get().DoScale2(this, endValue, duration);
        }

        public MiniTween DoScale2(Vector2 startValue, Vector2 endValue, float duration)
        {
            return this.Get().DoScale2(this, startValue, endValue, duration);
        }

        public MiniTween DoScaleX(float endValue, float duration)
        {
            return this.Get().DoScaleX(this, endValue, duration);
        }

        public MiniTween DoScaleX(float startValue, float endValue, float duration)
        {
            return this.Get().DoScaleX(this, startValue, endValue, duration);
        }

        public MiniTween DoScaleY(float endValue, float duration)
        {
            return this.Get().DoScaleY(this, endValue, duration);
        }

        public MiniTween DoScaleY(float startValue, float endValue, float duration)
        {
            return this.Get().DoScaleY(this, startValue, endValue, duration);
        }

        public MiniTween DoScaleZ(float endValue, float duration)
        {
            return this.Get().DoScaleZ(this, endValue, duration);
        }

        public MiniTween DoScaleZ(float startValue, float endValue, float duration)
        {
            return this.Get().DoScaleZ(this, startValue, endValue, duration);
        }

        public MiniTween DoRotation(Quaternion startValue, Quaternion endValue, float duration)
        {
            return this.Get().DoRotation(this, startValue, endValue, duration);
        }

        public MiniTween DoEulerAngleX(float endValue, float duration)
        {
            return this.Get().DoEulerAngleX(this, endValue, duration);
        }

        public MiniTween DoEulerAngleX(float startValue, float endValue, float duration)
        {
            return this.Get().DoEulerAngleX(this, startValue, endValue, duration);
        }

        public MiniTween DoEulerAngleY(float endValue, float duration)
        {
            return this.Get().DoEulerAngleY(this, endValue, duration);
        }

        public MiniTween DoEulerAngleY(float startValue, float endValue, float duration)
        {
            return this.Get().DoEulerAngleY(this, startValue, endValue, duration);
        }

        public MiniTween DoEulerAngleZ(float endValue, float duration)
        {
            return this.Get().DoEulerAngleZ(this, endValue, duration);
        }

        public MiniTween DoEulerAngleZ(float startValue, float endValue, float duration)
        {
            return this.Get().DoEulerAngleZ(this, startValue, endValue, duration);
        }

        public MiniTween DoLocalEulerAngleX(float endValue, float duration)
        {
            return this.Get().DoLocalEulerAngleX(this, endValue, duration);
        }

        public MiniTween DoLocalEulerAngleX(float startValue, float endValue, float duration)
        {
            return this.Get().DoLocalEulerAngleX(this, startValue, endValue, duration);
        }

        public MiniTween DoLocalEulerAngleY(float endValue, float duration)
        {
            return this.Get().DoLocalEulerAngleY(this, endValue, duration);
        }

        public MiniTween DoLocalEulerAngleY(float startValue, float endValue, float duration)
        {
            return this.Get().DoLocalEulerAngleY(this, startValue, endValue, duration);
        }

        public MiniTween DoLocalEulerAngleZ(float endValue, float duration)
        {
            return this.Get().DoLocalEulerAngleZ(this, endValue, duration);
        }

        public MiniTween DoLocalEulerAngleZ(float startValue, float endValue, float duration)
        {
            return this.Get().DoLocalEulerAngleZ(this, startValue, endValue, duration);
        }

        public MiniTween DoAnchoredPosition(Vector2 endValue, float duration)
        {
            return this.Get().DoAnchoredPosition(this, endValue, duration);
        }

        public MiniTween DoAnchoredPosition(Vector2 startValue, Vector2 endValue, float duration)
        {
            return this.Get().DoAnchoredPosition(this, startValue, endValue, duration);
        }

        public MiniTween DoAnchoredPositionX(float endValue, float duration)
        {
            return this.Get().DoAnchoredPositionX(this, endValue, duration);
        }

        public MiniTween DoAnchoredPositionX(float startValue, float endValue, float duration)
        {
            return this.Get().DoAnchoredPositionX(this, startValue, endValue, duration);
        }

        public MiniTween DoAnchoredPositionY(float endValue, float duration)
        {
            return this.Get().DoAnchoredPositionY(this, endValue, duration);
        }

        public MiniTween DoAnchoredPositionY(float startValue, float endValue, float duration)
        {
            return this.Get().DoAnchoredPositionY(this, startValue, endValue, duration);
        }

        /// <summary>
        /// 增量动画
        /// </summary>
        /// <param name="increaseValue"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        public MiniTween DoAnchoredPositionIncremental(Vector2 increaseValue, float duration)
        {
            return this.Get().DoAnchoredPositionIncremental(this, increaseValue, duration);
        }

        /// <summary>
        /// 增量动画，X轴
        /// </summary>
        /// <param name="increaseValue"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        public MiniTween DoAnchoredPositionIncrementalX(float increaseValue, float duration)
        {
            return this.Get().DoAnchoredPositionIncrementalX(this, increaseValue, duration);
        }

        /// <summary>
        /// 增量动画，Y轴
        /// </summary>
        /// <param name="increaseValue"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        public MiniTween DoAnchoredPositionIncrementalY(float increaseValue, float duration)
        {
            return this.Get().DoAnchoredPositionIncrementalY(this, increaseValue, duration);
        }

        #endregion
    }

    public static class RectTransformExtensions
    {
        #region Transform

        public static Vector2 Scale2(this Transform self)
        {
            return self.localScale;
        }

        public static Vector3 Scale(this Transform self)
        {
            return self.localScale;
        }

        public static Vector2 LocalPosition2(this Transform self)
        {
            return self.localPosition;
        }

        public static Vector3 LocalPosition(this Transform self)
        {
            return self.localPosition;
        }

        public static Vector2 Position2(this Transform self)
        {
            return self.position;
        }

        public static Vector3 Position(this Transform self)
        {
            return self.position;
        }

        public static Quaternion Rotation(this Transform self)
        {
            return self.rotation;
        }

        public static Quaternion LocalRotation(this Transform self)
        {
            return self.localRotation;
        }

        public static void SetScaleX(this Transform self, float x)
        {
            var localScale = self.Scale();
            localScale.x = x;
            self.SetScale(localScale);
        }

        public static void SetScaleY(this Transform self, float y)
        {
            var localScale = self.Scale();
            localScale.y = y;
            self.SetScale(localScale);
        }

        public static void SetScaleZ(this Transform self, float z)
        {
            var localScale = self.Scale();
            localScale.y = z;
            self.SetScale(localScale);
        }

        public static void SetScale2(this Transform self, float scale)
        {
            self.SetScale(new Vector3(scale, scale, 1));
        }

        public static void SetScale3(this Transform self, float scale)
        {
            self.SetScale(new Vector3(scale, scale, scale));
        }

        public static void SetScale(this Transform self, Vector2 scale)
        {
            self.localScale = scale;
        }

        public static void SetScale(this Transform self, Vector3 scale)
        {
            self.localScale = scale;
        }

        public static void SetLocalPosition(this Transform self, Vector3 local)
        {
            self.localPosition = local;
        }

        public static void SetLocalPositionX(this Transform self, float x)
        {
            var localPosition = self.LocalPosition();
            localPosition.x = x;
            self.SetLocalPosition(localPosition);
        }

        public static void SetLocalPositionY(this Transform self, float y)
        {
            var localPosition = self.LocalPosition();
            localPosition.y = y;
            self.SetLocalPosition(localPosition);
        }

        public static void SetLocalPositionZ(this Transform self, float z)
        {
            var localPosition = self.LocalPosition();
            localPosition.z = z;
            self.SetLocalPosition(localPosition);
        }

        public static void SetPosition(this Transform self, Vector3 pos)
        {
            self.position = pos;
        }

        public static void SetPositionX(this Transform self, float x)
        {
            var position = self.Position();
            position.x = x;
            self.SetPosition(position);
        }

        public static void SetPositionY(this Transform self, float y)
        {
            var position = self.Position();
            position.y = y;
            self.SetPosition(position);
        }

        public static void SetPositionZ(this Transform self, float z)
        {
            var position = self.Position();
            position.z = z;
            self.SetPosition(position);
        }

        public static void SetRotation(this Transform self, Quaternion rotation)
        {
            self.rotation = rotation;
        }

        public static void SetLocalRotation(this Transform self, Quaternion rotation)
        {
            self.localRotation = rotation;
        }

        public static void SetEulerAnglesX(this Transform self, float x)
        {
            var euler = self.Rotation().eulerAngles;
            euler.x = x;
            self.SetRotation(Quaternion.Euler(euler));
        }

        public static void SetEulerAnglesY(this Transform self, float y)
        {
            var euler = self.Rotation().eulerAngles;
            euler.y = y;
            self.SetRotation(Quaternion.Euler(euler));
        }

        public static void SetEulerAnglesZ(this Transform self, float z)
        {
            var euler = self.Rotation().eulerAngles;
            euler.z = z;
            self.SetRotation(Quaternion.Euler(euler));
        }

        public static void SetLocalEulerAnglesX(this Transform self, float x)
        {
            var euler = self.LocalRotation().eulerAngles;
            euler.x = x;
            self.SetLocalRotation(Quaternion.Euler(euler));
        }

        public static void SetLocalEulerAnglesY(this Transform self, float y)
        {
            var euler = self.LocalRotation().eulerAngles;
            euler.y = y;
            self.SetLocalRotation(Quaternion.Euler(euler));
        }

        public static void SetLocalEulerAnglesZ(this Transform self, float z)
        {
            var euler = self.LocalRotation().eulerAngles;
            euler.z = z;
            self.SetLocalRotation(Quaternion.Euler(euler));
        }

        public static void AddChildAt(this Transform self, Transform child, int index)
        {
            self.AddChildAt(child, index, true);
        }

        public static void AddChildAt(this Transform self, Transform child, int index, bool worldPositionStays)
        {
            child.SetParent(self, worldPositionStays);
            int childCount = self.childCount;
            index %= childCount;
            if (index < 0)
                index += childCount;
            child.SetSiblingIndex(index);
        }

        #endregion

        #region RectTransform

        public static Vector2 AnchoredPosition(this RectTransform self)
        {
            return self.anchoredPosition;
        }

        public static Vector3 AnchoredPosition3D(this RectTransform self)
        {
            return self.anchoredPosition3D;
        }

        public static Vector2 AnchorMin(this RectTransform self)
        {
            return self.anchorMin;
        }

        public static Vector2 AnchorMax(this RectTransform self)
        {
            return self.anchorMax;
        }

        public static Vector2 Pivot(this RectTransform self)
        {
            return self.pivot;
        }

        public static Vector2 OffsetMin(this RectTransform self)
        {
            return self.offsetMin;
        }

        public static Vector2 OffsetMax(this RectTransform self)
        {
            return self.offsetMax;
        }

        public static float Width(this RectTransform self)
        {
            return self.rect.width;
        }

        public static float Height(this RectTransform self)
        {
            return self.rect.height;
        }

        public static void GetLocalCorners(this RectTransform self, List<Vector3> fourCornersArray)
        {
            Vector3[] corners = new Vector3[4];
            self.GetLocalCorners(corners);
            fourCornersArray.AddRange(corners);
        }

        public static void GetWorldCorners(this RectTransform self, List<Vector3> fourCornersArray)
        {
            Vector3[] corners = new Vector3[4];
            self.GetWorldCorners(corners);
            fourCornersArray.AddRange(corners);
        }

        public static void SetSize(this RectTransform self, Vector2 size)
        {
            self.sizeDelta = size;
        }

        public static void SetSize(this RectTransform self, float width, float height)
        {
            self.SetSize(new Vector2(width, height));
        }

        public static void SetWidth(this RectTransform self, float width)
        {
            float height = self.Height();
            self.SetSize(width, height);
        }

        public static void SetHeight(this RectTransform self, float height)
        {
            float width = self.Width();
            self.SetSize(width, height);
        }

        public static void SetAnchoredPosition(this RectTransform self, Vector2 pos)
        {
            self.anchoredPosition = pos;
        }

        public static void SetAnchoredPosition3D(this RectTransform self, Vector2 pos)
        {
            self.SetAnchoredPosition3D(pos);
        }

        public static void SetAnchoredPosition3D(this RectTransform self, Vector3 pos)
        {
            self.anchoredPosition3D = pos;
        }

        public static void SetAnchoredPositionZToZero(this RectTransform self)
        {
            self.SetAnchoredPositionZ(0);
        }

        public static void SetAnchoredPosition(this RectTransform self, float x, float y)
        {
            self.SetAnchoredPosition(new Vector2(x, y));
        }

        public static void SetAnchoredPosition3D(this RectTransform self, float x, float y, float z)
        {
            self.SetAnchoredPosition3D(new Vector3(x, y, z));
        }

        public static void SetAnchoredPositionX(this RectTransform self, float x)
        {
            var pos = self.AnchoredPosition();
            pos.x = x;
            self.SetAnchoredPosition(pos);
        }

        public static void SetAnchoredPositionY(this RectTransform self, float y)
        {
            var pos = self.AnchoredPosition();
            pos.y = y;
            self.SetAnchoredPosition(pos);
        }

        public static void SetAnchoredPositionZ(this RectTransform self, float z)
        {
            var pos = self.AnchoredPosition3D();
            pos.z = z;
            self.SetAnchoredPosition3D(pos);
        }

        public static void SetAnchorMin(this RectTransform self, Vector2 anchorMin)
        {
            self.anchorMin = anchorMin;
        }

        public static void SetAnchorMax(this RectTransform self, Vector2 anchorMax)
        {
            self.anchorMax = anchorMax;
        }

        public static void SetAnchorX(this RectTransform self, Vector2 anchor)
        {
            var min = self.AnchorMin();
            var max = self.AnchorMax();
            min.x = anchor.x;
            max.x = anchor.y;
            self.SetAnchorMin(min);
            self.SetAnchorMax(max);
        }

        public static void SetAnchorY(this RectTransform self, Vector2 anchor)
        {
            var min = self.AnchorMin();
            var max = self.AnchorMax();
            min.y = anchor.x;
            max.y = anchor.y;
            self.SetAnchorMin(min);
            self.SetAnchorMax(max);
        }

        public static void SetAnchorX(this RectTransform self, float min, float max)
        {
            self.SetAnchorX(new Vector2(min, max));
        }

        public static void SetAnchorY(this RectTransform self, float min, float max)
        {
            self.SetAnchorY(new Vector2(min, max));
        }

        public static void SetPivot(this RectTransform self, Vector2 pivot)
        {
            self.pivot = pivot;
        }

        public static void SetOffsetMin(this RectTransform self, Vector2 offsetMin)
        {
            self.offsetMin = offsetMin;
        }

        public static void SetOffsetMax(this RectTransform self, Vector2 offsetMax)
        {
            self.offsetMax = offsetMax;
        }

        public static void SetOffset(this RectTransform self, float left, float right, float top, float bottom)
        {
            self.SetOffsetMin(new Vector2(left, bottom));
            self.SetOffsetMax(new Vector2(right, top));
        }

        public static void SetOffsetWithLeft(this RectTransform self, float left)
        {
            var offsetMin = self.OffsetMin();
            offsetMin.x = left;
            self.SetOffsetMin(offsetMin);
        }

        public static void SetOffsetWithBottom(this RectTransform self, float buttom)
        {
            var offsetMin = self.OffsetMin();
            offsetMin.y = buttom;
            self.SetOffsetMin(offsetMin);
        }

        public static void SetOffsetWithRight(this RectTransform self, float right)
        {
            var offsetMax = self.OffsetMax();
            offsetMax.x = right;
            self.SetOffsetMax(offsetMax);
        }

        public static void SetOffsetWithTop(this RectTransform self, float top)
        {
            var offsetMax = self.OffsetMax();
            offsetMax.y = top;
            self.SetOffsetMax(offsetMax);
        }

        #endregion

        #region MiniTween

        private static MiniTween DoVector3(this Transform self, XObject parent, Vector3 startValue, Vector3 endValue,
            float duration, Action<Vector3> setter)
        {
            var tweenMgr = Common.Instance.Get<MiniTweenManager>();
            if (tweenMgr is null)
                return null;

            var tween = tweenMgr.To(parent, startValue, endValue, duration);
            tween.AddListener(v =>
            {
                if (!self)
                {
                    tween.Cancel(parent);
                    return;
                }

                setter.Invoke(v);
            });

            return tween;
        }

        private static MiniTween DoVectorX(this Transform self, XObject parent, float startValue, float endValue,
            float duration, Func<Vector3> getter, Action<Vector3> setter)
        {
            var v3 = getter.Invoke();
            var target = v3;

            v3.x = startValue;
            target.x = endValue;

            return self.DoVector3(parent, v3, target, duration, setter);
        }

        private static MiniTween DoVectorY(this Transform self, XObject parent, float startValue, float endValue,
            float duration, Func<Vector3> getter, Action<Vector3> setter)
        {
            var v3 = getter.Invoke();
            var target = v3;

            v3.y = startValue;
            target.y = endValue;

            return self.DoVector3(parent, v3, target, duration, setter);
        }

        private static MiniTween DoVectorZ(this Transform self, XObject parent, float startValue, float endValue,
            float duration, Func<Vector3> getter, Action<Vector3> setter)
        {
            var v3 = getter.Invoke();
            var target = v3;

            v3.z = startValue;
            target.z = endValue;

            return self.DoVector3(parent, v3, target, duration, setter);
        }

        private static MiniTween DoFloat(this Transform self, XObject parent, float startValue, float endValue,
            float duration, Action<float> setter)
        {
            var tweenMgr = Common.Instance.Get<MiniTweenManager>();
            if (tweenMgr is null)
                return null;

            var tween = tweenMgr.To(parent, startValue, endValue, duration);
            tween.AddListener(v =>
            {
                if (!self)
                {
                    tween.Cancel(parent);
                    return;
                }

                setter.Invoke(v);
            });

            return tween;
        }

        public static MiniTween DoMove(this Transform self, XObject parent, Vector3 endValue, float duration)
        {
            return self.DoMove(parent, self.Position(), endValue, duration);
        }

        public static MiniTween DoMove(this Transform self, XObject parent, Vector3 startValue, Vector3 endValue,
            float duration)
        {
            return self.DoVector3(parent, startValue, endValue, duration, self.SetPosition);
        }

        public static MiniTween DoMoveX(this Transform self, XObject parent, float endValue, float duration)
        {
            var position = self.Position();
            return self.DoMoveX(parent, position.x, endValue, duration);
        }

        public static MiniTween DoMoveX(this Transform self, XObject parent, float startValue, float endValue,
            float duration)
        {
            return self.DoVectorX(parent, startValue, endValue, duration, self.Position, self.SetPosition);
        }

        public static MiniTween DoMoveY(this Transform self, XObject parent, float endValue, float duration)
        {
            var position = self.Position();
            return self.DoMoveY(parent, position.y, endValue, duration);
        }

        public static MiniTween DoMoveY(this Transform self, XObject parent, float startValue, float endValue,
            float duration)
        {
            return self.DoVectorY(parent, startValue, endValue, duration, self.Position, self.SetPosition);
        }

        public static MiniTween DoMoveZ(this Transform self, XObject parent, float endValue, float duration)
        {
            var position = self.Position();
            return self.DoMoveZ(parent, position.z, endValue, duration);
        }

        public static MiniTween DoMoveZ(this Transform self, XObject parent, float startValue, float endValue,
            float duration)
        {
            return self.DoVectorZ(parent, startValue, endValue, duration, self.Position, self.SetPosition);
        }

        /// <summary>
        /// 增量动画
        /// </summary>
        /// <param name="self"></param>
        /// <param name="parent"></param>
        /// <param name="increaseValue"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        public static MiniTween DoMoveIncremental(this Transform self, XObject parent, Vector3 increaseValue,
            float duration)
        {
            return self.DoMove(parent, self.Position() + increaseValue, duration);
        }

        /// <summary>
        /// 增量动画，X轴，世界坐标
        /// </summary>
        /// <param name="self"></param>
        /// <param name="parent"></param>
        /// <param name="increaseValue"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        public static MiniTween DoMoveIncrementalX(this Transform self, XObject parent, float increaseValue,
            float duration)
        {
            var position = self.Position();
            return self.DoMoveX(parent, position.x + increaseValue, duration);
        }

        /// <summary>
        /// 增量动画，Y轴，世界坐标
        /// </summary>
        /// <param name="self"></param>
        /// <param name="parent"></param>
        /// <param name="increaseValue"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        public static MiniTween DoMoveIncrementalY(this Transform self, XObject parent, float increaseValue,
            float duration)
        {
            var position = self.Position();
            return self.DoMoveY(parent, position.y + increaseValue, duration);
        }

        /// <summary>
        /// 增量动画，Z轴，世界坐标
        /// </summary>
        /// <param name="self"></param>
        /// <param name="parent"></param>
        /// <param name="increaseValue"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        public static MiniTween DoMoveIncrementalZ(this Transform self, XObject parent, float increaseValue,
            float duration)
        {
            var position = self.Position();
            return self.DoMoveZ(parent, position.z + increaseValue, duration);
        }

        public static MiniTween DoLocalMove(this Transform self, XObject parent, Vector3 endValue, float duration)
        {
            return self.DoLocalMove(parent, self.LocalPosition(), endValue, duration);
        }

        public static MiniTween DoLocalMove(this Transform self, XObject parent, Vector3 startValue, Vector3 endValue,
            float duration)
        {
            return self.DoVector3(parent, startValue, endValue, duration, self.SetLocalPosition);
        }

        public static MiniTween DoLocalMoveX(this Transform self, XObject parent, float endValue, float duration)
        {
            var position = self.LocalPosition();
            return self.DoLocalMoveX(parent, position.x, endValue, duration);
        }

        public static MiniTween DoLocalMoveX(this Transform self, XObject parent, float startValue, float endValue,
            float duration)
        {
            return self.DoVectorX(parent, startValue, endValue, duration, self.LocalPosition, self.SetLocalPosition);
        }

        public static MiniTween DoLocalMoveY(this Transform self, XObject parent, float endValue, float duration)
        {
            var position = self.LocalPosition();
            return self.DoLocalMoveY(parent, position.y, endValue, duration);
        }

        public static MiniTween DoLocalMoveY(this Transform self, XObject parent, float startValue, float endValue,
            float duration)
        {
            return self.DoVectorY(parent, startValue, endValue, duration, self.LocalPosition, self.SetLocalPosition);
        }

        public static MiniTween DoLocalMoveZ(this Transform self, XObject parent, float endValue, float duration)
        {
            var position = self.LocalPosition();
            return self.DoLocalMoveZ(parent, position.z, endValue, duration);
        }

        public static MiniTween DoLocalMoveZ(this Transform self, XObject parent, float startValue, float endValue,
            float duration)
        {
            return self.DoVectorZ(parent, startValue, endValue, duration, self.LocalPosition, self.SetLocalPosition);
        }

        /// <summary>
        /// 增量动画
        /// </summary>
        /// <param name="self"></param>
        /// <param name="parent"></param>
        /// <param name="increaseValue"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        public static MiniTween DoLocalMoveIncremental(this Transform self, XObject parent, Vector3 increaseValue,
            float duration)
        {
            return self.DoLocalMove(parent, self.LocalPosition() + increaseValue, duration);
        }

        /// <summary>
        /// 增量动画，X轴，局部坐标
        /// </summary>
        /// <param name="self"></param>
        /// <param name="parent"></param>
        /// <param name="increaseValue"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        public static MiniTween DoLocalMoveIncrementalX(this Transform self, XObject parent, float increaseValue,
            float duration)
        {
            var position = self.LocalPosition();
            return self.DoLocalMoveX(parent, position.x + increaseValue, duration);
        }

        /// <summary>
        /// 增量动画，Y轴，局部坐标
        /// </summary>
        /// <param name="self"></param>
        /// <param name="parent"></param>
        /// <param name="increaseValue"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        public static MiniTween DoLocalMoveIncrementalY(this Transform self, XObject parent, float increaseValue,
            float duration)
        {
            var position = self.LocalPosition();
            return self.DoLocalMoveY(parent, position.y + increaseValue, duration);
        }

        /// <summary>
        /// 增量动画，Z轴，局部坐标
        /// </summary>
        /// <param name="self"></param>
        /// <param name="parent"></param>
        /// <param name="increaseValue"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        public static MiniTween DoLocalMoveIncrementalZ(this Transform self, XObject parent, float increaseValue,
            float duration)
        {
            var position = self.LocalPosition();
            return self.DoLocalMoveZ(parent, position.z + increaseValue, duration);
        }

        public static MiniTween DoScale(this Transform self, XObject parent, Vector3 endValue, float duration)
        {
            var scale = self.Scale();
            return self.DoScale(parent, scale, endValue, duration);
        }

        public static MiniTween DoScale(this Transform self, XObject parent, Vector3 startValue, Vector3 endValue,
            float duration)
        {
            return self.DoVector3(parent, startValue, endValue, duration, self.SetScale);
        }

        public static MiniTween DoScale2(this Transform self, XObject parent, Vector2 endValue, float duration)
        {
            var scale = self.Scale();
            return self.DoScale2(parent, scale, endValue, duration);
        }

        public static MiniTween DoScale2(this Transform self, XObject parent, Vector2 startValue, Vector2 endValue,
            float duration)
        {
            var z = self.Scale().z;
            Vector3 start = new Vector3(startValue.x, startValue.y, z);
            Vector3 end = new Vector3(endValue.x, endValue.y, z);
            return self.DoScale(parent, start, end, duration);
        }

        public static MiniTween DoScaleX(this Transform self, XObject parent, float endValue, float duration)
        {
            var scale = self.Scale();
            return self.DoScaleX(parent, scale.x, endValue, duration);
        }

        public static MiniTween DoScaleX(this Transform self, XObject parent, float startValue, float endValue,
            float duration)
        {
            return self.DoVectorX(parent, startValue, endValue, duration, self.Scale, self.SetScale);
        }

        public static MiniTween DoScaleY(this Transform self, XObject parent, float endValue, float duration)
        {
            var scale = self.Scale();
            return self.DoScaleY(parent, scale.y, endValue, duration);
        }

        public static MiniTween DoScaleY(this Transform self, XObject parent, float startValue, float endValue,
            float duration)
        {
            return self.DoVectorY(parent, startValue, endValue, duration, self.Scale, self.SetScale);
        }

        public static MiniTween DoScaleZ(this Transform self, XObject parent, float endValue, float duration)
        {
            var scale = self.Scale();
            return self.DoScaleZ(parent, scale.z, endValue, duration);
        }

        public static MiniTween DoScaleZ(this Transform self, XObject parent, float startValue, float endValue,
            float duration)
        {
            return self.DoVectorZ(parent, startValue, endValue, duration, self.Scale, self.SetScale);
        }

        public static MiniTween DoRotation(this Transform self, XObject parent, Quaternion startValue,
            Quaternion endValue, float duration)
        {
            var tweenMgr = Common.Instance.Get<MiniTweenManager>();
            if (tweenMgr == null)
                return null;

            var tween = tweenMgr.To(parent, startValue, endValue, duration);
            tween.AddListener(v =>
            {
                if (!self)
                {
                    tween.Cancel(parent);
                    return;
                }

                self.SetRotation(v);
            });

            return tween;
        }

        public static MiniTween DoEulerAngleX(this Transform self, XObject parent, float endValue, float duration)
        {
            var rotation = self.Rotation();
            var euler = rotation.eulerAngles;
            return self.DoEulerAngleX(parent, euler.x, endValue, duration);
        }

        public static MiniTween DoEulerAngleX(this Transform self, XObject parent, float startValue, float endValue,
            float duration)
        {
            return self.DoFloat(parent, startValue, endValue, duration, self.SetEulerAnglesX);
        }

        public static MiniTween DoEulerAngleY(this Transform self, XObject parent, float endValue, float duration)
        {
            var rotation = self.Rotation();
            var euler = rotation.eulerAngles;
            return self.DoEulerAngleY(parent, euler.y, endValue, duration);
        }

        public static MiniTween DoEulerAngleY(this Transform self, XObject parent, float startValue, float endValue,
            float duration)
        {
            return self.DoFloat(parent, startValue, endValue, duration, self.SetEulerAnglesY);
        }

        public static MiniTween DoEulerAngleZ(this Transform self, XObject parent, float endValue, float duration)
        {
            var rotation = self.Rotation();
            var euler = rotation.eulerAngles;
            return self.DoEulerAngleZ(parent, euler.z, endValue, duration);
        }

        public static MiniTween DoEulerAngleZ(this Transform self, XObject parent, float startValue, float endValue,
            float duration)
        {
            return self.DoFloat(parent, startValue, endValue, duration, self.SetEulerAnglesZ);
        }

        public static MiniTween DoLocalEulerAngleX(this Transform self, XObject parent, float endValue, float duration)
        {
            var rotation = self.LocalRotation();
            var euler = rotation.eulerAngles;
            return self.DoLocalEulerAngleX(parent, euler.x, endValue, duration);
        }

        public static MiniTween DoLocalEulerAngleX(this Transform self, XObject parent, float startValue,
            float endValue, float duration)
        {
            return self.DoFloat(parent, startValue, endValue, duration, self.SetLocalEulerAnglesX);
        }

        public static MiniTween DoLocalEulerAngleY(this Transform self, XObject parent, float endValue, float duration)
        {
            var rotation = self.LocalRotation();
            var euler = rotation.eulerAngles;
            return self.DoLocalEulerAngleY(parent, euler.y, endValue, duration);
        }

        public static MiniTween DoLocalEulerAngleY(this Transform self, XObject parent, float startValue,
            float endValue, float duration)
        {
            return self.DoFloat(parent, startValue, endValue, duration, self.SetLocalEulerAnglesY);
        }

        public static MiniTween DoLocalEulerAngleZ(this Transform self, XObject parent, float endValue, float duration)
        {
            var rotation = self.LocalRotation();
            var euler = rotation.eulerAngles;
            return self.DoLocalEulerAngleZ(parent, euler.z, endValue, duration);
        }

        public static MiniTween DoLocalEulerAngleZ(this Transform self, XObject parent, float startValue,
            float endValue, float duration)
        {
            return self.DoFloat(parent, startValue, endValue, duration, self.SetLocalEulerAnglesZ);
        }

        public static MiniTween DoAnchoredPosition(this RectTransform self, XObject parent, Vector2 endValue,
            float duration)
        {
            var position = self.AnchoredPosition();
            return self.DoAnchoredPosition(parent, position, endValue, duration);
        }

        public static MiniTween DoAnchoredPosition(this RectTransform self, XObject parent, Vector2 startValue,
            Vector2 endValue, float duration)
        {
            var z = self.AnchoredPosition3D().z;
            var start = new Vector3(startValue.x, startValue.y, z);
            var end = new Vector3(endValue.x, endValue.y, z);

            return self.DoVector3(parent, start, end, duration, self.SetAnchoredPosition3D);
        }

        public static MiniTween DoAnchoredPositionX(this RectTransform self, XObject parent, float endValue,
            float duration)
        {
            var position = self.AnchoredPosition();
            return self.DoAnchoredPositionX(parent, position.x, endValue, duration);
        }

        public static MiniTween DoAnchoredPositionX(this RectTransform self, XObject parent, float startValue,
            float endValue, float duration)
        {
            return self.DoVectorX(parent, startValue, endValue, duration, self.AnchoredPosition3D,
                self.SetAnchoredPosition3D);
        }

        public static MiniTween DoAnchoredPositionY(this RectTransform self, XObject parent, float endValue,
            float duration)
        {
            var position = self.AnchoredPosition();
            return self.DoAnchoredPositionY(parent, position.y, endValue, duration);
        }

        public static MiniTween DoAnchoredPositionY(this RectTransform self, XObject parent, float startValue,
            float endValue, float duration)
        {
            return self.DoVectorY(parent, startValue, endValue, duration, self.AnchoredPosition3D,
                self.SetAnchoredPosition3D);
        }

        /// <summary>
        /// 增量动画
        /// </summary>
        /// <param name="self"></param>
        /// <param name="parent"></param>
        /// <param name="increaseValue"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        public static MiniTween DoAnchoredPositionIncremental(this RectTransform self, XObject parent,
            Vector2 increaseValue, float duration)
        {
            var position = self.AnchoredPosition();
            return self.DoAnchoredPosition(parent, position + increaseValue, duration);
        }

        /// <summary>
        /// 增量动画，X轴
        /// </summary>
        /// <param name="self"></param>
        /// <param name="parent"></param>
        /// <param name="increaseValue"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        public static MiniTween DoAnchoredPositionIncrementalX(this RectTransform self, XObject parent,
            float increaseValue, float duration)
        {
            var position = self.AnchoredPosition();
            return self.DoAnchoredPositionX(parent, position.x + increaseValue, duration);
        }

        /// <summary>
        /// 增量动画，Y轴
        /// </summary>
        /// <param name="self"></param>
        /// <param name="parent"></param>
        /// <param name="increaseValue"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        public static MiniTween DoAnchoredPositionIncrementalY(this RectTransform self, XObject parent,
            float increaseValue, float duration)
        {
            var position = self.AnchoredPosition();
            return self.DoAnchoredPositionY(parent, position.y + increaseValue, duration);
        }

        #endregion
    }
}