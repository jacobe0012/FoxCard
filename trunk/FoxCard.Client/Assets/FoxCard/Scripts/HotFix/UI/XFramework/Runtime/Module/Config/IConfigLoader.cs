using System.Collections.Generic;

namespace XFramework
{
    public interface IConfigLoader
    {
        byte[] LoadOne(string name);

        XFTask<byte[]> LoadOneAsync(string name);

        XFTask<Dictionary<string, byte[]>> LoadAllAsync();
    }
}