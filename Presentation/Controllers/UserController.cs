using MediatR;
using Microsoft.AspNetCore.Mvc;
using Outlou.Application.Login;
using Outlou.Application.RegisterUser;
using Presentation.Abstractions;

namespace Presentation.Controllers;
[Route("api/User")]
public sealed class UserController : ApiController
{
    public UserController(ISender sender) : base(sender)
    {
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest, CancellationToken cancellationToken)
    {
        var command = new LoginCommand(loginRequest.Email, loginRequest.Password);

        var tokenResult = await Sender.Send(command, cancellationToken);

        return tokenResult.IsFailure ? HandleFailure(tokenResult) : Ok(tokenResult.Value);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserRequest registerUserRequest, CancellationToken cancellationToken)
    {
        var command = new RegisterUserCommand(registerUserRequest.Email, registerUserRequest.Password);

        var tokenResult = await Sender.Send(command, cancellationToken);

        return tokenResult.IsFailure ? HandleFailure(tokenResult) : Ok(tokenResult.Value);
    }
}