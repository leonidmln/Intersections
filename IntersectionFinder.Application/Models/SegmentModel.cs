namespace IntersectionFinder.Application.Models;

public record SegmentModel
{
    public double StartX { get; set; } 
    public double StartY { get; set; }
    public double EndX { get; set; } 
    public double EndY { get; set; }
}
