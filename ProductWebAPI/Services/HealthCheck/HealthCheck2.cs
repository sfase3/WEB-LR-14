using Microsoft.Extensions.Diagnostics.HealthChecks;


    public class HealthCheck2 : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
        {

            bool isHealthy = true;

            if (isHealthy)
            {
                return Task.FromResult(HealthCheckResult.Healthy("Service is healthy."));
            }
            else
            {
                return Task.FromResult(HealthCheckResult.Unhealthy("Service is unhealthy."));
            }
        }
    }
