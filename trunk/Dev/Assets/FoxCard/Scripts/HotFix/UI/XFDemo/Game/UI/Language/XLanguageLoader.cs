using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XFramework
{
    public class XLanguageLoader : ILanguageLoader
    {
        private enum LanguageType
        {
            /// <summary>
            /// ¼òÌå
            /// </summary>
            CN = 1,
            /// <summary>
            /// ·±Ìå
            /// </summary>
            TC = 2,
            /// <summary>
            /// Ó¢ÎÄ
            /// </summary>
            EN = 3,
        }

        public int GetDefaultLanguageType()
        {
            return (int)LanguageType.CN;
        }

        public string GetValue(int languageType, string key)
        {
            var type = (LanguageType)languageType;

            // if (LanguageConfigManager.Instance is null)
            //     return key;
            //
            // if (LanguageConfigManager.Instance.GetConfigByKey(key, out var config))
            // {
            //     switch (type)
            //     {
            //         case LanguageType.CN:
            //             return config.CN;
            //         case LanguageType.TC:
            //             return config.TC;
            //         case LanguageType.EN:
            //             return config.EN;
            //         default:
            //             return key;
            //     }
            // }
            // else if (GenLanguageConfigManager.Instance != null && GenLanguageConfigManager.Instance.GetConfigByKey(key, out var config1))
            // {
            //     switch (type)
            //     {
            //         case LanguageType.CN:
            //             return config1.CN;
            //         case LanguageType.TC:
            //             return config1.TC;
            //         case LanguageType.EN:
            //             return config1.EN;
            //         default:
            //             return key;
            //     }
            // }

            return key;
        }
    }
}
