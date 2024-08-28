//---------------------------------------------------------------------
// JiYuStudio
// Author: 格伦
// Time: 2023-11-03 11:25:25
//---------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Cysharp.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using XFramework;


namespace HotFix_UI
{
    /// <summary>
    /// 存放一些通用UI的函数
    /// </summary>
    public static class JiYuUIHelper
    {

       

        public static bool IsCompositeEquipReward(Vector3 reward)
        {
            var rewardx = (int)reward.x;
            var rewardy = (int)reward.y;
            var rewardz = (int)reward.z;

            if (rewardx == 11 && rewardy % 100 == 0)
                return true;
            return false;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetFrameRate(bool isMax = false, int targetFrameRate = 60)
        {
            if (isMax)
            {
#if UNITY_ANDROID || UNITY_IOS
                int maxRefreshRate = Screen.currentResolution.refreshRate;
                Application.targetFrameRate = maxRefreshRate;
#else
                Application.targetFrameRate = -1;
#endif

                return;
            }

            Application.targetFrameRate = targetFrameRate;
        }


        public enum DefaultRectType
        {
            /// <summary>
            /// 默认居中
            /// </summary>
            Normal,

            /// <summary>
            /// 延展
            /// </summary>
            Expand
        }


        /// <summary>
        /// 尝试拿到当前页面的UI脚本
        /// </summary>
        /// <param name="uiType">ui类型字符串</param>
        /// <param name="ui">ui脚本</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetUI(string uiType, out UI ui)
        {
            ui = null;
            var uiManager = XFramework.Common.Instance?.Get<UIManager>();
            if (uiManager.TryGet(uiType, out var ui0))
            {
                ui = ui0;
                return true;
            }

            //Log.Debug($"没有拿到UI");
            return false;
        }


        /// 销毁某个父节点下的所有子节点,一般用于公共UI的销毁
        /// </summary>
        /// <param name="parent">某个父级节点</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DestroyAllChildren(UI parent)
        {
            // foreach (var item in UICommon_Labels)
            // {
            //     //UnityEngine.GameObject.DestroyImmediate(item.GameObject);
            // }
            var parentGo = parent.GameObject;
            int childCount = parentGo.transform.childCount;

            for (int i = childCount - 1; i >= 0; i--)
            {
                GameObject child = parentGo.transform.GetChild(i).gameObject;
                UnityEngine.GameObject.Destroy(child);
            }
        }


        /// <summary>
        /// 强制刷新UI布局
        /// </summary>
        /// <param name="parent">UI父节点</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ForceRefreshLayout(UI parent)
        {
            if (parent == null)
            {
                Log.Error($"parent is null");
                return;
            }

            RectTransform rectTransform = parent.GetComponent<RectTransform>();

            LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
        }


        /// <summary>
        /// 获取当前UI的基于Canvas的坐标
        /// 坐标原点为屏幕中心
        /// </summary>
        /// <param name="ui">当前UI</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 GetUIPos(UI ui)
        {
            Canvas canvas = ui.GameObject.transform.GetComponentInParent<Canvas>();
            if (canvas == null)
            {
                Log.Error($"没找到父节点有Canvas组件");
                return default;
            }

            Vector3 uiPos =
                canvas.transform.InverseTransformPoint(ui.GetRectTransform().Position());

            return uiPos;
        }

        /// <summary>
        /// 获取GameObject基于Canvas的坐标
        /// </summary>
        /// <param name="go">当前GameObject</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 GetGOPos(GameObject go)
        {
            if (go == null)
            {
                Log.Error($"go 为空");
                return default;
            }

            Canvas canvas = go.transform.GetComponentInParent<Canvas>();
            if (canvas == null)
            {
                Log.Error($"没找到父节点有Canvas组件");
                return default;
            }

            Vector3 uiPos =
                canvas.transform.InverseTransformPoint(go.transform.position);

            return uiPos;
        }


        /// <summary>
        /// 设置通用弹出TipUI的位置
        /// 预制件参考:Common_ItemTips
        /// </summary>
        /// <param name="itemUI">物品UI,通常是点击此物品才弹出TipUI</param>
        /// <param name="tipUI">弹出的TipUI</param>
        /// <param name="contentKey">正文气泡内容GO的key</param>
        /// <param name="arrowKey">Tip窗口正下方的箭头GO的key</param>
        /// <param name="contentGap">正文内容距离屏幕左右的间隔,单位:像素</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetTipPos(UI itemUI, UI tipUI, string contentKey, string arrowDownKey, string arrowUpKey,
            float contentGap = 30f)
        {
            var arrowDown = tipUI.GetFromReference(arrowDownKey);
            var arrowUp = tipUI.GetFromReference(arrowUpKey);


            var itemRect = itemUI.GetRectTransform();
            var tipRect = tipUI.GetRectTransform();
            bool isUp = false;

            isUp = GetUIPos(itemUI).y > Screen.height / 4f ? true : false;


            ScrollRect scrollRect = itemUI.GameObject.transform.GetComponentInParent<ScrollRect>();

            Canvas canvas = itemUI.GameObject.transform.GetComponentInParent<Canvas>();

            // var rewardPosX = itemRect.AnchoredPosition().x;
            // var parentPos = scrollRect.content.anchoredPosition;


            RectTransform canvasRect = canvas.transform.GetComponent<RectTransform>();
            Vector3 loadpos = canvas.transform.InverseTransformPoint(itemUI.GameObject.transform.position);


            if (scrollRect != null)
            {
                Vector3 scrollRectPos =
                    canvas.transform.InverseTransformPoint(scrollRect.transform.position);
                var scrollRectWidth = scrollRect.transform.GetComponent<RectTransform>().rect.width;
                var scrollRectLeftPos = scrollRectPos.x - scrollRectWidth / 2f;
                var scrollRectRightPos = scrollRectPos.x + scrollRectWidth / 2f;

                loadpos.x = math.clamp(loadpos.x, scrollRectLeftPos, scrollRectRightPos);
            }

            var itemUpOffset = itemRect.Width() / 2f * itemRect.Scale().x;
            var tipUpOffset = tipRect.Height() / 2f * tipRect.Scale().y;

            float offsetY = itemUpOffset + tipUpOffset;

            if (isUp)
            {
                offsetY -= itemRect.Width() * itemRect.Scale().x + tipRect.Height() * tipRect.Scale().y;
                arrowDown.SetActive(false);
                arrowUp.SetActive(true);
            }
            else
            {
                arrowDown.SetActive(true);
                arrowUp.SetActive(false);
            }


            var tipPos = new Vector3(loadpos.x, loadpos.y + offsetY);


            tipUI.GetRectTransform().SetAnchoredPosition(tipPos);
            var tipWidth = tipRect.Width() * tipRect.Scale().x;

            var arrow = tipUI.GetFromReference(arrowDownKey).GetRectTransform();
            var arrowWidth = arrow.Width() * arrow.Scale().x;


            var screenPosL = -(Screen.width / 2f);
            var screenPosR = Screen.width / 2f;
            var tipPosL = loadpos.x - tipWidth / 2;
            var tipPosR = loadpos.x + tipWidth / 2;
            var contentRect = tipUI.GetFromReference(contentKey).GetRectTransform();

            if (tipPosL < screenPosL + contentGap)
            {
                var contentPos = math.length(tipPosL) - math.length(screenPosL + contentGap);
                contentPos = math.min(contentPos, tipWidth / 2f - arrowWidth / 2f);

                contentRect.SetAnchoredPosition(new Vector2(contentPos, 0));
            }
            else if (tipPosR > screenPosR - contentGap)
            {
                var contentPos = math.length(tipPosR) - math.length(screenPosR - contentGap);
                contentPos = math.min(contentPos, tipWidth / 2f - arrowWidth / 2f);

                contentRect.SetAnchoredPosition(new Vector2(-contentPos, 0));
            }
            else
            {
                contentRect.SetAnchoredPosition(Vector2.zero);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string FormatNumber(int value)
        {
            if (value >= 1000 && value < 1000000)
            {
                // 将数字除以1000，并保留一位小数
                double formattedValue = System.Math.Round((double)value / 1000, 1);
                return $"{formattedValue}K";
            }
            else if (value >= 1000000)
            {
                // 将数字除以1000000，并保留一位小数
                double formattedValue = System.Math.Round((double)value / 1000000, 1);
                return $"{formattedValue}M";
            }
            else
            {
                return $"{value}";
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void SetCountText(UI countUI, int count)
        {
            if (count <= 1)
            {
                countUI.SetActive(false);
            }
            else
            {
                countUI.SetActive(true);
                countUI.GetTextMeshPro().SetTMPText($"x {FormatNumber(count)}");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsResourceReward(Vector3 reward)
        {
            var rewardx = (int)reward.x;
            var rewardy = (int)reward.y;
            var rewardz = (int)reward.z;
            if (rewardx >= 1 && rewardx <= 4)
                return true;
            return false;
        }


        /// <summary>
        /// 迅捷蟹
        /// 设置通用弹出TipsUI的位置,使用公用UI,且为相对位置
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetTipPos(UI tipUI, string contentKey, string arrowKey, float contentGap = 30f)
        {
            UI ContentUI = tipUI.GetFromReference(contentKey);
            UI arrowUI = tipUI.GetFromReference(arrowKey);

            Vector3 ContentRect = ContentUI.GetComponent<RectTransform>().anchoredPosition;
            Vector3 arrowRect = arrowUI.GetComponent<RectTransform>().anchoredPosition;

            ContentRect = new Vector3(ContentRect.x - contentGap, ContentRect.y, ContentRect.z);
            arrowRect = new Vector3(ContentRect.x + contentGap, arrowRect.y, arrowRect.z);

            tipUI.GetRectTransform(contentKey).SetAnchoredPosition3D(ContentRect);
            tipUI.GetRectTransform(arrowKey).SetAnchoredPosition3D(arrowRect);
        }


        /// <summary>
        /// 剔除rewardList中数量为0的reward串
        /// </summary>
        /// <param name="rewardList"></param>
        /// <returns></returns>
        public static List<Vector3> RewardRemoveEmptiness(List<Vector3> rewardList)
        {
            List<Vector3> vector3s = new List<Vector3>();
            for (int i = 0; i < rewardList.Count; i++)
            {
                if (rewardList[i].z == 0)
                {
                    vector3s.Add(rewardList[i]);
                }
            }

            for (int i = 0; i < vector3s.Count; i++)
            {
                rewardList.Remove(vector3s[i]);
            }

            return rewardList;
        }

        /// <summary>
        /// 通过链接下载文件到指定位置
        /// </summary>
        /// <param name="url">下载链接</param>
        /// <param name="localPath">存储地址</param>
        public static async UniTask DownloadByUrl(string url, string localPath)
        {
            if (url == null || localPath == null)
            {
                Log.Debug("url or localPath is null");
                return;
            }

            UnityWebRequest webRequest = new UnityWebRequest(url);
            DownloadHandlerFile download = new DownloadHandlerFile(localPath);
            webRequest.downloadHandler = download;
            await webRequest.SendWebRequest().ToUniTask();
            if (webRequest.isDone)
            {
                if (webRequest.result == UnityWebRequest.Result.ProtocolError
                    || webRequest.result == UnityWebRequest.Result.ConnectionError
                    || webRequest.result == UnityWebRequest.Result.DataProcessingError)
                {
                    //文件下载失败
                    Log.Debug("下载失败");
                }
                else
                {
                    //文件下载成功
                    Log.Debug("下载成功");
                }
            }
        }


        /// <summary>
        /// 将输入的string裁剪为最后一个“/”后的内容
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetNameStringFormUrl(string input)
        {
            if (input == null)
            {
                Log.Error("the input url is null");
                return null;
            }
            else
            {
                int lastSlashIndex = input.LastIndexOf("/");
                if (lastSlashIndex != -1)
                {
                    return input.Substring(lastSlashIndex + 1);
                }
                else
                {
                    Log.Debug("input do not have \"/\"");
                    return input;
                }
            }
        }

        /// <summary>
        /// 把List<notice>转变为Dictionary<id, Notice>方便查找
        /// </summary>
        /// <param name="input">输入的List<notice></param>
        /// <returns>Dictionary<id, Notice></returns>
        public static Dictionary<int, Notice> NoticeListToDic(List<Notice> input)
        {
            if (input == null)
            {
                return null;
            }

            Dictionary<int, Notice> resoult = new Dictionary<int, Notice>();
            for (int i = 0; i < input.Count; i++)
            {
                resoult.Add(input[i].id, input[i]);
            }

            return resoult;
        }


        #region 技能选择函数待做

//  private void UpdatePlayerSkill(int id)
// {
//     var userSkillTable = ConfigManager.Instance.Tables.Tbplayer_skill.DataMap;
//     var skillTable = ConfigManager.Instance.Tables.Tbskill.DataMap;
//     var skillEffectTable = ConfigManager.Instance.Tables.Tbskill_effect.DataMap;
//     int skillEffectID = skillTable[id].effectGroup[0];
//
//
//     //Log.Debug(buff1Args.ToString(), Color.green);
//     float coldDown = skillTable[id].cd / 1000f;
//
//
//     if (manager.HasBuffer<Skill>(player))
//     {
//         skills = manager.GetBuffer<Skill>(player);
//     }
//
//     for (int i = 0; i < skills.Length; i++)
//     {
//         if (userSkillTable[skills[i].Int32_0].group == userSkillTable[id].group)
//         {
//             var temp = skills[i];
//             temp.Int32_0 = id;
//             skills[i] = temp;
//             return;
//             //替换
//         }
//     }
//
//     if (id == 240201)
//     {
//         skills.Add(new Skill_240201
//         {
//             caster = default,
//             duration = coldDown,
//             cooldown = coldDown,
//             target = default,
//             tick = 0,
//             timeScale = 1,
//             id = id,
//             level = 1
//         }.ToSkill());
//     }
//
//     //添加一个技能或者buff
//     else if (id == 220101)
//     {
//         skills.Add(new SkillLightRing
//         {
//             caster = default,
//             duration = coldDown,
//             cooldown = coldDown,
//             target = default,
//             tick = 0,
//             timeScale = 1,
//             id = id,
//             level = 1
//         }.ToSkill());
//     }
//     else if (id == 220201)
//     {
//         skills.Add(new SkillGenerateTrap
//         {
//             caster = default,
//             duration = coldDown,
//             cooldown = coldDown,
//             target = default,
//             tick = 0,
//             timeScale = 1,
//             id = id,
//             level = 1
//         }.ToSkill());
//     }
//     else if (id == 210101)
//     {
//         skills.Add(new SkillMotoCrash
//         {
//             caster = default,
//             duration = coldDown,
//             cooldown = coldDown,
//             target = default,
//             tick = 0,
//             timeScale = 1,
//             id = id,
//             level = 1
//         }.ToSkill());
//     }
//     else if (id == 210201)
//     {
//         skills.Add(new Skill_210201
//         {
//             caster = default,
//             duration = coldDown,
//             cooldown = coldDown,
//             target = default,
//             tick = 0,
//             timeScale = 1,
//             id = id,
//             level = 1
//         }.ToSkill());
//     }
//
//
//     if (manager.HasBuffer<Buff>(player))
//     {
//         buffs = manager.GetBuffer<Buff>(player);
//     }
//
//     for (int i = 0; i < buffs.Length; i++)
//     {
//         if (buffs[i].Int32_0 < 1)
//         {
//             continue;
//         }
//
//         if (userSkillTable[buffs[i].Int32_0].group == userSkillTable[id].group)
//         {
//             buffs.RemoveAt(i);
//             buffs = manager.GetBuffer<Buff>(player);
//         }
//     }
//
//     var group = userSkillTable[id].group;
//     if (group == 2001)
//     {
//         int buff1Args = skillEffectTable[skillEffectID].buff1Para[0];
//         int buff2Args = skillEffectTable[skillEffectID].buff1Para[1];
//         buffs.Add(new Buff_30000020
//         {
//             id = id,
//             carrier = player,
//             duration = -1,
//             permanent = true,
//             buffArgs = new BuffArgs
//             {
//                 args0 = buff1Args,
//                 args1 = buff2Args,
//                 args2 = 0,
//                 args3 = 0,
//                 args4 = 0
//             }
//         }.ToBuff());
//     }
//
//
//     if (group == 2301)
//     {
//         int buff1Args = skillEffectTable[skillEffectID].buff1Para[0];
//         buffs.Add(new Buff_202
//         {
//             id = id,
//             carrier = player,
//             duration = -1,
//             permanent = true,
//             buffArgs = new BuffArgs { args0 = buff1Args }
//         }.ToBuff());
//     }
//
//     if (group == 2302)
//     {
//         int buff1Args = skillEffectTable[skillEffectID].buff1Para[0];
//         buffs.Add(new Buff_902
//         {
//             id = id,
//             carrier = player,
//             duration = -1,
//             permanent = true,
//             buffArgs = new BuffArgs { args0 = buff1Args }
//         }.ToBuff());
//     }
//
//     if (group == 2303)
//     {
//         int buff1Args = skillEffectTable[skillEffectID].buff1Para[0];
//         buffs.Add(new Buff_302
//         {
//             id = id,
//             carrier = player,
//             duration = -1,
//             permanent = true,
//             buffArgs = new BuffArgs { args0 = buff1Args }
//         }.ToBuff());
//     }
//
//     if (group == 2401)
//     {
//         int buff1Args = skillEffectTable[skillEffectID].buff1Para[0];
//         buffs.Add(new Buff_1204
//         {
//             id = id,
//             carrier = player,
//             duration = -1,
//             permanent = true,
//             buffArgs = new BuffArgs { args0 = buff1Args / 10000 }
//         }.ToBuff());
//     }
//     //throw new NotImplementedException();
// }

        #endregion
    }
}