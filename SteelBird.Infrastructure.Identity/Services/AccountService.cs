using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SteelBird.Application.Contracts;
using SteelBird.Application.Dtos.Authentication;
using SteelBird.Application.Wrappers;
using SteelBird.Domain.Enums;
using SteelBird.Infrastructure.Identity.Helpers;
using SteelBird.Infrastructure.Identity.Models;
using SteelBird.Shared.Setting;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SteelBird.Infrastructure.Identity.Services;

public class AccountService : IAccountService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly JwtSetting _jwtSettings;
    public AccountService(UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IOptionsMonitor<JwtSetting> jwtSettings,
        SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _jwtSettings = jwtSettings.CurrentValue;
        _signInManager = signInManager;
    }

    public async Task<LoginResponseDto> AuthenticateAsync(LoginRequestDto request, string ipAddress, CancellationToken ct)
    {
        var user = await _userManager.FindByEmailAsync(request.Username);
        if (user is null)
            return null;    //Result.Failure();//$"No Accounts Registered with {request.Username}."
        var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
        if (!result.Succeeded)
            return null;    //Result.Failure();//$"Invalid Credentials for '{request.Username}'."

        JwtSecurityToken jwtSecurityToken = await GenerateJWToken(user);
        LoginResponseDto response = new LoginResponseDto();
        response.Id = user.Id;
        response.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        response.Email = user.Email;
        response.UserName = user.UserName;
        var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
        response.Roles = rolesList.ToList();
        response.IsVerified = user.EmailConfirmed;
        return response;
    }

    public async Task<Result<object>> RegisterAsync(RegisterDto request, string origin, CancellationToken ct)
    {
        var userWithSameUserName = await _userManager.FindByNameAsync(request.Username);
        if (userWithSameUserName is not null)
            return Result.Failure();

        if (!_roleManager.RoleExistsAsync(UserRoles.ADMIN.ToString()).GetAwaiter().GetResult())
        {
            await _roleManager.CreateAsync(new IdentityRole(UserRoles.ADMIN.ToString()));
            //multiple role add
        }
        var user = new ApplicationUser
        {
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            UserName = request.Username
        };
        var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);
        if (userWithSameEmail == null)
        {
            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, UserRoles.ADMIN.ToString());
                return Result.Success(user.Id);
            }
            else
            {
                return Result.Failure();
            }
        }
        else
        {
            return Result.Failure();
        }
    }

    private async Task<JwtSecurityToken> GenerateJWToken(ApplicationUser user)
    {
        var userClaims = await _userManager.GetClaimsAsync(user);
        var roles = await _userManager.GetRolesAsync(user);

        var roleClaims = new List<Claim>();

        for (int i = 0; i < roles.Count; i++)
        {
            roleClaims.Add(new Claim("roles", roles[i]));
        }

        string ipAddress = IpHelper.GetIpAddress();

        var claims = new[]
        {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id),
                new Claim("ip", ipAddress)
            }
        .Union(userClaims)
        .Union(roleClaims);

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _jwtSettings.ValidIssuer,
            audience: _jwtSettings.ValidAudience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.expireInMinutes),
            signingCredentials: signingCredentials);
        return jwtSecurityToken;
    }

    private string RandomTokenString()
    {
        return new Guid().ToString();
    }

    private async Task<string> SendVerificationEmail(ApplicationUser user, string origin)
    {
        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
        var route = "api/account/confirm-email/";
        var _enpointUri = new Uri(string.Concat($"{origin}/", route));
        var verificationUri = QueryHelpers.AddQueryString(_enpointUri.ToString(), "userId", user.Id);
        verificationUri = QueryHelpers.AddQueryString(verificationUri, "code", code);
        //Email Service Call Here
        return verificationUri;
    }

    public async Task<Result<object>> ConfirmEmailAsync(string userId, string code, CancellationToken ct)
    {
        var user = await _userManager.FindByIdAsync(userId);
        code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
        var result = await _userManager.ConfirmEmailAsync(user, code);
        if (result.Succeeded)
        {
            return Result.Success(user.Id);
        }
        else
        {
            //throw new ApiException($"An error occured while confirming {user.Email}.");
            return Result.Failure();
        }
    }

}
