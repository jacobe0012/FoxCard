//---------------------------------------------------------------------
// JiYuStudio
// Author: 迅捷蟹
// Time: 2023-8-8 17:58:20
//---------------------------------------------------------------------


using System;
using System.Runtime.CompilerServices;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;


//插件也写了一些DoTween效果,但不清楚是否适合我们,所以自己写了一个
namespace XFramework
{
    public static class DoTweenEffect
    {
        public const float OnClickAnimTime = 0.08f;
        public const float OnPressAnimTime = 0.12f;
        public const float LongPressInterval = 0.12f;
        public const int MaxLongPressCount = 1;

        public const float smallScale = 0.85f;
        public const float bigScale = 1.1f;

        /// <summary>
        /// 基于UI框架的按钮通用动效：单击缩放和长按缩放 必须是xButton
        /// </summary>
        /// <param name="ui">有XButton按钮组件的ui</param>
        /// <param name="action">点击事件</param>
        /// <param name="onClickAnimTime">动效时间，默认0.15s</param>
        /// <param name="onPressAnimTime">动效时间，默认0.15s</param>
        /// <param name="coolDown">按钮冷却时间，默认0s</param>
        /// <typeparam name="T"></typeparam>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DoScaleTweenOnClickAndLongPress(UI ui, UnityAction action = null, float coolDown = 0,
            float onClickAnimTime = OnClickAnimTime,
            float onPressAnimTime = OnPressAnimTime, float smallScale = smallScale,
            float bigScale = bigScale)
        {
            var uiXBtn = ui.GetXButton();
            if (uiXBtn == null)
            {
                Log.Error($"{ui.Name}.GetXButton() is null");
                return;
            }

            //ui.GetRectTransform().SetScale(Vector3.one);
            uiXBtn.SetPointerActive(true);
            uiXBtn.SetLongPressInterval(LongPressInterval);
            uiXBtn.SetMaxLongPressCount(MaxLongPressCount);

            var scale = ui.GetRectTransform().Scale();


            uiXBtn.OnClick.Add(async () =>
            {
                ui.GetRectTransform().DoScale(scale * smallScale, onClickAnimTime).AddOnCompleted(
                    () =>
                    {
                        ui.GetRectTransform().DoScale(scale * bigScale, onClickAnimTime)
                            .AddOnCompleted(() => { ui.GetRectTransform().DoScale(scale, onClickAnimTime / 2f); });
                    });
                //AudioManager.Instance.PlaySFXAudio("Click", true);
                //AudioManager.Instance.
                //Log.Debug($"action2");
                action?.Invoke();
                if (coolDown > 0)
                {
                    uiXBtn?.SetEnabled(false);
                    await UniTask.Delay((int)(coolDown * 1000f));
                    uiXBtn?.SetEnabled(true);
                }
            });

            uiXBtn.OnLongPress.Add((f) =>
            {
                ui.GetRectTransform().DoScale(scale * smallScale, onPressAnimTime);
                uiXBtn.OnPointerExit.Add(() =>
                {
                    ui.GetRectTransform().DoScale(scale * bigScale, onClickAnimTime).AddOnCompleted(() =>
                    {
                        ui.GetRectTransform().DoScale(scale, onClickAnimTime / 2f).AddOnCompleted(() => { });
                    });
                });
            });
            //
            uiXBtn.OnLongPressEnd.Add((f) =>
            {
                ui.GetRectTransform().DoScale(scale * bigScale, onClickAnimTime).AddOnCompleted(() =>
                {
                    ui.GetRectTransform().DoScale(scale, onClickAnimTime / 2f);
                });
                //AudioManager.Instance.PlaySFXAudio("Click", true);
                action?.Invoke();
            });
        }


        /// <summary>
        /// 按钮的缩放
        /// </summary>
        /// <param name="obj">变化的对象</param>
        /// <param name="variation">变化值</param>
        /// <param name="startTime">正向时间</param>
        /// <param name="EndTime">反向时间</param>
        public static void GradualChange(GameObject obj, float variation, float startTime, float EndTime)
        {
            var InitScale = obj.transform.localScale;
            obj.transform.DOScale(obj.transform.localScale * variation, startTime).OnComplete(() =>
            {
                obj.transform.DOScale(InitScale, EndTime);
            });
        }


        /// <summary>
        /// 升级特效
        /// </summary>
        /// <param name="obj">变化的对象</param>
        /// <param name="changePosition">改变位置值</param>
        /// <param name="variation">改变缩放的值</param>
        /// <param name="startTime">正向时间</param>
        /// <param name="EndTime">反向时间</param>
        public static void LevelUp(GameObject obj, Vector3 changePosition, float variation, float startTime,
            float EndTime)
        {
            RectTransform objTransform = obj.GetComponent<RectTransform>();
            obj.transform.DOMove(objTransform.position + changePosition, startTime).SetEase(Ease.OutQuad)
                .OnComplete(() => { obj.transform.DOMove(objTransform.position, EndTime).SetEase(Ease.OutQuad); });


            obj.transform.DOScale(obj.transform.localScale * variation, startTime).OnComplete(() =>
            {
                obj.transform.DOScale(obj.transform.localScale, EndTime);
            });
        }

