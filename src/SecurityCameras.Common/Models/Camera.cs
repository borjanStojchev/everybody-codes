using System.Text.RegularExpressions;

namespace SecurityCameras.Common.Models;

public class Camera
{
    public string Name { get; set; }
    public double Lat { get; set; }
    public double Lon { get; set; }
    
    public int? Number
    {
        get
        {
            var match = Regex.Match(Name, @"UTR-CM-(\d+)");
            if (match.Success && int.TryParse(match.Groups[1].Value, out int number))
            {
                return number;
            }
            return null;
        }
    }
}