using SellingDreamsService.ContractsDto;
namespace SellingDreamsCommandHandler.Users.PatchUsers;

public class PatchUsersCommand : IUserPatchDto, ICommand
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Adress { get; set; }
    public string? EmailAdress { get; set; }
    public string? PhoneNumber { get; set; }
}
