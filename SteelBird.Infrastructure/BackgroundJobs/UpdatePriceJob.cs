using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SteelBird.Infrastructure.BackgroundJobs;

public class UpdatePriceJob : BackgroundService
{
    private readonly ILogger<UpdatePriceJob> _logger;
    private readonly IServiceProvider _serviceProvider;
    public UpdatePriceJob(
        ILogger<UpdatePriceJob> logger,
        IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        //Business Logic
        while (!stoppingToken.IsCancellationRequested)
        {
            // Wait with cancellation support
            await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
        }
    }
}
