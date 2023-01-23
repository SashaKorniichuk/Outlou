using Outlou.Application.Abstractions.Messaging;

namespace Outlou.Application.Login;

public record LoginCommand(string Email,string Password):ICommand<string>;