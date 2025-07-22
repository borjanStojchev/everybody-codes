using Cocona;
using SecurityCameras.CLI.Interfaces;
using SecurityCameras.Common.Interfaces;

namespace SecurityCameras.CLI.Commands;

public class SearchCommand
{
    private readonly ICameraSearchService _cameraSearchService;
    private readonly IOutputFormatter _formatter;
    public SearchCommand(ICameraSearchService cameraSearchService, IOutputFormatter formatter)
    {
        _cameraSearchService = cameraSearchService;
        _formatter = formatter;
    }
    
    [Command("search", Description = "Search for cameras by name.")]
    public async Task Search(string name)
    {
        var cameras = await _cameraSearchService.SearchCamerasAsync(name);
        var output = _formatter.FormatOutput(cameras);
        Console.WriteLine(output);
    }
}