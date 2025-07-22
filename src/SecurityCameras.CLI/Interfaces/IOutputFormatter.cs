using SecurityCameras.Common.Models;

namespace SecurityCameras.CLI.Interfaces;

public interface IOutputFormatter
{
    string FormatOutput(IEnumerable<Camera> cameras);
}