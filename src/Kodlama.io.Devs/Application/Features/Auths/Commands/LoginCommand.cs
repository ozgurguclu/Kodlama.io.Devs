using Application.Features.Auths.Dtos;
using Application.Features.Auths.Rules;
using Application.Services.AuthService;
using Application.Services.Repositories;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.JWT;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auths.Commands
{
    public class LoginCommand : IRequest<LoginDto>
    {
        public UserForLoginDto UserForLoginDto { get; set; }
        public string IpAddress { get; set; }

        public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginDto>
        {
            private readonly AuthBusinessRules _authBusinessRules;
            private readonly IUserRepository _userRepository;
            private readonly IAuthService _authService;

            public LoginCommandHandler(AuthBusinessRules authBusinessRules, IUserRepository userRepository, IAuthService authService)
            {
                _authBusinessRules = authBusinessRules;
                _userRepository = userRepository;
                _authService = authService;
            }

            public async Task<LoginDto> Handle(LoginCommand request, CancellationToken cancellationToken)
            {
                await _authBusinessRules.EmailShouldBeExistWhenUserTriedToLogin(request.UserForLoginDto.Email);

                User? user = await _userRepository.GetAsync(u => u.Email.ToLower() == request.UserForLoginDto.Email.ToLower());

                await _authBusinessRules.PasswordShouldBeValidWhenUserTriedToLogin(request.UserForLoginDto.Password, user.PasswordHash, user.PasswordSalt);

                AccessToken createAccessToken = await _authService.CreateAccessToken(user);
                RefreshToken createRefreshToken = await _authService.CreateRefreshToken(user, request.IpAddress);
                RefreshToken addedRefreshToken = await _authService.AddRefreshToken(createRefreshToken);

                LoginDto loginDto = new()
                {
                    RefreshToken = addedRefreshToken,
                    AccessToken = createAccessToken
                };

                return loginDto;
            }
        }
    }
}
