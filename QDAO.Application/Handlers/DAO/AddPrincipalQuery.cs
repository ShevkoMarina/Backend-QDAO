using MediatR;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using QDAO.Application.Services;
using QDAO.Domain;
using QDAO.Persistence;
using QDAO.Persistence.Repositories.User;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace QDAO.Application.Handlers.DAO
{
    public class AddPrincipalQuery
    {
        public record Request(int UserId, string PrincipalLogin, short RequiredApprovals) : IRequest<Response>;

        public record Response(RawTransaction Transaction);

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IDapperExecutor _database;
            private readonly TransactionCreator _transactionService;
            private readonly UserRepository _userRepository;

            public Handler(TransactionCreator transactionService, IDapperExecutor database, UserRepository userRepository)
            {
                _transactionService = transactionService;
                _database = database;
                _userRepository = userRepository;

            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var senderAddress = await _userRepository.GetUserAccountById(request.UserId, cancellationToken);

                var userAddress = await _database.QuerySingleOrDefaultAsync<string>(
                    GetUserByLogin,
                    cancellationToken,
                    new
                    {
                        login = request.PrincipalLogin
                    });

                if (userAddress == default)
                {
                    throw new ArgumentException("Пользователь не найден");
                }

                var txMessage = new AddPrincipalsTransaction
                {
                    PrincipalAccount = userAddress,
                    RequiredApprovals = request.RequiredApprovals
                };

                var dataHex = Nethereum.Hex.HexConvertors.Extensions.HexByteConvertorExtensions.ToHex(txMessage.GetCallData());

                var rawTx = await _transactionService.GetDefaultRawTransaction(senderAddress);

                rawTx.Data = dataHex;

                return new Response(rawTx);
            }

            private const string GetUserByLogin = @"--GetUserByLogin
                                                    select account from users where login = @login;";

        }

        [Function("addPrincipal")]
        public class AddPrincipalsTransaction : FunctionMessage
        {
            [Parameter("address", "_principal", 1)]
            public string PrincipalAccount { get; set; }

            [Parameter("uint8", "_requiredApprovals", 2)]
            public short RequiredApprovals { get; set; }
        }
    }
}
