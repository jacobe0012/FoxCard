// <auto-generated>
// THIS (.cs) FILE IS GENERATED BY MPC(MessagePack-CSharp). DO NOT CHANGE IT.
// </auto-generated>

#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 168
#pragma warning disable CS1591 // document public APIs

#pragma warning disable SA1129 // Do not use default value type constructor
#pragma warning disable SA1309 // Field names should not begin with underscore
#pragma warning disable SA1312 // Variable names should begin with lower-case letter
#pragma warning disable SA1403 // File may only contain a single namespace
#pragma warning disable SA1649 // File name should match first type name

namespace MessagePack.Formatters.HotFix_UI
{
    public sealed class PlayerResourceFormatter : global::MessagePack.Formatters.IMessagePackFormatter<global::HotFix_UI.PlayerResource>
    {

        public void Serialize(ref global::MessagePack.MessagePackWriter writer, global::HotFix_UI.PlayerResource value, global::MessagePack.MessagePackSerializerOptions options)
        {
            if (value == null)
            {
                writer.WriteNil();
                return;
            }

            global::MessagePack.IFormatterResolver formatterResolver = options.Resolver;
            writer.WriteArrayHeader(8);
            global::MessagePack.FormatterResolverExtensions.GetFormatterWithVerify<global::System.Collections.Generic.List<global::HotFix_UI.ItemInfo>>(formatterResolver).Serialize(ref writer, value.ItemList, options);
            writer.Write(value.LastSignTimeStamp);
            writer.Write(value.SignCount);
            writer.Write(value.LastLoginTimeStamp);
            writer.Write(value.LoginCount);
            writer.Write(value.ContinuousLoginCount);
            global::MessagePack.FormatterResolverExtensions.GetFormatterWithVerify<global::HotFix_UI.GameAchievement>(formatterResolver).Serialize(ref writer, value.GameAchieve, options);
            global::MessagePack.FormatterResolverExtensions.GetFormatterWithVerify<global::HotFix_UI.GameMail>(formatterResolver).Serialize(ref writer, value.GameMail, options);
        }

        public global::HotFix_UI.PlayerResource Deserialize(ref global::MessagePack.MessagePackReader reader, global::MessagePack.MessagePackSerializerOptions options)
        {
            if (reader.TryReadNil())
            {
                return null;
            }

            options.Security.DepthStep(ref reader);
            global::MessagePack.IFormatterResolver formatterResolver = options.Resolver;
            var length = reader.ReadArrayHeader();
            var ____result = new global::HotFix_UI.PlayerResource();

            for (int i = 0; i < length; i++)
            {
                switch (i)
                {
                    case 0:
                        ____result.ItemList = global::MessagePack.FormatterResolverExtensions.GetFormatterWithVerify<global::System.Collections.Generic.List<global::HotFix_UI.ItemInfo>>(formatterResolver).Deserialize(ref reader, options);
                        break;
                    case 1:
                        ____result.LastSignTimeStamp = reader.ReadInt64();
                        break;
                    case 2:
                        ____result.SignCount = reader.ReadInt32();
                        break;
                    case 3:
                        ____result.LastLoginTimeStamp = reader.ReadInt64();
                        break;
                    case 4:
                        ____result.LoginCount = reader.ReadInt32();
                        break;
                    case 5:
                        ____result.ContinuousLoginCount = reader.ReadInt32();
                        break;
                    case 6:
                        ____result.GameAchieve = global::MessagePack.FormatterResolverExtensions.GetFormatterWithVerify<global::HotFix_UI.GameAchievement>(formatterResolver).Deserialize(ref reader, options);
                        break;
                    case 7:
                        ____result.GameMail = global::MessagePack.FormatterResolverExtensions.GetFormatterWithVerify<global::HotFix_UI.GameMail>(formatterResolver).Deserialize(ref reader, options);
                        break;
                    default:
                        reader.Skip();
                        break;
                }
            }

            reader.Depth--;
            return ____result;
        }
    }

}

#pragma warning restore 168
#pragma warning restore 414
#pragma warning restore 618
#pragma warning restore 612

#pragma warning restore SA1129 // Do not use default value type constructor
#pragma warning restore SA1309 // Field names should not begin with underscore
#pragma warning restore SA1312 // Variable names should begin with lower-case letter
#pragma warning restore SA1403 // File may only contain a single namespace
#pragma warning restore SA1649 // File name should match first type name
