using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DTO_s;
using Services.Contracts;

namespace VehicleManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : Controller
{
    private readonly UserManager<AppUser> userManager;
    private readonly IAuthService authService;
    
    public AuthController(IAuthService authService, UserManager<AppUser> userManager)
    {
        this.userManager = userManager;
        this.authService = authService;
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDTO model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Wrong input, A required field is missing!");
        }

        var register = await authService.RegisterAsync(model);
        if (register.Error != null! )
        {
            return BadRequest(register.Error);
        }
        if (register.Result?.Errors?.Count() > 0)
        {
            return BadRequest(register.Result);
        }

        return Ok("User registered successfully!");
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDTO model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Wrong input, A required field is missing!");
        }
        
        var user = await userManager.FindByNameAsync(model.UserName);
        if (user == null || !await userManager.CheckPasswordAsync(user, model.Password))
        {
            return Unauthorized();
        }
        
        var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = authService.GetToken(authClaims); 
        
        return Ok(new { Token = new JwtSecurityTokenHandler().WriteToken(token) });
    }
    
    [Authorize]
    [HttpGet("TestToken")]
    public async Task<IActionResult> TestToken()
    {
        return Ok("You are logged in!");
    }
}