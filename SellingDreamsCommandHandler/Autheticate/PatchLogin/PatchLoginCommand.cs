namespace SellingDreamsCommandHandler.Authenticate;

public class PatchLoginCommand : ICommand
{
   public int Id { get; set; }
   public string? Password { get; set; }
   public int? UserId { get; set; }
}
