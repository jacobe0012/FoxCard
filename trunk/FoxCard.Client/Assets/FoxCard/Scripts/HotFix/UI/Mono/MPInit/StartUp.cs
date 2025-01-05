// using MessagePack;
// using MessagePack.Resolvers;
// using UnityEngine;
// using XFramework;
//
// public static class Startup
// {
//     static bool serializerRegistered = false;
//
//     [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
//     public static void Initialize()
//     {
//         if (!serializerRegistered)
//         {
//             StaticCompositeResolver.Instance.Register(
//                 MessagePack.Resolvers.GeneratedResolver.Instance,
//                 MessagePack.Resolvers.StandardResolver.Instance
//             );
//
//             var option = MessagePackSerializerOptions.Standard.WithResolver(StaticCompositeResolver.Instance);
//
//             MessagePackSerializer.DefaultOptions = option;
//             serializerRegistered = true;
//             Log.Debug($"MessagePackSerializer Startup");
//         }
//     }
//
// #if UNITY_EDITOR
//
//
//     [UnityEditor.InitializeOnLoadMethod]
//     static void EditorInitialize()
//     {
//         Initialize();
//     }
//
// #endif
// }