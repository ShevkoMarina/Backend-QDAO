using MediatR;
using QDAO.Domain;
using QDAO.Persistence;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace QDAO.Application.Handlers.Proposal
{
    public static class GetProposalsActiveForVotingQuery
    {
        public record Request() : IRequest<IReadOnlyCollection<ProposalThin>>;

        public sealed class Handler : IRequestHandler<Request, IReadOnlyCollection<ProposalThin>>
        {
            private readonly IDapperExecutor _database;

            public Handler(IDapperExecutor database)
            {
                _database = database;
            }

            public Task<IReadOnlyCollection<ProposalThin>> Handle(Request request, CancellationToken cancellationToken)
            {
                var result = _database.QueryAsync<ProposalThin>(
                    GetProposalsActiveForVoting,
                    cancellationToken);

                return result;
            }


            private const string GetProposalsActiveForVoting = @"--GetProposalsActiveForVoting
                                                                    select 
                                                                    id,
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
                                                                    where proposal_state.state = 1;";
        }
    }
}
