using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace ProductWebAPI.Services
{
    public interface IHealthCheck
    {
        Task WriteAsync(HttpContext httpContext, HealthReport result);
    }
}

