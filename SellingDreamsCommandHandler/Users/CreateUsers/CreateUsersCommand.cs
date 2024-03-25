namespace SellingDreamsCommandHandler.Users.CreateUsers;

public record CreateUsersCommand : ICommand
{
    public string Name { get; set; } = "";
    public string? Adress { get; set; }
    public string? EmailAdress { get; set; }
    public string? PhoneNumber { get; set; }
}