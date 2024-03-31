using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SellingDreamsCommandHandler;
using SellingDreamsCommandHandler.Authenticate.AuthenticateLogin;
using SellingDreamsCommandHandler.Authenticate.CreateLogin;

namespace SellingDreamsWebApi.Controllers;

public class AuthController : Controller
{
    private readonly IConfiguration _config;
    private readonly ICommandHandlerAsync<AuthenticateLoginCommand, AuthenticateLoginCommandResponse> _authenticateLoginCommand;
    private readonly ICommandHandlerAsync<CreateLoginCommand> _createLoginCommand;

    public AuthController(IConfiguration config,
            ICommandHandlerAsync<AuthenticateLoginCommand, AuthenticateLoginCommandResponse> authenticateLoginCommand,
            ICommandHandlerAsync<CreateLoginCommand> createLoginCommand)
    {
        _config = config;
        _authenticateLoginCommand = authenticateLoginCommand;
        _createLoginCommand = createLoginCommand;
    }

    [AllowAnonymous]
    [HttpPost("[action]")]
    public async Task<IActionResult> Authenticate([FromBody] AuthenticateLoginCommand command)
    {
        var isAuthenticated = await _authenticateLoginCommand.ExecuteAsync(command);
        if (!isAuthenticated.IsAuthorized)
            return Unauthorized();
        var tokenString = BuildToken(command.UserName);
        return Ok(new { token = tokenString });
    }

    [AllowAnonymous]
    [HttpPost("[action]")]
    public async Task<IActionResult> CreateLogin([FromBody] CreateLoginCommand command)
    {
        await _createLoginCommand.Execute(command);
        return Ok();
    }

    private string BuildToken(string userName)
    {
        var claims = new List<Claim> {new Claim(ClaimTypes.Name, userName), new Claim(ClaimTypes.Role, "User")};
        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImtvbWluIiwic3ViIjoia29taW4iLCJqdGkiOiIzZmEwYzVmYyIs"));
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        var token = new JwtSecurityToken(
                                claims: claims,
                                expires: DateTime.UtcNow.AddHours(1),
                                signingCredentials: cred);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
