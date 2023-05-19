using MediatR;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using QDAO.Application.Services;
using QDAO.Persistence.Repositories.User;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;

namespace QDAO.Application.Handlers.Token
{
    public class GetBalanceQuery
    {
        public record Request(int UserId) : IRequest<Response>;
        public record Response(long Balance, long Votes);


        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly ContractsManager _contractManager;
            private readonly UserRepository _userRepository;


            public Handler(ContractsManager contractsManager, UserRepository userRepository)
            {
                _contractManager = contractsManager;
                _userRepository = userRepository;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var userAccount = await _userRepository.GetUserAccountById(request.UserId, cancellationToken);

                var getBalanceMessage = new GetBalanceMessage
                {
                    Account = userAccount
                };
                var getBalanceHandler = _contractManager.Web3.Eth.GetContractQueryHandler<GetBalanceMessage>();
                long balance = await getBalanceHandler.QueryAsync<long>(_contractManager.GetTokenAddress(), getBalanceMessage);

                var getVotesMessage = new GetVotesMessage
                {
                    Account = userAccount
                };
                var getVotesHandler = _contractManager.Web3.Eth.GetContractQueryHandler<GetVotesMessage>();
                var votes = await getVotesHandler.QueryAsync<long>(_contractManager.GetTokenAddress(), getVotesMessage);


                return new Response(balance, votes);
            }
        }
    }

    [Function("balanceOf", "uint256")]
    public class GetBalanceMessage : FunctionMessage
    {
        [Parameter("address", "account", 1)]
        public string Account { get; set; }
    }

    [Function("getCurrentVotes", "uint256")]
    public class GetVotesMessage : FunctionMessage
    {
        [Parameter("address", "account", 1)]
        public string Account { get; set; }
    }
}
