using Outlou.Application.Abstractions.Messaging;

namespace Outlou.Application.RegisterUser;

public record RegisterUserCommand(string Email, string Password) : ICommand<string>;