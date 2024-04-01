using SellingDreamsService.Contracts;

namespace SellingDreamsCommandHandler.Authenticate.DeleteLogin;

public class DeleteLoginCommandHandler : ICommandHandlerAsync<DeleteLoginCommand>
{
    private readonly IAuthenticationRepository _repository;

    public DeleteLoginCommandHandler(IAuthenticationRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(DeleteLoginCommand command)
    {
        await _repository.DeLeteLogin(command.Id);
    }
}
