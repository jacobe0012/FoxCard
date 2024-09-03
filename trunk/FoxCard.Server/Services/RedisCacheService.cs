using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace FoxCard.Server.Services;

public class RedisCacheService : IRedisCacheService
{
    private readonly IDistributedCache _redisCache;

    public RedisCacheService(IDistributedCache redisCache)
    {
        _redisCache = redisCache;
    }

    public T? GetData<T>(string key)
    {
        var value = _redisCache.GetString(key);
        if (value == null)
        {
            return default;
        }

        return JsonConvert.DeserializeObject<T>(value);
    }

    public void SetData<T>(string key, T? data)
    {
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
        };
        _redisCache.SetString(key, JsonConvert.SerializeObject(data), options);
    }
}