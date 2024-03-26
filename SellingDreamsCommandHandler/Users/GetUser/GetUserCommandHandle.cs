using SellingDreamsService.Contracts;

namespace SellingDreamsCommandHandler.Users.GetUser;

public class GetUserCommandHandle : ICommandHandlerAsync<GetUserCommand, GetUserCommandResponse>
{
    private readonly IUserRepository _repository;

  public GetUserCommandHandle(IUserRepository repository)
  {
    _repository = repository;
  }

  public async Task<GetUserCommandResponse> ExecuteAsync(GetUserCommand command)
    {
        var result = await _repository.GetUser(command.Id);
    return new GetUserCommandResponse
    {
      Id = result.Id,
      Name = result.Name,
      Adress = result.Adress,
      EmailAdress = result.EmailAdress,
      PhoneNumber = result.PhoneNumber
    };
  }
}