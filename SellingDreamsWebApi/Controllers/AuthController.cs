using System.IdentityModel.Tokens.Jwt;
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
        var tokenString = BuildToken();
        return Ok(new { token = tokenString });
    }

    [AllowAnonymous]
    [HttpPost("[action]")]
    public async Task<IActionResult> CreateLogin([FromBody] CreateLoginCommand command)
    {
        await _createLoginCommand.Execute(command);
        return Ok();
    }

    private string BuildToken()
    {
        var configKey = _config["Authentication:Schemes:Bearer:Key"];
        if (configKey == null)
            throw new Exception("Could not find jwt token");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(_config["Authentication:Schemes:Bearer:ValidIssuer"],
            _config["Authentication:Schemes:Bearer:ValidIssuer"],
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
