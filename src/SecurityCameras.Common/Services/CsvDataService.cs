using System.Globalization;
using System.Reflection;
using SecurityCameras.Common.Interfaces;
using SecurityCameras.Common.Models;

namespace SecurityCameras.Common.Services;

public class CsvDataService : ICsvDataService
{
    public async Task<List<Camera>> ExtractCameras(string csvFilePath)
    {
        if (!File.Exists(csvFilePath))
        {
            Console.WriteLine("Csv data file not found.");
            return new List<Camera>();
        }
        
        var lines = await File.ReadAllLinesAsync(csvFilePath);
        if (lines == null || lines.Length == 0)
        {
            Console.WriteLine("Empty data file.");
            return new List<Camera>();
        }
        
        return ParseCsvLines(lines);
    }

    public List<Camera> ParseCsvLines(IEnumerable<string> lines)
    {
        var dataLines = lines.Skip(1);
        var cameras = new List<Camera>();
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