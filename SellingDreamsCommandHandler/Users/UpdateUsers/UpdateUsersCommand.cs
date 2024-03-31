namespace SellingDreamsCommandHandler.Users.UpdateUsers;

public class UpdateUsersCommand : ICommand
{
    public int UserId { get; set; }
    public string Name { get; set; } = "Not Defined";
    public string? Address { get; set; }
    public string EmailAddress { get; set; } = "Not Defined";
    public string? PhoneNumber { get; set; }
}
