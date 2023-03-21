using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace QDAO.Endpoint.HostedServices
{
    public class QuorumCrisisBackgroundService : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Yield();
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {

                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}
