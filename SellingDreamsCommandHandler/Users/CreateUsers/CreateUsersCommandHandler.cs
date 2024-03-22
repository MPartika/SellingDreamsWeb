
using SellingDreamsInfrastructure.Model;
using SellingDreamsService.Contracts;

namespace SellingDreamsCommandHandler.Users.CreateUsers;

public class CreateUsersCommandHandler : ICommandHandlerAsync<CreateUsersCommand>
{
    private readonly IUserRepository _repository;

    public CreateUsersCommandHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task Execute(CreateUsersCommand command)
    {
        await _repository.CreateUser(new User {
            Name = command.Name,
            Adress = command.Adress,
            EmailAdress = command.EmailAdress,
            PhoneNumber = command.PhoneNumber
        });
    }
}