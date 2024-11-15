using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models;
using Models.DTO_s;
using Services.Contracts;

namespace Services.Implementations;

public class AuthService : IAuthService
{
    private readonly IConfiguration configuration;
    private readonly UserManager<AppUser> userManager;

    public AuthService(IConfiguration configuration, UserManager<AppUser> userManager)
    {
        this.configuration = configuration;
        this.userManager = userManager;
    }

    public async Task<(IdentityResult Result ,string Error)> RegisterAsync(RegisterDTO registerDTO)
    {
        if (registerDTO.Password != registerDTO.ConfirmPassword)
        {
            return (null!, "Passwords do not match.");
        }
        
        // Check if the username already exists
        var existingUser = await userManager.FindByNameAsync(registerDTO.UserName);
        if (existingUser != null)
        {
            return (null!, "Username is already taken.");
        }
        
        var user = new AppUser()
        {
            UserName = registerDTO.UserName
        };
        var result = await userManager.CreateAsync(user, registerDTO.Password);
        
        return (result, null!);
    }
    
    public JwtSecurityToken GetToken(List<Claim> authClaims)
    {
        var authSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));

        var token = new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"],
            audience: configuration["Jwt:Audience"],
            expires: DateTime.Now.AddHours(8),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );
        return token;
    }
}