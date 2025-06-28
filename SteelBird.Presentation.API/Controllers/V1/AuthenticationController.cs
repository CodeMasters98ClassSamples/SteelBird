using Microsoft.AspNetCore.Mvc;
using SteelBird.Application.Contracts;
using SteelBird.Application.Dtos.Authentication;

namespace SteelBird.Presentation.API.Controllers.V1;

public class AuthenticationController : GeneralController
{
    private readonly IAccountService _accountService;
    public AuthenticationController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginRequestDto dto, CancellationToken ct = default)
    {
        var result = await _accountService.AuthenticateAsync(dto, "", ct);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterDto dto, CancellationToken ct = default)
    {
        var result = await _accountService.RegisterAsync(dto, "", ct);
        return Ok(result);
    }
}