        /// <summary>
        /// 退出效果
        /// </summary>
        /// <param name="Imageobj">退出依赖的图片</param>
        /// <param name="Textobj">退出的文字</param>
        /// <param name="AlphaValue">退出文字的Alpha值</param>
        /// <param name="AlphaTime">退出文字从0变化到Alpha值的时间</param>
        /// <param name="IncreateFontSize">退出文字字体大小变化增量</param>
        /// <param name="SizeChageTime">增量时间</param>
        /// <param name="YchangeValue">整个退出Y轴变化值</param>
        /// <param name="startTime">正向时间</param>
        public static void ExitEffct(GameObject Imageobj, GameObject Textobj, float AlphaValue, float AlphaTime,
            float IncreateFontSize, float SizeChageTime, float YchangeValue, float startTime)
        {
            CanvasGroup canvasGroup = Imageobj.GetComponent<CanvasGroup>();
            TMP_Text text = Textobj.GetComponent<TMP_Text>();
            //获得字体大小
            float textSize = text.fontSize;
            //获得字体空间初始Y值
            float Inity = Imageobj.GetComponent<RectTransform>().position.y;
            //变化通道
            //canvasGroup.DOFade(AlphaValue, AlphaTime);
            //字体大小变化
            DOTween.To(() => text.fontSize, x => text.fontSize = x, textSize + IncreateFontSize, SizeChageTime);
            //字体位置变化
            Imageobj.transform.DOMoveY(Inity + YchangeValue, startTime).OnComplete(() =>
            {
                canvasGroup.alpha = 0;
                Imageobj.transform.position =
                    new Vector3(Imageobj.transform.position.x, Inity, Imageobj.transform.position.z);
                //这里是字体瞬间复原
                text.fontSize = textSize;
            });
        }


        /// <summary>
        /// 移动动画
        /// </summary>
        /// <param name="obj1">对象1</param>
        /// <param name="obj2">对象2</param>
        /// <param name="EndPos1">终止位置1</param>
        /// <param name="EndPos2">终止位置2</param>
        /// <param name="durTime1">持续时间1</param>
        /// <param name="durTime2">持续时间2</param>
        /// <param name="isUseEndPos2">是否使用EndPos2</param>
        public static void MoveEffect(GameObject obj1, GameObject obj2, Vector3 EndPos1, Vector3 EndPos2,
            float durTime1, float durTime2 = 0, bool isUseEndPos2 = false)
        {
            RectTransform rectTransformObj = obj1.GetComponent<RectTransform>();
            if (!isUseEndPos2)
            {
                DOTween.To(() => rectTransformObj.anchoredPosition3D,
                    x => rectTransformObj.anchoredPosition3D = x,
                    EndPos1, durTime1);
            }
            else
            {
                RectTransform rectTransformObj2 = obj2.GetComponent<RectTransform>();
                DOTween.To(() => rectTransformObj.anchoredPosition3D, x => rectTransformObj.anchoredPosition3D = x,
                    EndPos1, durTime1).SetEase(Ease.OutQuad).OnComplete(() =>
                {
                    DOTween.To(() => rectTransformObj2.anchoredPosition3D,
                        x => rectTransformObj2.anchoredPosition3D = x,
                        EndPos2,
                        durTime2).SetEase(Ease.OutQuad);
                });
            }
        }


        //通用动画,框架
        public static void FadeInOut(UI ui, string key)
        {
            CanvasGroup canvasGroup = ui.GetFromReference(key).GetComponent<CanvasGroup>();


            DOTween.To(() => canvasGroup.alpha, x => canvasGroup.alpha = x, 1.0f, 1).SetEase(Ease.Linear).OnComplete(
                () =>
                {
                    // Fade out
                    DOTween.To(() => canvasGroup.alpha, x => canvasGroup.alpha = x, 0.0f, 1).SetEase(Ease.Linear)
                        .OnComplete(() => { FadeInOut(ui, key); });
                });
        }


        //通用动画,不走框架
        public static void FadeInOut(GameObject obj)
        {
            var CanvasGroupComponent = obj.GetComponent<CanvasGroup>();


            DOTween.To(() => CanvasGroupComponent.alpha, x => CanvasGroupComponent.alpha = x, 1.0f, 1)
                .SetEase(Ease.Linear).OnComplete(() =>
                {
                    // Fade out
                    DOTween.To(() => CanvasGroupComponent.alpha, x => CanvasGroupComponent.alpha = x, 0.0f, 1)
                        .SetEase(Ease.Linear).OnComplete(() => { FadeInOut(obj); });
                });
        }

        /// <summary>
        /// 黄金国修改
        /// 打开界面放大动画
        /// </summary>
        public static void OpenPanelScale(UI ui)
        {
            ui.GetRectTransform().SetScale(new Vector3(0, 0, 0));
            ui.GetRectTransform().DoScale(new Vector3(1, 1, 1), 0.15f);
        }
    }
}