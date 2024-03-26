using SellingDreamsService.Contracts;

namespace SellingDreamsCommandHandler.Users.PatchUsers;

public class PatchUsersCommandHandler : ICommandHandlerAsync<PatchUsersCommand>
{
    private readonly IUserRepository _repositary;

    public PatchUsersCommandHandler(IUserRepository repositary)
    {
        _repositary = repositary;
    }

    public async Task Execute(PatchUsersCommand command)
    {
       await _repositary.UpdateUser(command);
    }
}
