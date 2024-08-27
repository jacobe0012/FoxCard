using System.Collections.Generic;
using UnityEngine;

namespace XFramework
{
    public sealed class UnityConfigLoader : IConfigLoader
    {
        public async XFTask<Dictionary<string, byte[]>> LoadAllAsync()
        {
            Dictionary<string, byte[]> dict = new Dictionary<string, byte[]>();

            async XFTask Load(string fileName)
            {
                TextAsset asset =
                    await ResourcesManager.Instance.Loader.LoadAssetAsync<TextAsset>($"Config/Gen/{fileName}");
                if (asset is null)
                    return;

                dict[fileName] = asset.bytes;
                ResourcesManager.Instance.Loader.ReleaseAsset(asset);
            }

            using var tasks = XList<XFTask>.Create();
            var types = TypesManager.Instance.GetTypes(typeof(ConfigAttribute));
            foreach (var type in types)
            {
                tasks.Add(Load(type.Name));
            }

            await XFTaskHelper.WaitAll(tasks);
            return dict;
        }

        public byte[] LoadOne(string name)
        {
            var textAsset = ResourcesManager.Instance.Loader.LoadAsset<TextAsset>($"Config/Gen/{name}");
            if (textAsset is null)
                return null;

            var bytes = textAsset.bytes;
            ResourcesManager.Instance.Loader.ReleaseAsset(textAsset);

            return bytes;
        }

        public async XFTask<byte[]> LoadOneAsync(string name)
        {
            var textAsset = await ResourcesManager.Instance.Loader.LoadAssetAsync<TextAsset>($"Config/Gen/{name}");
            if (textAsset is null)
                return null;

            var bytes = textAsset.bytes;
            ResourcesManager.Instance.Loader.ReleaseAsset(textAsset);

            return bytes;
        }
    }
}