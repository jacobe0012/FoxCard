using System;
using System.Collections.Generic;
using cfg;
using cfg.config;
using SimpleJSON;
using UnityEngine;
using XFramework;
using YooAsset;

namespace HotFix_UI
{
    public sealed class ConfigManager : Singleton<ConfigManager>, IDisposable
    {
        private Tables tables;

        public Tables Tables => tables;

        public void Dispose()
        {
            tables = null;
            Instance = null;
        }

        public void InitTables(Tables newTables)
        {
            tables = newTables;
        }


        public void SwitchLanguages(Tblanguage.L10N l10N)
        {
            tables.Tblanguage.SwitchL10N(l10N);
        }
    }
}