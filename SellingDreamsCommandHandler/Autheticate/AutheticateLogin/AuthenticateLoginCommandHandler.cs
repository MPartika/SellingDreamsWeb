using SellingDreamsService.Contracts;

namespace SellingDreamsCommandHandler.Authenticate.AuthenticateLogin;

public class AuthenticateLoginCommandHandler : ICommandHandlerAsync<AuthenticateLoginCommand, AuthenticateLoginCommandResponse>
{
    private readonly IAuthenticationRepository _repository;

    public AuthenticateLoginCommandHandler(IAuthenticationRepository repository)
    {
        _repository = repository;
    }

    public async Task<AuthenticateLoginCommandResponse> ExecuteAsync(AuthenticateLoginCommand command)
    {
        var login = await _repository.GetLogin(command.UserName);
        if (login == null)
            return new AuthenticateLoginCommandResponse { IsAuthorized = false };

        if (AuthenticationHelper.VerifyPassword(command.Password, login.Password, login.Salt))
            return new AuthenticateLoginCommandResponse { IsAuthorized = true };

        return new AuthenticateLoginCommandResponse { IsAuthorized = false };
    }
}
