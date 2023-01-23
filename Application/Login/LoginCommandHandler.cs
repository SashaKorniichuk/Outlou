using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;
using Microsoft.AspNet.Identity;
using Outlou.Application.Abstractions;
using Outlou.Application.Abstractions.Messaging;

namespace Outlou.Application.Login;

internal sealed class LoginCommandHandler : ICommandHandler<LoginCommand, string>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtProvider _jwtProvider;

    public LoginCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, IJwtProvider jwtProvider)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtProvider = jwtProvider;
    }

    public async Task<Result<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByEmailAsync(request.Email, cancellationToken);

        if (user is null)
        {
            return Result.Failure<string>(DomainErrors.User.InvalidCredentials);
        }

        if (_passwordHasher.VerifyHashedPassword(user.Password, request.Password) == PasswordVerificationResult.Failed)
        {
            return Result.Failure<string>(DomainErrors.User.InvalidCredentials);
        }

        var token = _jwtProvider.Generate(user);

        return token;
    }
}