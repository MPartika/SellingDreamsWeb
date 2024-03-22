using SellingDreamsService.Contracts;

namespace SellingDreamsCommandHandler.Users.GetAllUsers;

public class GetAllUsersCommandHandler : ICommandHandlerListAsync<GetAllUsersCommand, GetAllUsersCommandResponse>
{
    private readonly IUserRepository _repository;

    public GetAllUsersCommandHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<GetAllUsersCommandResponse>> ExecuteAsync(GetAllUsersCommand command)
    {
        var respone = await _repository.GetUsers();
        return respone.Select(result => new GetAllUsersCommandResponse
        {
             Id = result.Id,
             Name = result.Name,
             Adress = result.Adress,
             EmailAdress = result.EmailAdress,
             PhoneNumber = result.PhoneNumber
        }).ToList();
    }
}
