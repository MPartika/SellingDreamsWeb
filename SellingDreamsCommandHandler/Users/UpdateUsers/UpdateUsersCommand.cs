namespace SellingDreamsCommandHandler.Users;

public class UpdateUsersCommand : ICommand
{
    public int UserId { get; set; }
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }
}
