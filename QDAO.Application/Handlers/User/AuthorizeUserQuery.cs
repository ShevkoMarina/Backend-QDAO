﻿using MediatR;
using QDAO.Application.Services;
using QDAO.Persistence;
using QDAO.Persistence.Repositories.User;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace QDAO.Application.Handlers.User
{
    public static class AuthorizeUserQuery
    {
        public record Request(string Login, string Password) : IRequest<Response>;

        public record Response(int Id, short Role, string Token, string Account);

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IDapperExecutor _database;
            private readonly SecurityService _securityService;

            public Handler(IDapperExecutor database, SecurityService securityService)
            {
                _database = database;
                _securityService = securityService;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var user = await _database.QuerySingleOrDefaultAsync<UserInfoDto>(
                GetByLoginAndPassword,
                cancellationToken,
                new
                {
                    login = request.Login,
                    password = request.Password
                });

                if (user == default)
                {
                    throw new ArgumentException("Неверные логин или пароль.");
                }

               
               var token = await _securityService.GetAccessToken(user.Id, user.Role, cancellationToken);

               return new Response(user.Id, user.Role, token, user.Account);
            }

            private const string GetByLoginAndPassword = @"--UserSql.GetByLoginAndPassword
                                    select 
                                    id,
                                    account,
                                    role
                                    from users where login = @login and password = @password;";
        }
    }
}