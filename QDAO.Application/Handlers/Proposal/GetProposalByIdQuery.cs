using MediatR;
using QDAO.Application.GrpcClients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QDAO.Application.Handlers.Proposal
{
    public class GetProposalByIdQuery
    {
        public record Request(long ProposalId) : IRequest<Response>;

        public record Response(Domain.Proposal Proposal);

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly ProposalGrpcClient _proposalGrpcClient;

            public Handler(ProposalGrpcClient proposalGrpcClient)
            {
                _proposalGrpcClient = proposalGrpcClient;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var proposalInfo = await _proposalGrpcClient.GetProposalById(request.ProposalId);

                // дообогащаем через базу
                /*
                var proposal = new Domain.Proposal
                {
                    Proposer = proposalInfo.Proposer,
                    Id = (uint)proposalInfo.Id,
                    StartBlock = (uint)proposalInfo.StartBlock,
                    EndBlock = (uint)proposalInfo.EndBlock,
                    Name = "test",
                    Description = "test"
                };
                */
                var proposal = new Domain.Proposal();

                return new Response(proposal);
            }
        }
    }
}
