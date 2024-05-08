using IntersectionFinder.API.Extensions;
using IntersectionFinder.Application.Models;
using IntersectionFinder.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace IntersectionFinder.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IntersectionController : ControllerBase
    {
        private readonly IGeometricService _geometricService;

        public IntersectionController(IGeometricService geometricService)
        {
            _geometricService = geometricService;
        }

        [HttpGet("Geometric")]
        public async Task<IActionResult> Get([FromQuery] SegmentModel segment)
        {
            var result = await _geometricService.FindIntersectionsAsync(segment);

            return result.ToActionResult();
        }

        [HttpGet("CustomeGeometric")]
        public async Task<IActionResult> GetIntersections([FromQuery] SegmentModel segment)
        {
            var result = await _geometricService.FindGeometricIntersectionsAsync(segment);

            return result.ToActionResult();
        }
    }
}
