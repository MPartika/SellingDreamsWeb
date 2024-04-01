
using SellingDreamsInfrastructure.Model;
using SellingDreamsService.Contracts;

namespace SellingDreamsCommandHandler.Authenticate.CreateLogin;

public class CreateLoginCommandHandler : ICommandHandlerAsync<CreateLoginCommand>
{
    private readonly IAuthenticationRepository _repository;

    public CreateLoginCommandHandler(IAuthenticationRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(CreateLoginCommand command)
    {
        var password = AuthenticationHelper.HashPassword(command.Password, out byte[] salt);
        await _repository.CreateLogin(new UserLogin {
            Password = password,
            UserName = command.UserName,
            Salt = salt,
            UserId = command.UserId
        });
    }
}
