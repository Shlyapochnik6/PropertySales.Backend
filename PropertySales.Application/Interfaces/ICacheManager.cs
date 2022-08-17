using Microsoft.Extensions.Caching.Memory;

namespace PropertySales.Application.Interfaces;

public interface ICacheManager<TEntity>
{
    MemoryCacheEntryOptions CacheEntryOptions { get; set; }
    Task<TEntity> GetOrSetCacheValue(object key, Func<Task<TEntity?>> query);
    void ChangeCacheValue(object key, TEntity newEntity);
    void RemoveCacheValue(object key);
}