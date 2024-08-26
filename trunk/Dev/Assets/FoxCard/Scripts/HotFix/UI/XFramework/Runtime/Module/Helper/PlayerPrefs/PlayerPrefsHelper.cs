using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace XFramework
{
    public static class PlayerPrefsHelper
    {
        public static void SetString(string key, string value, bool save = false)
        {
            PlayerPrefs.SetString(key, value);
            if (save)
                Save();
        }

        public static void SetInt(string key, int value, bool save = false)
        {
            PlayerPrefs.SetInt(key, value);
            if (save)
                Save();
        }

        public static void SetBool(string key, bool value, bool save = false)
        {
            PlayerPrefs.SetInt(key, value ? 1 : 0);
            if (save)
                Save();
        }

        public static void SetFloat(string key, float value, bool save = false)
        {
            PlayerPrefs.SetFloat(key, value);
            if (save)
                Save();
        }

        public static bool TryGetString(string key, out string value)
        {
            value = default;
            if (HasKey(key))
            {
                value = PlayerPrefs.GetString(key);
                return true;
            }
            return false;
        }

        public static bool TryGetInt(string key, out int value)
        {
            value = default;
            if (HasKey(key))
            {
                value = PlayerPrefs.GetInt(key);
                return true;
            }
            return false;
        }

        public static bool TryGetBool(string key, out bool value)
        {
            value = default;
            if (HasKey(key))
            {
                value = PlayerPrefs.GetInt(key) != 0;
                return true;
            }
            return false;
        }

        public static bool TryGetFloat(string key, out float value)
        {
            value = default;
            if (HasKey(key))
            {
                value = PlayerPrefs.GetFloat(key);
                return true;
            }
            return false;
        }

        public static string GetString(string key, string defaultValue = default)
        {
            return PlayerPrefs.GetString(key, defaultValue);
        }

        public static int GetInt(string key, int defaultValue = default)
        {
            return PlayerPrefs.GetInt(key, defaultValue);
        }

        public static bool GetBool(string key, bool defaultValue = default)
        {
            if (TryGetBool(key, out bool value))
                return value;

            return defaultValue;
        }

        public static float GetFloat(string key, float defaultValue = default)
        {
            return PlayerPrefs.GetFloat(key, defaultValue);
        }

        public static bool HasKey(string key)
        {
            return PlayerPrefs.HasKey(key);
        }

        public static void DeleteKey(string key)
        {
            PlayerPrefs.DeleteKey(key);
        }

        public static void DeleteAll()
        {
            PlayerPrefs.DeleteAll();
        }

        public static void Save()
        {
            PlayerPrefs.Save();
        }
    }
}
