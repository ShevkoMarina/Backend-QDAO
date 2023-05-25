using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nethereum.Web3;
using QDAO.Application.Handlers.Proposal;
using QDAO.Application.Pipelines;
using QDAO.Application.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace QDAO.Endpoint.HostedServices
{
    public class ProposalReadyForVotingBgService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public ProposalReadyForVotingBgService(IServiceProvider serviceProvider)
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
                    var pipeline = scope.ServiceProvider.GetRequiredService<ProposalStateTransitionPipeline>();

                    await pipeline.PipeAsync(stoppingToken);
                }                
                catch (Exception ex)
                {

                }
            }
        }
    }
}
