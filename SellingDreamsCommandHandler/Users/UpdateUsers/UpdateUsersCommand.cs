namespace SellingDreamsCommandHandler.Users.UpdateUsers;

public class UpdateUsersCommand : ICommand
{
    public int UserId { get; set; }
    public string Name { get; set; } = "";
    public string? Adress { get; set; }
    public string? EmailAdress { get; set; }
    public string? PhoneNumber { get; set; }
}
