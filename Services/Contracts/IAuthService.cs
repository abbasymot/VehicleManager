using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Models.DTO_s;

namespace Services.Contracts;

public interface IAuthService
{
    public Task<(IdentityResult Result, string Error)> RegisterAsync(RegisterDTO registerDTO);
    public JwtSecurityToken GetToken(List<Claim> authClaims);
}