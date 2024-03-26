namespace SellingDreamsCommandHandler.Users.CreateUsers;

public record CreateUsersCommand : ICommand
{
    public string Name { get; set; } = "Not Defined";
    public string? Adress { get; set; }
    public string EmailAdress { get; set; } = "Not Defined";
    public string? PhoneNumber { get; set; }
}
