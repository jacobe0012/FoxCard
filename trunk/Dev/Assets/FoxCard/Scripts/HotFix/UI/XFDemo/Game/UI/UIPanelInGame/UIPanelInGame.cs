//---------------------------------------------------------------------
// Author: xxx
// Time: #CreateTime#
//---------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using cfg.config;
using Cysharp.Threading.Tasks;
using HotFix_UI;
using Main;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace XFramework
{
    [UIEvent(UIType.UIPanelInGame)]
    internal sealed class UIPanelInGameEvent : AUIEvent, IUILayer
    {
        public override string Key => UIPathSet.UIPanelInGame;

        public override bool IsFromPool => true;

        public override bool AllowManagement => true;

        public UILayer Layer => UILayer.Low;

        public override UI OnCreate()
        {
            return UI.Create<UIPanelInGame>();
        }
    }

    public partial class UIPanelInGame : UI, IAwake
    {
        public List<Card> currentSelectedCards = new List<Card>();
        public List<UI> currentSelectedCardsUI = new List<UI>();
        public List<Card> randomSelectedList = new List<Card>();

        private Tblanguage tblanguage;
        private Tbcard_group tbcard_group;

        /// <summary>
        /// 最大出牌数量
        /// </summary>
        private const int MaxPlayedNum = 5;

        private CardGroupType cardGroupType;

        public void Initialize()
        {
            InitJson();


            InitPanel();
        }

        void InitJson()
        {
            tblanguage = ConfigManager.Instance.Tables.Tblanguage;
            tbcard_group = ConfigManager.Instance.Tables.Tbcard_group;
        }

        private void InitPanel()
        {
            var KBtn_Play = GetFromReference(UIPanelInGame.KBtn_Play);
            var KBtn_DisCard = GetFromReference(UIPanelInGame.KBtn_DisCard);
            var KBtn_SortPoints = GetFromReference(UIPanelInGame.KBtn_SortPoints);
            var KBtn_SortColor = GetFromReference(UIPanelInGame.KBtn_SortColor);
            var KBtn_HandInfo = GetFromReference(UIPanelInGame.KBtn_HandInfo);
            var KHorizontalPos_HandArea = GetFromReference(UIPanelInGame.KHorizontalPos_HandArea);
            var KText_CardType = GetFromReference(UIPanelInGame.KText_CardType);
            var KText_ScoreAdd = GetFromReference(UIPanelInGame.KText_ScoreAdd);
            var KText_ScoreMul = GetFromReference(UIPanelInGame.KText_ScoreMul);
            var KText_Grade = GetFromReference(UIPanelInGame.KText_Grade);


            KText_ScoreAdd.GetTextMeshPro().SetTMPText(0.ToString());
            KText_ScoreMul.GetTextMeshPro().SetTMPText(0.ToString());
            KText_CardType.GetTextMeshPro().SetTMPText("");
            KText_Grade.GetTextMeshPro().SetTMPText("");
            randomSelectedList = PlayerSingleton.Instance.allCards.OrderBy(item => Random.value)
                .Take(PlayerSingleton.Instance.handSize).ToList();
            CreateCardsList().Forget();
            // CreateHandAreaList().Forget();

            DoTweenEffect.DoScaleTweenOnClickAndLongPress(KBtn_Play, () => OnBtnPlayClick());

            DoTweenEffect.DoScaleTweenOnClickAndLongPress(KBtn_SortPoints, () => { SortHandCards(); });
            DoTweenEffect.DoScaleTweenOnClickAndLongPress(KBtn_SortColor, () => { SortHandCards(true); });

            DoTweenEffect.DoScaleTweenOnClickAndLongPress(KBtn_DisCard, () =>
            {
                if (currentSelectedCards.Count <= 0)
                {
                    return;
                }

                PlayerSingleton.Instance.allCards =
                    PlayerSingleton.Instance.allCards.Except(randomSelectedList).ToList();
                randomSelectedList = randomSelectedList.Except(currentSelectedCards).ToList();

                currentSelectedCards.Clear();
                currentSelectedCardsUI.Clear();
                int num = PlayerSingleton.Instance.handSize - randomSelectedList.Count;

                var newNumList = PlayerSingleton.Instance.allCards.OrderBy(item => Random.value).Take(num).ToList();
                randomSelectedList.AddRange(newNumList);
                CreateCardsList().Forget();
            });
        }


        async UniTaskVoid CreateCardsList()
        {
            var KHorizontalPos_HandArea = GetFromReference(UIPanelInGame.KHorizontalPos_HandArea);
            var KText_CardType = GetFromReference(UIPanelInGame.KText_CardType);
            var KText_ScoreAdd = GetFromReference(UIPanelInGame.KText_ScoreAdd);
            var KText_ScoreMul = GetFromReference(UIPanelInGame.KText_ScoreMul);
            var KText_Grade = GetFromReference(UIPanelInGame.KText_Grade);
            var list = KHorizontalPos_HandArea.GetList();
            list.Clear();
            //Log.Debug($"1 asfasf");
            foreach (var item in randomSelectedList)
            {
                //Log.Debug($"{item} asfasf");
                var ui = await list.CreateWithUITypeAsync(UIType.UICard, item, false) as UICard;
                var KBtn_Card = ui.GetFromReference(UICard.KBtn_Card);
                KBtn_Card.GetRectTransform().SetAnchoredPosition(Vector2.zero);
                DoTweenEffect.DoScaleTweenOnClickAndLongPress(KBtn_Card, () =>
                {
                    if (ui.isSelected)
                    {
                        currentSelectedCards.Remove(item);
                        currentSelectedCardsUI.Remove(ui);
                        KBtn_Card.GetRectTransform().DoAnchoredPositionY(0, 0.2f);
                    }
                    else
                    {
                        if (currentSelectedCards.Count >= MaxPlayedNum)
                        {
                            return;
                        }

                        currentSelectedCards.Add(item);
                        currentSelectedCardsUI.Add(ui);
                        KBtn_Card.GetRectTransform().DoAnchoredPositionY(50, 0.2f);
                    }

                    ui.isSelected = !ui.isSelected;
                    cardGroupType = CardUtility.DetectCardGroupType(currentSelectedCards);

                    KText_CardType.GetTextMeshPro()
                        .SetTMPText(tblanguage.Get(tbcard_group.Get((int)cardGroupType).langId).current);
                    KText_ScoreAdd.GetTextMeshPro()
                        .SetTMPText(PlayerSingleton.Instance.allCardGroups[cardGroupType].scoreAdd.ToString());
                    KText_ScoreMul.GetTextMeshPro()
                        .SetTMPText(PlayerSingleton.Instance.allCardGroups[cardGroupType].scoreMul.ToString());
                    int level = PlayerSingleton.Instance.allCardGroups[cardGroupType].level;

                    KText_Grade.GetTextMeshPro().SetTMPText($"{tblanguage.Get("common_grade").current}{level}");


                    if (currentSelectedCards.Count <= 0)
                    {
                        KText_CardType.GetTextMeshPro().SetTMPText("");
                        KText_Grade.GetTextMeshPro().SetTMPText("");
                        KText_ScoreAdd.GetTextMeshPro().SetTMPText(0.ToString());
                        KText_ScoreMul.GetTextMeshPro().SetTMPText(0.ToString());
                    }
                });
            }

            list.Sort((a, b) =>
            {
                var uia = a as UICard;
                var uib = b as UICard;
                return uib.card.points.CompareTo(uia.card.points);
            });
            //JiYuUIHelper.ForceRefreshLayout(KHorizontalPos_HandArea);
        }

        private void SortHandCards(bool sortByColor = false)
        {
            var KHorizontalPos_HandArea = GetFromReference(UIPanelInGame.KHorizontalPos_HandArea);
            var list = KHorizontalPos_HandArea.GetList();
            list.Sort((a, b) =>
            {
                var uia = a as UICard;
                var uib = b as UICard;
                if (sortByColor)
                {
                    return uia.card.color.CompareTo(uib.card.color);
                }

                return uib.card.points.CompareTo(uia.card.points);
            });
        }

        private void OnBtnPlayClick()
        {
            // Log.Debug($"before");
            // foreach (var VARIABLE in currentSelectedCards)
            // {
            //     Log.Debug($"{VARIABLE.points}");
            // }
            PlayCardsAnimation().Forget();


            var playerBuffs = PlayerSingleton.Instance.allBuffs;
            //溅射小丑牌
            bool containsSubClass = playerBuffs.OfType<Buff_101>().Any();

            foreach (var buff in playerBuffs)
            {
                if (!containsSubClass)
                {
                    var effectiveCards = CardUtility.GetEffectiveCards(cardGroupType, currentSelectedCards);
                    buff.OnPlayCard(effectiveCards);
                    //Log.Error($"effective");
                    foreach (var VARIABLE in effectiveCards)
                    {
                        //Log.Error($"{VARIABLE.points}");
                    }
                }
                else
                {
                    currentSelectedCards.Sort((a, b) => { return b.points.CompareTo(a.points); });
                    buff.OnPlayCard(currentSelectedCards);
                    //Log.Error($"currentSelectedCards");
                    foreach (var VARIABLE in currentSelectedCards)
                    {
                        //Log.Error($"{VARIABLE.points}");
                    }
                }
            }

            var curScore = PlayerSingleton.Instance.allCardGroups[cardGroupType].scoreAdd +
                           PlayerSingleton.Instance.currentScoreAdd;
        }

        private async UniTask PlayCardsAnimation()
        {
            currentSelectedCardsUI.Sort((a, b) =>
            {
                var uia = a as UICard;
                var uib = b as UICard;
                return uib.card.points.CompareTo(uia.card.points);
            });

            const float CardWidth = 200f;

            const float EveryCardAniTime = 0.15f;
            int width = Screen.width;
            int count = currentSelectedCardsUI.Count;
            float interval = (width - 5 * CardWidth) / 6f;
            // var KHorizontalPos_PlayArea = GetFromReference(UIPanelInGame.KHorizontalPos_PlayArea);
            // var list = KHorizontalPos_PlayArea.GetList();
            //Log.Error($"posLeftUp{posLeftUp}");
            //1:0   2:int/2 + cw/2 3:int +cw 4:1.5f*int+1.5f*cw 5:2*int+2*cw
            //-1-1-1-1-1-
            for (int i = 0; i < count; i++)
            {
                int index = i;
                float cardX = 0;
                // float cardX = (interval + CardWidth) * (index + 1) - CardWidth / 2f;
                // cardX += (5 - count) * (interval + CardWidth) / 2f;

                if ((index + 1) % 2 == 1)
                {
                    cardX = (CardWidth + interval) * ((index + 1) / 2);
                }
                else
                {
                    cardX = (CardWidth + interval) / 2f * ((index + 1) - 1);
                }

                cardX += width / 2f;
                //cardX = width / 2f;
                //cardX -= Screen.width / 2f;
                //Log.Error($"cardX{cardX}");
                var ui = currentSelectedCardsUI[index];
                var uipos = ui.GetRectTransform().AnchoredPosition();
                var uibtn = ui.GetFromReference(UICard.KBtn_Card);
                //posLeftUp-new Vector2(cardX, 500), EveryCardAniTime

                var uibtnRec = uibtn.GetRectTransform();
                var rightOffset = new Vector2(cardX, 158f) - uipos;

                var dir = math.normalize(rightOffset - uibtnRec.AnchoredPosition());
                var angle = Vector2.SignedAngle(Vector2.up, dir);

                //Log.Error($"dir{dir}angle{angle}");

                var qua = quaternion.AxisAngle(new Vector3(0, 0, 1), math.radians(angle));


                uibtnRec.SetRotation(qua);
                uibtnRec.DoAnchoredPosition(rightOffset, EveryCardAniTime).AddOnCompleted(
                    async () =>
                    {
                        uibtnRec.SetRotation(Quaternion.identity);
                        for (int j = 0; j < count - (index + 1); j++)
                        {
                            if ((index + 1) != count)
                            {
                                var finalPos = uibtnRec.AnchoredPosition() +
                                               new Vector2(-(interval + CardWidth) / 2f, 0);
                                uibtnRec.DoAnchoredPosition(finalPos, EveryCardAniTime);

                                float randomAngle = UnityEngine.Random.Range(10f, 20f);
                                uibtnRec.DoLocalEulerAngleZ(0, randomAngle, EveryCardAniTime / 2f).AddOnCompleted(() =>
                                {
                                    uibtnRec.DoLocalEulerAngleZ(randomAngle, 0, EveryCardAniTime / 2f);
                                });
                                await UniTask.Delay((int)(EveryCardAniTime * 1000f));
                            }
                        }

                        //uibtn.SetActive(false);
                        // uibtnRec.SetAnchoredPosition(Vector2.zero);
                        // ui.GameObject.transform.SetParent(KHorizontalPos_PlayArea.GameObject.transform);
                        // ui.SetParent(KHorizontalPos_PlayArea, false);
                        //uibtn.SetActive(true);
                    });


                await UniTask.Delay((int)(EveryCardAniTime * 1000f));
                // 使用 cardX 作为每个卡牌的 x 坐标
            }
        }


        protected override void OnClose()
        {
            base.OnClose();
        }
    }
}