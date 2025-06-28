using SteelBird.Application.Dtos.Authentication;
using SteelBird.Application.Wrappers;

namespace SteelBird.Application.Contracts;

public interface IAccountService
{
    Task<LoginResponseDto> AuthenticateAsync(LoginRequestDto request, string ipAddress, CancellationToken ct);
    Task<Result<object>> RegisterAsync(RegisterDto request, string origin, CancellationToken ct);
    Task<Result<object>> ConfirmEmailAsync(string userId, string code, CancellationToken ct);
}
