// //---------------------------------------------------------------------
// // JiYuStudio
// // Author: 格伦
// // Time: 2023-07-31 10:50:10
// //---------------------------------------------------------------------
//
//
// using System;
//
// using Main;
// using Unity.Collections;
//
// using Unity.Mathematics;
//
// using UnityEngine;
//
// namespace HotFix_UI
// {
//     //TODO: 边缘平滑过渡处理
//     //同步相机对于玩家的位置 
//     public class CameraUpdateMono : MonoBehaviour
//     {
//         // private EntityManager entityManager;
//         // private EntityQuery playerQuery;
//         // private EntityQuery boardLeftQuery;
//         // private EntityQuery boardRightQuery;
//         // private EntityQuery mapQuery;
//         private bool isInit;
//         private Vector3 offset;
//
//         private float minX;
//         private float maxX;
//
//
//         [Range(0, 1)] public float smoothSpeed = 0.125f;
//         public float cameraWidth = 120f;
//         public float mapWidth = 120f;
//
//         [Range(0, 1)] public float showBorderRatios = 0.5f;
//         [Range(-90, 90)] public float rotateDegrees = 15f;
//         const float maxFOV = 70f;
//         const float minFOV = 10f;
//
//
//         const float maxFOVToX = 33f;
//         //0
//
//         const float minFOVToX = 67f;
//         //10
//
//         private Camera camera;
//
//         //public bool onlyRotateMap;
//
//         private bool isDone;
//
//         // private Tbscene_module scene_moduleConfig;
//         // private Tblevel levelsConfig;
//         // private Tbscene sceneConfig;
//
//         private void Start()
//         {
//             entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
//             playerQuery = entityManager.CreateEntityQuery(typeof(PlayerData), typeof(LocalTransform));
//             //boardLeftQuery = entityManager.CreateEntityQuery(typeof(BoardLeft));
//             //boardRightQuery = entityManager.CreateEntityQuery(typeof(BoardRight));
//             mapQuery = entityManager.CreateEntityQuery(typeof(MapBaseData));
//
//             camera = GetComponent<Camera>();
//         }
//
//         // public void SetMap(bool onlyRotateMap)
//         // {
//         //     var boardLeft = boardLeftQuery.ToEntityArray(Allocator.Temp);
//         //     var boardRight = boardRightQuery.ToEntityArray(Allocator.Temp);
//         //     var map = mapQuery.ToEntityArray(Allocator.Temp);
//         //
//         //
//         //     var parent = entityManager.CreateEntity(typeof(Child), typeof(LocalTransform),
//         //         typeof(LocalToWorld));
//         //
//         //     var child = entityManager.GetBuffer<Child>(parent);
//         //     if (onlyRotateMap)
//         //     {
//         //         camera.transform.rotation = quaternion.EulerXYZ(0, 0, 0);
//         //
//         //         foreach (var e in boardLeft)
//         //         {
//         //             child.Add(new Child
//         //             {
//         //                 Value = e
//         //             });
//         //         }
//         //
//         //         foreach (var e in boardRight)
//         //         {
//         //             child.Add(new Child
//         //             {
//         //                 Value = e
//         //             });
//         //         }
//         //
//         //         foreach (var e in map)
//         //         {
//         //             child.Add(new Child
//         //             {
//         //                 Value = e
//         //             });
//         //         }
//         //
//         //         var parenttran = entityManager.GetComponentData<LocalTransform>(parent);
//         //
//         //         //parenttran.RotateX(math.radians(rotateDegrees));
//         //         var qua = quaternion.RotateX(math.radians(rotateDegrees));
//         //         parenttran.Scale = 1;
//         //         parenttran.Rotation = qua;
//         //         entityManager.SetComponentData<LocalTransform>(parent, parenttran);
//         //     }
//         //     else
//         //     {
//         //         camera.transform.rotation = quaternion.EulerXYZ(-math.radians(rotateDegrees), 0, 0);
//         //
//         //         foreach (var e in boardLeft)
//         //         {
//         //             child.Add(new Child
//         //             {
//         //                 Value = e
//         //             });
//         //         }
//         //
//         //         foreach (var e in boardRight)
//         //         {
//         //             child.Add(new Child
//         //             {
//         //                 Value = e
//         //             });
//         //         }
//         //
//         //         foreach (var e in map)
//         //         {
//         //             child.Add(new Child
//         //             {
//         //                 Value = e
//         //             });
//         //         }
//         //
//         //         var parenttran = entityManager.GetComponentData<LocalTransform>(parent);
//         //
//         //         //parenttran.RotateX(math.radians(rotateDegrees));
//         //         var qua = quaternion.RotateX(math.radians(0));
//         //         parenttran.Scale = 1;
//         //         parenttran.Rotation = qua;
//         //         entityManager.SetComponentData<LocalTransform>(parent, parenttran);
//         //     }
//         //
//         //
//         //     foreach (var e in boardLeft)
//         //     {
//         //         entityManager.AddComponentData(e, new Parent
//         //         {
//         //             Value = parent
//         //         });
//         //     }
//         //
//         //     foreach (var e in boardRight)
//         //     {
//         //         entityManager.AddComponentData(e, new Parent
//         //         {
//         //             Value = parent
//         //         });
//         //     }
//         //
//         //     foreach (var e in map)
//         //     {
//         //         entityManager.AddComponentData(e, new Parent
//         //         {
//         //             Value = parent
//         //         });
//         //     }
//         // }
//
//         public static Rect GetPerspectiveCameraRect(Camera camera, float distance)
//         {
//             Vector3 cameraPos = camera.transform.position;
//             if (distance <= 0 || distance > camera.farClipPlane)
//             {
//                 distance = camera.farClipPlane;
//             }
//
//             // fov表示相机的垂直视野角度
//             // 2 * tan(fov/2) = height / distance
//             float height = 2 * distance * Mathf.Tan(camera.fieldOfView / 2 * Mathf.Deg2Rad);
//             float width = height * camera.aspect;
//
//             Rect rect = new Rect
//             {
//                 xMin = cameraPos.x - width / 2,
//                 xMax = cameraPos.x + width / 2,
//                 yMin = cameraPos.y - height / 2,
//                 yMax = cameraPos.y + height / 2
//             };
//             return rect;
//         }
//
//         private void LateUpdate()
//         {
//             //Camera mainCamera = Camera.main;
//             if (playerQuery.IsEmpty) return;
//             if (!isInit)
//             {
//                 offset = transform.position;
//                 scene_moduleConfig = ConfigManager.Instance.Tables.Tbscene_module;
//                 levelsConfig = ConfigManager.Instance.Tables.Tblevel;
//                 sceneConfig = ConfigManager.Instance.Tables.Tbscene;
//                 isInit = true;
//             }
//
//             //var rect = GetPerspectiveCameraRect(camera, 1000);
//             //Debug.LogError($"{rect}");
//
//
//             var mapId = sceneConfig.Get(levelsConfig.Get(ResourcesSingleton.Instance.levelId).sceneId).mapId;
//             var map = scene_moduleConfig.Get(mapId);
//
//             //const float scale = 11.71875f;
//             //const float ppu = 100f;
//             // const float mapPiexlWidth = 1024f;
//             // const float mapPiexlHeight = 1024f;
//             //
//             // const float mapBorderPiexlWidth = 512f;
//             // const float mapBorderPiexlHeight = 1024f;
//
//             // float mapPiexlWidthToUnit = mapPiexlWidth / ppu * scale;
//             // float mapPiexlHeightToUnit = mapPiexlHeight / ppu * scale;
//
//             // float mapBorderPiexlWidthToUnit = (mapBorderPiexlWidth / ppu) * scale;
//             // float mapBorderPiexlHeightToUnit = (mapBorderPiexlHeight / ppu) * scale;
//
//             float camerascale = cameraWidth / mapWidth;
//             //var truecameraWidth = mapPiexlWidthToUnit * camerascale;
//
//
//             //camera.fieldOfView = math.clamp(maxFOV * camerascale, minFOV, maxFOV);
//
//
//             // minX = (boardLeft.Position.x - mapBorderPiexlWidthToUnit / 2f);
//             // maxX = (boardRight.Position.x + mapBorderPiexlWidthToUnit / 2f);
//
//
//             var player = playerQuery.ToComponentDataArray<LocalTransform>(Allocator.Temp);
//             foreach (var tran in player)
//             {
//                 var eVector3 = new Vector3(tran.Position.x, tran.Position.y, tran.Position.z);
//                 //
//                 //
//                 // if (!float.IsNaN(eVector3.x) && !float.IsNaN(eVector3.y) &&
//                 //     !float.IsNaN(eVector3.z))
//                 // {
//                 //     transform.position = eVector3 + offset;
//                 // }
//                 Vector3 desiredPosition = eVector3 + offset;
//                 Vector3 smoothedPosition = math.lerp(transform.position, desiredPosition, smoothSpeed);
//                 //var smoothedPosition = desiredPosition;
//                 // 限制相机的 X 坐标在指定范围内
//
//                 // 1-左右封闭
//                 // 2-上下封闭
//                 // 3-全封闭
//                 // 4-全开放
//                 float absX = math.abs((camera.fieldOfView - maxFOV) *
//                                       ((minFOVToX - maxFOVToX * showBorderRatios) / (minFOV - maxFOV)) +
//                                       maxFOVToX * showBorderRatios);
//                 switch (4)
//                     //switch (map.mapType)
//                 {
//                     case 1:
//                         smoothedPosition.x = math.clamp(smoothedPosition.x, -absX, absX);
//                         break;
//                     case 2:
//                         smoothedPosition.y = math.clamp(smoothedPosition.y, -absX, absX);
//                         break;
//                     case 3:
//                         smoothedPosition.x = math.clamp(smoothedPosition.x, -absX, absX);
//                         smoothedPosition.y = math.clamp(smoothedPosition.y, -absX, absX);
//                         break;
//                     case 4:
//
//                         break;
//                 }
//
//
//                 if (!float.IsNaN(smoothedPosition.x) && !float.IsNaN(smoothedPosition.y) &&
//                     !float.IsNaN(smoothedPosition.z))
//                 {
//                     transform.position = smoothedPosition;
//                 }
//             }
//         }
//     }
// }// //---------------------------------------------------------------------
// // JiYuStudio
// // Author: 格伦
// // Time: 2023-07-31 10:50:10
// //---------------------------------------------------------------------
//
//
// using System;
//
// using Main;
// using Unity.Collections;
//
// using Unity.Mathematics;
//
// using UnityEngine;
//
// namespace HotFix_UI
// {
//     //TODO: 边缘平滑过渡处理
//     //同步相机对于玩家的位置 
//     public class CameraUpdateMono : MonoBehaviour
//     {
//         // private EntityManager entityManager;
//         // private EntityQuery playerQuery;
//         // private EntityQuery boardLeftQuery;
//         // private EntityQuery boardRightQuery;
//         // private EntityQuery mapQuery;
//         private bool isInit;
//         private Vector3 offset;
//
//         private float minX;
//         private float maxX;
//
//
//         [Range(0, 1)] public float smoothSpeed = 0.125f;
//         public float cameraWidth = 120f;
//         public float mapWidth = 120f;
//
//         [Range(0, 1)] public float showBorderRatios = 0.5f;
//         [Range(-90, 90)] public float rotateDegrees = 15f;
//         const float maxFOV = 70f;
//         const float minFOV = 10f;
//
//
//         const float maxFOVToX = 33f;
//         //0
//
//         const float minFOVToX = 67f;
//         //10
//
//         private Camera camera;
//
//         //public bool onlyRotateMap;
//
//         private bool isDone;
//
//         // private Tbscene_module scene_moduleConfig;
//         // private Tblevel levelsConfig;
//         // private Tbscene sceneConfig;
//
//         private void Start()
//         {
//             entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
//             playerQuery = entityManager.CreateEntityQuery(typeof(PlayerData), typeof(LocalTransform));
//             //boardLeftQuery = entityManager.CreateEntityQuery(typeof(BoardLeft));
//             //boardRightQuery = entityManager.CreateEntityQuery(typeof(BoardRight));
//             mapQuery = entityManager.CreateEntityQuery(typeof(MapBaseData));
//
//             camera = GetComponent<Camera>();
//         }
//
//         // public void SetMap(bool onlyRotateMap)
//         // {
//         //     var boardLeft = boardLeftQuery.ToEntityArray(Allocator.Temp);
//         //     var boardRight = boardRightQuery.ToEntityArray(Allocator.Temp);
//         //     var map = mapQuery.ToEntityArray(Allocator.Temp);
//         //
//         //
//         //     var parent = entityManager.CreateEntity(typeof(Child), typeof(LocalTransform),
//         //         typeof(LocalToWorld));
//         //
//         //     var child = entityManager.GetBuffer<Child>(parent);
//         //     if (onlyRotateMap)
//         //     {
//         //         camera.transform.rotation = quaternion.EulerXYZ(0, 0, 0);
//         //
//         //         foreach (var e in boardLeft)
//         //         {
//         //             child.Add(new Child
//         //             {
//         //                 Value = e
//         //             });
//         //         }
//         //
//         //         foreach (var e in boardRight)
//         //         {
//         //             child.Add(new Child
//         //             {
//         //                 Value = e
//         //             });
//         //         }
//         //
//         //         foreach (var e in map)
//         //         {
//         //             child.Add(new Child
//         //             {
//         //                 Value = e
//         //             });
//         //         }
//         //
//         //         var parenttran = entityManager.GetComponentData<LocalTransform>(parent);
//         //
//         //         //parenttran.RotateX(math.radians(rotateDegrees));
//         //         var qua = quaternion.RotateX(math.radians(rotateDegrees));
//         //         parenttran.Scale = 1;
//         //         parenttran.Rotation = qua;
//         //         entityManager.SetComponentData<LocalTransform>(parent, parenttran);
//         //     }
//         //     else
//         //     {
//         //         camera.transform.rotation = quaternion.EulerXYZ(-math.radians(rotateDegrees), 0, 0);
//         //
//         //         foreach (var e in boardLeft)
//         //         {
//         //             child.Add(new Child
//         //             {
//         //                 Value = e
//         //             });
//         //         }
//         //
//         //         foreach (var e in boardRight)
//         //         {
//         //             child.Add(new Child
//         //             {
//         //                 Value = e
//         //             });
//         //         }
//         //
//         //         foreach (var e in map)
//         //         {
//         //             child.Add(new Child
//         //             {
//         //                 Value = e
//         //             });
//         //         }
//         //
//         //         var parenttran = entityManager.GetComponentData<LocalTransform>(parent);
//         //
//         //         //parenttran.RotateX(math.radians(rotateDegrees));
//         //         var qua = quaternion.RotateX(math.radians(0));
//         //         parenttran.Scale = 1;
//         //         parenttran.Rotation = qua;
//         //         entityManager.SetComponentData<LocalTransform>(parent, parenttran);
//         //     }
//         //
//         //
//         //     foreach (var e in boardLeft)
//         //     {
//         //         entityManager.AddComponentData(e, new Parent
//         //         {
//         //             Value = parent
//         //         });
//         //     }
//         //
//         //     foreach (var e in boardRight)
//         //     {
//         //         entityManager.AddComponentData(e, new Parent
//         //         {
//         //             Value = parent
//         //         });
//         //     }
//         //
//         //     foreach (var e in map)
//         //     {
//         //         entityManager.AddComponentData(e, new Parent
//         //         {
//         //             Value = parent
//         //         });
//         //     }
//         // }
//
//         public static Rect GetPerspectiveCameraRect(Camera camera, float distance)
//         {
//             Vector3 cameraPos = camera.transform.position;
//             if (distance <= 0 || distance > camera.farClipPlane)
//             {
//                 distance = camera.farClipPlane;
//             }
//
//             // fov表示相机的垂直视野角度
//             // 2 * tan(fov/2) = height / distance
//             float height = 2 * distance * Mathf.Tan(camera.fieldOfView / 2 * Mathf.Deg2Rad);
//             float width = height * camera.aspect;
//
//             Rect rect = new Rect
//             {
//                 xMin = cameraPos.x - width / 2,
//                 xMax = cameraPos.x + width / 2,
//                 yMin = cameraPos.y - height / 2,
//                 yMax = cameraPos.y + height / 2
//             };
//             return rect;
//         }
//
//         private void LateUpdate()
//         {
//             //Camera mainCamera = Camera.main;
//             if (playerQuery.IsEmpty) return;
//             if (!isInit)
//             {
//                 offset = transform.position;
//                 scene_moduleConfig = ConfigManager.Instance.Tables.Tbscene_module;
//                 levelsConfig = ConfigManager.Instance.Tables.Tblevel;
//                 sceneConfig = ConfigManager.Instance.Tables.Tbscene;
//                 isInit = true;
//             }
//
//             //var rect = GetPerspectiveCameraRect(camera, 1000);
//             //Debug.LogError($"{rect}");
//
//
//             var mapId = sceneConfig.Get(levelsConfig.Get(ResourcesSingleton.Instance.levelId).sceneId).mapId;
//             var map = scene_moduleConfig.Get(mapId);
//
//             //const float scale = 11.71875f;
//             //const float ppu = 100f;
//             // const float mapPiexlWidth = 1024f;
//             // const float mapPiexlHeight = 1024f;
//             //
//             // const float mapBorderPiexlWidth = 512f;
//             // const float mapBorderPiexlHeight = 1024f;
//
//             // float mapPiexlWidthToUnit = mapPiexlWidth / ppu * scale;
//             // float mapPiexlHeightToUnit = mapPiexlHeight / ppu * scale;
//
//             // float mapBorderPiexlWidthToUnit = (mapBorderPiexlWidth / ppu) * scale;
//             // float mapBorderPiexlHeightToUnit = (mapBorderPiexlHeight / ppu) * scale;
//
//             float camerascale = cameraWidth / mapWidth;
//             //var truecameraWidth = mapPiexlWidthToUnit * camerascale;
//
//
//             //camera.fieldOfView = math.clamp(maxFOV * camerascale, minFOV, maxFOV);
//
//
//             // minX = (boardLeft.Position.x - mapBorderPiexlWidthToUnit / 2f);
//             // maxX = (boardRight.Position.x + mapBorderPiexlWidthToUnit / 2f);
//
//
//             var player = playerQuery.ToComponentDataArray<LocalTransform>(Allocator.Temp);
//             foreach (var tran in player)
//             {
//                 var eVector3 = new Vector3(tran.Position.x, tran.Position.y, tran.Position.z);
//                 //
//                 //
//                 // if (!float.IsNaN(eVector3.x) && !float.IsNaN(eVector3.y) &&
//                 //     !float.IsNaN(eVector3.z))
//                 // {
//                 //     transform.position = eVector3 + offset;
//                 // }
//                 Vector3 desiredPosition = eVector3 + offset;
//                 Vector3 smoothedPosition = math.lerp(transform.position, desiredPosition, smoothSpeed);
//                 //var smoothedPosition = desiredPosition;
//                 // 限制相机的 X 坐标在指定范围内
//
//                 // 1-左右封闭
//                 // 2-上下封闭
//                 // 3-全封闭
//                 // 4-全开放
//                 float absX = math.abs((camera.fieldOfView - maxFOV) *
//                                       ((minFOVToX - maxFOVToX * showBorderRatios) / (minFOV - maxFOV)) +
//                                       maxFOVToX * showBorderRatios);
//                 switch (4)
//                     //switch (map.mapType)
//                 {
//                     case 1:
//                         smoothedPosition.x = math.clamp(smoothedPosition.x, -absX, absX);
//                         break;
//                     case 2:
//                         smoothedPosition.y = math.clamp(smoothedPosition.y, -absX, absX);
//                         break;
//                     case 3:
//                         smoothedPosition.x = math.clamp(smoothedPosition.x, -absX, absX);
//                         smoothedPosition.y = math.clamp(smoothedPosition.y, -absX, absX);
//                         break;
//                     case 4:
//
//                         break;
//                 }
//
//
//                 if (!float.IsNaN(smoothedPosition.x) && !float.IsNaN(smoothedPosition.y) &&
//                     !float.IsNaN(smoothedPosition.z))
//                 {
//                     transform.position = smoothedPosition;
//                 }
//             }
//         }
//     }
// }// //---------------------------------------------------------------------
// // JiYuStudio
// // Author: 格伦
// // Time: 2023-07-31 10:50:10
// //---------------------------------------------------------------------
//
//
// using System;
//
// using Main;
// using Unity.Collections;
//
// using Unity.Mathematics;
//
// using UnityEngine;
//
// namespace HotFix_UI
// {
//     //TODO: 边缘平滑过渡处理
//     //同步相机对于玩家的位置 
//     public class CameraUpdateMono : MonoBehaviour
//     {
//         // private EntityManager entityManager;
//         // private EntityQuery playerQuery;
//         // private EntityQuery boardLeftQuery;
//         // private EntityQuery boardRightQuery;
//         // private EntityQuery mapQuery;
//         private bool isInit;
//         private Vector3 offset;
//
//         private float minX;
//         private float maxX;
//
//
//         [Range(0, 1)] public float smoothSpeed = 0.125f;
//         public float cameraWidth = 120f;
//         public float mapWidth = 120f;
//
//         [Range(0, 1)] public float showBorderRatios = 0.5f;
//         [Range(-90, 90)] public float rotateDegrees = 15f;
//         const float maxFOV = 70f;
//         const float minFOV = 10f;
//
//
//         const float maxFOVToX = 33f;
//         //0
//
//         const float minFOVToX = 67f;
//         //10
//
//         private Camera camera;
//
//         //public bool onlyRotateMap;
//
//         private bool isDone;
//
//         // private Tbscene_module scene_moduleConfig;
//         // private Tblevel levelsConfig;
//         // private Tbscene sceneConfig;
//
//         private void Start()
//         {
//             entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
//             playerQuery = entityManager.CreateEntityQuery(typeof(PlayerData), typeof(LocalTransform));
//             //boardLeftQuery = entityManager.CreateEntityQuery(typeof(BoardLeft));
//             //boardRightQuery = entityManager.CreateEntityQuery(typeof(BoardRight));
//             mapQuery = entityManager.CreateEntityQuery(typeof(MapBaseData));
//
//             camera = GetComponent<Camera>();
//         }
//
//         // public void SetMap(bool onlyRotateMap)
//         // {
//         //     var boardLeft = boardLeftQuery.ToEntityArray(Allocator.Temp);
//         //     var boardRight = boardRightQuery.ToEntityArray(Allocator.Temp);
//         //     var map = mapQuery.ToEntityArray(Allocator.Temp);
//         //
//         //
//         //     var parent = entityManager.CreateEntity(typeof(Child), typeof(LocalTransform),
//         //         typeof(LocalToWorld));
//         //
//         //     var child = entityManager.GetBuffer<Child>(parent);
//         //     if (onlyRotateMap)
//         //     {
//         //         camera.transform.rotation = quaternion.EulerXYZ(0, 0, 0);
//         //
//         //         foreach (var e in boardLeft)
//         //         {
//         //             child.Add(new Child
//         //             {
//         //                 Value = e
//         //             });
//         //         }
//         //
//         //         foreach (var e in boardRight)
//         //         {
//         //             child.Add(new Child
//         //             {
//         //                 Value = e
//         //             });
//         //         }
//         //
//         //         foreach (var e in map)
//         //         {
//         //             child.Add(new Child
//         //             {
//         //                 Value = e
//         //             });
//         //         }
//         //
//         //         var parenttran = entityManager.GetComponentData<LocalTransform>(parent);
//         //
//         //         //parenttran.RotateX(math.radians(rotateDegrees));
//         //         var qua = quaternion.RotateX(math.radians(rotateDegrees));
//         //         parenttran.Scale = 1;
//         //         parenttran.Rotation = qua;
//         //         entityManager.SetComponentData<LocalTransform>(parent, parenttran);
//         //     }
//         //     else
//         //     {
//         //         camera.transform.rotation = quaternion.EulerXYZ(-math.radians(rotateDegrees), 0, 0);
//         //
//         //         foreach (var e in boardLeft)
//         //         {
//         //             child.Add(new Child
//         //             {
//         //                 Value = e
//         //             });
//         //         }
//         //
//         //         foreach (var e in boardRight)
//         //         {
//         //             child.Add(new Child
//         //             {
//         //                 Value = e
//         //             });
//         //         }
//         //
//         //         foreach (var e in map)
//         //         {
//         //             child.Add(new Child
//         //             {
//         //                 Value = e
//         //             });
//         //         }
//         //
//         //         var parenttran = entityManager.GetComponentData<LocalTransform>(parent);
//         //
//         //         //parenttran.RotateX(math.radians(rotateDegrees));
//         //         var qua = quaternion.RotateX(math.radians(0));
//         //         parenttran.Scale = 1;
//         //         parenttran.Rotation = qua;
//         //         entityManager.SetComponentData<LocalTransform>(parent, parenttran);
//         //     }
//         //
//         //
//         //     foreach (var e in boardLeft)
//         //     {
//         //         entityManager.AddComponentData(e, new Parent
//         //         {
//         //             Value = parent
//         //         });
//         //     }
//         //
//         //     foreach (var e in boardRight)
//         //     {
//         //         entityManager.AddComponentData(e, new Parent
//         //         {
//         //             Value = parent
//         //         });
//         //     }
//         //
//         //     foreach (var e in map)
//         //     {
//         //         entityManager.AddComponentData(e, new Parent
//         //         {
//         //             Value = parent
//         //         });
//         //     }
//         // }
//
//         public static Rect GetPerspectiveCameraRect(Camera camera, float distance)
//         {
//             Vector3 cameraPos = camera.transform.position;
//             if (distance <= 0 || distance > camera.farClipPlane)
//             {
//                 distance = camera.farClipPlane;
//             }
//
//             // fov表示相机的垂直视野角度
//             // 2 * tan(fov/2) = height / distance
//             float height = 2 * distance * Mathf.Tan(camera.fieldOfView / 2 * Mathf.Deg2Rad);
//             float width = height * camera.aspect;
//
//             Rect rect = new Rect
//             {
//                 xMin = cameraPos.x - width / 2,
//                 xMax = cameraPos.x + width / 2,
//                 yMin = cameraPos.y - height / 2,
//                 yMax = cameraPos.y + height / 2
//             };
//             return rect;
//         }
//
//         private void LateUpdate()
//         {
//             //Camera mainCamera = Camera.main;
//             if (playerQuery.IsEmpty) return;
//             if (!isInit)
//             {
//                 offset = transform.position;
//                 scene_moduleConfig = ConfigManager.Instance.Tables.Tbscene_module;
//                 levelsConfig = ConfigManager.Instance.Tables.Tblevel;
//                 sceneConfig = ConfigManager.Instance.Tables.Tbscene;
//                 isInit = true;
//             }
//
//             //var rect = GetPerspectiveCameraRect(camera, 1000);
//             //Debug.LogError($"{rect}");
//
//
//             var mapId = sceneConfig.Get(levelsConfig.Get(ResourcesSingleton.Instance.levelId).sceneId).mapId;
//             var map = scene_moduleConfig.Get(mapId);
//
//             //const float scale = 11.71875f;
//             //const float ppu = 100f;
//             // const float mapPiexlWidth = 1024f;
//             // const float mapPiexlHeight = 1024f;
//             //
//             // const float mapBorderPiexlWidth = 512f;
//             // const float mapBorderPiexlHeight = 1024f;
//
//             // float mapPiexlWidthToUnit = mapPiexlWidth / ppu * scale;
//             // float mapPiexlHeightToUnit = mapPiexlHeight / ppu * scale;
//
//             // float mapBorderPiexlWidthToUnit = (mapBorderPiexlWidth / ppu) * scale;
//             // float mapBorderPiexlHeightToUnit = (mapBorderPiexlHeight / ppu) * scale;
//
//             float camerascale = cameraWidth / mapWidth;
//             //var truecameraWidth = mapPiexlWidthToUnit * camerascale;
//
//
//             //camera.fieldOfView = math.clamp(maxFOV * camerascale, minFOV, maxFOV);
//
//
//             // minX = (boardLeft.Position.x - mapBorderPiexlWidthToUnit / 2f);
//             // maxX = (boardRight.Position.x + mapBorderPiexlWidthToUnit / 2f);
//
//
//             var player = playerQuery.ToComponentDataArray<LocalTransform>(Allocator.Temp);
//             foreach (var tran in player)
//             {
//                 var eVector3 = new Vector3(tran.Position.x, tran.Position.y, tran.Position.z);
//                 //
//                 //
//                 // if (!float.IsNaN(eVector3.x) && !float.IsNaN(eVector3.y) &&
//                 //     !float.IsNaN(eVector3.z))
//                 // {
//                 //     transform.position = eVector3 + offset;
//                 // }
//                 Vector3 desiredPosition = eVector3 + offset;
//                 Vector3 smoothedPosition = math.lerp(transform.position, desiredPosition, smoothSpeed);
//                 //var smoothedPosition = desiredPosition;
//                 // 限制相机的 X 坐标在指定范围内
//
//                 // 1-左右封闭
//                 // 2-上下封闭
//                 // 3-全封闭
//                 // 4-全开放
//                 float absX = math.abs((camera.fieldOfView - maxFOV) *
//                                       ((minFOVToX - maxFOVToX * showBorderRatios) / (minFOV - maxFOV)) +
//                                       maxFOVToX * showBorderRatios);
//                 switch (4)
//                     //switch (map.mapType)
//                 {
//                     case 1:
//                         smoothedPosition.x = math.clamp(smoothedPosition.x, -absX, absX);
//                         break;
//                     case 2:
//                         smoothedPosition.y = math.clamp(smoothedPosition.y, -absX, absX);
//                         break;
//                     case 3:
//                         smoothedPosition.x = math.clamp(smoothedPosition.x, -absX, absX);
//                         smoothedPosition.y = math.clamp(smoothedPosition.y, -absX, absX);
//                         break;
//                     case 4:
//
//                         break;
//                 }
//
//
//                 if (!float.IsNaN(smoothedPosition.x) && !float.IsNaN(smoothedPosition.y) &&
//                     !float.IsNaN(smoothedPosition.z))
//                 {
//                     transform.position = smoothedPosition;
//                 }
//             }
//         }
//     }
// }// //---------------------------------------------------------------------
// // JiYuStudio
// // Author: 格伦
// // Time: 2023-07-31 10:50:10
// //---------------------------------------------------------------------
//
//
// using System;
//
// using Main;
// using Unity.Collections;
//
// using Unity.Mathematics;
//
// using UnityEngine;
//
// namespace HotFix_UI
// {
//     //TODO: 边缘平滑过渡处理
//     //同步相机对于玩家的位置 
//     public class CameraUpdateMono : MonoBehaviour
//     {
//         // private EntityManager entityManager;
//         // private EntityQuery playerQuery;
//         // private EntityQuery boardLeftQuery;
//         // private EntityQuery boardRightQuery;
//         // private EntityQuery mapQuery;
//         private bool isInit;
//         private Vector3 offset;
//
//         private float minX;
//         private float maxX;
//
//
//         [Range(0, 1)] public float smoothSpeed = 0.125f;
//         public float cameraWidth = 120f;
//         public float mapWidth = 120f;
//
//         [Range(0, 1)] public float showBorderRatios = 0.5f;
//         [Range(-90, 90)] public float rotateDegrees = 15f;
//         const float maxFOV = 70f;
//         const float minFOV = 10f;
//
//
//         const float maxFOVToX = 33f;
//         //0
//
//         const float minFOVToX = 67f;
//         //10
//
//         private Camera camera;
//
//         //public bool onlyRotateMap;
//
//         private bool isDone;
//
//         // private Tbscene_module scene_moduleConfig;
//         // private Tblevel levelsConfig;
//         // private Tbscene sceneConfig;
//
//         private void Start()
//         {
//             entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
//             playerQuery = entityManager.CreateEntityQuery(typeof(PlayerData), typeof(LocalTransform));
//             //boardLeftQuery = entityManager.CreateEntityQuery(typeof(BoardLeft));
//             //boardRightQuery = entityManager.CreateEntityQuery(typeof(BoardRight));
//             mapQuery = entityManager.CreateEntityQuery(typeof(MapBaseData));
//
//             camera = GetComponent<Camera>();
//         }
//
//         // public void SetMap(bool onlyRotateMap)
//         // {
//         //     var boardLeft = boardLeftQuery.ToEntityArray(Allocator.Temp);
//         //     var boardRight = boardRightQuery.ToEntityArray(Allocator.Temp);
//         //     var map = mapQuery.ToEntityArray(Allocator.Temp);
//         //
//         //
//         //     var parent = entityManager.CreateEntity(typeof(Child), typeof(LocalTransform),
//         //         typeof(LocalToWorld));
//         //
//         //     var child = entityManager.GetBuffer<Child>(parent);
//         //     if (onlyRotateMap)
//         //     {
//         //         camera.transform.rotation = quaternion.EulerXYZ(0, 0, 0);
//         //
//         //         foreach (var e in boardLeft)
//         //         {
//         //             child.Add(new Child
//         //             {
//         //                 Value = e
//         //             });
//         //         }
//         //
//         //         foreach (var e in boardRight)
//         //         {
//         //             child.Add(new Child
//         //             {
//         //                 Value = e
//         //             });
//         //         }
//         //
//         //         foreach (var e in map)
//         //         {
//         //             child.Add(new Child
//         //             {
//         //                 Value = e
//         //             });
//         //         }
//         //
//         //         var parenttran = entityManager.GetComponentData<LocalTransform>(parent);
//         //
//         //         //parenttran.RotateX(math.radians(rotateDegrees));
//         //         var qua = quaternion.RotateX(math.radians(rotateDegrees));
//         //         parenttran.Scale = 1;
//         //         parenttran.Rotation = qua;
//         //         entityManager.SetComponentData<LocalTransform>(parent, parenttran);
//         //     }
//         //     else
//         //     {
//         //         camera.transform.rotation = quaternion.EulerXYZ(-math.radians(rotateDegrees), 0, 0);
//         //
//         //         foreach (var e in boardLeft)
//         //         {
//         //             child.Add(new Child
//         //             {
//         //                 Value = e
//         //             });
//         //         }
//         //
//         //         foreach (var e in boardRight)
//         //         {
//         //             child.Add(new Child
//         //             {
//         //                 Value = e
//         //             });
//         //         }
//         //
//         //         foreach (var e in map)
//         //         {
//         //             child.Add(new Child
//         //             {
//         //                 Value = e
//         //             });
//         //         }
//         //
//         //         var parenttran = entityManager.GetComponentData<LocalTransform>(parent);
//         //
//         //         //parenttran.RotateX(math.radians(rotateDegrees));
//         //         var qua = quaternion.RotateX(math.radians(0));
//         //         parenttran.Scale = 1;
//         //         parenttran.Rotation = qua;
//         //         entityManager.SetComponentData<LocalTransform>(parent, parenttran);
//         //     }
//         //
//         //
//         //     foreach (var e in boardLeft)
//         //     {
//         //         entityManager.AddComponentData(e, new Parent
//         //         {
//         //             Value = parent
//         //         });
//         //     }
//         //
//         //     foreach (var e in boardRight)
//         //     {
//         //         entityManager.AddComponentData(e, new Parent
//         //         {
//         //             Value = parent
//         //         });
//         //     }
//         //
//         //     foreach (var e in map)
//         //     {
//         //         entityManager.AddComponentData(e, new Parent
//         //         {
//         //             Value = parent
//         //         });
//         //     }
//         // }
//
//         public static Rect GetPerspectiveCameraRect(Camera camera, float distance)
//         {
//             Vector3 cameraPos = camera.transform.position;
//             if (distance <= 0 || distance > camera.farClipPlane)
//             {
//                 distance = camera.farClipPlane;
//             }
//
//             // fov表示相机的垂直视野角度
//             // 2 * tan(fov/2) = height / distance
//             float height = 2 * distance * Mathf.Tan(camera.fieldOfView / 2 * Mathf.Deg2Rad);
//             float width = height * camera.aspect;
//
//             Rect rect = new Rect
//             {
//                 xMin = cameraPos.x - width / 2,
//                 xMax = cameraPos.x + width / 2,
//                 yMin = cameraPos.y - height / 2,
//                 yMax = cameraPos.y + height / 2
//             };
//             return rect;
//         }
//
//         private void LateUpdate()
//         {
//             //Camera mainCamera = Camera.main;
//             if (playerQuery.IsEmpty) return;
//             if (!isInit)
//             {
//                 offset = transform.position;
//                 scene_moduleConfig = ConfigManager.Instance.Tables.Tbscene_module;
//                 levelsConfig = ConfigManager.Instance.Tables.Tblevel;
//                 sceneConfig = ConfigManager.Instance.Tables.Tbscene;
//                 isInit = true;
//             }
//
//             //var rect = GetPerspectiveCameraRect(camera, 1000);
//             //Debug.LogError($"{rect}");
//
//
//             var mapId = sceneConfig.Get(levelsConfig.Get(ResourcesSingleton.Instance.levelId).sceneId).mapId;
//             var map = scene_moduleConfig.Get(mapId);
//
//             //const float scale = 11.71875f;
//             //const float ppu = 100f;
//             // const float mapPiexlWidth = 1024f;
//             // const float mapPiexlHeight = 1024f;
//             //
//             // const float mapBorderPiexlWidth = 512f;
//             // const float mapBorderPiexlHeight = 1024f;
//
//             // float mapPiexlWidthToUnit = mapPiexlWidth / ppu * scale;
//             // float mapPiexlHeightToUnit = mapPiexlHeight / ppu * scale;
//
//             // float mapBorderPiexlWidthToUnit = (mapBorderPiexlWidth / ppu) * scale;
//             // float mapBorderPiexlHeightToUnit = (mapBorderPiexlHeight / ppu) * scale;
//
//             float camerascale = cameraWidth / mapWidth;
//             //var truecameraWidth = mapPiexlWidthToUnit * camerascale;
//
//
//             //camera.fieldOfView = math.clamp(maxFOV * camerascale, minFOV, maxFOV);
//
//
//             // minX = (boardLeft.Position.x - mapBorderPiexlWidthToUnit / 2f);
//             // maxX = (boardRight.Position.x + mapBorderPiexlWidthToUnit / 2f);
//
//
//             var player = playerQuery.ToComponentDataArray<LocalTransform>(Allocator.Temp);
//             foreach (var tran in player)
//             {
//                 var eVector3 = new Vector3(tran.Position.x, tran.Position.y, tran.Position.z);
//                 //
//                 //
//                 // if (!float.IsNaN(eVector3.x) && !float.IsNaN(eVector3.y) &&
//                 //     !float.IsNaN(eVector3.z))
//                 // {
//                 //     transform.position = eVector3 + offset;
//                 // }
//                 Vector3 desiredPosition = eVector3 + offset;
//                 Vector3 smoothedPosition = math.lerp(transform.position, desiredPosition, smoothSpeed);
//                 //var smoothedPosition = desiredPosition;
//                 // 限制相机的 X 坐标在指定范围内
//
//                 // 1-左右封闭
//                 // 2-上下封闭
//                 // 3-全封闭
//                 // 4-全开放
//                 float absX = math.abs((camera.fieldOfView - maxFOV) *
//                                       ((minFOVToX - maxFOVToX * showBorderRatios) / (minFOV - maxFOV)) +
//                                       maxFOVToX * showBorderRatios);
//                 switch (4)
//                     //switch (map.mapType)
//                 {
//                     case 1:
//                         smoothedPosition.x = math.clamp(smoothedPosition.x, -absX, absX);
//                         break;
//                     case 2:
//                         smoothedPosition.y = math.clamp(smoothedPosition.y, -absX, absX);
//                         break;
//                     case 3:
//                         smoothedPosition.x = math.clamp(smoothedPosition.x, -absX, absX);
//                         smoothedPosition.y = math.clamp(smoothedPosition.y, -absX, absX);
//                         break;
//                     case 4:
//
//                         break;
//                 }
//
//
//                 if (!float.IsNaN(smoothedPosition.x) && !float.IsNaN(smoothedPosition.y) &&
//                     !float.IsNaN(smoothedPosition.z))
//                 {
//                     transform.position = smoothedPosition;
//                 }
//             }
//         }
//     }
// }// //---------------------------------------------------------------------
// // JiYuStudio
// // Author: 格伦
// // Time: 2023-07-31 10:50:10
// //---------------------------------------------------------------------
//
//
// using System;
//
// using Main;
// using Unity.Collections;
//
// using Unity.Mathematics;
//
// using UnityEngine;
//
// namespace HotFix_UI
// {
//     //TODO: 边缘平滑过渡处理
//     //同步相机对于玩家的位置 
//     public class CameraUpdateMono : MonoBehaviour
//     {
//         // private EntityManager entityManager;
//         // private EntityQuery playerQuery;
//         // private EntityQuery boardLeftQuery;
//         // private EntityQuery boardRightQuery;
//         // private EntityQuery mapQuery;
//         private bool isInit;
//         private Vector3 offset;
//
//         private float minX;
//         private float maxX;
//
//
//         [Range(0, 1)] public float smoothSpeed = 0.125f;
//         public float cameraWidth = 120f;
//         public float mapWidth = 120f;
//
//         [Range(0, 1)] public float showBorderRatios = 0.5f;
//         [Range(-90, 90)] public float rotateDegrees = 15f;
//         const float maxFOV = 70f;
//         const float minFOV = 10f;
//
//
//         const float maxFOVToX = 33f;
//         //0
//
//         const float minFOVToX = 67f;
//         //10
//
//         private Camera camera;
//
//         //public bool onlyRotateMap;
//
//         private bool isDone;
//
//         // private Tbscene_module scene_moduleConfig;
//         // private Tblevel levelsConfig;
//         // private Tbscene sceneConfig;
//
//         private void Start()
//         {
//             entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
//             playerQuery = entityManager.CreateEntityQuery(typeof(PlayerData), typeof(LocalTransform));
//             //boardLeftQuery = entityManager.CreateEntityQuery(typeof(BoardLeft));
//             //boardRightQuery = entityManager.CreateEntityQuery(typeof(BoardRight));
//             mapQuery = entityManager.CreateEntityQuery(typeof(MapBaseData));
//
//             camera = GetComponent<Camera>();
//         }
//
//         // public void SetMap(bool onlyRotateMap)
//         // {
//         //     var boardLeft = boardLeftQuery.ToEntityArray(Allocator.Temp);
//         //     var boardRight = boardRightQuery.ToEntityArray(Allocator.Temp);
//         //     var map = mapQuery.ToEntityArray(Allocator.Temp);
//         //
//         //
//         //     var parent = entityManager.CreateEntity(typeof(Child), typeof(LocalTransform),
//         //         typeof(LocalToWorld));
//         //
//         //     var child = entityManager.GetBuffer<Child>(parent);
//         //     if (onlyRotateMap)
//         //     {
//         //         camera.transform.rotation = quaternion.EulerXYZ(0, 0, 0);
//         //
//         //         foreach (var e in boardLeft)
//         //         {
//         //             child.Add(new Child
//         //             {
//         //                 Value = e
//         //             });
//         //         }
//         //
//         //         foreach (var e in boardRight)
//         //         {
//         //             child.Add(new Child
//         //             {
//         //                 Value = e
//         //             });
//         //         }
//         //
//         //         foreach (var e in map)
//         //         {
//         //             child.Add(new Child
//         //             {
//         //                 Value = e
//         //             });
//         //         }
//         //
//         //         var parenttran = entityManager.GetComponentData<LocalTransform>(parent);
//         //
//         //         //parenttran.RotateX(math.radians(rotateDegrees));
//         //         var qua = quaternion.RotateX(math.radians(rotateDegrees));
//         //         parenttran.Scale = 1;
//         //         parenttran.Rotation = qua;
//         //         entityManager.SetComponentData<LocalTransform>(parent, parenttran);
//         //     }
//         //     else
//         //     {
//         //         camera.transform.rotation = quaternion.EulerXYZ(-math.radians(rotateDegrees), 0, 0);
//         //
//         //         foreach (var e in boardLeft)
//         //         {
//         //             child.Add(new Child
//         //             {
//         //                 Value = e
//         //             });
//         //         }
//         //
//         //         foreach (var e in boardRight)
//         //         {
//         //             child.Add(new Child
//         //             {
//         //                 Value = e
//         //             });
//         //         }
//         //
//         //         foreach (var e in map)
//         //         {
//         //             child.Add(new Child
//         //             {
//         //                 Value = e
//         //             });
//         //         }
//         //
//         //         var parenttran = entityManager.GetComponentData<LocalTransform>(parent);
//         //
//         //         //parenttran.RotateX(math.radians(rotateDegrees));
//         //         var qua = quaternion.RotateX(math.radians(0));
//         //         parenttran.Scale = 1;
//         //         parenttran.Rotation = qua;
//         //         entityManager.SetComponentData<LocalTransform>(parent, parenttran);
//         //     }
//         //
//         //
//         //     foreach (var e in boardLeft)
//         //     {
//         //         entityManager.AddComponentData(e, new Parent
//         //         {
//         //             Value = parent
//         //         });
//         //     }
//         //
//         //     foreach (var e in boardRight)
//         //     {
//         //         entityManager.AddComponentData(e, new Parent
//         //         {
//         //             Value = parent
//         //         });
//         //     }
//         //
//         //     foreach (var e in map)
//         //     {
//         //         entityManager.AddComponentData(e, new Parent
//         //         {
//         //             Value = parent
//         //         });
//         //     }
//         // }
//
//         public static Rect GetPerspectiveCameraRect(Camera camera, float distance)
//         {
//             Vector3 cameraPos = camera.transform.position;
//             if (distance <= 0 || distance > camera.farClipPlane)
//             {
//                 distance = camera.farClipPlane;
//             }
//
//             // fov表示相机的垂直视野角度
//             // 2 * tan(fov/2) = height / distance
//             float height = 2 * distance * Mathf.Tan(camera.fieldOfView / 2 * Mathf.Deg2Rad);
//             float width = height * camera.aspect;
//
//             Rect rect = new Rect
//             {
//                 xMin = cameraPos.x - width / 2,
//                 xMax = cameraPos.x + width / 2,
//                 yMin = cameraPos.y - height / 2,
//                 yMax = cameraPos.y + height / 2
//             };
//             return rect;
//         }
//
//         private void LateUpdate()
//         {
//             //Camera mainCamera = Camera.main;
//             if (playerQuery.IsEmpty) return;
//             if (!isInit)
//             {
//                 offset = transform.position;
//                 scene_moduleConfig = ConfigManager.Instance.Tables.Tbscene_module;
//                 levelsConfig = ConfigManager.Instance.Tables.Tblevel;
//                 sceneConfig = ConfigManager.Instance.Tables.Tbscene;
//                 isInit = true;
//             }
//
//             //var rect = GetPerspectiveCameraRect(camera, 1000);
//             //Debug.LogError($"{rect}");
//
//
//             var mapId = sceneConfig.Get(levelsConfig.Get(ResourcesSingleton.Instance.levelId).sceneId).mapId;
//             var map = scene_moduleConfig.Get(mapId);
//
//             //const float scale = 11.71875f;
//             //const float ppu = 100f;
//             // const float mapPiexlWidth = 1024f;
//             // const float mapPiexlHeight = 1024f;
//             //
//             // const float mapBorderPiexlWidth = 512f;
//             // const float mapBorderPiexlHeight = 1024f;
//
//             // float mapPiexlWidthToUnit = mapPiexlWidth / ppu * scale;
//             // float mapPiexlHeightToUnit = mapPiexlHeight / ppu * scale;
//
//             // float mapBorderPiexlWidthToUnit = (mapBorderPiexlWidth / ppu) * scale;
//             // float mapBorderPiexlHeightToUnit = (mapBorderPiexlHeight / ppu) * scale;
//
//             float camerascale = cameraWidth / mapWidth;
//             //var truecameraWidth = mapPiexlWidthToUnit * camerascale;
//
//
//             //camera.fieldOfView = math.clamp(maxFOV * camerascale, minFOV, maxFOV);
//
//
//             // minX = (boardLeft.Position.x - mapBorderPiexlWidthToUnit / 2f);
//             // maxX = (boardRight.Position.x + mapBorderPiexlWidthToUnit / 2f);
//
//
//             var player = playerQuery.ToComponentDataArray<LocalTransform>(Allocator.Temp);
//             foreach (var tran in player)
//             {
//                 var eVector3 = new Vector3(tran.Position.x, tran.Position.y, tran.Position.z);
//                 //
//                 //
//                 // if (!float.IsNaN(eVector3.x) && !float.IsNaN(eVector3.y) &&
//                 //     !float.IsNaN(eVector3.z))
//                 // {
//                 //     transform.position = eVector3 + offset;
//                 // }
//                 Vector3 desiredPosition = eVector3 + offset;
//                 Vector3 smoothedPosition = math.lerp(transform.position, desiredPosition, smoothSpeed);
//                 //var smoothedPosition = desiredPosition;
//                 // 限制相机的 X 坐标在指定范围内
//
//                 // 1-左右封闭
//                 // 2-上下封闭
//                 // 3-全封闭
//                 // 4-全开放
//                 float absX = math.abs((camera.fieldOfView - maxFOV) *
//                                       ((minFOVToX - maxFOVToX * showBorderRatios) / (minFOV - maxFOV)) +
//                                       maxFOVToX * showBorderRatios);
//                 switch (4)
//                     //switch (map.mapType)
//                 {
//                     case 1:
//                         smoothedPosition.x = math.clamp(smoothedPosition.x, -absX, absX);
//                         break;
//                     case 2:
//                         smoothedPosition.y = math.clamp(smoothedPosition.y, -absX, absX);
//                         break;
//                     case 3:
//                         smoothedPosition.x = math.clamp(smoothedPosition.x, -absX, absX);
//                         smoothedPosition.y = math.clamp(smoothedPosition.y, -absX, absX);
//                         break;
//                     case 4:
//
//                         break;
//                 }
//
//
//                 if (!float.IsNaN(smoothedPosition.x) && !float.IsNaN(smoothedPosition.y) &&
//                     !float.IsNaN(smoothedPosition.z))
//                 {
//                     transform.position = smoothedPosition;
//                 }
//             }
//         }
//     }
// }// //---------------------------------------------------------------------
// // JiYuStudio
// // Author: 格伦
// // Time: 2023-07-31 10:50:10
// //---------------------------------------------------------------------
//
//
// using System;
//
// using Main;
// using Unity.Collections;
//
// using Unity.Mathematics;
//
// using UnityEngine;
//
// namespace HotFix_UI
// {
//     //TODO: 边缘平滑过渡处理
//     //同步相机对于玩家的位置 
//     public class CameraUpdateMono : MonoBehaviour
//     {
//         // private EntityManager entityManager;
//         // private EntityQuery playerQuery;
//         // private EntityQuery boardLeftQuery;
//         // private EntityQuery boardRightQuery;
//         // private EntityQuery mapQuery;
//         private bool isInit;
//         private Vector3 offset;
//
//         private float minX;
//         private float maxX;
//
//
//         [Range(0, 1)] public float smoothSpeed = 0.125f;
//         public float cameraWidth = 120f;
//         public float mapWidth = 120f;
//
//         [Range(0, 1)] public float showBorderRatios = 0.5f;
//         [Range(-90, 90)] public float rotateDegrees = 15f;
//         const float maxFOV = 70f;
//         const float minFOV = 10f;
//
//
//         const float maxFOVToX = 33f;
//         //0
//
//         const float minFOVToX = 67f;
//         //10
//
//         private Camera camera;
//
//         //public bool onlyRotateMap;
//
//         private bool isDone;
//
//         // private Tbscene_module scene_moduleConfig;
//         // private Tblevel levelsConfig;
//         // private Tbscene sceneConfig;
//
//         private void Start()
//         {
//             entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
//             playerQuery = entityManager.CreateEntityQuery(typeof(PlayerData), typeof(LocalTransform));
//             //boardLeftQuery = entityManager.CreateEntityQuery(typeof(BoardLeft));
//             //boardRightQuery = entityManager.CreateEntityQuery(typeof(BoardRight));
//             mapQuery = entityManager.CreateEntityQuery(typeof(MapBaseData));
//
//             camera = GetComponent<Camera>();
//         }
//
//         // public void SetMap(bool onlyRotateMap)
//         // {
//         //     var boardLeft = boardLeftQuery.ToEntityArray(Allocator.Temp);
//         //     var boardRight = boardRightQuery.ToEntityArray(Allocator.Temp);
//         //     var map = mapQuery.ToEntityArray(Allocator.Temp);
//         //
//         //
//         //     var parent = entityManager.CreateEntity(typeof(Child), typeof(LocalTransform),
//         //         typeof(LocalToWorld));
//         //
//         //     var child = entityManager.GetBuffer<Child>(parent);
//         //     if (onlyRotateMap)
//         //     {
//         //         camera.transform.rotation = quaternion.EulerXYZ(0, 0, 0);
//         //
//         //         foreach (var e in boardLeft)
//         //         {
//         //             child.Add(new Child
//         //             {
//         //                 Value = e
//         //             });
//         //         }
//         //
//         //         foreach (var e in boardRight)
//         //         {
//         //             child.Add(new Child
//         //             {
//         //                 Value = e
//         //             });
//         //         }
//         //
//         //         foreach (var e in map)
//         //         {
//         //             child.Add(new Child
//         //             {
//         //                 Value = e
//         //             });
//         //         }
//         //
//         //         var parenttran = entityManager.GetComponentData<LocalTransform>(parent);
//         //
//         //         //parenttran.RotateX(math.radians(rotateDegrees));
//         //         var qua = quaternion.RotateX(math.radians(rotateDegrees));
//         //         parenttran.Scale = 1;
//         //         parenttran.Rotation = qua;
//         //         entityManager.SetComponentData<LocalTransform>(parent, parenttran);
//         //     }
//         //     else
//         //     {
//         //         camera.transform.rotation = quaternion.EulerXYZ(-math.radians(rotateDegrees), 0, 0);
//         //
//         //         foreach (var e in boardLeft)
//         //         {
//         //             child.Add(new Child
//         //             {
//         //                 Value = e
//         //             });
//         //         }
//         //
//         //         foreach (var e in boardRight)
//         //         {
//         //             child.Add(new Child
//         //             {
//         //                 Value = e
//         //             });
//         //         }
//         //
//         //         foreach (var e in map)
//         //         {
//         //             child.Add(new Child
//         //             {
//         //                 Value = e
//         //             });
//         //         }
//         //
//         //         var parenttran = entityManager.GetComponentData<LocalTransform>(parent);
//         //
//         //         //parenttran.RotateX(math.radians(rotateDegrees));
//         //         var qua = quaternion.RotateX(math.radians(0));
//         //         parenttran.Scale = 1;
//         //         parenttran.Rotation = qua;
//         //         entityManager.SetComponentData<LocalTransform>(parent, parenttran);
//         //     }
//         //
//         //
//         //     foreach (var e in boardLeft)
//         //     {
//         //         entityManager.AddComponentData(e, new Parent
//         //         {
//         //             Value = parent
//         //         });
//         //     }
//         //
//         //     foreach (var e in boardRight)
//         //     {
//         //         entityManager.AddComponentData(e, new Parent
//         //         {
//         //             Value = parent
//         //         });
//         //     }
//         //
//         //     foreach (var e in map)
//         //     {
//         //         entityManager.AddComponentData(e, new Parent
//         //         {
//         //             Value = parent
//         //         });
//         //     }
//         // }
//
//         public static Rect GetPerspectiveCameraRect(Camera camera, float distance)
//         {
//             Vector3 cameraPos = camera.transform.position;
//             if (distance <= 0 || distance > camera.farClipPlane)
//             {
//                 distance = camera.farClipPlane;
//             }
//
//             // fov表示相机的垂直视野角度
//             // 2 * tan(fov/2) = height / distance
//             float height = 2 * distance * Mathf.Tan(camera.fieldOfView / 2 * Mathf.Deg2Rad);
//             float width = height * camera.aspect;
//
//             Rect rect = new Rect
//             {
//                 xMin = cameraPos.x - width / 2,
//                 xMax = cameraPos.x + width / 2,
//                 yMin = cameraPos.y - height / 2,
//                 yMax = cameraPos.y + height / 2
//             };
//             return rect;
//         }
//
//         private void LateUpdate()
//         {
//             //Camera mainCamera = Camera.main;
//             if (playerQuery.IsEmpty) return;
//             if (!isInit)
//             {
//                 offset = transform.position;
//                 scene_moduleConfig = ConfigManager.Instance.Tables.Tbscene_module;
//                 levelsConfig = ConfigManager.Instance.Tables.Tblevel;
//                 sceneConfig = ConfigManager.Instance.Tables.Tbscene;
//                 isInit = true;
//             }
//
//             //var rect = GetPerspectiveCameraRect(camera, 1000);
//             //Debug.LogError($"{rect}");
//
//
//             var mapId = sceneConfig.Get(levelsConfig.Get(ResourcesSingleton.Instance.levelId).sceneId).mapId;
//             var map = scene_moduleConfig.Get(mapId);
//
//             //const float scale = 11.71875f;
//             //const float ppu = 100f;
//             // const float mapPiexlWidth = 1024f;
//             // const float mapPiexlHeight = 1024f;
//             //
//             // const float mapBorderPiexlWidth = 512f;
//             // const float mapBorderPiexlHeight = 1024f;
//
//             // float mapPiexlWidthToUnit = mapPiexlWidth / ppu * scale;
//             // float mapPiexlHeightToUnit = mapPiexlHeight / ppu * scale;
//
//             // float mapBorderPiexlWidthToUnit = (mapBorderPiexlWidth / ppu) * scale;
//             // float mapBorderPiexlHeightToUnit = (mapBorderPiexlHeight / ppu) * scale;
//
//             float camerascale = cameraWidth / mapWidth;
//             //var truecameraWidth = mapPiexlWidthToUnit * camerascale;
//
//
//             //camera.fieldOfView = math.clamp(maxFOV * camerascale, minFOV, maxFOV);
//
//
//             // minX = (boardLeft.Position.x - mapBorderPiexlWidthToUnit / 2f);
//             // maxX = (boardRight.Position.x + mapBorderPiexlWidthToUnit / 2f);
//
//
//             var player = playerQuery.ToComponentDataArray<LocalTransform>(Allocator.Temp);
//             foreach (var tran in player)
//             {
//                 var eVector3 = new Vector3(tran.Position.x, tran.Position.y, tran.Position.z);
//                 //
//                 //
//                 // if (!float.IsNaN(eVector3.x) && !float.IsNaN(eVector3.y) &&
//                 //     !float.IsNaN(eVector3.z))
//                 // {
//                 //     transform.position = eVector3 + offset;
//                 // }
//                 Vector3 desiredPosition = eVector3 + offset;
//                 Vector3 smoothedPosition = math.lerp(transform.position, desiredPosition, smoothSpeed);
//                 //var smoothedPosition = desiredPosition;
//                 // 限制相机的 X 坐标在指定范围内
//
//                 // 1-左右封闭
//                 // 2-上下封闭
//                 // 3-全封闭
//                 // 4-全开放
//                 float absX = math.abs((camera.fieldOfView - maxFOV) *
//                                       ((minFOVToX - maxFOVToX * showBorderRatios) / (minFOV - maxFOV)) +
//                                       maxFOVToX * showBorderRatios);
//                 switch (4)
//                     //switch (map.mapType)
//                 {
//                     case 1:
//                         smoothedPosition.x = math.clamp(smoothedPosition.x, -absX, absX);
//                         break;
//                     case 2:
//                         smoothedPosition.y = math.clamp(smoothedPosition.y, -absX, absX);
//                         break;
//                     case 3:
//                         smoothedPosition.x = math.clamp(smoothedPosition.x, -absX, absX);
//                         smoothedPosition.y = math.clamp(smoothedPosition.y, -absX, absX);
//                         break;
//                     case 4:
//
//                         break;
//                 }
//
//
//                 if (!float.IsNaN(smoothedPosition.x) && !float.IsNaN(smoothedPosition.y) &&
//                     !float.IsNaN(smoothedPosition.z))
//                 {
//                     transform.position = smoothedPosition;
//                 }
//             }
//         }
//     }
// }// //---------------------------------------------------------------------
// // JiYuStudio
// // Author: 格伦
// // Time: 2023-07-31 10:50:10
// //---------------------------------------------------------------------
//
//
// using System;
//
// using Main;
// using Unity.Collections;
//
// using Unity.Mathematics;
//
// using UnityEngine;
//
// namespace HotFix_UI
// {
//     //TODO: 边缘平滑过渡处理
//     //同步相机对于玩家的位置 
//     public class CameraUpdateMono : MonoBehaviour
//     {
//         // private EntityManager entityManager;
//         // private EntityQuery playerQuery;
//         // private EntityQuery boardLeftQuery;
//         // private EntityQuery boardRightQuery;
//         // private EntityQuery mapQuery;
//         private bool isInit;
//         private Vector3 offset;
//
//         private float minX;
//         private float maxX;
//
//
//         [Range(0, 1)] public float smoothSpeed = 0.125f;
//         public float cameraWidth = 120f;
//         public float mapWidth = 120f;
//
//         [Range(0, 1)] public float showBorderRatios = 0.5f;
//         [Range(-90, 90)] public float rotateDegrees = 15f;
//         const float maxFOV = 70f;
//         const float minFOV = 10f;
//
//
//         const float maxFOVToX = 33f;
//         //0
//
//         const float minFOVToX = 67f;
//         //10
//
//         private Camera camera;
//
//         //public bool onlyRotateMap;
//
//         private bool isDone;
//
//         // private Tbscene_module scene_moduleConfig;
//         // private Tblevel levelsConfig;
//         // private Tbscene sceneConfig;
//
//         private void Start()
//         {
//             entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
//             playerQuery = entityManager.CreateEntityQuery(typeof(PlayerData), typeof(LocalTransform));
//             //boardLeftQuery = entityManager.CreateEntityQuery(typeof(BoardLeft));
//             //boardRightQuery = entityManager.CreateEntityQuery(typeof(BoardRight));
//             mapQuery = entityManager.CreateEntityQuery(typeof(MapBaseData));
//
//             camera = GetComponent<Camera>();
//         }
//
//         // public void SetMap(bool onlyRotateMap)
//         // {
//         //     var boardLeft = boardLeftQuery.ToEntityArray(Allocator.Temp);
//         //     var boardRight = boardRightQuery.ToEntityArray(Allocator.Temp);
//         //     var map = mapQuery.ToEntityArray(Allocator.Temp);
//         //
//         //
//         //     var parent = entityManager.CreateEntity(typeof(Child), typeof(LocalTransform),
//         //         typeof(LocalToWorld));
//         //
//         //     var child = entityManager.GetBuffer<Child>(parent);
//         //     if (onlyRotateMap)
//         //     {
//         //         camera.transform.rotation = quaternion.EulerXYZ(0, 0, 0);
//         //
//         //         foreach (var e in boardLeft)
//         //         {
//         //             child.Add(new Child
//         //             {
//         //                 Value = e
//         //             });
//         //         }
//         //
//         //         foreach (var e in boardRight)
//         //         {
//         //             child.Add(new Child
//         //             {
//         //                 Value = e
//         //             });
//         //         }
//         //
//         //         foreach (var e in map)
//         //         {
//         //             child.Add(new Child
//         //             {
//         //                 Value = e
//         //             });
//         //         }
//         //
//         //         var parenttran = entityManager.GetComponentData<LocalTransform>(parent);
//         //
//         //         //parenttran.RotateX(math.radians(rotateDegrees));
//         //         var qua = quaternion.RotateX(math.radians(rotateDegrees));
//         //         parenttran.Scale = 1;
//         //         parenttran.Rotation = qua;
//         //         entityManager.SetComponentData<LocalTransform>(parent, parenttran);
//         //     }
//         //     else
//         //     {
//         //         camera.transform.rotation = quaternion.EulerXYZ(-math.radians(rotateDegrees), 0, 0);
//         //
//         //         foreach (var e in boardLeft)
//         //         {
//         //             child.Add(new Child
//         //             {
//         //                 Value = e
//         //             });
//         //         }
//         //
//         //         foreach (var e in boardRight)
//         //         {
//         //             child.Add(new Child
//         //             {
//         //                 Value = e
//         //             });
//         //         }
//         //
//         //         foreach (var e in map)
//         //         {
//         //             child.Add(new Child
//         //             {
//         //                 Value = e
//         //             });
//         //         }
//         //
//         //         var parenttran = entityManager.GetComponentData<LocalTransform>(parent);
//         //
//         //         //parenttran.RotateX(math.radians(rotateDegrees));
//         //         var qua = quaternion.RotateX(math.radians(0));
//         //         parenttran.Scale = 1;
//         //         parenttran.Rotation = qua;
//         //         entityManager.SetComponentData<LocalTransform>(parent, parenttran);
//         //     }
//         //
//         //
//         //     foreach (var e in boardLeft)
//         //     {
//         //         entityManager.AddComponentData(e, new Parent
//         //         {
//         //             Value = parent
//         //         });
//         //     }
//         //
//         //     foreach (var e in boardRight)
//         //     {
//         //         entityManager.AddComponentData(e, new Parent
//         //         {
//         //             Value = parent
//         //         });
//         //     }
//         //
//         //     foreach (var e in map)
//         //     {
//         //         entityManager.AddComponentData(e, new Parent
//         //         {
//         //             Value = parent
//         //         });
//         //     }
//         // }
//
//         public static Rect GetPerspectiveCameraRect(Camera camera, float distance)
//         {
//             Vector3 cameraPos = camera.transform.position;
//             if (distance <= 0 || distance > camera.farClipPlane)
//             {
//                 distance = camera.farClipPlane;
//             }
//
//             // fov表示相机的垂直视野角度
//             // 2 * tan(fov/2) = height / distance
//             float height = 2 * distance * Mathf.Tan(camera.fieldOfView / 2 * Mathf.Deg2Rad);
//             float width = height * camera.aspect;
//
//             Rect rect = new Rect
//             {
//                 xMin = cameraPos.x - width / 2,
//                 xMax = cameraPos.x + width / 2,
//                 yMin = cameraPos.y - height / 2,
//                 yMax = cameraPos.y + height / 2
//             };
//             return rect;
//         }
//
//         private void LateUpdate()
//         {
//             //Camera mainCamera = Camera.main;
//             if (playerQuery.IsEmpty) return;
//             if (!isInit)
//             {
//                 offset = transform.position;
//                 scene_moduleConfig = ConfigManager.Instance.Tables.Tbscene_module;
//                 levelsConfig = ConfigManager.Instance.Tables.Tblevel;
//                 sceneConfig = ConfigManager.Instance.Tables.Tbscene;
//                 isInit = true;
//             }
//
//             //var rect = GetPerspectiveCameraRect(camera, 1000);
//             //Debug.LogError($"{rect}");
//
//
//             var mapId = sceneConfig.Get(levelsConfig.Get(ResourcesSingleton.Instance.levelId).sceneId).mapId;
//             var map = scene_moduleConfig.Get(mapId);
//
//             //const float scale = 11.71875f;
//             //const float ppu = 100f;
//             // const float mapPiexlWidth = 1024f;
//             // const float mapPiexlHeight = 1024f;
//             //
//             // const float mapBorderPiexlWidth = 512f;
//             // const float mapBorderPiexlHeight = 1024f;
//
//             // float mapPiexlWidthToUnit = mapPiexlWidth / ppu * scale;
//             // float mapPiexlHeightToUnit = mapPiexlHeight / ppu * scale;
//
//             // float mapBorderPiexlWidthToUnit = (mapBorderPiexlWidth / ppu) * scale;
//             // float mapBorderPiexlHeightToUnit = (mapBorderPiexlHeight / ppu) * scale;
//
//             float camerascale = cameraWidth / mapWidth;
//             //var truecameraWidth = mapPiexlWidthToUnit * camerascale;
//
//
//             //camera.fieldOfView = math.clamp(maxFOV * camerascale, minFOV, maxFOV);
//
//
//             // minX = (boardLeft.Position.x - mapBorderPiexlWidthToUnit / 2f);
//             // maxX = (boardRight.Position.x + mapBorderPiexlWidthToUnit / 2f);
//
//
//             var player = playerQuery.ToComponentDataArray<LocalTransform>(Allocator.Temp);
//             foreach (var tran in player)
//             {
//                 var eVector3 = new Vector3(tran.Position.x, tran.Position.y, tran.Position.z);
//                 //
//                 //
//                 // if (!float.IsNaN(eVector3.x) && !float.IsNaN(eVector3.y) &&
//                 //     !float.IsNaN(eVector3.z))
//                 // {
//                 //     transform.position = eVector3 + offset;
//                 // }
//                 Vector3 desiredPosition = eVector3 + offset;
//                 Vector3 smoothedPosition = math.lerp(transform.position, desiredPosition, smoothSpeed);
//                 //var smoothedPosition = desiredPosition;
//                 // 限制相机的 X 坐标在指定范围内
//
//                 // 1-左右封闭
//                 // 2-上下封闭
//                 // 3-全封闭
//                 // 4-全开放
//                 float absX = math.abs((camera.fieldOfView - maxFOV) *
//                                       ((minFOVToX - maxFOVToX * showBorderRatios) / (minFOV - maxFOV)) +
//                                       maxFOVToX * showBorderRatios);
//                 switch (4)
//                     //switch (map.mapType)
//                 {
//                     case 1:
//                         smoothedPosition.x = math.clamp(smoothedPosition.x, -absX, absX);
//                         break;
//                     case 2:
//                         smoothedPosition.y = math.clamp(smoothedPosition.y, -absX, absX);
//                         break;
//                     case 3:
//                         smoothedPosition.x = math.clamp(smoothedPosition.x, -absX, absX);
//                         smoothedPosition.y = math.clamp(smoothedPosition.y, -absX, absX);
//                         break;
//                     case 4:
//
//                         break;
//                 }
//
//
//                 if (!float.IsNaN(smoothedPosition.x) && !float.IsNaN(smoothedPosition.y) &&
//                     !float.IsNaN(smoothedPosition.z))
//                 {
//                     transform.position = smoothedPosition;
//                 }
//             }
//         }
//     }
// }// //---------------------------------------------------------------------
// // JiYuStudio
// // Author: 格伦
// // Time: 2023-07-31 10:50:10
// //---------------------------------------------------------------------
//
//
// using System;
//
// using Main;
// using Unity.Collections;
//
// using Unity.Mathematics;
//
// using UnityEngine;
//
// namespace HotFix_UI
// {
//     //TODO: 边缘平滑过渡处理
//     //同步相机对于玩家的位置 
//     public class CameraUpdateMono : MonoBehaviour
//     {
//         // private EntityManager entityManager;
//         // private EntityQuery playerQuery;
//         // private EntityQuery boardLeftQuery;
//         // private EntityQuery boardRightQuery;
//         // private EntityQuery mapQuery;
//         private bool isInit;
//         private Vector3 offset;
//
//         private float minX;
//         private float maxX;
//
//
//         [Range(0, 1)] public float smoothSpeed = 0.125f;
//         public float cameraWidth = 120f;
//         public float mapWidth = 120f;
//
//         [Range(0, 1)] public float showBorderRatios = 0.5f;
//         [Range(-90, 90)] public float rotateDegrees = 15f;
//         const float maxFOV = 70f;
//         const float minFOV = 10f;
//
//
//         const float maxFOVToX = 33f;
//         //0
//
//         const float minFOVToX = 67f;
//         //10
//
//         private Camera camera;
//
//         //public bool onlyRotateMap;
//
//         private bool isDone;
//
//         // private Tbscene_module scene_moduleConfig;
//         // private Tblevel levelsConfig;
//         // private Tbscene sceneConfig;
//
//         private void Start()
//         {
//             entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
//             playerQuery = entityManager.CreateEntityQuery(typeof(PlayerData), typeof(LocalTransform));
//             //boardLeftQuery = entityManager.CreateEntityQuery(typeof(BoardLeft));
//             //boardRightQuery = entityManager.CreateEntityQuery(typeof(BoardRight));
//             mapQuery = entityManager.CreateEntityQuery(typeof(MapBaseData));
//
//             camera = GetComponent<Camera>();
//         }
//
//         // public void SetMap(bool onlyRotateMap)
//         // {
//         //     var boardLeft = boardLeftQuery.ToEntityArray(Allocator.Temp);
//         //     var boardRight = boardRightQuery.ToEntityArray(Allocator.Temp);
//         //     var map = mapQuery.ToEntityArray(Allocator.Temp);
//         //
//         //
//         //     var parent = entityManager.CreateEntity(typeof(Child), typeof(LocalTransform),
//         //         typeof(LocalToWorld));
//         //
//         //     var child = entityManager.GetBuffer<Child>(parent);
//         //     if (onlyRotateMap)
//         //     {
//         //         camera.transform.rotation = quaternion.EulerXYZ(0, 0, 0);
//         //
//         //         foreach (var e in boardLeft)
//         //         {
//         //             child.Add(new Child
//         //             {
//         //                 Value = e
//         //             });
//         //         }
//         //
//         //         foreach (var e in boardRight)
//         //         {
//         //             child.Add(new Child
//         //             {
//         //                 Value = e
//         //             });
//         //         }
//         //
//         //         foreach (var e in map)
//         //         {
//         //             child.Add(new Child
//         //             {
//         //                 Value = e
//         //             });
//         //         }
//         //
//         //         var parenttran = entityManager.GetComponentData<LocalTransform>(parent);
//         //
//         //         //parenttran.RotateX(math.radians(rotateDegrees));
//         //         var qua = quaternion.RotateX(math.radians(rotateDegrees));
//         //         parenttran.Scale = 1;
//         //         parenttran.Rotation = qua;
//         //         entityManager.SetComponentData<LocalTransform>(parent, parenttran);
//         //     }
//         //     else
//         //     {
//         //         camera.transform.rotation = quaternion.EulerXYZ(-math.radians(rotateDegrees), 0, 0);
//         //
//         //         foreach (var e in boardLeft)
//         //         {
//         //             child.Add(new Child
//         //             {
//         //                 Value = e
//         //             });
//         //         }
//         //
//         //         foreach (var e in boardRight)
//         //         {
//         //             child.Add(new Child
//         //             {
//         //                 Value = e
//         //             });
//         //         }
//         //
//         //         foreach (var e in map)
//         //         {
//         //             child.Add(new Child
//         //             {
//         //                 Value = e
//         //             });
//         //         }
//         //
//         //         var parenttran = entityManager.GetComponentData<LocalTransform>(parent);
//         //
//         //         //parenttran.RotateX(math.radians(rotateDegrees));
//         //         var qua = quaternion.RotateX(math.radians(0));
//         //         parenttran.Scale = 1;
//         //         parenttran.Rotation = qua;
//         //         entityManager.SetComponentData<LocalTransform>(parent, parenttran);
//         //     }
//         //
//         //
//         //     foreach (var e in boardLeft)
//         //     {
//         //         entityManager.AddComponentData(e, new Parent
//         //         {
//         //             Value = parent
//         //         });
//         //     }
//         //
//         //     foreach (var e in boardRight)
//         //     {
//         //         entityManager.AddComponentData(e, new Parent
//         //         {
//         //             Value = parent
//         //         });
//         //     }
//         //
//         //     foreach (var e in map)
//         //     {
//         //         entityManager.AddComponentData(e, new Parent
//         //         {
//         //             Value = parent
//         //         });
//         //     }
//         // }
//
//         public static Rect GetPerspectiveCameraRect(Camera camera, float distance)
//         {
//             Vector3 cameraPos = camera.transform.position;
//             if (distance <= 0 || distance > camera.farClipPlane)
//             {
//                 distance = camera.farClipPlane;
//             }
//
//             // fov表示相机的垂直视野角度
//             // 2 * tan(fov/2) = height / distance
//             float height = 2 * distance * Mathf.Tan(camera.fieldOfView / 2 * Mathf.Deg2Rad);
//             float width = height * camera.aspect;
//
//             Rect rect = new Rect
//             {
//                 xMin = cameraPos.x - width / 2,
//                 xMax = cameraPos.x + width / 2,
//                 yMin = cameraPos.y - height / 2,
//                 yMax = cameraPos.y + height / 2
//             };
//             return rect;
//         }
//
//         private void LateUpdate()
//         {
//             //Camera mainCamera = Camera.main;
//             if (playerQuery.IsEmpty) return;
//             if (!isInit)
//             {
//                 offset = transform.position;
//                 scene_moduleConfig = ConfigManager.Instance.Tables.Tbscene_module;
//                 levelsConfig = ConfigManager.Instance.Tables.Tblevel;
//                 sceneConfig = ConfigManager.Instance.Tables.Tbscene;
//                 isInit = true;
//             }
//
//             //var rect = GetPerspectiveCameraRect(camera, 1000);
//             //Debug.LogError($"{rect}");
//
//
//             var mapId = sceneConfig.Get(levelsConfig.Get(ResourcesSingleton.Instance.levelId).sceneId).mapId;
//             var map = scene_moduleConfig.Get(mapId);
//
//             //const float scale = 11.71875f;
//             //const float ppu = 100f;
//             // const float mapPiexlWidth = 1024f;
//             // const float mapPiexlHeight = 1024f;
//             //
//             // const float mapBorderPiexlWidth = 512f;
//             // const float mapBorderPiexlHeight = 1024f;
//
//             // float mapPiexlWidthToUnit = mapPiexlWidth / ppu * scale;
//             // float mapPiexlHeightToUnit = mapPiexlHeight / ppu * scale;
//
//             // float mapBorderPiexlWidthToUnit = (mapBorderPiexlWidth / ppu) * scale;
//             // float mapBorderPiexlHeightToUnit = (mapBorderPiexlHeight / ppu) * scale;
//
//             float camerascale = cameraWidth / mapWidth;
//             //var truecameraWidth = mapPiexlWidthToUnit * camerascale;
//
//
//             //camera.fieldOfView = math.clamp(maxFOV * camerascale, minFOV, maxFOV);
//
//
//             // minX = (boardLeft.Position.x - mapBorderPiexlWidthToUnit / 2f);
//             // maxX = (boardRight.Position.x + mapBorderPiexlWidthToUnit / 2f);
//
//
//             var player = playerQuery.ToComponentDataArray<LocalTransform>(Allocator.Temp);
//             foreach (var tran in player)
//             {
//                 var eVector3 = new Vector3(tran.Position.x, tran.Position.y, tran.Position.z);
//                 //
//                 //
//                 // if (!float.IsNaN(eVector3.x) && !float.IsNaN(eVector3.y) &&
//                 //     !float.IsNaN(eVector3.z))
//                 // {
//                 //     transform.position = eVector3 + offset;
//                 // }
//                 Vector3 desiredPosition = eVector3 + offset;
//                 Vector3 smoothedPosition = math.lerp(transform.position, desiredPosition, smoothSpeed);
//                 //var smoothedPosition = desiredPosition;
//                 // 限制相机的 X 坐标在指定范围内
//
//                 // 1-左右封闭
//                 // 2-上下封闭
//                 // 3-全封闭
//                 // 4-全开放
//                 float absX = math.abs((camera.fieldOfView - maxFOV) *
//                                       ((minFOVToX - maxFOVToX * showBorderRatios) / (minFOV - maxFOV)) +
//                                       maxFOVToX * showBorderRatios);
//                 switch (4)
//                     //switch (map.mapType)
//                 {
//                     case 1:
//                         smoothedPosition.x = math.clamp(smoothedPosition.x, -absX, absX);
//                         break;
//                     case 2:
//                         smoothedPosition.y = math.clamp(smoothedPosition.y, -absX, absX);
//                         break;
//                     case 3:
//                         smoothedPosition.x = math.clamp(smoothedPosition.x, -absX, absX);
//                         smoothedPosition.y = math.clamp(smoothedPosition.y, -absX, absX);
//                         break;
//                     case 4:
//
//                         break;
//                 }
//
//
//                 if (!float.IsNaN(smoothedPosition.x) && !float.IsNaN(smoothedPosition.y) &&
//                     !float.IsNaN(smoothedPosition.z))
//                 {
//                     transform.position = smoothedPosition;
//                 }
//             }
//         }
//     }
// }