using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XFramework
{
    [DisallowMultipleComponent]
    public class UIReference : Reference
    {
        private static Dictionary<int, Object> textList = new Dictionary<int, Object>();

        [SerializeField, HideInInspector]
        private List<ElementData> _textData = new List<ElementData>();

        private Dictionary<string, Object> _textDict = new Dictionary<string, Object>();

        public static IEnumerable<Object> TextList()
        {
            return textList.Values;
        }

        internal static void AddText(int instanceId, Object text)
        { 
            if (!Application.isPlaying)
                return;

            textList.Add(instanceId, text);
        }

        internal static void RemoveText(int instanceId)
        {
            textList.Remove(instanceId);
        }

        public Object GetText(string key)
        {
            _textDict.TryGetValue(key, out Object text);
            return text;
        }

        public IEnumerable<string> GetAllTextKeys()
        {
            return _textDict.Keys;
        }

        public IEnumerable<Object> GetAllText()
        {
            return _textDict.Values;
        }

        public override void OnAfterDeserialize()
        {
            base.OnAfterDeserialize();

            _textDict.Clear();
            foreach (var data in _textData)
            {
                var key = data.Key;
                var obj = data.Object;
                if (obj != null)
                {
                    if (!_textDict.ContainsKey(key))
                        _textDict.Add(key, obj);
                }
            }
        }
    }
}
