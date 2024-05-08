using IntersectionFinder.Application.Models;
using IntersectionFinder.Domain.Entities;
using IntersectionFinder.Infrastructure;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using System.Data.Entity.Spatial;

namespace IntersectionFinder.Application.Repositories;

public interface IRectangleRepository
{
    Task<IList<Rectangle>> GetRectanglesWithIntersectionsAsync(SegmentModel segment);
    Task<IList<Rectangle>> GetRectanglesWithIntersectionsAlternativeMethodAsync(Geometry segment);
}

public class RectangleRepository : IRectangleRepository
{
    private readonly IntersectionsContext _context;

    public RectangleRepository(IntersectionsContext context)
    {
        _context = context;
    }

    public async Task<IList<Rectangle>> GetRectanglesWithIntersectionsAsync(SegmentModel segment)
    {
        var query = from rectangle in _context.Rectangles.Include(x => x.RectangleSegments)
                    where rectangle.RectangleSegments.Any(x =>
                        IntersectionsContext.CheckSegmentIntersection(
                            segment.StartX,
                            segment.StartY,
                            segment.EndX,
                            segment.EndY,
                            x.StartX,
                            x.StartY,
                            x.EndX,
                            x.EndY
                            )
                        )
                    select rectangle;

        return await query.ToListAsync();
    }

    public async Task<IList<Rectangle>> GetRectanglesWithIntersectionsAlternativeMethodAsync(Geometry segment)
    {
        var query = from rectangle in _context.Rectangles
                    where
                        rectangle.Geometry != null
                        && rectangle.Geometry.Intersects(segment)
                    select rectangle;
        
        return await query.ToListAsync();
    }
}
