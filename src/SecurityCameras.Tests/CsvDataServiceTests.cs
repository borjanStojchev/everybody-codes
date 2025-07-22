using SecurityCameras.Common.Services;

namespace SecurityCameras.Tests;

public class CsvDataServiceTests
{
    [Fact]
    public void ParseCsvLines_ValidLines_ReturnsCameras()
    {
        // Arrange
        var lines = new List<string>
        {
            "Camera;Latitude;Longitude",
            "UTR-CM-501 Neude;52.093421;5.118278",
            "UTR-CM-502 Potterstraat;52.093599;5.118325"
        };

        var service = new CsvDataService();

        // Act
        var result = service.ParseCsvLines(lines);

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Equal("UTR-CM-501 Neude", result[0].Name);
        Assert.Equal(52.093421, result[0].Lat);
        Assert.Equal(5.118278, result[0].Lon);
        Assert.Equal(501, result[0].Number);
    }
    
    [Fact]
    public void ParseCsvLines_InvalidLines()
    {
        // Arrange
        var lines = new List<string>
        {
            "Camera;Latitude;Longitude",
            "INVALID LINE",
            "UTR-CM-503;not-a-number;coords"
        };

        var service = new CsvDataService();

        // Act
        var result = service.ParseCsvLines(lines);

        // Assert
        Assert.Empty(result);
    }
    
    [Fact]
    public void ParseCsvLines_InvalidLine_SkipsIt()
    {
        // Arrange
        var lines = new List<string>
        {
            "Camera;Latitude;Longitude",
            "INVALID LINE",
            "UTR-CM-502 Potterstraat;52.093599;5.118325"
        };

        var service = new CsvDataService();

        // Act
        var result = service.ParseCsvLines(lines);

        // Assert
        Assert.Equal("UTR-CM-502 Potterstraat", result[0].Name);
        Assert.Equal(52.093599, result[0].Lat);
        Assert.Equal(5.118325, result[0].Lon);
        Assert.Equal(502, result[0].Number);
    }
}