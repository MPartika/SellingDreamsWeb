namespace SellingDreamsCommandHandler.Users;

public class GetAllUsersCommandResponse : ICommandResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string? Address { get; set; }
    public string? EmailAddress { get; set; }
    public string? PhoneNumber { get; set; }
}
