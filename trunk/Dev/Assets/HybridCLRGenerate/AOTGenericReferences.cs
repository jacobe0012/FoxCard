using System.Collections.Generic;
public class AOTGenericReferences : UnityEngine.MonoBehaviour
{

	// {{ AOT assemblies
	public static readonly IReadOnlyList<string> PatchedAOTAssemblyList = new List<string>
	{
		"DOTween.dll",
		"Newtonsoft.Json.dll",
		"System.Core.dll",
		"System.dll",
		"UniTask.dll",
		"UnityEngine.CoreModule.dll",
		"YooAsset.dll",
		"mscorlib.dll",
	};
	// }}

	// {{ constraint implement type
	// }} 

	// {{ AOT generic types
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<HotFix_Logic.HotUpdateMain.<GoToUIScene>d__7>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<HotFix_Logic.HotUpdateMain.<InitTypeAndMetaData>d__4>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<HotFix_Logic.HotUpdateMain.<LoadMetadataForAOTAssemblies>d__3>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<HotFix_UI.JiYuUIHelper.<DownloadByUrl>d__14>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<HotFix_UI.JsonManager.<LoadPlayerData>d__4,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<HotFix_UI.JsonManager.<SavePlayerData>d__3>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<XFramework.DemoEntry.<Loader>d__4,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<XFramework.ImageComponent.<SetSpriteAsync>d__3<object>>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<XFramework.LoadingScene.<WaitForCompleted>d__3>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<XFramework.ResourcesLoader.<InitAsync>d__0>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<XFramework.ResourcesManager.<InstantiateAsync>d__16,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<XFramework.ResourcesManager.<InstantiateAsync>d__17,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<XFramework.ResourcesManager.<InstantiateAsync>d__18,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<XFramework.ResourcesManager.<InstantiateAsync>d__19,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<XFramework.ResourcesManager.<LoadAssetAsync>d__10<object>,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<XFramework.ResourcesManager.<LoadAssetAsync>d__11<object>,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<XFramework.ResourcesManager.<LoadAssetAsync>d__12,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<XFramework.Scene.<WaitForCompleted>d__14>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<XFramework.SceneResManager.<UnloadSceneAsync>d__6>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<XFramework.SceneResManager.<WaitForCompleted>d__7>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<XFramework.UIHelper.<CreateAsync>d__10<object,object>,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<XFramework.UIHelper.<CreateAsync>d__11,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<XFramework.UIHelper.<CreateAsync>d__12<object>,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<XFramework.UIHelper.<CreateAsync>d__13,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<XFramework.UIHelper.<CreateAsync>d__14<object>,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<XFramework.UIHelper.<CreateAsync>d__8,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<XFramework.UIHelper.<CreateAsync>d__9<object>,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<XFramework.UIHelper.<CreateAsyncNew>d__6<object>,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<XFramework.UIHelper.<CreateAsyncNew>d__7,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<XFramework.UIImageExtensions.<SetSpriteAsync>d__4>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<XFramework.UIManager.<CreateAsync>d__21,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<XFramework.UIManager.<CreateAsync>d__22<object>,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<XFramework.UIManager.<CreateAsync>d__23<object,object>,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<XFramework.UIManager.<CreateAsync>d__24,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<XFramework.UIManager.<CreateAsync>d__25<object>,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<XFramework.UIManager.<CreateAsync>d__26,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<XFramework.UIManager.<CreateAsync>d__27<object>,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<XFramework.UIManager.<CreateAsync>d__28<object,object>,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<XFramework.UIManager.<CreateAsyncNew>d__19<object>,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<XFramework.UIManager.<CreateAsyncNew>d__20,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<XFramework.UIManager.<CreateInnerAsync>d__10,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<XFramework.UIManager.<GetGameObjectAsync>d__11,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<XFramework.UIPanelInGame.<PlayCardsAnimation>d__24>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<XFramework.YooResourcesLoader.<InstantiateAsync>d__10,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<XFramework.YooResourcesLoader.<InstantiateAsync>d__11,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<XFramework.YooResourcesLoader.<InstantiateAsync>d__8,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<XFramework.YooResourcesLoader.<InstantiateAsync>d__9,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<XFramework.YooResourcesLoader.<LoadAssetAsync>d__3<object>,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<XFramework.YooResourcesLoader.<LoadAssetAsync>d__4,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<XFramework.YooSceneLoader.<UnloadSceneAsync>d__4>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<XFramework.YooSceneLoader.<WaitForCompleted>d__5>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask.<>c<cfg.Tables.<LoadAsync>d__10>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<HotFix_Logic.HotUpdateMain.<GoToUIScene>d__7>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<HotFix_Logic.HotUpdateMain.<InitTypeAndMetaData>d__4>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<HotFix_Logic.HotUpdateMain.<LoadMetadataForAOTAssemblies>d__3>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<HotFix_UI.JiYuUIHelper.<DownloadByUrl>d__14>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<HotFix_UI.JsonManager.<LoadPlayerData>d__4,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<HotFix_UI.JsonManager.<SavePlayerData>d__3>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<XFramework.DemoEntry.<Loader>d__4,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<XFramework.ImageComponent.<SetSpriteAsync>d__3<object>>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<XFramework.LoadingScene.<WaitForCompleted>d__3>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<XFramework.ResourcesLoader.<InitAsync>d__0>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<XFramework.ResourcesManager.<InstantiateAsync>d__16,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<XFramework.ResourcesManager.<InstantiateAsync>d__17,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<XFramework.ResourcesManager.<InstantiateAsync>d__18,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<XFramework.ResourcesManager.<InstantiateAsync>d__19,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<XFramework.ResourcesManager.<LoadAssetAsync>d__10<object>,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<XFramework.ResourcesManager.<LoadAssetAsync>d__11<object>,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<XFramework.ResourcesManager.<LoadAssetAsync>d__12,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<XFramework.Scene.<WaitForCompleted>d__14>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<XFramework.SceneResManager.<UnloadSceneAsync>d__6>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<XFramework.SceneResManager.<WaitForCompleted>d__7>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<XFramework.UIHelper.<CreateAsync>d__10<object,object>,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<XFramework.UIHelper.<CreateAsync>d__11,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<XFramework.UIHelper.<CreateAsync>d__12<object>,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<XFramework.UIHelper.<CreateAsync>d__13,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<XFramework.UIHelper.<CreateAsync>d__14<object>,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<XFramework.UIHelper.<CreateAsync>d__8,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<XFramework.UIHelper.<CreateAsync>d__9<object>,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<XFramework.UIHelper.<CreateAsyncNew>d__6<object>,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<XFramework.UIHelper.<CreateAsyncNew>d__7,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<XFramework.UIImageExtensions.<SetSpriteAsync>d__4>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<XFramework.UIManager.<CreateAsync>d__21,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<XFramework.UIManager.<CreateAsync>d__22<object>,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<XFramework.UIManager.<CreateAsync>d__23<object,object>,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<XFramework.UIManager.<CreateAsync>d__24,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<XFramework.UIManager.<CreateAsync>d__25<object>,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<XFramework.UIManager.<CreateAsync>d__26,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<XFramework.UIManager.<CreateAsync>d__27<object>,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<XFramework.UIManager.<CreateAsync>d__28<object,object>,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<XFramework.UIManager.<CreateAsyncNew>d__19<object>,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<XFramework.UIManager.<CreateAsyncNew>d__20,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<XFramework.UIManager.<CreateInnerAsync>d__10,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<XFramework.UIManager.<GetGameObjectAsync>d__11,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<XFramework.UIPanelInGame.<PlayCardsAnimation>d__24>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<XFramework.YooResourcesLoader.<InstantiateAsync>d__10,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<XFramework.YooResourcesLoader.<InstantiateAsync>d__11,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<XFramework.YooResourcesLoader.<InstantiateAsync>d__8,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<XFramework.YooResourcesLoader.<InstantiateAsync>d__9,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<XFramework.YooResourcesLoader.<LoadAssetAsync>d__3<object>,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<XFramework.YooResourcesLoader.<LoadAssetAsync>d__4,object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<XFramework.YooSceneLoader.<UnloadSceneAsync>d__4>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<XFramework.YooSceneLoader.<WaitForCompleted>d__5>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask<cfg.Tables.<LoadAsync>d__10>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskVoid.<>c<HotFix_Logic.HotUpdateMain.<Start>d__6>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskVoid.<>c<XFramework.AudioManager.<LoadAudio>d__13>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskVoid.<>c<XFramework.UIPanelInGame.<CreateCardsList>d__21>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskVoid<HotFix_Logic.HotUpdateMain.<Start>d__6>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskVoid<XFramework.AudioManager.<LoadAudio>d__13>
	// Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskVoid<XFramework.UIPanelInGame.<CreateCardsList>d__21>
	// Cysharp.Threading.Tasks.CompilerServices.IStateMachineRunnerPromise<object>
	// Cysharp.Threading.Tasks.ITaskPoolNode<object>
	// Cysharp.Threading.Tasks.IUniTaskSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>>>
	// Cysharp.Threading.Tasks.IUniTaskSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>>
	// Cysharp.Threading.Tasks.IUniTaskSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>
	// Cysharp.Threading.Tasks.IUniTaskSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>
	// Cysharp.Threading.Tasks.IUniTaskSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>
	// Cysharp.Threading.Tasks.IUniTaskSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>
	// Cysharp.Threading.Tasks.IUniTaskSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>
	// Cysharp.Threading.Tasks.IUniTaskSource<System.ValueTuple<byte,System.ValueTuple<byte,object>>>
	// Cysharp.Threading.Tasks.IUniTaskSource<System.ValueTuple<byte,object>>
	// Cysharp.Threading.Tasks.IUniTaskSource<object>
	// Cysharp.Threading.Tasks.Internal.StatePool<Cysharp.Threading.Tasks.UniTask.Awaiter<object>>
	// Cysharp.Threading.Tasks.Internal.StateTuple<Cysharp.Threading.Tasks.UniTask.Awaiter<object>>
	// Cysharp.Threading.Tasks.UniTask.Awaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>>>
	// Cysharp.Threading.Tasks.UniTask.Awaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>>
	// Cysharp.Threading.Tasks.UniTask.Awaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>
	// Cysharp.Threading.Tasks.UniTask.Awaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>
	// Cysharp.Threading.Tasks.UniTask.Awaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>
	// Cysharp.Threading.Tasks.UniTask.Awaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>
	// Cysharp.Threading.Tasks.UniTask.Awaiter<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>
	// Cysharp.Threading.Tasks.UniTask.Awaiter<System.ValueTuple<byte,System.ValueTuple<byte,object>>>
	// Cysharp.Threading.Tasks.UniTask.Awaiter<System.ValueTuple<byte,object>>
	// Cysharp.Threading.Tasks.UniTask.Awaiter<object>
	// Cysharp.Threading.Tasks.UniTask.IsCanceledSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>>>
	// Cysharp.Threading.Tasks.UniTask.IsCanceledSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>>
	// Cysharp.Threading.Tasks.UniTask.IsCanceledSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>
	// Cysharp.Threading.Tasks.UniTask.IsCanceledSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>
	// Cysharp.Threading.Tasks.UniTask.IsCanceledSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>
	// Cysharp.Threading.Tasks.UniTask.IsCanceledSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>
	// Cysharp.Threading.Tasks.UniTask.IsCanceledSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>
	// Cysharp.Threading.Tasks.UniTask.IsCanceledSource<System.ValueTuple<byte,System.ValueTuple<byte,object>>>
	// Cysharp.Threading.Tasks.UniTask.IsCanceledSource<System.ValueTuple<byte,object>>
	// Cysharp.Threading.Tasks.UniTask.IsCanceledSource<object>
	// Cysharp.Threading.Tasks.UniTask.MemoizeSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>>>
	// Cysharp.Threading.Tasks.UniTask.MemoizeSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>>
	// Cysharp.Threading.Tasks.UniTask.MemoizeSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>
	// Cysharp.Threading.Tasks.UniTask.MemoizeSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>
	// Cysharp.Threading.Tasks.UniTask.MemoizeSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>
	// Cysharp.Threading.Tasks.UniTask.MemoizeSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>
	// Cysharp.Threading.Tasks.UniTask.MemoizeSource<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>
	// Cysharp.Threading.Tasks.UniTask.MemoizeSource<System.ValueTuple<byte,System.ValueTuple<byte,object>>>
	// Cysharp.Threading.Tasks.UniTask.MemoizeSource<System.ValueTuple<byte,object>>
	// Cysharp.Threading.Tasks.UniTask.MemoizeSource<object>
	// Cysharp.Threading.Tasks.UniTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>>>>
	// Cysharp.Threading.Tasks.UniTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>>>
	// Cysharp.Threading.Tasks.UniTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>>
	// Cysharp.Threading.Tasks.UniTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>
	// Cysharp.Threading.Tasks.UniTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>
	// Cysharp.Threading.Tasks.UniTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>
	// Cysharp.Threading.Tasks.UniTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>
	// Cysharp.Threading.Tasks.UniTask<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>
	// Cysharp.Threading.Tasks.UniTask<System.ValueTuple<byte,System.ValueTuple<byte,object>>>
	// Cysharp.Threading.Tasks.UniTask<System.ValueTuple<byte,object>>
	// Cysharp.Threading.Tasks.UniTask<object>
	// Cysharp.Threading.Tasks.UniTaskCompletionSource<object>
	// Cysharp.Threading.Tasks.UniTaskCompletionSourceCore<Cysharp.Threading.Tasks.AsyncUnit>
	// Cysharp.Threading.Tasks.UniTaskCompletionSourceCore<object>
	// Cysharp.Threading.Tasks.UniTaskExtensions.<>c__0<object>
	// Cysharp.Threading.Tasks.UniTaskExtensions.<>c__19<object>
	// DG.Tweening.Core.DOGetter<UnityEngine.Color>
	// DG.Tweening.Core.DOGetter<UnityEngine.Quaternion>
	// DG.Tweening.Core.DOGetter<UnityEngine.Vector2>
	// DG.Tweening.Core.DOGetter<UnityEngine.Vector3>
	// DG.Tweening.Core.DOGetter<float>
	// DG.Tweening.Core.DOGetter<int>
	// DG.Tweening.Core.DOGetter<object>
	// DG.Tweening.Core.DOSetter<UnityEngine.Color>
	// DG.Tweening.Core.DOSetter<UnityEngine.Quaternion>
	// DG.Tweening.Core.DOSetter<UnityEngine.Vector2>
	// DG.Tweening.Core.DOSetter<UnityEngine.Vector3>
	// DG.Tweening.Core.DOSetter<float>
	// DG.Tweening.Core.DOSetter<int>
	// DG.Tweening.Core.DOSetter<object>
	// DG.Tweening.Core.TweenerCore<UnityEngine.Quaternion,UnityEngine.Vector3,DG.Tweening.Plugins.Options.QuaternionOptions>
	// DG.Tweening.Core.TweenerCore<UnityEngine.Vector2,UnityEngine.Vector2,DG.Tweening.Plugins.CircleOptions>
	// DG.Tweening.Core.TweenerCore<UnityEngine.Vector3,object,DG.Tweening.Plugins.Options.PathOptions>
	// DG.Tweening.Plugins.Core.ABSTweenPlugin<DG.Tweening.Color2,DG.Tweening.Color2,DG.Tweening.Plugins.Options.ColorOptions>
	// DG.Tweening.Plugins.Core.ABSTweenPlugin<UnityEngine.Color,UnityEngine.Color,DG.Tweening.Plugins.Options.ColorOptions>
	// DG.Tweening.Plugins.Core.ABSTweenPlugin<UnityEngine.Quaternion,UnityEngine.Vector3,DG.Tweening.Plugins.Options.QuaternionOptions>
	// DG.Tweening.Plugins.Core.ABSTweenPlugin<UnityEngine.Rect,UnityEngine.Rect,DG.Tweening.Plugins.Options.RectOptions>
	// DG.Tweening.Plugins.Core.ABSTweenPlugin<UnityEngine.Vector2,UnityEngine.Vector2,DG.Tweening.Plugins.CircleOptions>
	// DG.Tweening.Plugins.Core.ABSTweenPlugin<UnityEngine.Vector2,UnityEngine.Vector2,DG.Tweening.Plugins.Options.VectorOptions>
	// DG.Tweening.Plugins.Core.ABSTweenPlugin<UnityEngine.Vector3,UnityEngine.Vector3,DG.Tweening.Plugins.Options.VectorOptions>
	// DG.Tweening.Plugins.Core.ABSTweenPlugin<UnityEngine.Vector3,object,DG.Tweening.Plugins.Options.PathOptions>
	// DG.Tweening.Plugins.Core.ABSTweenPlugin<UnityEngine.Vector3,object,DG.Tweening.Plugins.Options.Vector3ArrayOptions>
	// DG.Tweening.Plugins.Core.ABSTweenPlugin<UnityEngine.Vector4,UnityEngine.Vector4,DG.Tweening.Plugins.Options.VectorOptions>
	// DG.Tweening.Plugins.Core.ABSTweenPlugin<double,double,DG.Tweening.Plugins.Options.NoOptions>
	// DG.Tweening.Plugins.Core.ABSTweenPlugin<float,float,DG.Tweening.Plugins.Options.FloatOptions>
	// DG.Tweening.Plugins.Core.ABSTweenPlugin<int,int,DG.Tweening.Plugins.Options.NoOptions>
	// DG.Tweening.Plugins.Core.ABSTweenPlugin<long,long,DG.Tweening.Plugins.Options.NoOptions>
	// DG.Tweening.Plugins.Core.ABSTweenPlugin<object,object,DG.Tweening.Plugins.Options.NoOptions>
	// DG.Tweening.Plugins.Core.ABSTweenPlugin<object,object,DG.Tweening.Plugins.Options.StringOptions>
	// DG.Tweening.Plugins.Core.ABSTweenPlugin<uint,uint,DG.Tweening.Plugins.Options.UintOptions>
	// DG.Tweening.Plugins.Core.ABSTweenPlugin<ulong,ulong,DG.Tweening.Plugins.Options.NoOptions>
	// System.Action<DG.Tweening.Plugins.Options.PathOptions,object,UnityEngine.Quaternion,object>
	// System.Action<HotFix_UI.Card>
	// System.Action<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Action<System.ValueTuple<object,object>>
	// System.Action<UnityEngine.Quaternion>
	// System.Action<UnityEngine.Vector2>
	// System.Action<UnityEngine.Vector3>
	// System.Action<UnityEngine.Vector4>
	// System.Action<byte>
	// System.Action<float>
	// System.Action<int>
	// System.Action<long>
	// System.Action<object,object>
	// System.Action<object>
	// System.Buffers.ArrayPool<int>
	// System.Buffers.TlsOverPerCoreLockedStacksArrayPool.LockedStack<int>
	// System.Buffers.TlsOverPerCoreLockedStacksArrayPool.PerCoreLockedStacks<int>
	// System.Buffers.TlsOverPerCoreLockedStacksArrayPool<int>
	// System.ByReference<int>
	// System.ByReference<ushort>
	// System.Collections.Concurrent.ConcurrentQueue.<Enumerate>d__28<object>
	// System.Collections.Concurrent.ConcurrentQueue.Segment<object>
	// System.Collections.Concurrent.ConcurrentQueue<object>
	// System.Collections.Generic.ArraySortHelper<HotFix_UI.Card>
	// System.Collections.Generic.ArraySortHelper<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.ArraySortHelper<System.ValueTuple<object,object>>
	// System.Collections.Generic.ArraySortHelper<UnityEngine.Vector3>
	// System.Collections.Generic.ArraySortHelper<byte>
	// System.Collections.Generic.ArraySortHelper<int>
	// System.Collections.Generic.ArraySortHelper<long>
	// System.Collections.Generic.ArraySortHelper<object>
	// System.Collections.Generic.Comparer<HotFix_UI.Card>
	// System.Collections.Generic.Comparer<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Collections.Generic.Comparer<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.Comparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>>
	// System.Collections.Generic.Comparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>
	// System.Collections.Generic.Comparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>
	// System.Collections.Generic.Comparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>
	// System.Collections.Generic.Comparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>
	// System.Collections.Generic.Comparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>
	// System.Collections.Generic.Comparer<System.ValueTuple<byte,System.ValueTuple<byte,object>>>
	// System.Collections.Generic.Comparer<System.ValueTuple<byte,object>>
	// System.Collections.Generic.Comparer<System.ValueTuple<object,object>>
	// System.Collections.Generic.Comparer<UnityEngine.Vector3>
	// System.Collections.Generic.Comparer<byte>
	// System.Collections.Generic.Comparer<float>
	// System.Collections.Generic.Comparer<int>
	// System.Collections.Generic.Comparer<long>
	// System.Collections.Generic.Comparer<object>
	// System.Collections.Generic.Dictionary.Enumerator<int,HotFix_UI.CardGroup>
	// System.Collections.Generic.Dictionary.Enumerator<int,object>
	// System.Collections.Generic.Dictionary.Enumerator<long,object>
	// System.Collections.Generic.Dictionary.Enumerator<object,XFramework.GameObjectPool.MyRect>
	// System.Collections.Generic.Dictionary.Enumerator<object,object>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<int,HotFix_UI.CardGroup>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<int,object>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<long,object>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<object,XFramework.GameObjectPool.MyRect>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<object,object>
	// System.Collections.Generic.Dictionary.KeyCollection<int,HotFix_UI.CardGroup>
	// System.Collections.Generic.Dictionary.KeyCollection<int,object>
	// System.Collections.Generic.Dictionary.KeyCollection<long,object>
	// System.Collections.Generic.Dictionary.KeyCollection<object,XFramework.GameObjectPool.MyRect>
	// System.Collections.Generic.Dictionary.KeyCollection<object,object>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<int,HotFix_UI.CardGroup>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<int,object>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<long,object>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<object,XFramework.GameObjectPool.MyRect>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<object,object>
	// System.Collections.Generic.Dictionary.ValueCollection<int,HotFix_UI.CardGroup>
	// System.Collections.Generic.Dictionary.ValueCollection<int,object>
	// System.Collections.Generic.Dictionary.ValueCollection<long,object>
	// System.Collections.Generic.Dictionary.ValueCollection<object,XFramework.GameObjectPool.MyRect>
	// System.Collections.Generic.Dictionary.ValueCollection<object,object>
	// System.Collections.Generic.Dictionary<int,HotFix_UI.CardGroup>
	// System.Collections.Generic.Dictionary<int,object>
	// System.Collections.Generic.Dictionary<long,object>
	// System.Collections.Generic.Dictionary<object,XFramework.GameObjectPool.MyRect>
	// System.Collections.Generic.Dictionary<object,object>
	// System.Collections.Generic.EqualityComparer<HotFix_UI.Card>
	// System.Collections.Generic.EqualityComparer<HotFix_UI.CardGroup>
	// System.Collections.Generic.EqualityComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>>
	// System.Collections.Generic.EqualityComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>
	// System.Collections.Generic.EqualityComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>
	// System.Collections.Generic.EqualityComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>
	// System.Collections.Generic.EqualityComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>
	// System.Collections.Generic.EqualityComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>
	// System.Collections.Generic.EqualityComparer<System.ValueTuple<byte,System.ValueTuple<byte,object>>>
	// System.Collections.Generic.EqualityComparer<System.ValueTuple<byte,object>>
	// System.Collections.Generic.EqualityComparer<XFramework.GameObjectPool.MyRect>
	// System.Collections.Generic.EqualityComparer<byte>
	// System.Collections.Generic.EqualityComparer<int>
	// System.Collections.Generic.EqualityComparer<long>
	// System.Collections.Generic.EqualityComparer<object>
	// System.Collections.Generic.HashSet.Enumerator<int>
	// System.Collections.Generic.HashSet.Enumerator<object>
	// System.Collections.Generic.HashSet<int>
	// System.Collections.Generic.HashSet<object>
	// System.Collections.Generic.HashSetEqualityComparer<int>
	// System.Collections.Generic.HashSetEqualityComparer<object>
	// System.Collections.Generic.ICollection<HotFix_UI.Card>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<int,HotFix_UI.CardGroup>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<object,XFramework.GameObjectPool.MyRect>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.ICollection<System.ValueTuple<object,object>>
	// System.Collections.Generic.ICollection<UnityEngine.Vector3>
	// System.Collections.Generic.ICollection<byte>
	// System.Collections.Generic.ICollection<int>
	// System.Collections.Generic.ICollection<long>
	// System.Collections.Generic.ICollection<object>
	// System.Collections.Generic.IComparer<HotFix_UI.Card>
	// System.Collections.Generic.IComparer<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Collections.Generic.IComparer<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.IComparer<System.ValueTuple<object,object>>
	// System.Collections.Generic.IComparer<UnityEngine.Vector3>
	// System.Collections.Generic.IComparer<byte>
	// System.Collections.Generic.IComparer<float>
	// System.Collections.Generic.IComparer<int>
	// System.Collections.Generic.IComparer<long>
	// System.Collections.Generic.IComparer<object>
	// System.Collections.Generic.IDictionary<int,object>
	// System.Collections.Generic.IDictionary<long,object>
	// System.Collections.Generic.IDictionary<object,double>
	// System.Collections.Generic.IDictionary<object,float>
	// System.Collections.Generic.IDictionary<object,int>
	// System.Collections.Generic.IDictionary<object,long>
	// System.Collections.Generic.IDictionary<object,object>
	// System.Collections.Generic.IEnumerable<HotFix_UI.Card>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.UIntPtr,object>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<int,HotFix_UI.CardGroup>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<object,XFramework.GameObjectPool.MyRect>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.IEnumerable<System.ValueTuple<object,object>>
	// System.Collections.Generic.IEnumerable<UnityEngine.Vector3>
	// System.Collections.Generic.IEnumerable<byte>
	// System.Collections.Generic.IEnumerable<int>
	// System.Collections.Generic.IEnumerable<long>
	// System.Collections.Generic.IEnumerable<object>
	// System.Collections.Generic.IEnumerator<HotFix_UI.Card>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.UIntPtr,object>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<int,HotFix_UI.CardGroup>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<object,XFramework.GameObjectPool.MyRect>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.IEnumerator<System.ValueTuple<object,object>>
	// System.Collections.Generic.IEnumerator<UnityEngine.Vector3>
	// System.Collections.Generic.IEnumerator<byte>
	// System.Collections.Generic.IEnumerator<int>
	// System.Collections.Generic.IEnumerator<long>
	// System.Collections.Generic.IEnumerator<object>
	// System.Collections.Generic.IEqualityComparer<HotFix_UI.Card>
	// System.Collections.Generic.IEqualityComparer<int>
	// System.Collections.Generic.IEqualityComparer<long>
	// System.Collections.Generic.IEqualityComparer<object>
	// System.Collections.Generic.IList<HotFix_UI.Card>
	// System.Collections.Generic.IList<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.IList<System.ValueTuple<object,object>>
	// System.Collections.Generic.IList<UnityEngine.Vector3>
	// System.Collections.Generic.IList<byte>
	// System.Collections.Generic.IList<int>
	// System.Collections.Generic.IList<long>
	// System.Collections.Generic.IList<object>
	// System.Collections.Generic.KeyValuePair<System.UIntPtr,object>
	// System.Collections.Generic.KeyValuePair<int,HotFix_UI.CardGroup>
	// System.Collections.Generic.KeyValuePair<int,object>
	// System.Collections.Generic.KeyValuePair<long,object>
	// System.Collections.Generic.KeyValuePair<object,XFramework.GameObjectPool.MyRect>
	// System.Collections.Generic.KeyValuePair<object,object>
	// System.Collections.Generic.List.Enumerator<HotFix_UI.Card>
	// System.Collections.Generic.List.Enumerator<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.List.Enumerator<System.ValueTuple<object,object>>
	// System.Collections.Generic.List.Enumerator<UnityEngine.Vector3>
	// System.Collections.Generic.List.Enumerator<byte>
	// System.Collections.Generic.List.Enumerator<int>
	// System.Collections.Generic.List.Enumerator<long>
	// System.Collections.Generic.List.Enumerator<object>
	// System.Collections.Generic.List<HotFix_UI.Card>
	// System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.List<System.ValueTuple<object,object>>
	// System.Collections.Generic.List<UnityEngine.Vector3>
	// System.Collections.Generic.List<byte>
	// System.Collections.Generic.List<int>
	// System.Collections.Generic.List<long>
	// System.Collections.Generic.List<object>
	// System.Collections.Generic.ObjectComparer<HotFix_UI.Card>
	// System.Collections.Generic.ObjectComparer<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Collections.Generic.ObjectComparer<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.ObjectComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>
	// System.Collections.Generic.ObjectComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>
	// System.Collections.Generic.ObjectComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>
	// System.Collections.Generic.ObjectComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>
	// System.Collections.Generic.ObjectComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>
	// System.Collections.Generic.ObjectComparer<System.ValueTuple<byte,System.ValueTuple<byte,object>>>
	// System.Collections.Generic.ObjectComparer<System.ValueTuple<byte,object>>
	// System.Collections.Generic.ObjectComparer<System.ValueTuple<object,object>>
	// System.Collections.Generic.ObjectComparer<UnityEngine.Vector3>
	// System.Collections.Generic.ObjectComparer<byte>
	// System.Collections.Generic.ObjectComparer<float>
	// System.Collections.Generic.ObjectComparer<int>
	// System.Collections.Generic.ObjectComparer<long>
	// System.Collections.Generic.ObjectComparer<object>
	// System.Collections.Generic.ObjectEqualityComparer<HotFix_UI.Card>
	// System.Collections.Generic.ObjectEqualityComparer<HotFix_UI.CardGroup>
	// System.Collections.Generic.ObjectEqualityComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>
	// System.Collections.Generic.ObjectEqualityComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>
	// System.Collections.Generic.ObjectEqualityComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>
	// System.Collections.Generic.ObjectEqualityComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>
	// System.Collections.Generic.ObjectEqualityComparer<System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>
	// System.Collections.Generic.ObjectEqualityComparer<System.ValueTuple<byte,System.ValueTuple<byte,object>>>
	// System.Collections.Generic.ObjectEqualityComparer<System.ValueTuple<byte,object>>
	// System.Collections.Generic.ObjectEqualityComparer<XFramework.GameObjectPool.MyRect>
	// System.Collections.Generic.ObjectEqualityComparer<byte>
	// System.Collections.Generic.ObjectEqualityComparer<int>
	// System.Collections.Generic.ObjectEqualityComparer<long>
	// System.Collections.Generic.ObjectEqualityComparer<object>
	// System.Collections.Generic.Queue.Enumerator<long>
	// System.Collections.Generic.Queue.Enumerator<object>
	// System.Collections.Generic.Queue<long>
	// System.Collections.Generic.Queue<object>
	// System.Collections.Generic.SortedDictionary.<>c__DisplayClass34_0<long,object>
	// System.Collections.Generic.SortedDictionary.<>c__DisplayClass34_0<object,object>
	// System.Collections.Generic.SortedDictionary.<>c__DisplayClass34_1<long,object>
	// System.Collections.Generic.SortedDictionary.<>c__DisplayClass34_1<object,object>
	// System.Collections.Generic.SortedDictionary.Enumerator<long,object>
	// System.Collections.Generic.SortedDictionary.Enumerator<object,object>
	// System.Collections.Generic.SortedDictionary.KeyCollection.<>c__DisplayClass5_0<long,object>
	// System.Collections.Generic.SortedDictionary.KeyCollection.<>c__DisplayClass5_0<object,object>
	// System.Collections.Generic.SortedDictionary.KeyCollection.<>c__DisplayClass6_0<long,object>
	// System.Collections.Generic.SortedDictionary.KeyCollection.<>c__DisplayClass6_0<object,object>
	// System.Collections.Generic.SortedDictionary.KeyCollection.Enumerator<long,object>
	// System.Collections.Generic.SortedDictionary.KeyCollection.Enumerator<object,object>
	// System.Collections.Generic.SortedDictionary.KeyCollection<long,object>
	// System.Collections.Generic.SortedDictionary.KeyCollection<object,object>
	// System.Collections.Generic.SortedDictionary.KeyValuePairComparer<long,object>
	// System.Collections.Generic.SortedDictionary.KeyValuePairComparer<object,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection.<>c__DisplayClass5_0<long,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection.<>c__DisplayClass5_0<object,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection.<>c__DisplayClass6_0<long,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection.<>c__DisplayClass6_0<object,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection.Enumerator<long,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection.Enumerator<object,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection<long,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection<object,object>
	// System.Collections.Generic.SortedDictionary<long,object>
	// System.Collections.Generic.SortedDictionary<object,object>
	// System.Collections.Generic.SortedSet.<>c__DisplayClass52_0<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Collections.Generic.SortedSet.<>c__DisplayClass52_0<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.SortedSet.<>c__DisplayClass53_0<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Collections.Generic.SortedSet.<>c__DisplayClass53_0<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.SortedSet.Enumerator<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Collections.Generic.SortedSet.Enumerator<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.SortedSet.Node<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Collections.Generic.SortedSet.Node<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.SortedSet<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Collections.Generic.SortedSet<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.Stack.Enumerator<object>
	// System.Collections.Generic.Stack<object>
	// System.Collections.Generic.TreeSet<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Collections.Generic.TreeSet<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.TreeWalkPredicate<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Collections.Generic.TreeWalkPredicate<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.ValueListBuilder<int>
	// System.Collections.ObjectModel.ReadOnlyCollection<HotFix_UI.Card>
	// System.Collections.ObjectModel.ReadOnlyCollection<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.ObjectModel.ReadOnlyCollection<System.ValueTuple<object,object>>
	// System.Collections.ObjectModel.ReadOnlyCollection<UnityEngine.Vector3>
	// System.Collections.ObjectModel.ReadOnlyCollection<byte>
	// System.Collections.ObjectModel.ReadOnlyCollection<int>
	// System.Collections.ObjectModel.ReadOnlyCollection<long>
	// System.Collections.ObjectModel.ReadOnlyCollection<object>
	// System.Comparison<HotFix_UI.Card>
	// System.Comparison<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Comparison<System.ValueTuple<object,object>>
	// System.Comparison<UnityEngine.Vector3>
	// System.Comparison<byte>
	// System.Comparison<int>
	// System.Comparison<long>
	// System.Comparison<object>
	// System.Func<HotFix_UI.Card,HotFix_UI.Card>
	// System.Func<HotFix_UI.Card,byte>
	// System.Func<HotFix_UI.Card,float>
	// System.Func<HotFix_UI.Card,int>
	// System.Func<System.Collections.Generic.KeyValuePair<object,object>,byte>
	// System.Func<System.Threading.Tasks.VoidTaskResult>
	// System.Func<UnityEngine.Vector3>
	// System.Func<int,byte>
	// System.Func<int,object,byte>
	// System.Func<int>
	// System.Func<object,Cysharp.Threading.Tasks.UniTask<object>>
	// System.Func<object,System.Threading.Tasks.VoidTaskResult>
	// System.Func<object,byte>
	// System.Func<object,int,int,object>
	// System.Func<object,int>
	// System.Func<object,object,object>
	// System.Func<object,object>
	// System.Func<object>
	// System.IComparable<object>
	// System.IEquatable<object>
	// System.Linq.Buffer<HotFix_UI.Card>
	// System.Linq.Buffer<object>
	// System.Linq.Enumerable.<ExceptIterator>d__77<HotFix_UI.Card>
	// System.Linq.Enumerable.<OfTypeIterator>d__97<object>
	// System.Linq.Enumerable.<SelectManyIterator>d__17<object,HotFix_UI.Card>
	// System.Linq.Enumerable.<TakeIterator>d__25<HotFix_UI.Card>
	// System.Linq.Enumerable.Iterator<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Linq.Enumerable.Iterator<int>
	// System.Linq.Enumerable.Iterator<object>
	// System.Linq.Enumerable.WhereArrayIterator<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Linq.Enumerable.WhereArrayIterator<object>
	// System.Linq.Enumerable.WhereEnumerableIterator<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Linq.Enumerable.WhereEnumerableIterator<int>
	// System.Linq.Enumerable.WhereEnumerableIterator<object>
	// System.Linq.Enumerable.WhereListIterator<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Linq.Enumerable.WhereListIterator<object>
	// System.Linq.Enumerable.WhereSelectArrayIterator<object,int>
	// System.Linq.Enumerable.WhereSelectArrayIterator<object,object>
	// System.Linq.Enumerable.WhereSelectEnumerableIterator<object,int>
	// System.Linq.Enumerable.WhereSelectEnumerableIterator<object,object>
	// System.Linq.Enumerable.WhereSelectListIterator<object,int>
	// System.Linq.Enumerable.WhereSelectListIterator<object,object>
	// System.Linq.EnumerableSorter<HotFix_UI.Card,float>
	// System.Linq.EnumerableSorter<HotFix_UI.Card,int>
	// System.Linq.EnumerableSorter<HotFix_UI.Card>
	// System.Linq.EnumerableSorter<object,int>
	// System.Linq.EnumerableSorter<object>
	// System.Linq.GroupedEnumerable<HotFix_UI.Card,int,HotFix_UI.Card>
	// System.Linq.IdentityFunction.<>c<HotFix_UI.Card>
	// System.Linq.IdentityFunction<HotFix_UI.Card>
	// System.Linq.Lookup.<GetEnumerator>d__12<int,HotFix_UI.Card>
	// System.Linq.Lookup.Grouping.<GetEnumerator>d__7<int,HotFix_UI.Card>
	// System.Linq.Lookup.Grouping<int,HotFix_UI.Card>
	// System.Linq.Lookup<int,HotFix_UI.Card>
	// System.Linq.OrderedEnumerable.<GetEnumerator>d__1<HotFix_UI.Card>
	// System.Linq.OrderedEnumerable.<GetEnumerator>d__1<object>
	// System.Linq.OrderedEnumerable<HotFix_UI.Card,float>
	// System.Linq.OrderedEnumerable<HotFix_UI.Card,int>
	// System.Linq.OrderedEnumerable<HotFix_UI.Card>
	// System.Linq.OrderedEnumerable<object,int>
	// System.Linq.OrderedEnumerable<object>
	// System.Linq.Set<HotFix_UI.Card>
	// System.Nullable<UnityEngine.Vector3>
	// System.Nullable<byte>
	// System.Nullable<double>
	// System.Nullable<float>
	// System.Nullable<int>
	// System.Nullable<long>
	// System.Nullable<short>
	// System.Predicate<HotFix_UI.Card>
	// System.Predicate<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Predicate<System.ValueTuple<object,object>>
	// System.Predicate<UnityEngine.Vector3>
	// System.Predicate<byte>
	// System.Predicate<int>
	// System.Predicate<long>
	// System.Predicate<object>
	// System.ReadOnlySpan<int>
	// System.ReadOnlySpan<ushort>
	// System.Runtime.CompilerServices.AsyncTaskMethodBuilder<System.Threading.Tasks.VoidTaskResult>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable.ConfiguredTaskAwaiter<System.Threading.Tasks.VoidTaskResult>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable.ConfiguredTaskAwaiter<object>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable<System.Threading.Tasks.VoidTaskResult>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable<object>
	// System.Runtime.CompilerServices.TaskAwaiter<System.Threading.Tasks.VoidTaskResult>
	// System.Runtime.CompilerServices.TaskAwaiter<object>
	// System.Span<int>
	// System.Threading.Tasks.ContinuationTaskFromResultTask<System.Threading.Tasks.VoidTaskResult>
	// System.Threading.Tasks.ContinuationTaskFromResultTask<object>
	// System.Threading.Tasks.Task<System.Threading.Tasks.VoidTaskResult>
	// System.Threading.Tasks.Task<object>
	// System.Threading.Tasks.TaskFactory.<>c__DisplayClass35_0<System.Threading.Tasks.VoidTaskResult>
	// System.Threading.Tasks.TaskFactory.<>c__DisplayClass35_0<object>
	// System.Threading.Tasks.TaskFactory<System.Threading.Tasks.VoidTaskResult>
	// System.Threading.Tasks.TaskFactory<object>
	// System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>>>
	// System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>>
	// System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>>
	// System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>>
	// System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>>
	// System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>>
	// System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>>
	// System.ValueTuple<byte,System.ValueTuple<byte,System.ValueTuple<byte,object>>>
	// System.ValueTuple<byte,System.ValueTuple<byte,object>>
	// System.ValueTuple<byte,object>
	// System.ValueTuple<object,object>
	// UnityEngine.Events.InvokableCall<UnityEngine.Vector2>
	// UnityEngine.Events.InvokableCall<byte>
	// UnityEngine.Events.InvokableCall<float>
	// UnityEngine.Events.InvokableCall<int>
	// UnityEngine.Events.InvokableCall<object>
	// UnityEngine.Events.UnityAction<UnityEngine.Vector2>
	// UnityEngine.Events.UnityAction<byte>
	// UnityEngine.Events.UnityAction<float>
	// UnityEngine.Events.UnityAction<int>
	// UnityEngine.Events.UnityAction<object>
	// UnityEngine.Events.UnityEvent<UnityEngine.Vector2>
	// UnityEngine.Events.UnityEvent<byte>
	// UnityEngine.Events.UnityEvent<float>
	// UnityEngine.Events.UnityEvent<int>
	// UnityEngine.Events.UnityEvent<object>
	// }}

