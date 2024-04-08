using SellingDreamsInfrastructure.Model;
using SellingDreamsService.Contracts;

namespace SellingDreamsCommandHandler.Users.UpdateUsers;

public class UpdateUsersCommandHandler : ICommandHandlerAsync<UpdateUsersCommand>
{
    private readonly IUserRepository _repository;

    public UpdateUsersCommandHandler(IUserRepository repository)
    {
        this._repository = repository;
    }

    public async Task ExecuteAsync(UpdateUsersCommand command)
    {
        await _repository.UpdateUser(
            new User
            {
                Id = command.UserId,
                PhoneNumber = command.PhoneNumber,
                Address = command.Address
            }
        );
    }
}
