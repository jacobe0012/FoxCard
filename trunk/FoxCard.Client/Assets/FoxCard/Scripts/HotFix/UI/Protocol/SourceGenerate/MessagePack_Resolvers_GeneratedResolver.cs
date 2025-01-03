// <auto-generated>
// THIS (.cs) FILE IS GENERATED BY MPC(MessagePack-CSharp). DO NOT CHANGE IT.
// </auto-generated>

#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 168
#pragma warning disable CS1591 // document public APIs

#pragma warning disable SA1312 // Variable names should begin with lower-case letter
#pragma warning disable SA1649 // File name should match first type name

namespace MessagePack.Resolvers
{
    public class GeneratedResolver : global::MessagePack.IFormatterResolver
    {
        public static readonly global::MessagePack.IFormatterResolver Instance = new GeneratedResolver();

        private GeneratedResolver()
        {
        }

        public global::MessagePack.Formatters.IMessagePackFormatter<T> GetFormatter<T>()
        {
            return FormatterCache<T>.Formatter;
        }

        private static class FormatterCache<T>
        {
            internal static readonly global::MessagePack.Formatters.IMessagePackFormatter<T> Formatter;

            static FormatterCache()
            {
                var f = GeneratedResolverGetFormatterHelper.GetFormatter(typeof(T));
                if (f != null)
                {
                    Formatter = (global::MessagePack.Formatters.IMessagePackFormatter<T>)f;
                }
            }
        }
    }

    internal static class GeneratedResolverGetFormatterHelper
    {
        private static readonly global::System.Collections.Generic.Dictionary<global::System.Type, int> lookup;

        static GeneratedResolverGetFormatterHelper()
        {
            lookup = new global::System.Collections.Generic.Dictionary<global::System.Type, int>(18)
            {
                { typeof(global::System.Collections.Generic.List<global::HotFix_UI.AchieveItem>), 0 },
                { typeof(global::System.Collections.Generic.List<global::HotFix_UI.MailItem>), 1 },
                { typeof(global::System.Collections.Generic.List<int>), 2 },
                { typeof(global::System.Collections.Generic.List<UnityEngine.Vector3>), 3 },
                { typeof(global::HotFix_UI.AchieveItem), 4 },
                { typeof(global::HotFix_UI.GameAchievement), 5 },
                { typeof(global::HotFix_UI.GameMail), 6 },
                { typeof(global::HotFix_UI.GameSign), 7 },
                { typeof(global::HotFix_UI.GameSignAcc7), 8 },
                { typeof(global::HotFix_UI.LocationData), 9 },
                { typeof(global::HotFix_UI.MailItem), 10 },
                { typeof(global::HotFix_UI.MyData), 11 },
                { typeof(global::HotFix_UI.MyMessage), 12 },
                { typeof(global::HotFix_UI.OtherData), 13 },
                { typeof(global::HotFix_UI.PlayerData), 14 },
                { typeof(global::HotFix_UI.PlayerResource), 15 },
                { typeof(global::HotFix_UI.PlayerServerData), 16 },
                { typeof(global::HotFix_UI.Rewards), 17 },
            };
        }

        internal static object GetFormatter(global::System.Type t)
        {
            int key;
            if (!lookup.TryGetValue(t, out key))
            {
                return null;
            }

            switch (key)
            {
                case 0: return new global::MessagePack.Formatters.ListFormatter<global::HotFix_UI.AchieveItem>();
                case 1: return new global::MessagePack.Formatters.ListFormatter<global::HotFix_UI.MailItem>();
                case 2: return new global::MessagePack.Formatters.ListFormatter<int>();
                case 3: return new global::MessagePack.Formatters.ListFormatter<UnityEngine.Vector3>();
                case 4: return new MessagePack.Formatters.HotFix_UI.AchieveItemFormatter();
                case 5: return new MessagePack.Formatters.HotFix_UI.GameAchievementFormatter();
                case 6: return new MessagePack.Formatters.HotFix_UI.GameMailFormatter();
                case 7: return new MessagePack.Formatters.HotFix_UI.GameSignFormatter();
                case 8: return new MessagePack.Formatters.HotFix_UI.GameSignAcc7Formatter();
                case 9: return new MessagePack.Formatters.HotFix_UI.LocationDataFormatter();
                case 10: return new MessagePack.Formatters.HotFix_UI.MailItemFormatter();
                case 11: return new MessagePack.Formatters.HotFix_UI.MyDataFormatter();
                case 12: return new MessagePack.Formatters.HotFix_UI.MyMessageFormatter();
                case 13: return new MessagePack.Formatters.HotFix_UI.OtherDataFormatter();
                case 14: return new MessagePack.Formatters.HotFix_UI.PlayerDataFormatter();
                case 15: return new MessagePack.Formatters.HotFix_UI.PlayerResourceFormatter();
                case 16: return new MessagePack.Formatters.HotFix_UI.PlayerServerDataFormatter();
                case 17: return new MessagePack.Formatters.HotFix_UI.RewardsFormatter();
                default: return null;
            }
        }
    }
}

#pragma warning restore 168
#pragma warning restore 414
#pragma warning restore 618
#pragma warning restore 612

#pragma warning restore SA1312 // Variable names should begin with lower-case letter
#pragma warning restore SA1649 // File name should match first type name
