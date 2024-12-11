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
            lookup = new global::System.Collections.Generic.Dictionary<global::System.Type, int>(10)
            {
                { typeof(global::System.Collections.Generic.List<global::HotFix_UI.ItemInfo>), 0 },
                { typeof(global::System.Collections.Generic.List<UnityEngine.Vector3>), 1 },
                { typeof(global::HotFix_UI.ItemInfo), 2 },
                { typeof(global::HotFix_UI.LocationData), 3 },
                { typeof(global::HotFix_UI.MyData), 4 },
                { typeof(global::HotFix_UI.MyMessage), 5 },
                { typeof(global::HotFix_UI.OtherData), 6 },
                { typeof(global::HotFix_UI.PlayerData), 7 },
                { typeof(global::HotFix_UI.PlayerResource), 8 },
                { typeof(global::HotFix_UI.Rewards), 9 },
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
                case 0: return new global::MessagePack.Formatters.ListFormatter<global::HotFix_UI.ItemInfo>();
                case 1: return new global::MessagePack.Formatters.ListFormatter<UnityEngine.Vector3>();
                case 2: return new MessagePack.Formatters.HotFix_UI.ItemInfoFormatter();
                case 3: return new MessagePack.Formatters.HotFix_UI.LocationDataFormatter();
                case 4: return new MessagePack.Formatters.HotFix_UI.MyDataFormatter();
                case 5: return new MessagePack.Formatters.HotFix_UI.MyMessageFormatter();
                case 6: return new MessagePack.Formatters.HotFix_UI.OtherDataFormatter();
                case 7: return new MessagePack.Formatters.HotFix_UI.PlayerDataFormatter();
                case 8: return new MessagePack.Formatters.HotFix_UI.PlayerResourceFormatter();
                case 9: return new MessagePack.Formatters.HotFix_UI.RewardsFormatter();
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
