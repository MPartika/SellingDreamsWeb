namespace SellingDreamsCommandHandler.Authenticate;

public class CreateLoginCommand : ICommand
{
    public string UserName { get; set; } = "";
    public string Password { get; set; } = "";
    public int? UserId { get; set; }
}
