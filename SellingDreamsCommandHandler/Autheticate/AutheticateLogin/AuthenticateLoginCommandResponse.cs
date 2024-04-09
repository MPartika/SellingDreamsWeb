namespace SellingDreamsCommandHandler.Authenticate;

public class AuthenticateLoginCommandResponse : ICommandResponse
{
    public bool IsAuthorized { get; set; }
}

