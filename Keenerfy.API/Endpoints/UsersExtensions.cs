using Keenerfy.API.Requests;
using Keenerfy.Keenerfy.Database;
using Keenerfy.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Keenerfy.API.Endpoints;
public static class UsersExtensions
{
    public static void UserEndpoints(this WebApplication app)
    {
        var groupBuilder = app.MapGroup("register")
           .WithTags("Identity");

        groupBuilder.MapPost("/register", async(UserManager<User> userManager, [FromBody] UsersRequest userRequest) =>
        {

            User user = new();
            user.UserName = userRequest.UserName;
            user.Cpf = userRequest.Cpf;
            user.Email = userRequest.Email;

            var result = await userManager.CreateAsync(user, userRequest.Password);

            if (result.Succeeded)
            {
                return Results.Ok(user);
            }
            return Results.BadRequest(result.Errors);
        });

        var groupBuilderLogin = app.MapGroup("login")
            .WithTags("Identity");

        groupBuilderLogin.MapPost("/login", [AllowAnonymous] async ([FromBody] LoginsRequest request, UserManager<User> userManager, SignInManager<User> signInManager, ITokenService tokenService) =>
        {
            var user = await userManager.FindByEmailAsync(request.Email);
            if (user != null)
            {
                var result = await signInManager.CheckPasswordSignInAsync(user, request.Password, true);
                if (result is not null)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    var token = await tokenService.GenerateTokenAsync(user);
                    return Results.Ok(new { token = token, message = "Login successful" });
                }
            }
            return Results.BadRequest();
        });

    }

}
public interface ITokenService
{
    Task<string> GenerateTokenAsync(User user);
}

public class TokenService : ITokenService
{
    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _configuration;

    public TokenService(UserManager<User> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task<string> GenerateTokenAsync(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()), // ID do usuário
            new(ClaimTypes.Email, user.Email), // E-mail do usuário
            new(ClaimTypes.Name, user.UserName) // Nome de usuário
            // Você pode adicionar mais claims se precisar
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