	public void RefMethods()
	{
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,HotFix_Logic.HotUpdateMain.<GoToUIScene>d__7>(Cysharp.Threading.Tasks.UniTask.Awaiter&,HotFix_Logic.HotUpdateMain.<GoToUIScene>d__7&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,HotFix_Logic.HotUpdateMain.<InitTypeAndMetaData>d__4>(Cysharp.Threading.Tasks.UniTask.Awaiter&,HotFix_Logic.HotUpdateMain.<InitTypeAndMetaData>d__4&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,HotFix_Logic.HotUpdateMain.<LoadMetadataForAOTAssemblies>d__3>(Cysharp.Threading.Tasks.UniTask.Awaiter&,HotFix_Logic.HotUpdateMain.<LoadMetadataForAOTAssemblies>d__3&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,HotFix_UI.JsonManager.<SavePlayerData>d__3>(Cysharp.Threading.Tasks.UniTask.Awaiter&,HotFix_UI.JsonManager.<SavePlayerData>d__3&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,XFramework.ImageComponent.<SetSpriteAsync>d__3<object>>(Cysharp.Threading.Tasks.UniTask.Awaiter&,XFramework.ImageComponent.<SetSpriteAsync>d__3<object>&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,XFramework.ResourcesLoader.<InitAsync>d__0>(Cysharp.Threading.Tasks.UniTask.Awaiter&,XFramework.ResourcesLoader.<InitAsync>d__0&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,XFramework.Scene.<WaitForCompleted>d__14>(Cysharp.Threading.Tasks.UniTask.Awaiter&,XFramework.Scene.<WaitForCompleted>d__14&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,XFramework.SceneResManager.<UnloadSceneAsync>d__6>(Cysharp.Threading.Tasks.UniTask.Awaiter&,XFramework.SceneResManager.<UnloadSceneAsync>d__6&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,XFramework.SceneResManager.<WaitForCompleted>d__7>(Cysharp.Threading.Tasks.UniTask.Awaiter&,XFramework.SceneResManager.<WaitForCompleted>d__7&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,XFramework.UIPanelInGame.<PlayCardsAnimation>d__24>(Cysharp.Threading.Tasks.UniTask.Awaiter&,XFramework.UIPanelInGame.<PlayCardsAnimation>d__24&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,XFramework.YooSceneLoader.<UnloadSceneAsync>d__4>(Cysharp.Threading.Tasks.UniTask.Awaiter&,XFramework.YooSceneLoader.<UnloadSceneAsync>d__4&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,XFramework.YooSceneLoader.<WaitForCompleted>d__5>(Cysharp.Threading.Tasks.UniTask.Awaiter&,XFramework.YooSceneLoader.<WaitForCompleted>d__5&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter<object>,HotFix_UI.JiYuUIHelper.<DownloadByUrl>d__14>(Cysharp.Threading.Tasks.UniTask.Awaiter<object>&,HotFix_UI.JiYuUIHelper.<DownloadByUrl>d__14&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter<object>,XFramework.LoadingScene.<WaitForCompleted>d__3>(Cysharp.Threading.Tasks.UniTask.Awaiter<object>&,XFramework.LoadingScene.<WaitForCompleted>d__3&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter<object>,XFramework.UIImageExtensions.<SetSpriteAsync>d__4>(Cysharp.Threading.Tasks.UniTask.Awaiter<object>&,XFramework.UIImageExtensions.<SetSpriteAsync>d__4&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter<object>,cfg.Tables.<LoadAsync>d__10>(Cysharp.Threading.Tasks.UniTask.Awaiter<object>&,cfg.Tables.<LoadAsync>d__10&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.AwaitUnsafeOnCompleted<object,XFramework.LoadingScene.<WaitForCompleted>d__3>(object&,XFramework.LoadingScene.<WaitForCompleted>d__3&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,HotFix_UI.JsonManager.<LoadPlayerData>d__4>(Cysharp.Threading.Tasks.UniTask.Awaiter&,HotFix_UI.JsonManager.<LoadPlayerData>d__4&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,XFramework.DemoEntry.<Loader>d__4>(Cysharp.Threading.Tasks.UniTask.Awaiter&,XFramework.DemoEntry.<Loader>d__4&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,XFramework.YooResourcesLoader.<InstantiateAsync>d__10>(Cysharp.Threading.Tasks.UniTask.Awaiter&,XFramework.YooResourcesLoader.<InstantiateAsync>d__10&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,XFramework.YooResourcesLoader.<InstantiateAsync>d__11>(Cysharp.Threading.Tasks.UniTask.Awaiter&,XFramework.YooResourcesLoader.<InstantiateAsync>d__11&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,XFramework.YooResourcesLoader.<InstantiateAsync>d__8>(Cysharp.Threading.Tasks.UniTask.Awaiter&,XFramework.YooResourcesLoader.<InstantiateAsync>d__8&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,XFramework.YooResourcesLoader.<InstantiateAsync>d__9>(Cysharp.Threading.Tasks.UniTask.Awaiter&,XFramework.YooResourcesLoader.<InstantiateAsync>d__9&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,XFramework.YooResourcesLoader.<LoadAssetAsync>d__3<object>>(Cysharp.Threading.Tasks.UniTask.Awaiter&,XFramework.YooResourcesLoader.<LoadAssetAsync>d__3<object>&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,XFramework.YooResourcesLoader.<LoadAssetAsync>d__4>(Cysharp.Threading.Tasks.UniTask.Awaiter&,XFramework.YooResourcesLoader.<LoadAssetAsync>d__4&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter<object>,HotFix_UI.JsonManager.<LoadPlayerData>d__4>(Cysharp.Threading.Tasks.UniTask.Awaiter<object>&,HotFix_UI.JsonManager.<LoadPlayerData>d__4&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter<object>,XFramework.ResourcesManager.<InstantiateAsync>d__16>(Cysharp.Threading.Tasks.UniTask.Awaiter<object>&,XFramework.ResourcesManager.<InstantiateAsync>d__16&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter<object>,XFramework.ResourcesManager.<InstantiateAsync>d__17>(Cysharp.Threading.Tasks.UniTask.Awaiter<object>&,XFramework.ResourcesManager.<InstantiateAsync>d__17&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter<object>,XFramework.ResourcesManager.<InstantiateAsync>d__18>(Cysharp.Threading.Tasks.UniTask.Awaiter<object>&,XFramework.ResourcesManager.<InstantiateAsync>d__18&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter<object>,XFramework.ResourcesManager.<InstantiateAsync>d__19>(Cysharp.Threading.Tasks.UniTask.Awaiter<object>&,XFramework.ResourcesManager.<InstantiateAsync>d__19&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter<object>,XFramework.ResourcesManager.<LoadAssetAsync>d__10<object>>(Cysharp.Threading.Tasks.UniTask.Awaiter<object>&,XFramework.ResourcesManager.<LoadAssetAsync>d__10<object>&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter<object>,XFramework.ResourcesManager.<LoadAssetAsync>d__11<object>>(Cysharp.Threading.Tasks.UniTask.Awaiter<object>&,XFramework.ResourcesManager.<LoadAssetAsync>d__11<object>&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter<object>,XFramework.ResourcesManager.<LoadAssetAsync>d__12>(Cysharp.Threading.Tasks.UniTask.Awaiter<object>&,XFramework.ResourcesManager.<LoadAssetAsync>d__12&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter<object>,XFramework.UIHelper.<CreateAsync>d__10<object,object>>(Cysharp.Threading.Tasks.UniTask.Awaiter<object>&,XFramework.UIHelper.<CreateAsync>d__10<object,object>&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter<object>,XFramework.UIHelper.<CreateAsync>d__11>(Cysharp.Threading.Tasks.UniTask.Awaiter<object>&,XFramework.UIHelper.<CreateAsync>d__11&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter<object>,XFramework.UIHelper.<CreateAsync>d__12<object>>(Cysharp.Threading.Tasks.UniTask.Awaiter<object>&,XFramework.UIHelper.<CreateAsync>d__12<object>&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter<object>,XFramework.UIHelper.<CreateAsync>d__13>(Cysharp.Threading.Tasks.UniTask.Awaiter<object>&,XFramework.UIHelper.<CreateAsync>d__13&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter<object>,XFramework.UIHelper.<CreateAsync>d__14<object>>(Cysharp.Threading.Tasks.UniTask.Awaiter<object>&,XFramework.UIHelper.<CreateAsync>d__14<object>&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter<object>,XFramework.UIHelper.<CreateAsync>d__8>(Cysharp.Threading.Tasks.UniTask.Awaiter<object>&,XFramework.UIHelper.<CreateAsync>d__8&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter<object>,XFramework.UIHelper.<CreateAsync>d__9<object>>(Cysharp.Threading.Tasks.UniTask.Awaiter<object>&,XFramework.UIHelper.<CreateAsync>d__9<object>&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter<object>,XFramework.UIHelper.<CreateAsyncNew>d__6<object>>(Cysharp.Threading.Tasks.UniTask.Awaiter<object>&,XFramework.UIHelper.<CreateAsyncNew>d__6<object>&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter<object>,XFramework.UIHelper.<CreateAsyncNew>d__7>(Cysharp.Threading.Tasks.UniTask.Awaiter<object>&,XFramework.UIHelper.<CreateAsyncNew>d__7&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter<object>,XFramework.UIManager.<CreateAsync>d__21>(Cysharp.Threading.Tasks.UniTask.Awaiter<object>&,XFramework.UIManager.<CreateAsync>d__21&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter<object>,XFramework.UIManager.<CreateAsync>d__22<object>>(Cysharp.Threading.Tasks.UniTask.Awaiter<object>&,XFramework.UIManager.<CreateAsync>d__22<object>&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter<object>,XFramework.UIManager.<CreateAsync>d__23<object,object>>(Cysharp.Threading.Tasks.UniTask.Awaiter<object>&,XFramework.UIManager.<CreateAsync>d__23<object,object>&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter<object>,XFramework.UIManager.<CreateAsync>d__24>(Cysharp.Threading.Tasks.UniTask.Awaiter<object>&,XFramework.UIManager.<CreateAsync>d__24&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter<object>,XFramework.UIManager.<CreateAsync>d__25<object>>(Cysharp.Threading.Tasks.UniTask.Awaiter<object>&,XFramework.UIManager.<CreateAsync>d__25<object>&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter<object>,XFramework.UIManager.<CreateAsync>d__26>(Cysharp.Threading.Tasks.UniTask.Awaiter<object>&,XFramework.UIManager.<CreateAsync>d__26&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter<object>,XFramework.UIManager.<CreateAsync>d__27<object>>(Cysharp.Threading.Tasks.UniTask.Awaiter<object>&,XFramework.UIManager.<CreateAsync>d__27<object>&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter<object>,XFramework.UIManager.<CreateAsync>d__28<object,object>>(Cysharp.Threading.Tasks.UniTask.Awaiter<object>&,XFramework.UIManager.<CreateAsync>d__28<object,object>&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter<object>,XFramework.UIManager.<CreateAsyncNew>d__19<object>>(Cysharp.Threading.Tasks.UniTask.Awaiter<object>&,XFramework.UIManager.<CreateAsyncNew>d__19<object>&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter<object>,XFramework.UIManager.<CreateAsyncNew>d__20>(Cysharp.Threading.Tasks.UniTask.Awaiter<object>&,XFramework.UIManager.<CreateAsyncNew>d__20&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter<object>,XFramework.UIManager.<CreateInnerAsync>d__10>(Cysharp.Threading.Tasks.UniTask.Awaiter<object>&,XFramework.UIManager.<CreateInnerAsync>d__10&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter<object>,XFramework.UIManager.<GetGameObjectAsync>d__11>(Cysharp.Threading.Tasks.UniTask.Awaiter<object>&,XFramework.UIManager.<GetGameObjectAsync>d__11&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.Start<HotFix_Logic.HotUpdateMain.<GoToUIScene>d__7>(HotFix_Logic.HotUpdateMain.<GoToUIScene>d__7&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.Start<HotFix_Logic.HotUpdateMain.<InitTypeAndMetaData>d__4>(HotFix_Logic.HotUpdateMain.<InitTypeAndMetaData>d__4&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.Start<HotFix_Logic.HotUpdateMain.<LoadMetadataForAOTAssemblies>d__3>(HotFix_Logic.HotUpdateMain.<LoadMetadataForAOTAssemblies>d__3&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.Start<HotFix_UI.JiYuUIHelper.<DownloadByUrl>d__14>(HotFix_UI.JiYuUIHelper.<DownloadByUrl>d__14&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.Start<HotFix_UI.JsonManager.<SavePlayerData>d__3>(HotFix_UI.JsonManager.<SavePlayerData>d__3&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.Start<XFramework.ImageComponent.<SetSpriteAsync>d__3<object>>(XFramework.ImageComponent.<SetSpriteAsync>d__3<object>&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.Start<XFramework.LoadingScene.<WaitForCompleted>d__3>(XFramework.LoadingScene.<WaitForCompleted>d__3&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.Start<XFramework.ResourcesLoader.<InitAsync>d__0>(XFramework.ResourcesLoader.<InitAsync>d__0&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.Start<XFramework.Scene.<WaitForCompleted>d__14>(XFramework.Scene.<WaitForCompleted>d__14&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.Start<XFramework.SceneResManager.<UnloadSceneAsync>d__6>(XFramework.SceneResManager.<UnloadSceneAsync>d__6&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.Start<XFramework.SceneResManager.<WaitForCompleted>d__7>(XFramework.SceneResManager.<WaitForCompleted>d__7&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.Start<XFramework.UIImageExtensions.<SetSpriteAsync>d__4>(XFramework.UIImageExtensions.<SetSpriteAsync>d__4&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.Start<XFramework.UIPanelInGame.<PlayCardsAnimation>d__24>(XFramework.UIPanelInGame.<PlayCardsAnimation>d__24&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.Start<XFramework.YooResourcesLoader.<InitAsync>d__0>(XFramework.YooResourcesLoader.<InitAsync>d__0&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.Start<XFramework.YooSceneLoader.<UnloadSceneAsync>d__4>(XFramework.YooSceneLoader.<UnloadSceneAsync>d__4&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.Start<XFramework.YooSceneLoader.<WaitForCompleted>d__5>(XFramework.YooSceneLoader.<WaitForCompleted>d__5&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder.Start<cfg.Tables.<LoadAsync>d__10>(cfg.Tables.<LoadAsync>d__10&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.Start<HotFix_UI.JsonManager.<LoadPlayerData>d__4>(HotFix_UI.JsonManager.<LoadPlayerData>d__4&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.Start<XFramework.DemoEntry.<Loader>d__4>(XFramework.DemoEntry.<Loader>d__4&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.Start<XFramework.ResourcesManager.<InstantiateAsync>d__16>(XFramework.ResourcesManager.<InstantiateAsync>d__16&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.Start<XFramework.ResourcesManager.<InstantiateAsync>d__17>(XFramework.ResourcesManager.<InstantiateAsync>d__17&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.Start<XFramework.ResourcesManager.<InstantiateAsync>d__18>(XFramework.ResourcesManager.<InstantiateAsync>d__18&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.Start<XFramework.ResourcesManager.<InstantiateAsync>d__19>(XFramework.ResourcesManager.<InstantiateAsync>d__19&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.Start<XFramework.ResourcesManager.<LoadAssetAsync>d__10<object>>(XFramework.ResourcesManager.<LoadAssetAsync>d__10<object>&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.Start<XFramework.ResourcesManager.<LoadAssetAsync>d__11<object>>(XFramework.ResourcesManager.<LoadAssetAsync>d__11<object>&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.Start<XFramework.ResourcesManager.<LoadAssetAsync>d__12>(XFramework.ResourcesManager.<LoadAssetAsync>d__12&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.Start<XFramework.UIHelper.<CreateAsync>d__10<object,object>>(XFramework.UIHelper.<CreateAsync>d__10<object,object>&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.Start<XFramework.UIHelper.<CreateAsync>d__11>(XFramework.UIHelper.<CreateAsync>d__11&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.Start<XFramework.UIHelper.<CreateAsync>d__12<object>>(XFramework.UIHelper.<CreateAsync>d__12<object>&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.Start<XFramework.UIHelper.<CreateAsync>d__13>(XFramework.UIHelper.<CreateAsync>d__13&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.Start<XFramework.UIHelper.<CreateAsync>d__14<object>>(XFramework.UIHelper.<CreateAsync>d__14<object>&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.Start<XFramework.UIHelper.<CreateAsync>d__8>(XFramework.UIHelper.<CreateAsync>d__8&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.Start<XFramework.UIHelper.<CreateAsync>d__9<object>>(XFramework.UIHelper.<CreateAsync>d__9<object>&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.Start<XFramework.UIHelper.<CreateAsyncNew>d__6<object>>(XFramework.UIHelper.<CreateAsyncNew>d__6<object>&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.Start<XFramework.UIHelper.<CreateAsyncNew>d__7>(XFramework.UIHelper.<CreateAsyncNew>d__7&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.Start<XFramework.UIManager.<CreateAsync>d__21>(XFramework.UIManager.<CreateAsync>d__21&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.Start<XFramework.UIManager.<CreateAsync>d__22<object>>(XFramework.UIManager.<CreateAsync>d__22<object>&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.Start<XFramework.UIManager.<CreateAsync>d__23<object,object>>(XFramework.UIManager.<CreateAsync>d__23<object,object>&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.Start<XFramework.UIManager.<CreateAsync>d__24>(XFramework.UIManager.<CreateAsync>d__24&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.Start<XFramework.UIManager.<CreateAsync>d__25<object>>(XFramework.UIManager.<CreateAsync>d__25<object>&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.Start<XFramework.UIManager.<CreateAsync>d__26>(XFramework.UIManager.<CreateAsync>d__26&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.Start<XFramework.UIManager.<CreateAsync>d__27<object>>(XFramework.UIManager.<CreateAsync>d__27<object>&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.Start<XFramework.UIManager.<CreateAsync>d__28<object,object>>(XFramework.UIManager.<CreateAsync>d__28<object,object>&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.Start<XFramework.UIManager.<CreateAsyncNew>d__19<object>>(XFramework.UIManager.<CreateAsyncNew>d__19<object>&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.Start<XFramework.UIManager.<CreateAsyncNew>d__20>(XFramework.UIManager.<CreateAsyncNew>d__20&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.Start<XFramework.UIManager.<CreateInnerAsync>d__10>(XFramework.UIManager.<CreateInnerAsync>d__10&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.Start<XFramework.UIManager.<GetGameObjectAsync>d__11>(XFramework.UIManager.<GetGameObjectAsync>d__11&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.Start<XFramework.YooResourcesLoader.<InstantiateAsync>d__10>(XFramework.YooResourcesLoader.<InstantiateAsync>d__10&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.Start<XFramework.YooResourcesLoader.<InstantiateAsync>d__11>(XFramework.YooResourcesLoader.<InstantiateAsync>d__11&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.Start<XFramework.YooResourcesLoader.<InstantiateAsync>d__8>(XFramework.YooResourcesLoader.<InstantiateAsync>d__8&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.Start<XFramework.YooResourcesLoader.<InstantiateAsync>d__9>(XFramework.YooResourcesLoader.<InstantiateAsync>d__9&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.Start<XFramework.YooResourcesLoader.<LoadAssetAsync>d__3<object>>(XFramework.YooResourcesLoader.<LoadAssetAsync>d__3<object>&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<object>.Start<XFramework.YooResourcesLoader.<LoadAssetAsync>d__4>(XFramework.YooResourcesLoader.<LoadAssetAsync>d__4&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskVoidMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,HotFix_Logic.HotUpdateMain.<Start>d__6>(Cysharp.Threading.Tasks.UniTask.Awaiter&,HotFix_Logic.HotUpdateMain.<Start>d__6&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskVoidMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter<object>,XFramework.AudioManager.<LoadAudio>d__13>(Cysharp.Threading.Tasks.UniTask.Awaiter<object>&,XFramework.AudioManager.<LoadAudio>d__13&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskVoidMethodBuilder.AwaitUnsafeOnCompleted<object,XFramework.UIPanelInGame.<CreateCardsList>d__21>(object&,XFramework.UIPanelInGame.<CreateCardsList>d__21&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskVoidMethodBuilder.Start<HotFix_Logic.HotUpdateMain.<Start>d__6>(HotFix_Logic.HotUpdateMain.<Start>d__6&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskVoidMethodBuilder.Start<XFramework.AudioManager.<LoadAudio>d__13>(XFramework.AudioManager.<LoadAudio>d__13&)
		// System.Void Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskVoidMethodBuilder.Start<XFramework.UIPanelInGame.<CreateCardsList>d__21>(XFramework.UIPanelInGame.<CreateCardsList>d__21&)
		// Cysharp.Threading.Tasks.UniTask.Awaiter Cysharp.Threading.Tasks.EnumeratorAsyncExtensions.GetAwaiter<object>(object)
		// Cysharp.Threading.Tasks.Internal.StateTuple<Cysharp.Threading.Tasks.UniTask.Awaiter<object>> Cysharp.Threading.Tasks.Internal.StateTuple.Create<Cysharp.Threading.Tasks.UniTask.Awaiter<object>>(Cysharp.Threading.Tasks.UniTask.Awaiter<object>)
		// Cysharp.Threading.Tasks.UniTask<object> Cysharp.Threading.Tasks.UniTaskExtensions.AsUniTask<object>(System.Threading.Tasks.Task<object>,bool)
		// System.Void Cysharp.Threading.Tasks.UniTaskExtensions.Forget<object>(Cysharp.Threading.Tasks.UniTask<object>)
		// DG.Tweening.Core.TweenerCore<UnityEngine.Color,UnityEngine.Color,DG.Tweening.Plugins.Options.ColorOptions> DG.Tweening.Core.Extensions.Blendable<UnityEngine.Color,UnityEngine.Color,DG.Tweening.Plugins.Options.ColorOptions>(DG.Tweening.Core.TweenerCore<UnityEngine.Color,UnityEngine.Color,DG.Tweening.Plugins.Options.ColorOptions>)
		// object DG.Tweening.Core.Extensions.SetSpecialStartupMode<object>(object,DG.Tweening.Core.Enums.SpecialStartupMode)
		// DG.Tweening.Core.TweenerCore<UnityEngine.Vector2,UnityEngine.Vector2,DG.Tweening.Plugins.CircleOptions> DG.Tweening.Core.TweenManager.GetTweener<UnityEngine.Vector2,UnityEngine.Vector2,DG.Tweening.Plugins.CircleOptions>()
		// DG.Tweening.Core.TweenerCore<UnityEngine.Vector3,object,DG.Tweening.Plugins.Options.PathOptions> DG.Tweening.Core.TweenManager.GetTweener<UnityEngine.Vector3,object,DG.Tweening.Plugins.Options.PathOptions>()
		// DG.Tweening.Core.TweenerCore<UnityEngine.Vector2,UnityEngine.Vector2,DG.Tweening.Plugins.CircleOptions> DG.Tweening.DOTween.ApplyTo<UnityEngine.Vector2,UnityEngine.Vector2,DG.Tweening.Plugins.CircleOptions>(DG.Tweening.Core.DOGetter<UnityEngine.Vector2>,DG.Tweening.Core.DOSetter<UnityEngine.Vector2>,UnityEngine.Vector2,float,DG.Tweening.Plugins.Core.ABSTweenPlugin<UnityEngine.Vector2,UnityEngine.Vector2,DG.Tweening.Plugins.CircleOptions>)
		// DG.Tweening.Core.TweenerCore<UnityEngine.Vector3,object,DG.Tweening.Plugins.Options.PathOptions> DG.Tweening.DOTween.ApplyTo<UnityEngine.Vector3,object,DG.Tweening.Plugins.Options.PathOptions>(DG.Tweening.Core.DOGetter<UnityEngine.Vector3>,DG.Tweening.Core.DOSetter<UnityEngine.Vector3>,object,float,DG.Tweening.Plugins.Core.ABSTweenPlugin<UnityEngine.Vector3,object,DG.Tweening.Plugins.Options.PathOptions>)
		// DG.Tweening.Core.TweenerCore<UnityEngine.Vector2,UnityEngine.Vector2,DG.Tweening.Plugins.CircleOptions> DG.Tweening.DOTween.To<UnityEngine.Vector2,UnityEngine.Vector2,DG.Tweening.Plugins.CircleOptions>(DG.Tweening.Plugins.Core.ABSTweenPlugin<UnityEngine.Vector2,UnityEngine.Vector2,DG.Tweening.Plugins.CircleOptions>,DG.Tweening.Core.DOGetter<UnityEngine.Vector2>,DG.Tweening.Core.DOSetter<UnityEngine.Vector2>,UnityEngine.Vector2,float)
		// DG.Tweening.Core.TweenerCore<UnityEngine.Vector3,object,DG.Tweening.Plugins.Options.PathOptions> DG.Tweening.DOTween.To<UnityEngine.Vector3,object,DG.Tweening.Plugins.Options.PathOptions>(DG.Tweening.Plugins.Core.ABSTweenPlugin<UnityEngine.Vector3,object,DG.Tweening.Plugins.Options.PathOptions>,DG.Tweening.Core.DOGetter<UnityEngine.Vector3>,DG.Tweening.Core.DOSetter<UnityEngine.Vector3>,object,float)
		// DG.Tweening.Plugins.Core.ABSTweenPlugin<UnityEngine.Vector2,UnityEngine.Vector2,DG.Tweening.Plugins.CircleOptions> DG.Tweening.Plugins.Core.PluginsManager.GetDefaultPlugin<UnityEngine.Vector2,UnityEngine.Vector2,DG.Tweening.Plugins.CircleOptions>()
		// DG.Tweening.Plugins.Core.ABSTweenPlugin<UnityEngine.Vector3,object,DG.Tweening.Plugins.Options.PathOptions> DG.Tweening.Plugins.Core.PluginsManager.GetDefaultPlugin<UnityEngine.Vector3,object,DG.Tweening.Plugins.Options.PathOptions>()
		// object DG.Tweening.TweenSettingsExtensions.OnComplete<object>(object,DG.Tweening.TweenCallback)
		// object DG.Tweening.TweenSettingsExtensions.OnStart<object>(object,DG.Tweening.TweenCallback)
		// object DG.Tweening.TweenSettingsExtensions.OnUpdate<object>(object,DG.Tweening.TweenCallback)
		// object DG.Tweening.TweenSettingsExtensions.SetEase<object>(object,DG.Tweening.Ease)
		// object DG.Tweening.TweenSettingsExtensions.SetLoops<object>(object,int,DG.Tweening.LoopType)
		// object DG.Tweening.TweenSettingsExtensions.SetRelative<object>(object)
		// object DG.Tweening.TweenSettingsExtensions.SetTarget<object>(object,object)
		// object DG.Tweening.TweenSettingsExtensions.SetUpdate<object>(object,DG.Tweening.UpdateType)
		// bool DG.Tweening.Tweener.Setup<UnityEngine.Vector2,UnityEngine.Vector2,DG.Tweening.Plugins.CircleOptions>(DG.Tweening.Core.TweenerCore<UnityEngine.Vector2,UnityEngine.Vector2,DG.Tweening.Plugins.CircleOptions>,DG.Tweening.Core.DOGetter<UnityEngine.Vector2>,DG.Tweening.Core.DOSetter<UnityEngine.Vector2>,UnityEngine.Vector2,float,DG.Tweening.Plugins.Core.ABSTweenPlugin<UnityEngine.Vector2,UnityEngine.Vector2,DG.Tweening.Plugins.CircleOptions>)
		// bool DG.Tweening.Tweener.Setup<UnityEngine.Vector3,object,DG.Tweening.Plugins.Options.PathOptions>(DG.Tweening.Core.TweenerCore<UnityEngine.Vector3,object,DG.Tweening.Plugins.Options.PathOptions>,DG.Tweening.Core.DOGetter<UnityEngine.Vector3>,DG.Tweening.Core.DOSetter<UnityEngine.Vector3>,object,float,DG.Tweening.Plugins.Core.ABSTweenPlugin<UnityEngine.Vector3,object,DG.Tweening.Plugins.Options.PathOptions>)
		// object Newtonsoft.Json.JsonConvert.DeserializeObject<object>(string)
		// object Newtonsoft.Json.JsonConvert.DeserializeObject<object>(string,Newtonsoft.Json.JsonSerializerSettings)
		// object System.Activator.CreateInstance<object>()
		// byte[] System.Array.Empty<byte>()
		// object[] System.Array.Empty<object>()
		// bool System.Linq.Enumerable.Any<HotFix_UI.Card>(System.Collections.Generic.IEnumerable<HotFix_UI.Card>,System.Func<HotFix_UI.Card,bool>)
		// bool System.Linq.Enumerable.Any<object>(System.Collections.Generic.IEnumerable<object>)
		// bool System.Linq.Enumerable.Any<object>(System.Collections.Generic.IEnumerable<object>,System.Func<object,bool>)
		// int System.Linq.Enumerable.Count<HotFix_UI.Card>(System.Collections.Generic.IEnumerable<HotFix_UI.Card>)
		// int System.Linq.Enumerable.Count<object>(System.Collections.Generic.IEnumerable<object>,System.Func<object,bool>)
		// System.Collections.Generic.KeyValuePair<object,object> System.Linq.Enumerable.ElementAt<System.Collections.Generic.KeyValuePair<object,object>>(System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<object,object>>,int)
		// System.Collections.Generic.IEnumerable<HotFix_UI.Card> System.Linq.Enumerable.Except<HotFix_UI.Card>(System.Collections.Generic.IEnumerable<HotFix_UI.Card>,System.Collections.Generic.IEnumerable<HotFix_UI.Card>)
		// System.Collections.Generic.IEnumerable<HotFix_UI.Card> System.Linq.Enumerable.ExceptIterator<HotFix_UI.Card>(System.Collections.Generic.IEnumerable<HotFix_UI.Card>,System.Collections.Generic.IEnumerable<HotFix_UI.Card>,System.Collections.Generic.IEqualityComparer<HotFix_UI.Card>)
		// System.Collections.Generic.KeyValuePair<object,object> System.Linq.Enumerable.First<System.Collections.Generic.KeyValuePair<object,object>>(System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<object,object>>)
		// System.Collections.Generic.IEnumerable<System.Linq.IGrouping<int,HotFix_UI.Card>> System.Linq.Enumerable.GroupBy<HotFix_UI.Card,int>(System.Collections.Generic.IEnumerable<HotFix_UI.Card>,System.Func<HotFix_UI.Card,int>)
		// System.Collections.Generic.IEnumerable<object> System.Linq.Enumerable.OfType<object>(System.Collections.IEnumerable)
		// System.Collections.Generic.IEnumerable<object> System.Linq.Enumerable.OfTypeIterator<object>(System.Collections.IEnumerable)
		// System.Linq.IOrderedEnumerable<HotFix_UI.Card> System.Linq.Enumerable.OrderBy<HotFix_UI.Card,float>(System.Collections.Generic.IEnumerable<HotFix_UI.Card>,System.Func<HotFix_UI.Card,float>)
		// System.Linq.IOrderedEnumerable<HotFix_UI.Card> System.Linq.Enumerable.OrderBy<HotFix_UI.Card,int>(System.Collections.Generic.IEnumerable<HotFix_UI.Card>,System.Func<HotFix_UI.Card,int>)
		// System.Linq.IOrderedEnumerable<HotFix_UI.Card> System.Linq.Enumerable.OrderByDescending<HotFix_UI.Card,int>(System.Collections.Generic.IEnumerable<HotFix_UI.Card>,System.Func<HotFix_UI.Card,int>)
		// System.Linq.IOrderedEnumerable<object> System.Linq.Enumerable.OrderByDescending<object,int>(System.Collections.Generic.IEnumerable<object>,System.Func<object,int>)
		// System.Collections.Generic.IEnumerable<int> System.Linq.Enumerable.Select<object,int>(System.Collections.Generic.IEnumerable<object>,System.Func<object,int>)
		// System.Collections.Generic.IEnumerable<object> System.Linq.Enumerable.Select<object,object>(System.Collections.Generic.IEnumerable<object>,System.Func<object,object>)
		// System.Collections.Generic.IEnumerable<HotFix_UI.Card> System.Linq.Enumerable.SelectMany<object,HotFix_UI.Card>(System.Collections.Generic.IEnumerable<object>,System.Func<object,System.Collections.Generic.IEnumerable<HotFix_UI.Card>>)
		// System.Collections.Generic.IEnumerable<HotFix_UI.Card> System.Linq.Enumerable.SelectManyIterator<object,HotFix_UI.Card>(System.Collections.Generic.IEnumerable<object>,System.Func<object,System.Collections.Generic.IEnumerable<HotFix_UI.Card>>)
		// System.Collections.Generic.IEnumerable<HotFix_UI.Card> System.Linq.Enumerable.Take<HotFix_UI.Card>(System.Collections.Generic.IEnumerable<HotFix_UI.Card>,int)
		// System.Collections.Generic.IEnumerable<HotFix_UI.Card> System.Linq.Enumerable.TakeIterator<HotFix_UI.Card>(System.Collections.Generic.IEnumerable<HotFix_UI.Card>,int)
		// System.Collections.Generic.List<HotFix_UI.Card> System.Linq.Enumerable.ToList<HotFix_UI.Card>(System.Collections.Generic.IEnumerable<HotFix_UI.Card>)
		// System.Collections.Generic.List<int> System.Linq.Enumerable.ToList<int>(System.Collections.Generic.IEnumerable<int>)
		// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<object,object>> System.Linq.Enumerable.Where<System.Collections.Generic.KeyValuePair<object,object>>(System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<object,object>>,System.Func<System.Collections.Generic.KeyValuePair<object,object>,bool>)
		// System.Collections.Generic.IEnumerable<object> System.Linq.Enumerable.Where<object>(System.Collections.Generic.IEnumerable<object>,System.Func<object,bool>)
		// System.Collections.Generic.IEnumerable<int> System.Linq.Enumerable.Iterator<object>.Select<int>(System.Func<object,int>)
		// System.Collections.Generic.IEnumerable<object> System.Linq.Enumerable.Iterator<object>.Select<object>(System.Func<object,object>)
		// System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.YieldAwaitable.YieldAwaiter,DG.Tweening.DOTweenModuleUnityVersion.<AsyncWaitForCompletion>d__10>(System.Runtime.CompilerServices.YieldAwaitable.YieldAwaiter&,DG.Tweening.DOTweenModuleUnityVersion.<AsyncWaitForCompletion>d__10&)
		// System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.YieldAwaitable.YieldAwaiter,DG.Tweening.DOTweenModuleUnityVersion.<AsyncWaitForElapsedLoops>d__13>(System.Runtime.CompilerServices.YieldAwaitable.YieldAwaiter&,DG.Tweening.DOTweenModuleUnityVersion.<AsyncWaitForElapsedLoops>d__13&)
		// System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.YieldAwaitable.YieldAwaiter,DG.Tweening.DOTweenModuleUnityVersion.<AsyncWaitForKill>d__12>(System.Runtime.CompilerServices.YieldAwaitable.YieldAwaiter&,DG.Tweening.DOTweenModuleUnityVersion.<AsyncWaitForKill>d__12&)
		// System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.YieldAwaitable.YieldAwaiter,DG.Tweening.DOTweenModuleUnityVersion.<AsyncWaitForPosition>d__14>(System.Runtime.CompilerServices.YieldAwaitable.YieldAwaiter&,DG.Tweening.DOTweenModuleUnityVersion.<AsyncWaitForPosition>d__14&)
		// System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.YieldAwaitable.YieldAwaiter,DG.Tweening.DOTweenModuleUnityVersion.<AsyncWaitForRewind>d__11>(System.Runtime.CompilerServices.YieldAwaitable.YieldAwaiter&,DG.Tweening.DOTweenModuleUnityVersion.<AsyncWaitForRewind>d__11&)
		// System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.YieldAwaitable.YieldAwaiter,DG.Tweening.DOTweenModuleUnityVersion.<AsyncWaitForStart>d__15>(System.Runtime.CompilerServices.YieldAwaitable.YieldAwaiter&,DG.Tweening.DOTweenModuleUnityVersion.<AsyncWaitForStart>d__15&)
		// System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder<System.Threading.Tasks.VoidTaskResult>.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.YieldAwaitable.YieldAwaiter,DG.Tweening.DOTweenModuleUnityVersion.<AsyncWaitForCompletion>d__10>(System.Runtime.CompilerServices.YieldAwaitable.YieldAwaiter&,DG.Tweening.DOTweenModuleUnityVersion.<AsyncWaitForCompletion>d__10&)
		// System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder<System.Threading.Tasks.VoidTaskResult>.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.YieldAwaitable.YieldAwaiter,DG.Tweening.DOTweenModuleUnityVersion.<AsyncWaitForElapsedLoops>d__13>(System.Runtime.CompilerServices.YieldAwaitable.YieldAwaiter&,DG.Tweening.DOTweenModuleUnityVersion.<AsyncWaitForElapsedLoops>d__13&)
		// System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder<System.Threading.Tasks.VoidTaskResult>.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.YieldAwaitable.YieldAwaiter,DG.Tweening.DOTweenModuleUnityVersion.<AsyncWaitForKill>d__12>(System.Runtime.CompilerServices.YieldAwaitable.YieldAwaiter&,DG.Tweening.DOTweenModuleUnityVersion.<AsyncWaitForKill>d__12&)
		// System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder<System.Threading.Tasks.VoidTaskResult>.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.YieldAwaitable.YieldAwaiter,DG.Tweening.DOTweenModuleUnityVersion.<AsyncWaitForPosition>d__14>(System.Runtime.CompilerServices.YieldAwaitable.YieldAwaiter&,DG.Tweening.DOTweenModuleUnityVersion.<AsyncWaitForPosition>d__14&)
		// System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder<System.Threading.Tasks.VoidTaskResult>.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.YieldAwaitable.YieldAwaiter,DG.Tweening.DOTweenModuleUnityVersion.<AsyncWaitForRewind>d__11>(System.Runtime.CompilerServices.YieldAwaitable.YieldAwaiter&,DG.Tweening.DOTweenModuleUnityVersion.<AsyncWaitForRewind>d__11&)
		// System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder<System.Threading.Tasks.VoidTaskResult>.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.YieldAwaitable.YieldAwaiter,DG.Tweening.DOTweenModuleUnityVersion.<AsyncWaitForStart>d__15>(System.Runtime.CompilerServices.YieldAwaitable.YieldAwaiter&,DG.Tweening.DOTweenModuleUnityVersion.<AsyncWaitForStart>d__15&)
		// System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder.Start<DG.Tweening.DOTweenModuleUnityVersion.<AsyncWaitForCompletion>d__10>(DG.Tweening.DOTweenModuleUnityVersion.<AsyncWaitForCompletion>d__10&)
		// System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder.Start<DG.Tweening.DOTweenModuleUnityVersion.<AsyncWaitForElapsedLoops>d__13>(DG.Tweening.DOTweenModuleUnityVersion.<AsyncWaitForElapsedLoops>d__13&)
		// System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder.Start<DG.Tweening.DOTweenModuleUnityVersion.<AsyncWaitForKill>d__12>(DG.Tweening.DOTweenModuleUnityVersion.<AsyncWaitForKill>d__12&)
		// System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder.Start<DG.Tweening.DOTweenModuleUnityVersion.<AsyncWaitForPosition>d__14>(DG.Tweening.DOTweenModuleUnityVersion.<AsyncWaitForPosition>d__14&)
		// System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder.Start<DG.Tweening.DOTweenModuleUnityVersion.<AsyncWaitForRewind>d__11>(DG.Tweening.DOTweenModuleUnityVersion.<AsyncWaitForRewind>d__11&)
		// System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder.Start<DG.Tweening.DOTweenModuleUnityVersion.<AsyncWaitForStart>d__15>(DG.Tweening.DOTweenModuleUnityVersion.<AsyncWaitForStart>d__15&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,XFramework.DemoEntry.<LoadAsync>d__3>(Cysharp.Threading.Tasks.UniTask.Awaiter&,XFramework.DemoEntry.<LoadAsync>d__3&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,XFramework.DoTweenEffect.<>c__DisplayClass6_0.<<DoScaleTweenOnClickAndLongPress>b__0>d>(Cysharp.Threading.Tasks.UniTask.Awaiter&,XFramework.DoTweenEffect.<>c__DisplayClass6_0.<<DoScaleTweenOnClickAndLongPress>b__0>d&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter,XFramework.UIPanelInGame.<>c__DisplayClass24_1.<<PlayCardsAnimation>b__1>d>(Cysharp.Threading.Tasks.UniTask.Awaiter&,XFramework.UIPanelInGame.<>c__DisplayClass24_1.<<PlayCardsAnimation>b__1>d&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter<object>,XFramework.AudioManager.<PlayAudio>d__34>(Cysharp.Threading.Tasks.UniTask.Awaiter<object>&,XFramework.AudioManager.<PlayAudio>d__34&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter<object>,XFramework.GraphicComponentExtensions.<SetMaterial>d__1>(Cysharp.Threading.Tasks.UniTask.Awaiter<object>&,XFramework.GraphicComponentExtensions.<SetMaterial>d__1&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter<object>,XFramework.MenuScene.<OnCompleted>d__1>(Cysharp.Threading.Tasks.UniTask.Awaiter<object>&,XFramework.MenuScene.<OnCompleted>d__1&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<Cysharp.Threading.Tasks.UniTask.Awaiter<object>,XFramework.UIImageExtensions.<SetSprite>d__5>(Cysharp.Threading.Tasks.UniTask.Awaiter<object>&,XFramework.UIImageExtensions.<SetSprite>d__5&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<XFramework.AudioManager.<PlayAudio>d__34>(XFramework.AudioManager.<PlayAudio>d__34&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<XFramework.DemoEntry.<LoadAsync>d__3>(XFramework.DemoEntry.<LoadAsync>d__3&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<XFramework.DoTweenEffect.<>c__DisplayClass6_0.<<DoScaleTweenOnClickAndLongPress>b__0>d>(XFramework.DoTweenEffect.<>c__DisplayClass6_0.<<DoScaleTweenOnClickAndLongPress>b__0>d&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<XFramework.GraphicComponentExtensions.<SetMaterial>d__1>(XFramework.GraphicComponentExtensions.<SetMaterial>d__1&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<XFramework.MenuScene.<OnCompleted>d__1>(XFramework.MenuScene.<OnCompleted>d__1&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<XFramework.UIImageExtensions.<SetSprite>d__5>(XFramework.UIImageExtensions.<SetSprite>d__5&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<XFramework.UIPanelInGame.<>c__DisplayClass24_1.<<PlayCardsAnimation>b__1>d>(XFramework.UIPanelInGame.<>c__DisplayClass24_1.<<PlayCardsAnimation>b__1>d&)
		// System.Threading.Tasks.Task<object> System.Threading.Tasks.Task.Run<object>(System.Func<object>)
		// object UnityEngine.Component.GetComponent<object>()
		// object UnityEngine.Component.GetComponentInParent<object>()
		// object UnityEngine.GameObject.AddComponent<object>()
		// object UnityEngine.GameObject.GetComponent<object>()
		// System.Void UnityEngine.GameObject.GetComponentsInChildren<object>(bool,System.Collections.Generic.List<object>)
		// object[] UnityEngine.GameObject.GetComponentsInChildren<object>()
		// object[] UnityEngine.GameObject.GetComponentsInChildren<object>(bool)
		// bool UnityEngine.GameObject.TryGetComponent<object>(object&)
		// object UnityEngine.Object.FindObjectOfType<object>()
		// object UnityEngine.Object.Instantiate<object>(object)
		// YooAsset.AssetHandle YooAsset.ResourcePackage.LoadAssetAsync<object>(string,uint)
		// YooAsset.AssetHandle YooAsset.ResourcePackage.LoadAssetSync<object>(string)
		// string string.Join<object>(string,System.Collections.Generic.IEnumerable<object>)
		// string string.JoinCore<object>(System.Char*,int,System.Collections.Generic.IEnumerable<object>)
	}
}