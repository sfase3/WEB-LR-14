using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;


public class HealthCheck: ProductWebAPI.Services.IHealthCheck
{
        public Task WriteAsync(HttpContext httpContext, HealthReport result)
        {
            httpContext.Response.ContentType = "application/json";

            var response = new
            {
                Status = result.Status.ToString(),
                TotalChecks = result.Entries.Count,
                Results = result.Entries
            };
            var jsonResponse = JsonConvert.SerializeObject(response);
            return httpContext.Response.WriteAsync(jsonResponse);
        }
    }
