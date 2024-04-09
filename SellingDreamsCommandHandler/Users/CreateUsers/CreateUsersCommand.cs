namespace SellingDreamsCommandHandler.Users;

public record CreateUsersCommand : ICommand
{
    public string Name { get; set; } = "Not Defined";
    public string? Address { get; set; }
    public string EmailAddress { get; set; } = "Not Defined";
    public string? PhoneNumber { get; set; }
}
