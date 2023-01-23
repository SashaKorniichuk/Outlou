using Domain.Entities;

namespace Outlou.Application.Abstractions;

public interface IJwtProvider
{
    string Generate(User user);
}

