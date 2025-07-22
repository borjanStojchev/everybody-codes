using Microsoft.Extensions.Caching.Memory;
using Moq;
using SecurityCameras.Common.Interfaces;
using SecurityCameras.Common.Models;
using SecurityCameras.Common.Services;

namespace SecurityCameras.Tests;

public class CameraSearchServiceTests
{
    private readonly Mock<ICsvDataService> _csvDataServiceMock;
    private readonly IMemoryCache _memoryCache;
    private readonly CameraSearchService _service;
    
    public CameraSearchServiceTests()
    {
        _csvDataServiceMock = new Mock<ICsvDataService>();
        _memoryCache = new MemoryCache(new MemoryCacheOptions());
        _service = new CameraSearchService(_csvDataServiceMock.Object, _memoryCache);
    }
    [Fact]
    public async Task SearchCamerasAsync_WithEmptySearchTerm_ReturnsAllCameras()
    {
        // Arrange
        var cameraList = new List<Camera>
        {
            new Camera { Name = "UTR-CM-101", Lat = 52.1, Lon = 5.1 },
            new Camera { Name = "UTR-CM-102", Lat = 52.2, Lon = 5.2},
        };

        _csvDataServiceMock
            .Setup(s => s.ExtractCameras(It.IsAny<string>()))
            .ReturnsAsync(cameraList);

        // Act
        var result = await _service.SearchCamerasAsync("");

        // Assert
        Assert.Equal(2, result.Count());
    }
    
    [Fact]
    public async Task SearchCamerasAsync_WithMatchingName_ReturnsFilteredCameras()
    {
        // Arrange
        var cameraList = new List<Camera>
        {
            new Camera { Name = "UTR-CM-101 Neude", Lat = 52.1, Lon = 5.1 },
            new Camera { Name = "UTR-CM-102 Potterstraat", Lat = 52.2, Lon = 5.2 },
        };

        _csvDataServiceMock
            .Setup(s => s.ExtractCameras(It.IsAny<string>()))
            .ReturnsAsync(cameraList);

        // Act
        var result = await _service.SearchCamerasAsync("Neude");

        // Assert
        Assert.Single(result);
        Assert.Contains(result, c => c.Number == 101);
    }

    [Fact]
    public async Task SearchCamerasAsync_WithLatitudeMatch_ReturnsCamera()
    {
        // Arrange
        var cameraList = new List<Camera>
        {
            new Camera { Name = "UTR-CM-1021 Test Cam", Lat = 52.333, Lon = 5.1},
        };

        _csvDataServiceMock
            .Setup(s => s.ExtractCameras(It.IsAny<string>()))
            .ReturnsAsync(cameraList);

        // Act
        var result = await _service.SearchCamerasAsync("52,333");

        // Assert
        Assert.Single(result);
    }

    [Fact]
    public async Task GetAllCamerasAsync_CachesResult_AfterFirstCall()
    {
        // Arrange
        var cameraList = new List<Camera>
        {
            new Camera { Name = "UTR-CM-101-Neude", Lat = 52.1, Lon = 5.1 },
        };

        _csvDataServiceMock
            .Setup(s => s.ExtractCameras(It.IsAny<string>()))
            .ReturnsAsync(cameraList)
            .Verifiable();

        // Act
        var firstCall = await _service.GetAllCamerasAsync();
        var secondCall = await _service.GetAllCamerasAsync();

        // Assert
        _csvDataServiceMock.Verify(s => s.ExtractCameras(It.IsAny<string>()), Times.Once);
        Assert.Single(secondCall);
    }
}