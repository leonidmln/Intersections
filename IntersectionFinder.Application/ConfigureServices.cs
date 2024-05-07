using IntersectionFinder.Application.Models;
using IntersectionFinder.Application.Repositories;
using IntersectionFinder.Application.Services;
using IntersectionFinder.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IntersectionFinder.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(
      this IServiceCollection services,
      IConfiguration configuration)
    {
        var intersectionsConnectionString = configuration["ConnectionStrings:Intersections"];

        services.AddDbContext<IntersectionsContext>(
            cfg =>
            cfg.UseSqlServer(intersectionsConnectionString), 
            ServiceLifetime.Scoped, ServiceLifetime.Singleton);

        services.AddScoped<IBinderService, BinderService>();
        services.AddScoped<IGeometricService, GeometricService>();
        services.AddScoped<ISegmentValidationService, SegmentValidationService>();
        services.AddScoped<IRectangleRepository, RectangleRepository>();
        return services;
    }
}
