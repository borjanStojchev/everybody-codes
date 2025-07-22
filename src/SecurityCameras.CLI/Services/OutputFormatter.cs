using SecurityCameras.CLI.Interfaces;
using SecurityCameras.Common.Models;

namespace SecurityCameras.CLI.Services;

public class OutputFormatter : IOutputFormatter
{
    public string FormatOutput(IEnumerable<Camera> cameras)
    {
        var result = string.Empty;
        if (!cameras.Any())
        {
            Console.WriteLine("No cameras found.");
            return string.Empty;
        }

        foreach (var camera in cameras)
        {
            result += $"{camera.Number.ToString()} | {camera.Name} | {camera.Lat:F6} | {camera.Lon:F6}\n";
        }
        return result;
    }
}