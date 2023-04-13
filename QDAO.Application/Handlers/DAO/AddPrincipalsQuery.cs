using MediatR;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using QDAO.Application.Services;
using QDAO.Domain;
using QDAO.Persistence.Repositories.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace QDAO.Application.Handlers.DAO
{
    public class AddPrincipalsQuery
    {
        public record Request(int[] UserIds, short RequiredApprovals) : IRequest<Response>;

        public record Response(RawTransaction Transaction);

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly UserRepository _userRepository;
            private readonly TransactionCreator _transactionService;

            public Handler(UserRepository userRepository)
            {
                _userRepository = userRepository;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                // бля из приложения надо по логину или по адресу делать запрос а не идентификатору
                var users = await _userRepository.GetUsers(request.UserIds, cancellationToken);

                var userAddresses = users.Select(user => user.Address);

                var txMessage = new AddPrincipalsTransaction
                {
                    Signers = userAddresses.ToList(),
                    RequiredApprovals = request.RequiredApprovals
                };

                var dataHex = Nethereum.Hex.HexConvertors.Extensions.HexByteConvertorExtensions.ToHex(txMessage.GetCallData());

                var rawTx = await _transactionService.GetDefaultRawTransaction("0x618E0fFEe21406f493D22f9163c48E2D036de6B0");

                rawTx.Data = dataHex;

                return new Response(rawTx);
            }
        }

        [Function("createMultisig")]
        public class AddPrincipalsTransaction : FunctionMessage
        {
            [Parameter("address[]", "signers", 1)]
            public List<string> Signers { get; set; }

            [Parameter("uint8", "requiredApprovals", 2)]
            public short RequiredApprovals { get; set; }
        }
    }
}
