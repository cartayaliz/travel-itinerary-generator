using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Turist.Services.Services;

public static class CacheHelper
{
    public static async Task<T> GetOrCreateAsync<T>(
        this IDistributedCache cache,
        string key,
        Func<Task<T>> factory,
        TimeSpan ttl)
    {
        var json = await cache.GetStringAsync(key);
        if (json is not null)
        {
            try
            {
                var guardado = JsonSerializer.Deserialize<T>(json);
                if (guardado is not null) return guardado;
            }
            catch (JsonException) { /* clave vieja: se recalcula */ }
        }

        var resultado = await factory();
        await cache.SetStringAsync(
            key,
            JsonSerializer.Serialize(resultado),
            new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = ttl });

        return resultado;
    }

}