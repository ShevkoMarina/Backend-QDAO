using MediatR;
using QDAO.Application.GrpcClients;
using System.Threading;
using System.Threading.Tasks;

namespace QDAO.Application.Handlers.Admin
{
    public class InitializeGovernorCommand
    {
        public record Request() : IRequest<Response>;

        public record Response();

        public class Handler : IRequestHandler<Request, Response>
        {
            private AdminGrpcClient _adminGrpcClient;

            public Handler(AdminGrpcClient adminGrpcClient)
            {
                _adminGrpcClient = adminGrpcClient;
            }

            public async Task<Response> Handle(Request request, CancellationToken ct)
            {
                await _adminGrpcClient.InitializeGovernor();
                return new Response();
            }
        }
    }
}
