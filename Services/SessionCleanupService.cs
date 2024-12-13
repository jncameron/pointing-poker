using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using PointingPoker.Data;

namespace PointingPoker.Services
{
  public class SessionCleanupService : BackgroundService
  {
    private readonly IServiceProvider _serviceProvider;

    public SessionCleanupService(IServiceProvider serviceProvider)
    {
      _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
      while (!stoppingToken.IsCancellationRequested)
      {
        using (var scope = _serviceProvider.CreateScope())
        {
          var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
          var expiredSessions = context.Sessions
              .Where(s => s.CreatedAt.AddMinutes(30) < DateTime.UtcNow && s.Status == "Active")
              .ToList();

          foreach (var session in expiredSessions)
          {
            session.Status = "Expired";
          }

          await context.SaveChangesAsync();
        }

        await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken); // Check every 5 minutes
      }
    }
  }
}
