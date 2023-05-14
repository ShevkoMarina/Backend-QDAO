using MediatR;
using QDAO.Domain;
using QDAO.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace QDAO.Application.Handlers.Proposal
{
    public static class GetProposalInfoById
    {
        public record Request(long ProposalId) : IRequest<ProposalInfoDto>;

        public sealed class Handler : IRequestHandler<Request, ProposalInfoDto>
        {
            private readonly IDapperExecutor _database;

            public Handler(IDapperExecutor database)
            {
                _database = database;
            }

            public async Task<ProposalInfoDto> Handle(Request request, CancellationToken ct)
            {
                var result = await _database.QuerySingleOrDefaultAsync<ProposalInfoDto>(
                    GetVotingProposalInfo,
                    ct,
                    new
                    {
                        proposal_id = request.ProposalId
                    });


                return result;
            }

            private const string GetVotingProposalInfo = @"--GetVotingProposalInfo
                                                            select 
                                                            p.id as Id,
                                                            p.name as Name,
                                                            p.description as Description,
                                                            v.votes_for as VotesFor,
                                                            v.votes_against as VotesAgainst,
                                                            state as State
                                                            from proposal p
                                                            join voting v on p.id = v.proposal_id
                                                            join lateral (
                                                                select state
                                                                from proposal_state_log psl
                                                                where psl.proposal_id = p.id
                                                                order by psl.created_at DESC NULLS LAST
                                                                limit 1
                                                                ) AS proposal_state on true
                                                            where p.id = @proposal_id";
        }
    }


    public record ProposalInfoDto(
        long Id, 
        string Name,
        string Description, 
        long VotesFor,
        long VotesAgainst,
        ProposalState State);
}
