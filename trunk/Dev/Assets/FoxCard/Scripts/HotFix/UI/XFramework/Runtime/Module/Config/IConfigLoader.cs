using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XFramework
{
    public interface IConfigLoader
    {
        byte[] LoadOne(string name);

        XFTask<byte[]> LoadOneAsync(string name);

        XFTask<Dictionary<string, byte[]>> LoadAllAsync();
    }
}
