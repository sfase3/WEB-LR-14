namespace ProductWebAPI.Services.Background
{
    using Microsoft.Extensions.Hosting;
    using System;
    using System.IO;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    public class Background1 : BackgroundService
    {
        private readonly HttpClient _httpClient;
        private readonly string[] _webPages = { "https://www.atbmarket.com"};
        private readonly string _logFilePath = "file_background.log";

        public Background1(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                foreach (var page in _webPages)
                {
                    try
                    {
                        var response = await _httpClient.GetAsync(page, stoppingToken);
                        var isSuccessStatusCode = response.IsSuccessStatusCode;
                        var logMessage = $"{DateTime.UtcNow}: {page} {(isSuccessStatusCode ? "is reachable" : "is not reachable")}";
                        Result(logMessage);
                    }
                    catch (Exception ex)
                    {
                        Result($"{DateTime.UtcNow}: Error occurred while checking {page}: {ex.Message}");
                    }
                }

                await Task.Delay(TimeSpan.FromMinutes(10), stoppingToken);
            }
        }

        private void Result(string message)
        {
            try
            {
                using (var writer = File.AppendText(_logFilePath))
                {
                    writer.WriteLine(message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while writing to log file: {ex.Message}");
            }
        }
    }
}
