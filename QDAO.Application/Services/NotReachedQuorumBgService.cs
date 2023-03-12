using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace QDAO.Application.Services
{
    public class NotReachedQuorumBgService : BackgroundService
    {
        protected override Task ExecuteAsync(CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
