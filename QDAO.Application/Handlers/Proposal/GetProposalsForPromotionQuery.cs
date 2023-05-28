using MediatR;
using QDAO.Domain;
using QDAO.Persistence;
using QDAO.Persistence.Repositories.Proposal;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace QDAO.Application.Handlers.Proposal
{
    public static class GetProposalsForPromotionQuery
    {
        public record Request() : IRequest<IReadOnlyCollection<ProposalThin>>;

        public sealed class Handler : IRequestHandler<Request, IReadOnlyCollection<ProposalThin>>
        {
            private static IDapperExecutor _database;

            public Handler(IDapperExecutor database)
            {
                _database = database;
            }

            public async Task<IReadOnlyCollection<ProposalThin>> Handle(Request request, CancellationToken cancellationToken)
            {
                var proposals = await _database.QueryAsync<ProposalThin>(GetProposalsForPromotion, cancellationToken);

                return proposals;
            }

            private const string GetProposalsForPromotion = @"--GetProposalsForPromotion
                                                                select id,
                                                                name,
                                                                description,
                                                                state
                                                                from proposal p
                                                                join lateral (
                                                                    select state, created_at
                                                                    from proposal_state_log psl
                                                                    where psl.proposal_id = p.id
                                                                    order by psl.created_at DESC NULLS LAST
                                                                    limit 1
                                                                    ) AS proposal_state on true
                                                                where state in (4, 5, 6, 7);";
        }
    }
}
