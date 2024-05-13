using Microsoft.AspNetCore.SignalR;

namespace ProductWebAPI.Services.Background
{
    public class NoticeService : BackgroundService
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public NoticeService(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await _hubContext.Clients.All.SendAsync("ReceiveMessage", "Служба сповіщень", $"Час: {DateTime.Now}");
                await Task.Delay(TimeSpan.FromSeconds(20), stoppingToken);
            }
        }
    }
}
