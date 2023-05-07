using MediatR;
using QDAO.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace QDAO.Application.Handlers.Proposal
{
    public static class GetVotingProposalInfoQuery
    {
        public record Request(long ProposalId) : IRequest<VotingProposalInfo>;


        public sealed class Handler : IRequestHandler<Request, VotingProposalInfo>
        {
            private readonly IDapperExecutor _database;

            public Handler(IDapperExecutor database)
            {
                _database = database;

            }

            public async Task<VotingProposalInfo> Handle(Request request, CancellationToken ct)
            {
                var result = await _database.QuerySingleOrDefaultAsync<VotingProposalInfo>(
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
                                                            v.votes_against as VotesAgainst
                                                            from proposal p
                                                            join voting v on p.id = v.proposal_id
                                                            where p.id = @proposal_id
                                                            limit 1;";
        }
    }


    public record VotingProposalInfo(
        long Id, 
        string Name,
        string Description, 
        long VotesFor,
        long VotesAgainst);
}
