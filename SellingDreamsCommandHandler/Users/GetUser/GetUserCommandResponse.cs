namespace SellingDreamsCommandHandler.Users.GetUser;

public class GetUserCommandResponse : ICommandResponse
{
  public int Id { get; set; }
  public string Name { get; set; } = "";
  public string? Adress { get; set; }
  public string? EmailAdress { get; set; }
  public string? PhoneNumber { get; set; }
}
