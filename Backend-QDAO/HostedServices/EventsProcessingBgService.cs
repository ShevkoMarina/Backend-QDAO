using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QDAO.Application.Pipelines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace QDAO.Endpoint.HostedServices
{
    public class EventsProcessingBgService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public EventsProcessingBgService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Yield();

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _serviceProvider.CreateScope();
                    var pipeline = scope.ServiceProvider.GetRequiredService<EventsProcessingPipeline>();

                    await pipeline.PipeAsync(stoppingToken);
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}
