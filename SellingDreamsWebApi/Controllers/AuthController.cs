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
        await _createLoginCommand.ExecuteAsync(command);
        return Ok();
    }

    private string BuildToken(string userName)
    {
        var issuer = _config["JwtSettings:Issuer"];
        var audience = _config.GetSection("JwtSettings:Audience").Get<string[]>();
        var keyConfig = _config["JwtSettings:Key"];
        if (keyConfig is null || audience is null)
            throw new Exception("key is missing");
        var key = Encoding.ASCII.GetBytes(keyConfig);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("Id", Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }),
            Expires = DateTime.UtcNow.AddMinutes(30),
            Issuer = issuer,
            Claims = new Dictionary<string, object> {{JwtRegisteredClaimNames.Aud, audience}},
            SigningCredentials = new SigningCredentials
            (new SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha512Signature)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
