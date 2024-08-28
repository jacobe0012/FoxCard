// using TMPro;
// using Unity.Entities;
// using UnityEngine;
//
// namespace Main
// {
//     public class FPSShowMono : MonoBehaviour
//     {
//         public TMP_Text FPSTex;
//
//         public TMP_Text entitiesCount;
//         //public TMP_Text stateMatchTex;
//
//
//         private float time;
//         private float frameCount;
//         private float fps;
//
//         private EntityManager entityManager;
//
//         private void Start()
//         {
//             entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
//         }
//
//         private void LateUpdate()
//         {
//             var query = entityManager.CreateEntityQuery(typeof(EnemyData));
//             if (query.IsEmpty)
//             {
//                 return;
//             }
//
//
//             var count = entityManager.CreateEntityQuery(typeof(EnemyData)).CalculateEntityCount();
//             //statematchine show
//
//             //entities counts
//             entitiesCount.text = $"Enemy: {count}";
//             //Fps
//             if (time >= 1 && frameCount >= 1)
//             {
//                 fps = frameCount / time;
//
//                 time = 0;
//
//                 frameCount = 0;
//             }
//
//             FPSTex.color = fps >= 60
//                 ? Color.white
//                 : (fps > 40 ? Color.yellow : Color.red);
//
//             FPSTex.text = "FPS:" + fps.ToString("f2");
//
//             time += Time.deltaTime;
//
//             frameCount++;
//         }
//     }
// }