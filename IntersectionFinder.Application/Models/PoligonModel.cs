namespace IntersectionFinder.Application.Models;

public record PoligonModel
{
    public string Name { get; set; } = string.Empty;
    public ICollection<SegmentModel> Segments { get; set; } = new List<SegmentModel>();
}
