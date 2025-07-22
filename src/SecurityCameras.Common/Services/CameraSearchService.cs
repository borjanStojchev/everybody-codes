using Microsoft.Extensions.Caching.Memory;
using SecurityCameras.Common.Interfaces;
using SecurityCameras.Common.Models;

namespace SecurityCameras.Common.Services;

public class CameraSearchService : ICameraSearchService
{
    private readonly string _dataFilePath = "../../data/cameras-defb.csv";
    private readonly ICsvDataService _csvDataService;
    private readonly IMemoryCache _cache;
    private const string CacheKey = "securityCameras";

    public CameraSearchService(ICsvDataService csvDataService, IMemoryCache cache)
    {
        _csvDataService = csvDataService;
        _cache = cache;
    }
    
    public async Task<IEnumerable<Camera>> SearchCamerasAsync(string searchTerm)
    {
        var cameras = await GetCamerasAsync();
        if (!cameras.Any())
        {
            Console.WriteLine("No cameras found.");
            return cameras;       
        }
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            return cameras;
        }
        
        var results = cameras
            .Where(c => c.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
            .OrderBy(c => c.Number);

        return results;
    }

    public async Task<IEnumerable<Camera>> GetAllCamerasAsync()
    {
        return await GetCamerasAsync();
    }

    private async Task<IEnumerable<Camera>> GetCamerasAsync()
    {
        var cachedData = GetCachedData();
        if (cachedData == null)
        {
            var cameras = await _csvDataService.ExtractCameras(_dataFilePath);
            _cache.Set(CacheKey, cameras);
            return cameras;
        }
        else
            return cachedData;
    }
    
    private IEnumerable<Camera> GetCachedData()
    {
        if (!_cache.TryGetValue(CacheKey, out IEnumerable<Camera> cameras))
        {
            return null;
        }
        return cameras;
    }
}