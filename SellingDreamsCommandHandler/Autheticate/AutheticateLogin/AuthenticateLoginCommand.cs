namespace SellingDreamsCommandHandler.Authenticate.AuthenticateLogin;

public class AuthenticateLoginCommand : ICommand
{
  public string UserName { get; set; } = "";
  public string Password { get; set; } = "";
}
