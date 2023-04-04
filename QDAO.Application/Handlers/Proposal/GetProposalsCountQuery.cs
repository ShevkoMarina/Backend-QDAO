using MediatR;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QDAO.Application.Handlers.Proposal
{
    public static class GetProposalsCountQuery
    {
        public record Request() : IRequest<Response>;

        public record Response(BigInteger ProposalsCount);

        public class Handler : IRequestHandler<Request, Response>
        {


            public async Task<Response> Handle(Request request, CancellationToken ct)
            {
                var web3 = new Web3("HTTP://127.0.0.1:8545");

                var contractAddress = "0x25B9a573399CF9D1E50fcdE89aB8782271531CeE"; // delegator 

                var message = new GetProposalsCountMessage();

                var handler = web3.Eth.GetContractQueryHandler<GetProposalsCountMessage>();

                var count = await handler.QueryAsync<BigInteger>(contractAddress, message);

                return new Response(count);
              
            }


            [Function("proposalCount", "uint256")]
            public class GetProposalsCountMessage : FunctionMessage
            {

            }
        }
    }
}
