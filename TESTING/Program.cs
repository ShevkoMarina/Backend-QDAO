using MediatR;
using QDAO.Application.Handlers.Admin;
using System.Threading;
using System.Threading.Tasks;

namespace TESTING
{
    class Program
    {
        private static IMediator _mediator;

        public Program(Mediator mediator)
        {
            _mediator = mediator;
        }

        static async Task Main(string[] args)
        {
            var initAdminQuery = new AddAdminUser.Request();

            var response = await _mediator.Send(initAdminQuery, CancellationToken.None);

        }
    }
}
