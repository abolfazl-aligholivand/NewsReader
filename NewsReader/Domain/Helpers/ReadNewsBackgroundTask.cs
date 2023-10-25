using MediatR;
using NewsReader.Domain.Data;
using NewsReader.Domain.Repository.IRepository;
using ReadNews.Features.NewsReader.Commands;

namespace NewsReader.Domain.Helpers
{
    public class ReadNewsBackgroundTask : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly TimeSpan _period = TimeSpan.FromMinutes(5);
        public ReadNewsBackgroundTask(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            PeriodicTimer timer = new PeriodicTimer(_period);

            while (!stoppingToken.IsCancellationRequested &&
               await timer.WaitForNextTickAsync(stoppingToken))
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    try
                    {
                        var _mediator = scope.ServiceProvider.GetService<IMediator>();
                        await _mediator.Send(new ReadNewsCommand());
                    }
                    catch (Exception ex)
                    {
                        var _logger = scope.ServiceProvider.GetService<ILogger>();
                        _logger.LogError(ex.Message);
                    }
                }
            }
        }
    }
}
