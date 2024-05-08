using IntersectionFinder.Application.Models;
using IntersectionFinder.Application.Repositories;
using IntersectionFinder.Application.Services;
using IntersectionFinder.Domain.Entities;
using Moq;

namespace IntersectionFinder.UnitTests;

[TestFixture]
public class GeometricServiceTests
{
    private readonly IBinderService _binderService = new BinderService();
    private readonly Mock<IRectangleRepository> _mockRectangleRepository = new();
    private readonly ISegmentValidationService _segmentValidationService = new SegmentValidationService();
    private GeometricService _geometricService;

    [SetUp]
    public void Setup()
    {
        _geometricService = new GeometricService(_binderService, _segmentValidationService, _mockRectangleRepository.Object);
    }

    [Test]
    public async Task FindIntersectionsAsync_SegmentValidationFails_ReturnsFailedResult()
    {
        // Arrange
        const string errorMessage = "The segment has collapsed to a point.";
        var segment = new SegmentModel()
        {
            StartX = 0,
            StartY = 5,
            EndX = 0,
            EndY = 5,
        };
        
        // Act
        var result = await _geometricService.FindIntersectionsAsync(segment);

        // Assert
        Assert.True(result.IsFailed);
        Assert.True(result.Errors.Any(x => x.Message == errorMessage));
    }

    [Test]
    public async Task FindIntersectionsAsync_SegmentIsValid_ReturnsSuccessResult()
    {
        // Arrange
        const string rectangleName = "Rectangle 1";
        var segment = new SegmentModel()
        {
            StartX = 0,
            StartY = 5,
            EndX = 10,
            EndY = 15,
        };

        var searchResult = new List<Rectangle>() { 
            new Rectangle
            {
                RectangleName = rectangleName
            }
        };

        _mockRectangleRepository.Setup(x => x.GetRectanglesWithIntersectionsAsync(segment))
            .ReturnsAsync(searchResult);


        // Act
        var result = await _geometricService.FindIntersectionsAsync(segment);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Greater(searchResult.Count, 0);
        Assert.That(searchResult[0].RectangleName, Is.EqualTo(rectangleName));
    }
}