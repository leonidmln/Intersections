namespace IntersectionFinder.Application.Models;
using FluentResults;

public interface ISegmentValidationService
{
    Result ValidateSegment(SegmentModel segmentModel);
}

public class SegmentValidationService : ISegmentValidationService
{
    public SegmentValidationService() { }

    public Result ValidateSegment(SegmentModel segmentModel)
    {
        if (segmentModel.StartX == segmentModel.EndX
            && segmentModel.StartY == segmentModel.EndY)
        {
            return Result.Fail(new Error("The segment has collapsed to a point."));
        }

        return Result.Ok();
    }
}
