namespace IntersectionFinder.DTO;

public record SegmentDto
{
    public double StartX { get; set; } 
    public double StartY { get; set; }
    public double EndX { get; set; } 
    public double EndY { get; set; }
}
