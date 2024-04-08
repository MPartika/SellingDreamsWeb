using SellingDreamsService.ContractsDto;
namespace SellingDreamsCommandHandler.Users.PatchUsers;

public class PatchUsersCommand : IUserPatchDto, ICommand
{
    public int Id { get; set; }
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }
}
