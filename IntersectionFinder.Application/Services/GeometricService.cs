using FluentResults;
using IntersectionFinder.Application.Models;
using IntersectionFinder.Application.Repositories;
using NetTopologySuite.Geometries;

namespace IntersectionFinder.Application.Services;

public interface IGeometricService
{
    Task<Result<IEnumerable<PoligonModel>>> FindIntersectionsAsync(SegmentModel segment);
    Task<Result<IEnumerable<PoligonModel>>> FindGeometricIntersectionsAsync(SegmentModel segment);
}

public class GeometricService : IGeometricService
{
    private readonly IBinderService _binderService;
    private readonly IRectangleRepository _rectangleRepository;
    private readonly ISegmentValidationService _segmentValidationService;
    
    public GeometricService(
        IBinderService binderService,
        ISegmentValidationService segmentValidationService,
        IRectangleRepository rectangleRepository)
    {
        _binderService = binderService;
        _segmentValidationService = segmentValidationService;
        _rectangleRepository = rectangleRepository;
    }

    public async Task<Result<IEnumerable<PoligonModel>>> FindIntersectionsAsync(SegmentModel segment)
    {
        var validationResult = _segmentValidationService.ValidateSegment(segment);
        if (validationResult.IsFailed)
        {
            return validationResult;
        }

        var list = await _rectangleRepository.GetRectanglesWithIntersectionsAsync(segment);

        return Result.Ok(list.Select(x => _binderService.ConvertToViewModel(x)));
    }

    public async Task<Result<IEnumerable<PoligonModel>>> FindGeometricIntersectionsAsync(SegmentModel segment)
    {
        var validationResult = _segmentValidationService.ValidateSegment(segment);
        if (validationResult.IsFailed)
        {
            return validationResult;
        }

        LineString line = new LineString(
            new[] { new Coordinate(segment.StartX, segment.StartY), new Coordinate(segment.EndX, segment.EndY) });

        var list = await _rectangleRepository.GetRectanglesWithIntersectionsAlternativeMethodAsync(line);

        return Result.Ok(list.Select(x => _binderService.ConvertToCustomeModel(x)));
    }
}
