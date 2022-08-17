using Microsoft.Extensions.Caching.Memory;
using PropertySales.Application.Common.Exceptions;
using PropertySales.Application.Interfaces;

namespace PropertySales.Application.Common.Caches;

public class CacheManager<TEntity> : ICacheManager<TEntity>
{
    private readonly IMemoryCache _cache;
    public MemoryCacheEntryOptions CacheEntryOptions { get; set; } = new();
    
    public CacheManager(IMemoryCache cache)
    {
        _cache = cache;
    }

    public async Task<TEntity> GetOrSetCacheValue(object key, Func<Task<TEntity?>> query)
    {
        if (!_cache.TryGetValue(key, out TEntity? entity))
        {
            entity = await query();
            if (entity == null)
                throw new NotFoundException(typeof(TEntity).Name, key);

            _cache.Set(key, entity, CacheEntryOptions);
        }

        if (entity == null)
        {
            throw new NullReferenceException(
                $"{typeof(TEntity).Name} value should not be null");
        }

        return entity;
    }
    
    public void ChangeCacheValue(object key, TEntity newEntity)
    {
        if (!_cache.TryGetValue(key, out TEntity entity))
            return;
        
        if (entity == null)
            return;

        _cache.Set(key, newEntity, CacheEntryOptions);
    }

    public void RemoveCacheValue(object key)
    {
        if (_cache.TryGetValue(key, out TEntity _))
        {
            _cache.Remove(key);
        }
    }
}