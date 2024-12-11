using cfg;
using SimpleJSON;


namespace FoxCard.Server.Datas.Config.Scripts;

public class MyConfig : IDisposable
{
    public static Tables? Tables { get; set; }

    public static void InitConfig()
    {
        Tables = new cfg.Tables(LoadJson);
    }

    // private static JsonElement LoadJson(string file)
    // {
    //     return JsonDocument.Parse(System.IO.File.ReadAllBytes("Datas/Config/Json/" + file + ".json"))
    //         .RootElement;
    // }
    private static JSONNode LoadJson(string file)
    {
        return JSON.Parse(File.ReadAllText("Datas/Config/Json/" + file + ".json", System.Text.Encoding.UTF8));
    }


    public void Dispose()
    {
        Tables = null;
    }
}