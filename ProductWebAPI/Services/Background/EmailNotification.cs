
using Microsoft.EntityFrameworkCore;
using ProductWebAPI.Models;
using ProductWebAPI.Services.Background;

public class EmailNotification : BackgroundService
{
    private readonly ILogger<EmailNotification> _logger;
    private readonly HealthCheckDB _dbContext;
    private readonly EmailService _emailService;

    public EmailNotification(ILogger<EmailNotification> logger, HealthCheckDB dbContext, EmailService emailService)
    {
        _logger = logger;
        _dbContext = dbContext;
        _emailService = emailService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("EmailNotificationService is starting.");

        stoppingToken.Register(() =>
            _logger.LogInformation("EmailNotificationService is stopping."));

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                foreach (var entry in _dbContext.ChangeTracker.Entries<HealthCheckDate>())
                {
                    if (entry.State == EntityState.Added)
                    {
                        await _emailService.SendEmailAsync("maksimkamychko@gmail.com", "Новий запис додано", "Текст повідомлення про новий запис.");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing database changes.");
            }

            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
        }
    }
}
