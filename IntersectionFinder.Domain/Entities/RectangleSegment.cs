#nullable disable
using System.ComponentModel.DataAnnotations;

namespace IntersectionFinder.Domain.Entities;

public class RectangleSegment
{
    [Key]
    public int RectangleSegmentId { get; set; }
    public int RectangleId { get; set; }
    public double StartX { get; set; }
    public double StartY { get; set; }
    public double EndX { get; set; }
    public double EndY { get; set; }
    public virtual Rectangle Rectangle { get; set; }
}
