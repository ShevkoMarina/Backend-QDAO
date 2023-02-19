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
                var web3 = new Web3("https://eth-goerli.g.alchemy.com/v2/PU1jr72jAHmucb_oUHObuiwoCCsdtODL");

                var contractAddress = "0x2980343ce6E94aA17c5499139AB3532D98095321"; // Governor 

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
