using IntersectionFinder.Application.Models;
using IntersectionFinder.Domain.Entities;

namespace IntersectionFinder.Application.Services;

public interface IBinderService
{
    PoligonModel ConvertToViewModel(Rectangle rectangle);
}

public class BinderService: IBinderService
{
//    public BinderService() { }

    public PoligonModel ConvertToViewModel(Rectangle rectangle)
    {
        return new PoligonModel()
        {
            Name = rectangle.RectangleName,
            Segments = rectangle.RectangleSegments.Select((x) =>
                new SegmentModel
                {
                    StartX = x.StartX,
                    StartY = x.StartY,
                    EndX = x.EndX,
                    EndY = x.EndY,
                }
            ).ToList()
        };
    }
}
