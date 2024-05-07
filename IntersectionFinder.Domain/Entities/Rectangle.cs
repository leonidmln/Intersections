using System.ComponentModel.DataAnnotations;

namespace IntersectionFinder.Domain.Entities;

public class Rectangle
{
    [Key]
    public int RectangleId { get; set; }
    public string RectangleName { get; set; } = string.Empty;
    public virtual ICollection<RectangleSegment> RectangleSegments { get; set; } = new List<RectangleSegment>();
}
