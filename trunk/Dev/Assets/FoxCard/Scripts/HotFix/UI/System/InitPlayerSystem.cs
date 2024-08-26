// //---------------------------------------------------------------------
// // JiYuStudio
// // Author: 格伦
// // Time: 2023-09-27 12:06:32
// //---------------------------------------------------------------------
//
// using System;
// using System.Linq;
// using System.Threading;
// using cfg.config;
// using Common;
// using Google.Protobuf;
// using HotFix_UI;
// using Main;
// using Spine.Unity;
// using Unity.Collections;
// using Unity.Entities;
// using Unity.Mathematics;
// using Unity.Physics;
// using Unity.Transforms;
// using UnityEditor;
// using UnityEngine;
// using XFramework;
// using Material = UnityEngine.Material;
//
// namespace HotFix_UI
// {
//     //初始化玩家系统 
//     //初始化完成才会添加WorldBlackBoardTag
//     [DisableAutoCreation]
//     [UpdateInGroup(typeof(InitializationSystemGroup))]
//     [UpdateBefore(typeof(HandleInputSystem))]
//     public partial class InitPlayerSystem : SystemBase
//     {
//         private BattleProperty mybattleProperty;
//
//         protected override void OnCreate()
//         {
//             RequireForUpdate(SystemAPI.QueryBuilder().WithAll<PrefabMapData, GlobalConfigData>()
//                 .WithNone<WorldBlackBoardTag>().Build());
//         }
//
//
//         protected override void OnUpdate()
//         {
//             var global = XFramework.Common.Instance.Get<Global>();
//
//             if (global.isStandAlone)
//             {
//                 mybattleProperty = new BattleProperty
//                 {
//                     Properties =
//                     {
//                         "103030;0", "104000;0", "102020;0", "103010;10", "106220;0", "106120;0", "102000;100",
//                         "102120;0", "110000;0", "102100;0", "108000;10", "107010;28000", "117000;0", "115000;0",
//                         "101420;0", "101520;0", "111000;0", "113000;0", "101600;0", "105000;0", "105100;20000",
//                         "102130;0", "107100;1500", "102030;0", "103020;0", "106230;0", "107000;28000", "102110;0",
//                         "102010;100", "103000;10", "101220;0", "101320;0", "109000;2000", "106130;0", "107020;0",
//                         "118200;0", "116000;0", "118100;0", "114000;0", "112000;0"
//                     },
//                     WeaponId = 101,
//                     Skills = { 100101 }
//                 };
//             }
//
//             if (mybattleProperty == null) return;
//             var sbe = SystemAPI.GetSingletonEntity<PrefabMapData>();
//             var prefabMapData = SystemAPI.GetComponent<PrefabMapData>(sbe);
//             var config = SystemAPI.GetComponent<GlobalConfigData>(sbe);
//
//             var player = EntityManager.Instantiate(prefabMapData.prefabHashMap["Player"]);
//
//             var playerHybridData = EntityManager.GetComponentData<SpineHybridTempData>(player);
//
//
//             var chaStats = new ChaStats();
//             var playerData = new PlayerData();
//             ref var uservarConfig =
//                 ref config.value.Value.configTbattr_variables.configTbattr_variables;
//
//             Log.Debug($"前端处理后端数据>>>", Color.green);
//
//             var skillConfig = ConfigManager.Instance.Tables.Tbskill;
//             var constConfig = ConfigManager.Instance.Tables.Tbconstant;
//             int equip_default_weapon_id = constConfig.Get("equip_default_weapon_id").constantValue;
//             int equip_default_weapon_skill = constConfig.Get("equip_default_weapon_skill").constantValue;
//             var skills = EntityManager.GetBuffer<Skill>(player);
//             var playerEquipSkills = EntityManager.GetBuffer<PlayerEquipSkillBuffer>(player);
//
//
//             Log.Debug($"后端传入数据Properties", Color.cyan);
//             JiYuUIHelper.InitPlayerProperty(ref playerData, mybattleProperty);
//             Log.Debug($"{mybattleProperty.Properties}", Color.green);
//             Log.Debug($"后端传入数据Skills", Color.cyan);
//             //武器技能 1 00000开头
//             //装备词条 4 00000开头
//             //天赋技能 7 00000开头
//             Log.Debug($"技能:{mybattleProperty.Skills}", Color.green);
//             var skillList = skillConfig.DataList;
//
//             foreach (var skill in mybattleProperty.Skills)
//             {
//                 int sixthNumber = skill / 100000;
//
//                 if (sixthNumber == 1)
//                 {
//                     skills.Add(new Skill
//                     {
//                         CurrentTypeId = (Skill.TypeId)skill,
//                         Entity_5 = player,
//                     });
//                 }
//                 else
//                 {
//                     playerEquipSkills.Add(new PlayerEquipSkillBuffer
//                     {
//                         id = skill
//                     });
//                 }
//             }
//
//             #region 测试用
//
//             //
//             // for (int i = 0; i < skillList.Count; i++)
//             // {
//             //     if (skillList[i].id / 100000 == 4 && skillList[i].calculateYn == 1)
//             //     {
//             //         if (skillList[i].id >= 406013 && skillList[i].id <= 406016)
//             //             playerEquipSkills.Add(new PlayerEquipSkillBuffer { id = skillList[i].id });
//             //     }
//             // }
//             //
//             // //添加装备技能
//             // AddEquipSkill(playerEquipSkills, skills, player);
//             //skills.Clear();
//
//             #endregion
//
//             //
//             //playerData.defaultProperty.maxMoveSpeed *= 10;
//             chaStats.chaProperty = playerData.defaultProperty;
//
//             chaStats.chaResource.actionSpeed = 1;
//             chaStats.chaResource.hp = playerData.defaultProperty.maxHp;
//             chaStats.chaResource.direction = math.up();
//
//
//             //TODO: 删掉
//             //skills.Add(new Skill
//             //{
//             //    CurrentTypeId = (Skill.TypeId)220201,
//             //    Entity_5 = player
//             //});
//             //skills.Add(new Skill_210601
//             //{
//             //    caster = default,
//             //    duration = 1,
//             //    cooldown = 1,
//             //    target = default,
//             //    tick = 0,
//             //    timeScale = 1,
//             //    id = 210605,
//             //    level = 1
//             //}.ToSkill());
//
//             var mass = EntityManager.GetComponentData<PhysicsMass>(player);
//             mass.InverseMass = 1f / (float)playerData.defaultProperty.mass;
//
//             //ResourcesSingleton.Instance.playerProperty.chaProperty = chaStats.chaProperty;
//             ResourcesSingleton.Instance.playerProperty.playerData = playerData;
//
//             EntityManager.SetComponentData(player, mass);
//             EntityManager.SetComponentData(player, chaStats);
//             EntityManager.SetComponentData(player, playerData);
//
//
//             //InitWeaponShow
//             GameObject tmpObject = GameObject.Instantiate(playerHybridData.prefab);
//             SkeletonAnimation animator = tmpObject.GetComponent<SkeletonAnimation>();
//
//             global.VirtualCamera.Follow = tmpObject.transform;
//             //Material material = tmpObject.GetComponent<MeshRenderer>().material;
//
//             if (mybattleProperty.WeaponId == 0)
//             {
//                 Log.Debug($"默认WeaponId:{equip_default_weapon_id}", Color.green);
//                 animator.skeleton.SetAttachment("Weapon", equip_default_weapon_id.ToString());
//             }
//             else
//             {
//                 Log.Debug($"后端传入WeaponId:{mybattleProperty.WeaponId}", Color.green);
//                 if (mybattleProperty.WeaponId == 101 || mybattleProperty.WeaponId == 201)
//                 {
//                     animator.skeleton.SetAttachment("Weapon",
//                         mybattleProperty.WeaponId.ToString());
//                 }
//             }
//
//             EntityManager.AddComponentData(player, new SpineHybridData()
//             {
//                 go = tmpObject,
//                 skeletonAnimation = animator
//             });
//             EntityManager.RemoveComponent<SpineHybridTempData>(player);
//
//             //给玩家添加自定义DIYbuff 
//             var buffs = EntityManager.GetBuffer<Buff>(player);
//             buffs.Add(new Buff_TurnPushForceAtk
//             {
//                 id = 0,
//                 priority = 0,
//                 maxStack = 0,
//                 tags = 0,
//                 tickTime = 0.02f,
//                 timeElapsed = 0,
//                 ticked = 0,
//                 duration = 0,
//                 permanent = true,
//                 caster = default,
//                 carrier = player,
//                 canBeStacked = false,
//                 buffStack = default,
//                 buffArgs = default,
//                 totalMoveDistance = 0,
//                 xMetresPerInvoke = 0,
//                 lastPosition = default,
//                 lastpushForcePlus = 0,
//                 lastatkPlus = 0
//             }.ToBuff());
//
//             //TODO:删掉
//
//
//             EntityManager.AddComponent<WorldBlackBoardTag>(sbe);
//             Enabled = false;
//         }
//
//         private void AddEquipSkill(DynamicBuffer<PlayerEquipSkillBuffer> playerEquipSkills, DynamicBuffer<Skill> skills,
//             Entity player)
//         {
//             for (int i = 0; i < playerEquipSkills.Length; i++)
//             {
//                 if (playerEquipSkills[i].id / 1000 != 401)
//                 {
//                     Debug.Log($"id:{playerEquipSkills[i].id}");
//                     skills.Add(new Skill
//                     {
//                         CurrentTypeId = (Skill.TypeId)playerEquipSkills[i].id,
//                         Entity_5 = player,
//                     });
//                 }
//             }
//         }
//
//         void OnClickPlayBtnFinishResponse(object sender, WebMessageHandler.Execute e)
//         {
//             WebMessageHandler.Instance.RemoveHandler(2, 7, OnClickPlayBtnFinishResponse);
//
//             var battleProperty = new BattleProperty();
//             battleProperty.MergeFrom(e.data);
//
//             if (e.data.IsEmpty)
//             {
//                 Log.Debug("e.data.IsEmpty", Color.red);
//                 return;
//             }
//
//             mybattleProperty = battleProperty;
//             // if (battleProperty.IsEmpty)
//             // {
//             //     Log.Debug($"battleProperty.IsEmpty", Color.red);
//             // }
//
//
//             //Log.Debug($"WeaponId:{battleProperty.WeaponId}", Color.red);
//
//             //Log.Debug($"{battleProperty.Properties.Count}", Color.red);
//             foreach (var prop in battleProperty.Properties)
//             {
//                 //prop.Split()
//                 //Log.Debug($"battleProperty.Properties:{prop}", Color.red);
//             }
//
//             foreach (var skill in battleProperty.Skills)
//             {
//                 //Log.Debug($"battleProperty.skills:{skill}", Color.red);
//             }
//         }
//
//         protected override void OnStartRunning()
//         {
//             Log.Debug($"InitPlayerSystem Start", Color.cyan);
//
//             WebMessageHandler.Instance.AddHandler(2, 7, OnClickPlayBtnFinishResponse);
//             NetWorkManager.Instance.SendMessage(2, 7);
//         }
//
//         protected override void OnStopRunning()
//         {
//             Log.Debug($"InitPlayerSystem Stop", Color.cyan);
//             mybattleProperty = null;
//         }
//     }
// }