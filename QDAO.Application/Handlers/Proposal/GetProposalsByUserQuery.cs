using MediatR;
using QDAO.Domain;
using QDAO.Persistence.Repositories.Proposal;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace QDAO.Application.Handlers.Proposal
{
    public class GetProposalsByUserQuery
    {
        public record Request(int UserId) : IRequest<Response>;
        public record Response(IReadOnlyCollection<ProposalThin> Proposals);

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly ProposalRepository _proposalRepository;


            public Handler(ProposalRepository proposalRepository)
            {
                _proposalRepository = proposalRepository;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var proposals = await _proposalRepository.GetProposalsByProposerId(request.UserId, cancellationToken);

                var orderedByIdProposals = proposals.OrderBy(p => p.Id).ToList();

                return new Response(orderedByIdProposals);
            }
        }
    }
}
