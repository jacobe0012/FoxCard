// //---------------------------------------------------------------------
// // JiYuStudio
// // Author: 格伦
// // Time: 2023-07-18 16:15:10
// //---------------------------------------------------------------------
//
//
// using System.Threading.Tasks;
// using cfg;
// using cfg.blobstruct;
// using cfg.item.blobstruct;
// using Main;
// using SimpleJSON;
// using Unity.Collections;
// using Unity.Entities;
// using Unity.Loading;
// using UnityEngine;
// using UnityEngine.SceneManagement;
// using YooAsset;
//
// namespace HotFix_Logic
// {
//     //TODO: 之后放到UI场景的开始游戏点击事件
//     //初始化配置表数据，用户输入，创建黑板实体，切换运行时场景 
//     public class InitConfigMono : MonoBehaviour
//     {
//         //public static Tables tables;
//
//         private EntityManager entityManager;
//         private EntityQuery _entityQuery;
//
//         private BlobAssetReference<GenGenBlobAssetReference.ConfigData> blobAssetReference;
//
//
//         async private void Start()
//         {
//             Application.targetFrameRate = 60;
//             entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
//             _entityQuery = entityManager.CreateEntityQuery(typeof(SwitchSceneData));
//
//             // 在热更层配置表转blob 
//
//             // ConfigManager.Instance.tables = new Tables();
//             // await ConfigManager.Instance.tables.LoadAsync(Loader);
//
//
//             InitGlobalConfigData();
//             // 初始化用户输入
//             await InitInputPrefab();
//
//             // 创建黑板实体
//             var entity = CreateBlackBoardEntity();
//             entityManager.AddComponent<WorldBlackBoardTag>(entity);
//
//             //切换运行时场景
//             var switchSceneData = _entityQuery.ToComponentDataArray<SwitchSceneData>(Allocator.Temp)[0];
//         }
//
//
//         private Entity CreateBlackBoardEntity()
//         {
//             //创建世界黑板entity 为避免多个原型 所有单例组件都应加到这一个entity上
//             var e = entityManager.CreateSingleton(new GlobalConfigData
//             {
//                 value = blobAssetReference
//             }, "WorldBlackBoard");
//
//             return e;
//         }
//
//         async private Task<JSONNode> Loader(string name)
//         {
//             var package = YooAssets.GetPackage("DefaultPackage");
//             RawFileOperationHandle handle =
//                 package.LoadRawFileAsync("Assets/ApesGang/ConfigJsonData/" + name + ".json");
//             await handle.Task;
//             string fileText = handle.GetRawFileText();
//             Debug.Log($"[{GetType().FullName}] LoadJson:{handle.GetAssetInfo().AssetPath}");
//             return JSON.Parse(fileText);
//         }
//
//         /// <summary>
//         /// 在热更层配置表转blob 
//         /// </summary>
//         private void InitGlobalConfigData()
//         {
//             blobAssetReference = GenGenBlobAssetReference.CreateBlob(ConfigManager.Instance.tables);
//
//             //entityManager.AddComponentData(e, new WorldBlackBoardTag());
//             Debug.Log($"[{GetType().FullName}] InitGlobalConfigData Created");
//             Debug.Log(
//                 $"[{GetType().FullName}] TestConfig:{ConfigManager.Instance.tables.Tbequip_data.Get(151, 3).mainEntryInit}");
//         }
//
//         /// <summary>
//         /// 实例化用户输入预制件
//         /// </summary>
//         private async Task InitInputPrefab()
//         {
//             // await UniTask.Delay(1500);
//             //YooAssets.Initialize();
//             var package = YooAssets.GetPackage("DefaultPackage");
//             AssetOperationHandle handle =
//                 package.LoadAssetAsync<GameObject>("Assets/ApesGang/Prefabs/Rewired Input Manager.prefab");
//             AssetOperationHandle handle0 =
//                 package.LoadAssetAsync<GameObject>("Assets/ApesGang/Prefabs/Rewired Touch Control Canvas.prefab");
//
//             await Task.WhenAll(handle.Task, handle0.Task);
//
//             GameObject input = handle.AssetObject as GameObject;
//             GameObject touch = handle0.AssetObject as GameObject;
//             Instantiate(input);
//             var ins = Instantiate(touch);
//             DontDestroyOnLoad(ins);
//
//             // transform = instantiate.transform;
//
//             Debug.Log($"[{GetType().FullName}] {input.name}创建好了这个go");
//         }
//     }
// }