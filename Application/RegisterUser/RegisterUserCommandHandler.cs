using Domain.Entities;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;
using Microsoft.AspNet.Identity;
using Outlou.Application.Abstractions;
using Outlou.Application.Abstractions.Messaging;

namespace Outlou.Application.RegisterUser;

internal sealed class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, string>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtProvider _jwtProvider;

    public RegisterUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork,IPasswordHasher passwordHasher,IJwtProvider jwtProvider)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
        _jwtProvider = jwtProvider;
    }

    public async Task<Result<string>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        if (!await _userRepository.IsEmailUniqueAsync(request.Email, cancellationToken))
        {
            return Result.Failure<string>(DomainErrors.User.EmailAlreadyInUse);
        }

        var hashedPassword = _passwordHasher.HashPassword(request.Password);

        var user = new User(Guid.NewGuid(), request.Email, hashedPassword);

         _userRepository.Add(user);

         await _unitOfWork.SaveChangesAsync(cancellationToken);

         var jwtToken = _jwtProvider.Generate(user);

         return jwtToken;
    }
}

