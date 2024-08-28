// using UnityEngine;
// using UnityEngine.SceneManagement;
//
// namespace XFramework
// {
//     internal class BuiltinSceneInstance
//     {
//         public string Key { get; private set; }
//
//         public AsyncOperation Operation { get; private set; }
//
//         public BuiltinSceneInstance(string key, AsyncOperation asyncOperation)
//         {
//             Key = key;
//             Operation = asyncOperation;
//         }
//     }
//
//     public class BuiltinSceneLoader : SceneLoader
//     {
//         public override object LoadScene(string key, LoadSceneMode loadSceneMode = LoadSceneMode.Single)
//         {
//             SceneManager.LoadScene(key, loadSceneMode);
//             BuiltinSceneInstance scene = new BuiltinSceneInstance(key, null);
//             return scene;
//         }
//
//         public override object LoadSceneAsync(string key, LoadSceneMode loadSceneMode = LoadSceneMode.Single)
//         {
//             AsyncOperation operation = SceneManager.LoadSceneAsync(key, loadSceneMode);
//             BuiltinSceneInstance scene = new BuiltinSceneInstance(key, operation);
//             return scene;
//         }
//
//         public override bool IsDone(SceneObject sceneObjet)
//         {
//             BuiltinSceneInstance scene = (BuiltinSceneInstance)sceneObjet.SceneHandle;
//             if (scene.Operation is null)
//                 return true;
//
//             return scene.Operation.isDone;
//         }
//
//         public override float Progress(SceneObject sceneObjet)
//         {
//             BuiltinSceneInstance scene = (BuiltinSceneInstance)sceneObjet.SceneHandle;
//             if (scene.Operation is null)
//                 return 1f;
//
//             if (scene.Operation.isDone)
//                 return 1f;
//
//             return scene.Operation.progress;
//         }
//
//         public async override XFTask UnloadSceneAsync(object handle)
//         {
//             BuiltinSceneInstance scene = (BuiltinSceneInstance)handle;
//             string key = scene.Key;
//             AsyncOperation asyncOperation = SceneManager.UnloadSceneAsync(key);
//             XFTask tcs = XFTask.Create(true);
//             asyncOperation.completed += (o) =>
//             {
//                 tcs.SetResult();
//             };
//             await tcs;
//         }
//
//         public async override XFTask WaitForCompleted(SceneObject sceneObjet)
//         {
//             BuiltinSceneInstance scene = (BuiltinSceneInstance)sceneObjet.SceneHandle;
//             if (scene.Operation is null)
//                 return;
//
//             XFTask tcs = XFTask.Create(true);
//             scene.Operation.completed += (o) =>
//             {
//                 tcs.SetResult();
//             };
//             await tcs;
//         }
//     }
// }

