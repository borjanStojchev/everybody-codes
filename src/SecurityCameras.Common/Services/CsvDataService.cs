using System.Globalization;
using System.Reflection;
using SecurityCameras.Common.Interfaces;
using SecurityCameras.Common.Models;

namespace SecurityCameras.Common.Services;

public class CsvDataDataService : ICsvDataService
{
    public async Task<List<Camera>> ExtractCameras(string csvFilePath)
    {
        var cameras = new List<Camera>();
        if (!File.Exists(csvFilePath))
        {
            Console.WriteLine("Csv data file not found.");
            return cameras;
        }
        
        var lines = await File.ReadAllLinesAsync(csvFilePath);
        if (lines == null || lines.Length == 0)
        {
            Console.WriteLine("Empty data file.");
            return cameras;
        }
        var dataLines = lines.Skip(1);

        foreach (var line in dataLines)
        {
            var parts = line.Split(';');
            if (parts.Length != 3)
            {
                Console.WriteLine($"Invalid line: {line}");
                continue;
            }

            if (double.TryParse(parts[1], NumberStyles.Any, CultureInfo.InvariantCulture, out double lat) &&
                double.TryParse(parts[2], NumberStyles.Any, CultureInfo.InvariantCulture, out double lon))
            {
                cameras.Add(new Camera
                {
                    Name = parts[0],
                    Lat = lat,
                    Lon = lon
                });
            }
        }
        return cameras;
    }
}