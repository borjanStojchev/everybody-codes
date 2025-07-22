using SecurityCameras.Common.Models;

namespace SecurityCameras.Common.Interfaces;

public interface ICameraSearchService
{
    Task<IEnumerable<Camera>> SearchCamerasAsync(string searchTerm);
    Task<IEnumerable<Camera>> GetAllCamerasAsync();
}