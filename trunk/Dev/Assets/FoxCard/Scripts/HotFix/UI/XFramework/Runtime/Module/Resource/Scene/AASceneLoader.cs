// using UnityEngine.AddressableAssets;
// using UnityEngine.ResourceManagement.AsyncOperations;
// using UnityEngine.ResourceManagement.ResourceProviders;
// using UnityEngine.SceneManagement;
//
// namespace XFramework
// {
//     internal class AASceneInstance
//     {
//         public string Key { get; private set; }
//
//         public AsyncOperationHandle<SceneInstance> Operation { get; private set; } 
//
//         public AASceneInstance(string key, AsyncOperationHandle<SceneInstance> op)
//         {
//             this.Key = key;
//             this.Operation = op;
//         }
//     }
//
//     public class AASceneLoader : SceneLoader
//     {
//         public override bool IsDone(SceneObject sceneObjet)
//         {
//             AASceneInstance instance = (AASceneInstance)sceneObjet.SceneHandle;
//             if (instance is null)
//                 return true;
//
//             return instance.Operation.IsDone;
//         }
//
//         public override object LoadScene(string key, LoadSceneMode loadSceneMode = LoadSceneMode.Single)
//         {
//             var op = Addressables.LoadSceneAsync(key, loadSceneMode);
//             op.WaitForCompletion();
//
//             if (!op.IsValid())
//             {
//                 Log.Error($"资源无效, key is {key}");
//                 return null;
//             }
//
//             if (op.Status != AsyncOperationStatus.Succeeded)
//             {
//                 Log.Error($"场景加载失败, key is {key}");
//                 return null;
//             }
//
//             var sceneInstance = new AASceneInstance(key, op);
//             return sceneInstance;
//         }
//
//         public override object LoadSceneAsync(string key, LoadSceneMode loadSceneMode = LoadSceneMode.Single)
//         {
//             var op = Addressables.LoadSceneAsync(key, loadSceneMode);
//             var sceneInstance = new AASceneInstance(key, op);
//             return sceneInstance;
//         }
//
//         public override float Progress(SceneObject sceneObjet)
//         {
//             AASceneInstance instance = (AASceneInstance)sceneObjet.SceneHandle;
//             if (instance is null)
//                 return 1f;
//
//             return instance.Operation.PercentComplete;
//         }
//
//         public override async XFTask UnloadSceneAsync(object handle)
//         {
//             AASceneInstance instance = (AASceneInstance)handle;
//             var op = Addressables.UnloadSceneAsync(instance.Operation);
//             await op.Task;
//         }
//
//         public async override XFTask WaitForCompleted(SceneObject sceneObjet)
//         {
//             AASceneInstance instance = (AASceneInstance)sceneObjet.SceneHandle;
//             if (instance is null)
//                 return;
//
//             if (instance.Operation.IsDone)
//                 return;
//
//             await instance.Operation.Task;
//         }
//     }
// }
