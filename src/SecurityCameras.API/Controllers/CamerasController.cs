using Microsoft.AspNetCore.Mvc;
using SecurityCameras.Common.Interfaces;
using SecurityCameras.Common.Models;

namespace SecurityCameras.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CamerasController : ControllerBase
{
    private readonly ICameraSearchService _cameraSearchService;

    public CamerasController(ILogger<CamerasController> logger, ICameraSearchService cameraSearchService)
    {
        _cameraSearchService = cameraSearchService;
    }

    [HttpGet()]
    public async Task<IActionResult> GetAllCameras()
    {
        var cityAverages = await _cameraSearchService.GetAllCamerasAsync();
        return Ok(cityAverages);
    }
    
    [HttpGet("Search")]
    public async Task<IActionResult> SearchCameras(string name)
    {
        var cityAverages = await _cameraSearchService.SearchCamerasAsync(name);
        return Ok(cityAverages);
    }
}