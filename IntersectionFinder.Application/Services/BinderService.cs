using IntersectionFinder.Application.Models;
using IntersectionFinder.Domain.Entities;
using NetTopologySuite.Geometries;

namespace IntersectionFinder.Application.Services;

public interface IBinderService
{
    PoligonModel ConvertToViewModel(Rectangle rectangle);
    PoligonModel ConvertToCustomModel(Rectangle rectangle);
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

    public PoligonModel ConvertToCustomModel(Rectangle rectangle)
    {
        var polygon = rectangle.Geometry as Polygon;
        var segments = GetSegments(polygon);

        return new PoligonModel()
        {
            Name = rectangle.RectangleName,
            Segments = segments
        };
    }

    private static IList<SegmentModel> GetSegments(Polygon? polygon)
    {
        var segments = new List<SegmentModel>();
        if (polygon == null)
        { 
            return segments;
        }

        var coordinates = polygon.ExteriorRing.Coordinates;
        for (int i = 0; i < coordinates.Length - 1; i++)
        {
            segments.Add(new SegmentModel
            {
                StartX = coordinates[i].X,
                StartY = coordinates[i].Y,
                EndX = coordinates[i + 1].X,
                EndY = coordinates[i + 1].Y
            });
        }
        
        return segments;
    }
}
