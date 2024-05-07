using FluentResults;
using IntersectionFinder.Application.Models;
using IntersectionFinder.Application.Repositories;

namespace IntersectionFinder.Application.Services;

public interface IGeometricService
{
    Task<Result<IEnumerable<PoligonModel>>> FindIntersections(SegmentModel segment);
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

    public async Task<Result<IEnumerable<PoligonModel>>> FindIntersections(SegmentModel segment)
    {
        var validationResult = _segmentValidationService.ValidateSegment(segment);
        if (validationResult.IsFailed)
        {
            return validationResult;
        }

        var list = await _rectangleRepository.GetRectanglesWithIntersections(segment);

        return Result.Ok(list.Select(x => _binderService.ConvertToViewModel(x)));
    }
}
