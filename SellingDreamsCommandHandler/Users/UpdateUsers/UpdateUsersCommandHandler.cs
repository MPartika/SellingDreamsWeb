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

    public async Task Execute(UpdateUsersCommand command)
    {
        await _repository.UpdateUser(
            new User
            {
                Id = command.UserId,
                Name = command.Name,
                EmailAdress = command.EmailAdress,
                PhoneNumber = command.PhoneNumber,
                Adress = command.Adress
            }
        );
    }
}