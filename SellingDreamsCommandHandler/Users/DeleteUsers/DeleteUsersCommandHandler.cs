using SellingDreamsService.Contracts;

namespace SellingDreamsCommandHandler.Users.DeleteUsers;

public class DeleteUsersCommandHandler : ICommandHandlerAsync<DeleteUsersCommand>
{
    private readonly IUserRepository _repository;

    public DeleteUsersCommandHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(DeleteUsersCommand command)
    {
        await _repository.DeleteUser(command.Id);
    }
}

