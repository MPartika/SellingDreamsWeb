using SellingDreamsService.Contracts;

namespace SellingDreamsCommandHandler.Users.PatchUsers;

public class PatchUsersCommandHandler : ICommandHandlerAsync<PatchUsersCommand>
{
    private readonly IUserRepository _repository;

    public PatchUsersCommandHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(PatchUsersCommand command)
    {
       await _repository.UpdateUser(command);
    }
}
