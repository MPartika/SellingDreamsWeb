namespace SellingDreamsCommandHandler.Authenticate.AuthenticateLogin;

public class AuthenticateLoginCommandResponse : ICommandResponse
{
    public bool IsAuthorized { get; set; }
}

