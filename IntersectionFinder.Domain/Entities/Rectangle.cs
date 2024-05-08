using NetTopologySuite.Geometries;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Spatial;

namespace IntersectionFinder.Domain.Entities;

public class Rectangle
{
    [Key]
    public int RectangleId { get; set; }
    public string RectangleName { get; set; } = string.Empty;
    public virtual ICollection<RectangleSegment> RectangleSegments { get; set; } = new List<RectangleSegment>();
    public Geometry? Geometry { get; set; }
}
