using SecurityCameras.Common.Models;

namespace SecurityCameras.Common.Interfaces;

public interface ICsvDataService
{
    public Task<List<Camera>> ExtractCameras(string csvFilePath);
}